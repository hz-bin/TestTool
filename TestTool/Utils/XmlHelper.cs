using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static ProcessTestClient.Define;

namespace ProcessTestClient
{
    class XmlHelper
    {
        ILog log;

        public XmlHelper()
        {
            log = LogManager.GetLogger("Main");
        }

        /// <summary>读取测试脚本xml文件</summary>
        /// <param name="xmlpath">xml文件路径</param>
        /// <param name="listTestCase">测试用例结构体</param>
        /// <returns>成功返回0，失败返回错误码</returns>
        public Int32 AddTestCase(string xmlpath, List<TestCaseStruct> listTestCase)
        {
            try
            {
                log.InfoFormat("加载测试用例：{0}", xmlpath);
                XElement root = XElement.Load(xmlpath);

                var TestCaseList = root.Elements("TestCase");

                foreach (XElement TestCase in TestCaseList)
                {
                    TestCaseStruct testCaseStruct = new TestCaseStruct();
                    bool bIsRepeatedId = false;

                    testCaseStruct.strTestCaseId = TestCase.Attribute("ID").Value;
                    testCaseStruct.strTitle = TestCase.Attribute("Title").Value;
                    if (null != TestCase.Attribute("AreaId"))
                    {
                        testCaseStruct.AreaId = TestCase.Attribute("AreaId").Value;
                    }
                    if (null != TestCase.Attribute("NumId"))
                    {
                        testCaseStruct.NumId = TestCase.Attribute("NumId").Value;
                    }
                    if (null != TestCase.Attribute("FailOp"))
                    {
                        testCaseStruct.strFailOp = TestCase.Attribute("FailOp").Value;
                    }

                    foreach (var EleNode in TestCase.Nodes())
                    {
                        if (EleNode.NodeType == System.Xml.XmlNodeType.Element)
                        {
                            TestCaseContent testCaseContent = new TestCaseContent();
                            XElement Node = (XElement)EleNode;

                            if ("Decp".Equals(Node.Name.LocalName))
                            {
                                testCaseContent.tag = XmlTag.DECP;
                                testCaseContent.str = Node.Value;
                                testCaseStruct.ContentList.Add(testCaseContent);
                            }
                            else if ("Step".Equals(Node.Name.LocalName))
                            {
                                testCaseContent.tag = XmlTag.STEP;
                                testCaseContent.str = Node.Value;
                                testCaseStruct.ContentList.Add(testCaseContent);
                            }
                            else if ("Expt".Equals(Node.Name.LocalName))
                            {
                                testCaseContent.tag = XmlTag.EXPT;
                                testCaseContent.str = Node.Value;
                                testCaseStruct.ContentList.Add(testCaseContent);
                            }
                            else
                            {
                                log.ErrorFormat("文件：{0} 中存在异常节点 Name: {1}, InnerXml: {2}", xmlpath, Node.Name.LocalName, Node.Value);
                                return -1;
                            }
                        }
                    }

                    foreach (TestCaseStruct tcs in listTestCase)
                    {
                        if (tcs.strTestCaseId.Equals(testCaseStruct.strTestCaseId))
                        {
                            bIsRepeatedId = true;
                            log.ErrorFormat("重复的测试用例编号：{0}", tcs.strTestCaseId);
                        }
                    }
                    if (false == bIsRepeatedId)
                    {
                        listTestCase.Add(testCaseStruct);
                    }
                }

                return 0;
            }
            catch (System.Exception ex)
            {
                log.ErrorFormat("读取文件{0}失败, Message: {1}, StackTrace: {2}", xmlpath, ex.Message, ex.StackTrace);
                return -1;
            }
        }
    }
}
