using NUnit.Framework;

namespace GWC.Web.Testing.IntegrationTesting
{
    public class BillingCalendarServiceIntegrationFixture : BaseIntegrationTestFixture
    {
        [Test]
        public void GetBillingDate()
        {
            var billingCalendarDtos = billingCalendarService.GetAll();
            Assert.That(billingCalendarDtos, Is.Not.Null);
        }
    }
}