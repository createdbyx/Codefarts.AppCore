namespace AppCoreTests
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using Codefarts.AppCore;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class BindableCollection_Events_DefaultProvider
    {
        private IPlatformProvider provider;
        Dictionary<NotifyCollectionChangedAction, int> raisedEvents;

        [TestInitialize]
        public virtual void TestInit()
        {
            this.provider = new DefaultPlatformProvider();

            this.raisedEvents = new Dictionary<NotifyCollectionChangedAction, int>();
            var enums = Enum.GetValues(typeof(NotifyCollectionChangedAction));
            foreach (NotifyCollectionChangedAction value in enums)
            {
                this.raisedEvents[value] = 0;
            }
        }

        [TestCleanup]
        public virtual void TestCleanup()
        {
            PlatformProvider.Current = this.provider;
            this.raisedEvents.Clear();
            this.raisedEvents = null;
        }

        private void CheckEventCount(int expectedAdds, int expectedMoves, int expectedRemoves, int expectedReplaces, int expectedResets)
        {
            Assert.AreEqual(expectedAdds, this.raisedEvents[NotifyCollectionChangedAction.Add], $"For action '{NotifyCollectionChangedAction.Add}'");
            Assert.AreEqual(expectedMoves, this.raisedEvents[NotifyCollectionChangedAction.Move], $"For action '{NotifyCollectionChangedAction.Move}'");
            Assert.AreEqual(expectedRemoves, this.raisedEvents[NotifyCollectionChangedAction.Remove], $"For action '{NotifyCollectionChangedAction.Remove}'");
            Assert.AreEqual(expectedReplaces, this.raisedEvents[NotifyCollectionChangedAction.Replace], $"For action '{NotifyCollectionChangedAction.Replace}'");
            Assert.AreEqual(expectedResets, this.raisedEvents[NotifyCollectionChangedAction.Reset], $"For action '{NotifyCollectionChangedAction.Reset}'");
        }

        [TestMethod]
        public void Add()
        {
            var list = new BindableCollection<int>();
            list.CollectionChanged += (s, e) => { this.raisedEvents[e.Action] += 1; };
            for (var i = 0; i < 6; i++)
            {
                list.Add(i);
            }

            this.CheckEventCount(expectedAdds: 6, 0, 0, 0, 0);
        }

        [TestMethod]
        public void AddRange()
        {
            var list = new BindableCollection<int>();
            list.CollectionChanged += (s, e) => { this.raisedEvents[e.Action] += 1; };
            list.AddRange(new[] { 0, 1, 2, 3, 4, 5 });
            this.CheckEventCount(0, 0, 0, 0, expectedResets: 1);
        }

        [TestMethod]
        public void ClearItems()
        {
            var list = new BindableCollection<int>();
            list.CollectionChanged += (s, e) => { this.raisedEvents[e.Action] += 1; };

            list.AddRange(new[] { 0, 1, 2, 3, 4, 5 });

            list.Clear();

            this.CheckEventCount(0, 0, 0, 0, expectedResets: 2);
        }

        [TestMethod]
        public void Remove()
        {
            var list = new BindableCollection<int>();
            list.CollectionChanged += (s, e) => { this.raisedEvents[e.Action] += 1; };

            list.AddRange(new[] { 0, 1, 2, 3, 4, 5 });

            list.RemoveAt(0);

            // Note: reset once because of call to AddRange
            this.CheckEventCount(0, 0, expectedRemoves: 1, 0, expectedResets: 1);
        }

        [TestMethod]
        public void SetItem()
        {
            var list = new BindableCollection<int>();
            list.CollectionChanged += (s, e) => { this.raisedEvents[e.Action] += 1; };

            list.AddRange(new int[6]);
            for (var i = 0; i < list.Count; i++)
            {
                list[i] = i;
            }

            // Note: reset once because of call to AddRange
            this.CheckEventCount(0, 0, 0, expectedReplaces: 6, expectedResets: 1);
        }

        [TestMethod]
        public void InsertItem()
        {
            var list = new BindableCollection<int>();
            list.CollectionChanged += (s, e) => { this.raisedEvents[e.Action] += 1; };
            for (var i = 0; i < 6; i++)
            {
                list.Insert(i, i);
            }

            this.CheckEventCount(expectedAdds: 6, 0, 0, 0, 0);
        }

        [TestMethod]
        public void RemoveItems()
        {
            var list = new BindableCollection<int>();
            list.CollectionChanged += (s, e) => { this.raisedEvents[e.Action] += 1; };

            list.AddRange(new[] { 0, 1, 2, 3, 4, 5 });
            list.RemoveRange(new[] { 0, 2, 4 });

            // should be 2 resets One from AddRange and another from RemoveRange
            this.CheckEventCount(0, 0, 0, 0, expectedResets: 2);
        }
    }
}