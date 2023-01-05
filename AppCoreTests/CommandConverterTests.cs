using System.Globalization;
using AppCoreTests.Mockups;
using Codefarts.AppCore.Commands;
using Codefarts.AppCore.Interfaces;
using Codefarts.AppCore.Wpf.Commands;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AppCoreTests
{
    [TestClass]
    [TestCategory("CommandConverter")]
    public class CommandConverterTests
    {
        [TestInitialize]
        public void StartUp()
        {
        }

        [TestCleanup]
        public void Cleanup()
        {
        }

        [TestMethod]
        public void GenericValidConvert()
        {
            var converter = new CommandConverter();
            var command = new DelegateCommand();
            var result = converter.Convert(command, typeof(System.Windows.Input.ICommand), null, CultureInfo.CurrentCulture);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GenericValidConvertBack()
        {
            var converter = new CommandConverter();
            var command = new DelegateCommand();

            var convertResult = converter.Convert(command, typeof(System.Windows.Input.ICommand), null, CultureInfo.CurrentCulture);

            var convertBackResult = converter.ConvertBack(convertResult, typeof(ICommand<object>), null, CultureInfo.CurrentCulture);

            Assert.IsNotNull(convertBackResult);
            Assert.AreSame(command, convertBackResult);
        }

        [TestMethod]
        public void ValidConvert()
        {
            var converter = new CommandConverter();
            var command = new EmptyCommand();
            var result = converter.Convert(command, typeof(System.Windows.Input.ICommand), null, CultureInfo.CurrentCulture);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ValidConvertBack()
        {
            var converter = new CommandConverter();
            var command = new EmptyCommand();

            var convertResult = converter.Convert(command, typeof(System.Windows.Input.ICommand), null, CultureInfo.CurrentCulture);

            var convertBackResult = converter.ConvertBack(convertResult, typeof(ICommand<object>), null, CultureInfo.CurrentCulture);

            Assert.IsNotNull(convertBackResult);
            Assert.AreSame(command, convertBackResult);
        }

        [TestMethod]
        public void ConvertWithNullValue()
        {
            var converter = new CommandConverter();

            var result = converter.Convert(null, typeof(System.Windows.Input.ICommand), null, CultureInfo.CurrentCulture);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void ConvertBackWithNullValue()
        {
            var converter = new CommandConverter();

            var result = converter.ConvertBack(null, typeof(System.Windows.Input.ICommand), null, CultureInfo.CurrentCulture);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void ConvertExistingWpfCommand()
        {
            var converter = new CommandConverter();
            var command = new SimpleWpfCommand();
            var result = converter.Convert(command, typeof(System.Windows.Input.ICommand), null, CultureInfo.CurrentCulture);

            Assert.IsNotNull(result);
            Assert.AreSame(command, result);
        }
    }
}