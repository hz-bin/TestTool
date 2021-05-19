using log4net;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static ProcessTestClient.Define;

namespace ProcessTestClient
{
    public class TestFrame
    {
        public delegate void UpdateListViewCallBack(int index, Color color, string strResult, UInt32 ulSuccNum, UInt32 ulFailNum);
        public delegate void UpdateUIStatusCallBack(bool bIsRunning);

        ILog log;
        public Keyword keyword;
        Thread testCaseThread;
        UpdateListViewCallBack UpdateListView;
        UpdateUIStatusCallBack UpdateUIStatus;

        int lRunningIndex = 0;  // 正在运行的用例在ListView中的位置
        UInt32 ulSuccNum = 0;   // 运行成功用例
        UInt32 ulFailNum = 0;   // 运行失败用例


        /// <summary>构造函数</summary>
        public TestFrame()
        {
        }

        public TestFrame(string filePath)
        {
            log = log4net.LogManager.GetLogger("Main");

            keyword = new Keyword(filePath);
        }

        public List<TestCaseStruct> GetTestCaseList()
        {
            return keyword.TestCaseList;
        }

        public void SetUpdateListViewCallBack(UpdateListViewCallBack func)
        {
            UpdateListView = func;
        }

        public void SetUpdateUIStatusCallBack(UpdateUIStatusCallBack func)
        {
            UpdateUIStatus = func;
        }

        private void RunTestCase(object param)
        {
            ulSuccNum = 0;
            ulFailNum = 0;

            List<CheckedTestCaseInfo> CheckItems = (List<CheckedTestCaseInfo>)param;
            foreach (CheckedTestCaseInfo item in CheckItems)
            {
                UpdateListView(item.Index, Color.Orange, RUNNING, ulSuccNum, ulFailNum);
                lRunningIndex = item.Index;

                log.InfoFormat("开始运行测试用例：{0}", item.TestCaseId);
                Int32 lRet = keyword.ExecTestCaseFunc(item.TestCaseId);
                log.InfoFormat("结束运行测试用例：{0}", item.TestCaseId);

                if (0 == lRet)
                {
                    ulSuccNum++;
                    log.InfoFormat("测试用例{0}运行成功\n", item.TestCaseId);
                    UpdateListView(item.Index, Color.LightGreen, PASS, ulSuccNum, ulFailNum);
                }
                else
                {
                    ulFailNum++;
                    log.InfoFormat("测试用例{0}运行失败\n", item.TestCaseId);
                    UpdateListView(item.Index, Color.LightPink, FAIL, ulSuccNum, ulFailNum);
                }
                Thread.Sleep(200);
            }

            UpdateUIStatus(false);
        }

        public void StartTestCase(List<CheckedTestCaseInfo> CheckItems)
        {
            testCaseThread = new Thread(new ParameterizedThreadStart(RunTestCase));
            testCaseThread.Start(CheckItems);
        }

        public void StopTestCase()
        {
            testCaseThread.Abort();
            UpdateListView(lRunningIndex, Color.Red, ABORT, ulSuccNum, ulFailNum);
        }

        public void LoadTestCase(string filePath)
        {
            keyword.LoadTestCase(filePath);
        }

        public List<string> GetTestCaseDetail(string strId, XmlTag tag)
        {
            List<string> stepList = new List<string>();

            foreach (TestCaseStruct tcs in keyword.TestCaseList)
            {
                if (tcs.strTestCaseId.Equals(strId))
                {
                    foreach (TestCaseContent tcc in tcs.ContentList)
                    {
                        if (tcc.tag == tag)
                        {
                            stepList.Add(tcc.str);
                        }
                    }
                    break;
                }
            }

            return stepList;
        }
    }
}

