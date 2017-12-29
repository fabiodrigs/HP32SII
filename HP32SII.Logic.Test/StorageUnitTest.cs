using NUnit.Framework;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using HP32SII;

namespace HP32SII.Logic.Test
{
    [TestFixture, ExcludeFromCodeCoverage]
    class StorageUnitTest
    {
        StorageUnit storageUnit = new StorageUnit();

        [Test]
        public void TestStoreAndRecall()
        {
            string key = "S";
            double value = 27.0;

            storageUnit.Store(key, value);
            Assert.That(value, Is.EqualTo(storageUnit.Recall(key)));
        }

        [Test]
        public void TestClearAll()
        {
            storageUnit.Store("A", 1.0);
            storageUnit.Store("Z", 26.0);

            storageUnit.ClearAll();

            Assert.That(0.0, Is.EqualTo(storageUnit.Recall("A")));
            Assert.That(0.0, Is.EqualTo(storageUnit.Recall("Z")));
        }
    }
}
