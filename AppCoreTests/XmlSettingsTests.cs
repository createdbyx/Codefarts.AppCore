using System;
using System.IO;
using System.Reflection;
using Codefarts.AppCore.SettingProviders.Xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AppCoreTests;

[TestClass]
[TestCategory("XmlSettings")]
public class XmlSettingsTests
{
    private string fileName;

    [TestInitialize]
    public void Setup()
    {
        this.fileName = Path.GetTempFileName();
        if (File.Exists(this.fileName))
        {
            File.Delete(this.fileName);
        }
    }


    [TestCleanup]
    public void Cleanup()
    {
        if (File.Exists(this.fileName))
        {
            File.Delete(this.fileName);
        }
    }

    [TestMethod]
    public void CtorWithFilenameAndCreateTrue()
    {
        var settings = new XmlSettingsProvider(this.fileName, true);
        Assert.IsTrue(File.Exists(this.fileName));
    }

    [TestMethod]
    public void CtorWithNullFilename()
    {
        Assert.ThrowsException<ArgumentNullException>(() =>
        {
            var settings = new XmlSettingsProvider(null, true);
            Assert.IsFalse(File.Exists(this.fileName));
        });
    }

    [TestMethod]
    public void CtorWithEmptyFilename()
    {
        Assert.ThrowsException<ArgumentNullException>(() =>
        {
            var settings = new XmlSettingsProvider(string.Empty, true);
            Assert.IsFalse(File.Exists(this.fileName));
        });
    }

    [TestMethod]
    public void CtorWithWhitespaceFilename()
    {
        Assert.ThrowsException<ArgumentNullException>(() =>
        {
            var settings = new XmlSettingsProvider("  ", true);
            Assert.IsFalse(File.Exists(this.fileName));
        });
    }


    [TestMethod]
    public void CtorWithBadDirectoryCharactersInFilename()
    {
        Assert.ThrowsException<ArgumentException>(() =>
        {
            var file = Path.Combine(Path.GetTempPath(), "aaa" + Path.GetInvalidPathChars()[0] + "bbb", "tsst.xml");
            var settings = new XmlSettingsProvider(file, true);
            Assert.IsFalse(File.Exists(this.fileName));
        });
    }


    [TestMethod]
    public void CtorWithBadCharactersInFilename()
    {
        Assert.ThrowsException<ArgumentException>(() =>
        {
            var file = Path.Combine(Path.GetTempPath(), "aaa" + Path.GetInvalidFileNameChars()[0] + "bbb.xml");
            var settings = new XmlSettingsProvider(file, true);
            Assert.IsFalse(File.Exists(this.fileName));
        });
    }

    [TestMethod]
    public void CtorWithMissingDestinationDirectory()
    {
        string dirName = Path.Combine(Path.GetTempPath(), "aaa");
        var file = Path.Combine(dirName, "test.xml");

        if (Directory.Exists(dirName))
        {
            Directory.Delete(dirName, true);
        }
        
        var settings = new XmlSettingsProvider(file, true);
        Assert.IsFalse(File.Exists(this.fileName));
    }
}