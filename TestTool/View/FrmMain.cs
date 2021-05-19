using log4net;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using static ProcessTestClient.Define;

namespace ProcessTestClient
{
    public partial class FrmMain : Form
    {
        ILog log;
        ConcurrentQueue<string> udpRecvDataQueue = new ConcurrentQueue<string>();
        delegate void FlushClient();    // 更新界面
        List<string> ignoreError = new List<string>();
        bool bScrollToCaret = true;
        string TestCaseFilePath = "";
        TestFrame testFrame;
        int lRunTime = 0;
        string strTimeInit = "时间：00:00";
        string strTotalInit = "总数：--";
        string strSuccInit = "成功：--";
        string strFailInit = "失败：--";
        System.Timers.Timer TestCaseRunTimer;

        public void Log(string sss)
        {
            log.DebugFormat(sss);
        }

        public FrmMain()
        {
            InitializeComponent();

            log4net.Config.XmlConfigurator.ConfigureAndWatch(new System.IO.FileInfo(@"Lib/Log4net.xml"));
            log = LogManager.GetLogger("Main");

            // 接收日志线程
            Thread udpRecvLogThread = new Thread(UdpRecvLog);
            udpRecvLogThread.IsBackground = true;
            udpRecvLogThread.Start();

            // 更新时间定时器
            lRunTime = 0;
            TestCaseRunTimer = new System.Timers.Timer(1000);
            TestCaseRunTimer.Elapsed += new ElapsedEventHandler(UpdateRunTime);
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            // 默认测试用例文件
            TestCaseFilePath = Application.StartupPath + @"\TestCase\TestCaseFile.xml";
            // 初始化测试框架
            testFrame = new TestFrame(TestCaseFilePath);
            testFrame.SetUpdateListViewCallBack(UpdateListView);
            testFrame.SetUpdateUIStatusCallBack(UpdateUIStatus);

            UpdateUIStatus(false);

            // 加载测试用例
            LoadTestCase();
        }
        
        // 运行时间更新
        private void UpdateRunTime(object sender, ElapsedEventArgs e)
        {
            lRunTime++;
            int minute = lRunTime / 60;
            int second = lRunTime % 60;

            if (!IS_DEBUG)
            {
                lblTime.Text = "时间：" + string.Format("{0:D2}", minute) + ":" + string.Format("{0:D2}", second);
            }
        }

        // 向List中添加一个测试用例
        private void AddOneCase(TestCaseStruct tcs)
        {
            ListViewItem lvitem = new ListViewItem();
            string str = tcs.strTestCaseId.PadRight(15) + tcs.strTitle;
            lvitem.Text = tcs.strTestCaseId;
            lvitem.SubItems.Add(tcs.strTitle);
            lvitem.SubItems.Add("");
            lvitem.SubItems.Add("");
            lvitem.ToolTipText = str;

            lvwTestCase.Items.Add(lvitem);
        }

        // 更新测试用例执行结果
        private void UpdateListView(int index, Color color, string strResult, UInt32 ulSuccNum, UInt32 ulFailNum)
        {
            if (!IS_DEBUG)
            {
                lvwTestCase.Items[index].BackColor = color;
                lvwTestCase.Items[index].SubItems[2].Text = strResult;

                lblSuccNum.Text = "成功：" + ulSuccNum.ToString();
                lblFailNum.Text = "失败：" + ulFailNum.ToString();

                lvwTestCase.EnsureVisible(index);
            }
        }

        private void UdpRecvLog()
        {
            try
            {
                IPEndPoint remoteEndPoint = null;
                UdpClient udpRecv = new UdpClient(new IPEndPoint(IPAddress.Parse("127.0.0.100"), 27100));

                while (true)
                {
                    byte[] aucData = udpRecv.Receive(ref remoteEndPoint);

                    if (aucData.Length > 0)
                    {
                        udpRecvDataQueue.Enqueue(Utils.ByteArrayToString(aucData));
                        UpdateLog();
                    }
                }
            }
            catch (System.Exception ex)
            {
                log.ErrorFormat("UdpRecvLog === Message: {0}, StackTrace:{1}", ex.Message, ex.StackTrace);
            }
        }
        
