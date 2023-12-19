using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MBM.BillingEngine;

namespace MBMUnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
        }

        [TestMethod]
        public void SyncCSGTestMethod1()
        {
            MBM.BillingEngine.SyncCSGLogic s = new SyncCSGLogic();
            s.SyncCSG(930,"sdas020","TPC10006");
        }
    }
}
