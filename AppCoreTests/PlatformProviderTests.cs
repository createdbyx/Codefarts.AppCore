namespace AppCoreTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Codefarts.AppCore;

    [TestClass]
    public class PlatformProviderTests
    {
        [TestMethod]
        public void DefaultProvider()
        {
            Assert.IsInstanceOfType(PlatformProvider.Current, typeof(DefaultPlatformProvider));
        }
    }
}