        static int lLogLength = 0;
        private void UpdateLog()
        {
            if (rtbLog.InvokeRequired)   // 等待异步
            {
                FlushClient fc = new FlushClient(UpdateLog);
                this.Invoke(fc);        // 通过代理调用刷新方法
            }
            else
            {
                if (udpRecvDataQueue.Count > 0)
                {
                    string str;
                    bool bRet = udpRecvDataQueue.TryDequeue(out str);
                    if (true == bRet)
                    {
                        int len = Utils.GetLength(str);
                        if (str.Contains("[ERROR]"))
                        {
                            rtbLog.Select(lLogLength, len);
                            rtbLog.SelectionColor = Color.Red;
                        }
                        if (str.Contains("[ WARN]") || ContainsErrorText(str))
                        {
                            rtbLog.Select(lLogLength, len);
                            rtbLog.SelectionColor = Color.DarkOrange;
                        }

                        rtbLog.AppendText(str);
                        if (true == bScrollToCaret)
                        {
                            rtbLog.SelectionStart = rtbLog.TextLength;
                            rtbLog.ScrollToCaret();
                        }

                        lLogLength += len;
                    }
                }
            }
        }

        // 判断字符串str是否包含配置文件配置的字符串
        private bool ContainsErrorText(string str)
        {
            foreach (string s in ignoreError)
            {
                if (str.Contains(s))
                {
                    return true;
                }
            }
            return false;
        }

        // 双击测试用例，显示用例内容
        private void lvwTestCase_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (lvwTestCase.SelectedItems.Count > 0)
            {
                string strId = lvwTestCase.SelectedItems[0].SubItems[0].Text;

                foreach (TestCaseStruct tcs in testFrame.GetTestCaseList())
                {
                    if (tcs.strTestCaseId.Equals(strId))
                    {
                        log.InfoFormat("测试用例\"" + strId + " " + tcs.strTitle + "\"内容：");
                        for (int i = 0; i < tcs.ContentList.Count; i++)
                        {
                            string strEnd = "";
                            if (i == (tcs.ContentList.Count - 1))
                            {
                                strEnd = "\n";
                            }
                            if (tcs.ContentList[i].tag == XmlTag.STEP)
                            {
                                log.InfoFormat("<Step>" + tcs.ContentList[i].str + "</Step>" + strEnd);
                            }
                            else if (tcs.ContentList[i].tag == XmlTag.DECP)
                            {
                                log.InfoFormat("<Decp>" + tcs.ContentList[i].str + "</Decp>" + strEnd);
                            }
                            else if (tcs.ContentList[i].tag == XmlTag.EXPT)
                            {
                                log.InfoFormat("<Expt>" + tcs.ContentList[i].str + "</Expt>" + strEnd);
                            }
                        }
                    }
                }
            }
        }

        private void InitUiControls()
        {
            lvwTestCase.Items.Clear();

            lblTotalNum.Text = strTotalInit;
            lblSuccNum.Text = strSuccInit;
            lblFailNum.Text = strFailInit;
            lblTime.Text = strTimeInit;

            ckbSelectAll.Text = "全选";
            ckbSelectAll.Checked = false;
        }

        // 更新UI状态
        private void UpdateUIStatus(bool bIsRunning)
        {
            rbtnExecTestCase.Enabled = !bIsRunning;
            rbtnStopTestCase.Enabled = bIsRunning;
            rbtnReloadTestCase.Enabled = !bIsRunning;
            if (!IS_DEBUG)
            {
                btnFilterByID.Enabled = !bIsRunning;
                txbTestCaseID.Enabled = !bIsRunning;
            }

            if (false == bIsRunning)
            {
                TestCaseRunTimer.Stop();
            }
        }

