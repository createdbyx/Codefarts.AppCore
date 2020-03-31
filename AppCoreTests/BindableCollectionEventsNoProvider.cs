namespace AppCoreTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    [TestCategory("Collections Events")]
    public class BindableCollectionEventsNoProvider : BindableCollectionEventsDefaultProvider
    {
        [TestInitialize]
        public override void TestInit()
        {
            base.TestInit();
            this.provider = null;
        }
    }
}