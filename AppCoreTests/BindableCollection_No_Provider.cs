namespace AppCoreTests
{
    using Codefarts.AppCore;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class BindableCollection_No_Provider : BindableCollection_DefaultProvider
    {
        [TestInitialize]
        public override void TestInit()
        {
            base.TestInit();
            PlatformProvider.Current = null;
        }
    }
}