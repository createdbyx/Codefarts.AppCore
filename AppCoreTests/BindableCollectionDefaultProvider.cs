namespace AppCoreTests
{
    using Codefarts.AppCore;
    using Codefarts.AppCore.Interfaces;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    [TestCategory("Collections")]
    public class BindableCollectionDefaultProvider
    {
        protected IPlatformProvider provider;

        [TestInitialize]
        public virtual void TestInit()
        {
            this.provider = new DefaultPlatformProvider();
        }

        [TestCleanup]
        public virtual void TestCleanup()
        {
        }

        [TestMethod]
        public void Add()
        {
            var list = new BindableCollection<int>(this.provider);
            for (var i = 0; i < 6; i++)
            {
                list.Add(i);
            }

            Assert.AreEqual(6, list.Count);

            for (var i = 0; i < list.Count; i++)
            {
                Assert.AreEqual(i, list[i], "Bad value at index: " + i);
            }
        }

        [TestMethod]
        public void AddRange()
        {
            var list = new BindableCollection<int>(this.provider);
            list.AddRange(new[] { 0, 1, 2, 3, 4, 5 });
            Assert.AreEqual(6, list.Count);
        }

        [TestMethod]
        public void ClearItems()
        {
            var list = new BindableCollection<int>(this.provider);
            list.AddRange(new[] { 0, 1, 2, 3, 4, 5 });
            Assert.AreEqual(6, list.Count);
            list.Clear();
            Assert.AreEqual(0, list.Count);
        }

        [TestMethod]
        public void Remove()
        {
            var list = new BindableCollection<int>(this.provider);
            list.AddRange(new[] { 0, 1, 2, 3, 4, 5 });
            Assert.AreEqual(6, list.Count);

            list.RemoveAt(0);
            Assert.AreEqual(5, list.Count);
        }

        [TestMethod]
        public void SetItem()
        {
            var list = new BindableCollection<int>(this.provider);
            list.AddRange(new int[6]);
            for (var i = 0; i < list.Count; i++)
            {
                list[i] = i;
            }

            Assert.AreEqual(6, list.Count);

            for (var i = 0; i < list.Count; i++)
            {
                Assert.AreEqual(i, list[i], "Bad value at index: " + i);
            }
        }

        [TestMethod]
        public void InsertItem()
        {
            var list = new BindableCollection<int>(this.provider);
            for (var i = 0; i < 6; i++)
            {
                list.Insert(i, i);
            }

            Assert.AreEqual(6, list.Count);

            for (var i = 0; i < list.Count; i++)
            {
                Assert.AreEqual(i, list[i], "Bad value at index: " + i);
            }
        }

        [TestMethod]
        public void RemoveItems()
        {
            var list = new BindableCollection<int>(this.provider);
            list.AddRange(new[] { 0, 1, 2, 3, 4, 5 });
            Assert.AreEqual(6, list.Count);

            list.RemoveRange(new[] { 0, 2, 4 });
            Assert.AreEqual(3, list.Count);

            foreach (var value in list)
            {
                switch (value)
                {
                    case 0:
                    case 2:
                    case 4:
                        Assert.Fail("List contains value that should have been removed! Value: " + value);
                        break;
                }
            }
        }
    }
}