        // 执行测试用例
        private void rbtnExecTestCase_Click(object sender, EventArgs e)
        {
            List<CheckedTestCaseInfo> CheckItems = new List<CheckedTestCaseInfo>();
            for (int i = 0; i < lvwTestCase.Items.Count; i++)
            {
                ListViewItem item = lvwTestCase.Items[i];
                if (true == item.Checked)
                {
                    item.BackColor = Color.White;
                    item.SubItems[2].Text = "";
                    CheckItems.Add(new CheckedTestCaseInfo(i, item.Text));
                }
            }

            if (CheckItems.Count > 0)
            {
                lRunTime = 0;
                lblTime.Text = strTimeInit;
                TestCaseRunTimer.Start();
                testFrame.StartTestCase(CheckItems);
                UpdateUIStatus(true);
            }
            else
            {
                MessageBox.Show("请选择要执行的测试用例");
            }
        }

        // 停止测试用例
        private void rbtnStopTestCase_Click(object sender, EventArgs e)
        {
            TestCaseRunTimer.Stop();
            testFrame.StopTestCase();
            log.InfoFormat("停止测试用例执行!");

            UpdateUIStatus(false);
        }

        // 重新加载测试用例
        private void rbtnReloadTestCase_Click(object sender, EventArgs e)
        {
            testFrame.LoadTestCase(TestCaseFilePath);

            InitUiControls();

            // 加载测试用例
            LoadTestCase();
        }

        // 加载测试用例
        private void LoadTestCase()
        {
            InitUiControls();

            // 加载测试用例
            foreach (TestCaseStruct tcs in testFrame.GetTestCaseList())
            {
                AddOneCase(tcs);
            }

            lblTotalNum.Text = "总数：" + lvwTestCase.Items.Count;
        }

        private void ckbSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbSelectAll.Checked == true && ckbSelectAll.Text == "全选")
            {
                foreach (ListViewItem item in lvwTestCase.Items)
                {
                    item.Checked = true;
                }
                ckbSelectAll.Text = "反选";
            }
            if (ckbSelectAll.Checked == false && ckbSelectAll.Text == "反选")
            {
                foreach (ListViewItem item in lvwTestCase.Items)
                {
                    item.Checked = !item.Checked;
                }
                ckbSelectAll.Text = "全选";
            }
        }

        private void txbTestCaseID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnFilterByID_Click(sender, e);
            }
        }

        private void btnFilterByID_Click(object sender, EventArgs e)
        {
            string strFilter = txbTestCaseID.Text.Trim().ToUpper();

            if (!String.IsNullOrEmpty(strFilter))
            {
                InitUiControls();

                string[] strFilterArr = null;
                int MatchMode = 0;

                // 以"&&"分割，需要同时满足多个过滤条件
                if (strFilter.Contains("&&"))
                {
                    strFilterArr = strFilter.Split(new string[] { "&&" }, StringSplitOptions.None);
                    MatchMode = 1;
                }
                // 以"||"分割，只需满足其中一个条件即可
                else if (strFilter.Contains("||"))
                {
                    strFilterArr = strFilter.Split(new string[] { "||" }, StringSplitOptions.None);
                    MatchMode = 2;
                }
                else
                {
                    MatchMode = 0;
                }

                // 加载测试用例
                foreach (TestCaseStruct tcs in testFrame.GetTestCaseList())
                {
                    bool bIsAdd = false;
                    string id = tcs.strTestCaseId;
                    
                    if (0 == MatchMode)
                    {
                        if (id.Contains(strFilter))
                        {
                            bIsAdd = true;
                        }
                    }
                    else if (1 == MatchMode)
                    {
                        int matchNum = 0;
                        foreach (string str in strFilterArr)
                        {
                            if (id.Contains(str))
                            {
                                matchNum++;
                            }
                        }
                        if (matchNum == strFilterArr.Length)
                        {
                            bIsAdd = true;
                        }
                    }
                    else if (2 == MatchMode)
                    {
                        foreach (string str in strFilterArr)
                        {
                            if (id.Contains(str))
                            {
                                bIsAdd = true;
                            }
                        }
                    }

                    if (true == bIsAdd)
                    {
                        AddOneCase(tcs);
                    }
                }
                lblTotalNum.Text = "总数：" + lvwTestCase.Items.Count;
            }
            else
            {
                LoadTestCase();
            }
        }
    }
}
