namespace AppCoreTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    [TestCategory("Collections")]
    public class BindableCollectionNoProvider : BindableCollectionDefaultProvider
    {
        [TestInitialize]
        public override void TestInit()
        {
            base.TestInit();
            this.provider = null;
        }
    }
}