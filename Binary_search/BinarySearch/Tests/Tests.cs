using Library;
using NUnit.Framework;
using PersonNS;

namespace Tests {
    [TestFixture]
    public class Test {
        [SetUp]
        public void Init() {
            gen = new Generator();
        }

        private Generator gen;

        /* [Test]
        public void DummyTest() 
        {
            Assert.AreEqual(1, 1);
        } */

        [Test]
        public void TestTooLow()
        {
            var expected = -1;
            var actual = MainClass.TestArray(gen.NextArray(10, 10));
            
            Assert.AreEqual(expected, actual);

        }

        [Test]
        public void TestTooHigh()
        {
            
            Assert.AreNotEqual(expected, actual);
        }

        [Test]
        public void TestElement()
        {
            
            
            Assert.AreNotEqual(expected, actual);
        }

        [Test]
        public void TestEmptyArray()
        {
            
            Assert.AreEqual(expected, actual);
        }
    }
}
