using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ExternalApps.UnitTests
{
    [TestClass]
    public class CustomerTests
    {
        public Mock<Mail> _MockMail;

        [TestInitialize]
        public void SetUp()
        {
            _MockMail = new Mock<Mail>();
        }

        [TestMethod]
        public void AddCustomer_Always_ReturnsTrue()
        {
            _MockMail.Setup(x => x.Send("smtp.gmail.com", "from@gmail.com", "jasdjasd93003endd=", "to@gmail.com", "subject", "body", 25));
            Customer c1 = new Customer();
            bool IsInserted = c1.AddCustomer(_MockMail.Object);

            Assert.IsTrue(IsInserted);
            Assert.AreEqual(IsInserted, true);
        }
    }
}
