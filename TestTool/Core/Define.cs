using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessTestClient
{
    public class Define
    {
        public const bool IS_DEBUG      = false;
        public const int MAX_BUFF_LEN   = 4096;

        public const string PASS        = "通过";
        public const string FAIL        = "失败";
        public const string RUNNING     = "运行中";
        public const string ABORT       = "终止";

        public class RecvData
        {
            public int size;
            public byte[] buffer;

            public RecvData()
            {

            }

            public RecvData(int _size, byte[] _buffer)
            {
                size = _size;
                buffer = new byte[_size];
                Array.Copy(_buffer, buffer, size);
            }
        }

        public enum XmlTag
        {
            STEP = 0,
            DECP = 1,
            EXPT = 2
        }

        public class TestCaseContent
        {
            public XmlTag tag;
            public string str;
        }

        public class TestCaseStruct
        {
            public string strTestCaseId;    // 测试用例编号
            public string strTitle;         // 测试用例标题
            public string strFailOp;        // 用例执行失败要进行的操作
            public string AreaId;           // 玩家区号
            public string NumId;            // 玩家id
            public List<TestCaseContent> ContentList;

            public TestCaseStruct()
            {
                ContentList = new List<TestCaseContent>();
            }
        }

        public class CheckedTestCaseInfo
        {
            public int Index;
            public string TestCaseId;

            public CheckedTestCaseInfo(int _index, string _id)
            {
                this.Index = _index;
                this.TestCaseId = _id;
            }
        }
    }
}
