using log4net;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static ProcessTestClient.Define;

namespace ProcessTestClient
{
    /// <summary>关键字类</summary>
    public class Keyword
    {
        public List<TestCaseStruct> TestCaseList;   // 保存读取的配置文件的所有值
        ILog log;                                   // 日志
        XmlHelper xmlHelper;
        Dictionary<string, object> keywordDic;      // 保存脚本中的关键字

        public Keyword()
        {
        }

        public Keyword(string filePath)
        {
            TestCaseList = new List<TestCaseStruct>();
            log = LogManager.GetLogger("Main");
            xmlHelper = new XmlHelper();
            keywordDic = new Dictionary<string, object>();

            LoadTestCase(filePath);
        }

        /// <summary>非关键字。加载测试用例</summary>
        public void LoadTestCase(string filePath)
        {
            TestCaseList.Clear();
            try
            {
                // 读取TestCaseFile.xml文件，保存测试脚本文件名
                string strTestCasePath = filePath;
                XDocument root = XDocument.Load(strTestCasePath);
                var TestCaseFileList = root.Element("Root").Element("TestCaseFiles").Elements("TestCaseFile");

                foreach (XElement caseFile in TestCaseFileList)
                {
                    string name = caseFile.Attribute("Name").Value;
                    FileInfo fi = new FileInfo(Application.StartupPath + @"\TestCase\" + name);
                  
                    // 加载测试用例
                    Int32 lRet = xmlHelper.AddTestCase(fi.FullName, TestCaseList);

                    if (0 != lRet)
                    {
                        log.ErrorFormat("Add xml TestCase Error, Error Code {0}.", lRet);
                        MessageBox.Show("Load File " + fi.FullName + " Failed");
                    }
                }
                
                log.InfoFormat("加载测试用例完成, 共{0}个测试用例!\n", TestCaseList.Count);
            }
            catch (System.Exception ex)
            {
                log.ErrorFormat("LoadTestCase === Message: {0}, StackTrace: {1}", ex.Message, ex.StackTrace);
            }
        }
        
        /// <summary>执行每一个测试用例之前，先清空dicKeyword的内容，然后将dicConfig的内容拷贝到dicKeyword中</summary>
        private void PreOperation()
        {
            keywordDic.Clear();
        }
        
        /// <summary>从listTestCase中找Id为strTestCaseId的测试用例</summary>
        /// <returns>返回找到的测试用例</returns>
        private TestCaseStruct FindTestCase(string strTestCaseId)
        {
            foreach (TestCaseStruct tcs in TestCaseList)
            {
                if (tcs.strTestCaseId == strTestCaseId)
                {
                    return tcs;
                }
            }

            return null;
        }

        /// <summary>执行某一步</summary>
        /// <param name="str">步骤内容</param>
        /// <returns></returns>
        private Int32 ExecStep(string str)
        {
            try
            {
                str = str.Trim();
                int lSpaceIndex = str.IndexOf(' ');
                string strFunc = str.Substring(0, lSpaceIndex);
                string strParam = str.Substring(lSpaceIndex + 1, str.Length - strFunc.Length - 1);

                if (String.IsNullOrEmpty(strFunc) || String.IsNullOrEmpty(strParam))
                {
                    log.ErrorFormat("参数错误：func={0}, param={1}", strFunc, strParam);
                    return -1;
                }

                List<string> paramList = new List<string>();
                string[] strParams = strParam.Split(',');
                foreach (string param in strParams)
                {
                    paramList.Add(param);
                }
                Type t = this.GetType();
                MethodInfo mif = t.GetMethod(strFunc);
                return (Int32)mif.Invoke(this, paramList.ToArray());
            }
            catch (System.Exception ex)
            {
                log.ErrorFormat("ExecStep === Step: {0}, Message: {1}, StackTrace: {2}", str, ex.Message, ex.StackTrace);
                return -1;
            }
        }

        /// <summary>非关键字。执行指定的测试用例</summary>
        /// <param name="strTestCaseId">测试用例编号</param>
        /// <param name="strDoPreOperation">0表示不执行前置操作，1表示执行前置操作</param>
        /// <returns>成功返回0，失败返回-1</returns>
        public Int32 ExecTestCaseFunc(string strTestCaseId, string strDoPreOperation = "1")
        {
            Int32 lExecRet = 0;
            if ("1" == strDoPreOperation)
            {
                PreOperation();
            }

            // 从listTestCase找测试用例
            TestCaseStruct tcs = FindTestCase(strTestCaseId);
            if (null == tcs)
            {
                log.ErrorFormat("没有找到测试用例：{0}", strTestCaseId);
                return -1;
            }

            // 执行测试用例每一步
            foreach (TestCaseContent content in tcs.ContentList)
            {
                if (content.tag == XmlTag.STEP)
                {
                    if (!String.IsNullOrWhiteSpace(content.str))
                    {
                        content.str = content.str.Trim();
                        log.InfoFormat("Step: {0}", content.str);
                        lExecRet = ExecStep(content.str);

                        if (0 != lExecRet)
                        {
                            break;
                        }
                    }
                }
            }

            if (0 != lExecRet)
            {
                string strFailOp = tcs.strFailOp;
                if (!string.IsNullOrEmpty(strFailOp))
                {
                    log.InfoFormat("测试用例{0}执行失败，开始执行FailOp", tcs.strTestCaseId);
                    string[] strFailOpContents = strFailOp.Split(';');
                    foreach (string strSubFailOp in strFailOpContents)
                    {
                        log.InfoFormat("Step: {0}", strSubFailOp);
                        Int32 lRet = ExecStep(strSubFailOp);
                        if (0 != lRet)
                        {
                            log.InfoFormat("FailOp: {0}，执行失败", strSubFailOp);
                        }
                        else
                        {
                            log.InfoFormat("FailOp: {0}，执行成功", strSubFailOp);
                        }
                    }
                    log.InfoFormat("测试用例{0}的FailOp执行结束", tcs.strTestCaseId);
                }
            }

            return lExecRet;
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////以下为关键字///////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////
        public Int32 Add(string a, string b, string ret)
        {
            keywordDic[ret] = Convert.ToString(Convert.ToInt32(a) + Convert.ToInt32(b));
            return 0;
        }

        public Int32 Sleep(string strMillisecond)
        {
            Thread.Sleep(Int32.Parse(strMillisecond));
            return 0;
        }

        public Int32 Equal(string s1, string s2)
        {
            if (Utils.IsKeyFormat(s1))
            {
                s1 = (string)keywordDic[s1];
            }
            if (Utils.IsKeyFormat(s2))
            {
                s2 = (string)keywordDic[s2];
            }

            return s1 == s2 ? 0 : -1;
        }
    }
}