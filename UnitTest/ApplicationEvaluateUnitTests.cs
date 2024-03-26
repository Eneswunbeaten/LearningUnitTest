using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTest
{
    [TestClass]
    public class ApplicationEvaluateUnitTests
    {
         [TestMethod]
        public void Application_WithUnderAge_ShouldTransferredToAutoRejected()
        {
            var evaluator = new ApplicationEvaluator();
        }
    }
}
