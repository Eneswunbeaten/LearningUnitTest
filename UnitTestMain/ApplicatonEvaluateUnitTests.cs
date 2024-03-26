using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTestMain
{
    [TestClass]
    public class ApplicatonEvaluateUnitTests
    {
        [TestMethod]
        public void Application_WithUnderAge_ShouldTransferredToAutoRejected()
        {
            var evaluator = new ApplicationEvaluator();
        }
    }
}
