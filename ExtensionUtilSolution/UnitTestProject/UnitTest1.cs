using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LinqExtensionUtils;
using ReflectionExtensionUtils;
using LogUtils;
using System.Collections.Generic;
using EmailUtils;
using WCFDynamicProxy;

namespace UnitTestProject
{
    public class Temp
    {
        public String TestString { get; set; }
    }
    [TestClass]
    public class UnitTest1
    {
        public object DynamicProxyFactory { get; private set; }

        [TestMethod]
        public void TestMethod1()
        {
            Temp c = new Temp();
            String v = c.MemberName(x => x.TestString);
            Assert.AreEqual("TestString", v);
            String q = MembersOf<Temp>.GetName(x => x.TestString);
            Assert.AreEqual("TestString", q);
        }

        [TestMethod]
        public void TestLog()
        {
            ApplicationLog.Instance.WriteDebug("Hi");
        }

        [TestMethod]
        public void TestLogging()
        {
            for (int i = 0; i < 1500000; i++)
            {
                //ApplicationLog.Instance.WriteInfo(String.Format("{0} :: {1} :: {2} :: {3} :: {4} :: {5} :: {6} :: {7} :: {8} :: {9} ::", DateTime.Now, DateTime.UtcNow, DateTime.UtcNow, DateTime.UtcNow, DateTime.UtcNow, DateTime.UtcNow, DateTime.UtcNow, DateTime.UtcNow, DateTime.UtcNow, DateTime.UtcNow));
                Temp c = new Temp()
                {
                    TestString = String.Format("{0} :: {1} :: {2} :: {3} :: {4} :: {5} :: {6} :: {7} :: {8} :: {9} ::", DateTime.Now, DateTime.UtcNow, DateTime.UtcNow, DateTime.UtcNow, DateTime.UtcNow, DateTime.UtcNow, DateTime.UtcNow, DateTime.UtcNow, DateTime.UtcNow, DateTime.UtcNow),
                };
                List<Temp> lst = new List<Temp>() { c };
                lst.WriteFromList(null, "WriteDebug");
                c.WriteFromObject();
            }
        }

        [TestMethod]
        public void TestEmailUtil()
        {
            List<EmailUtil> lst = new List<EmailUtil>()
            {
                new EmailUtil() { ToEmail = "gaikwadsd01@gmail.com", ToName = "Sunita Gaikwad" },
                new EmailUtil() { ToEmail = "pratik.gaikwad@outlook.in", ToName = "Pratik outlook" }
            };
            foreach (EmailUtil u in lst)
            {
                u.Subject = String.Format("Test mail - {0}", DateTime.Today.Date.ToString("MM-dd-yyyy"));
                u.Body = "Please find attached file for today.";
                bool b = EmailUtil.Instance.SendEmail(u, strAttchmentFileName: @"C:\PortfoliExcels\07-18-2015.xlsx");
                Assert.IsTrue(b);
            }
        }

        [TestMethod]
        public void TestWCFProxy()
        {
            string wsdlUrl = "http://localhost/HappyHutService/HappyHutService.svc";
            DynamicProxyFactory factory = new DynamicProxyFactory(string.Format("{0}?wsdl", wsdlUrl));
            Assert.IsNotNull(factory);
        }
    }
}
