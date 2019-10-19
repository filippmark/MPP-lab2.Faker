using FakerImplementation;
using ClassesForDTO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace FakerTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void ShouldCtorParamsInit()
        {
            var faker = new Faker();
            var dto = faker.Create<ClassWithParametrCTOR>();
            Assert.AreNotEqual(dto.ch, default(long));
            Assert.AreNotEqual(dto.dateTime, default(DateTime));
            Assert.AreNotEqual(dto.str, default(string));
        }

        [TestMethod]
        public void ShouldRecClassBeNull()
        {
            var faker = new Faker();
            var dto = faker.Create<CycleDepClass1>();
            Assert.IsNull(dto.Cl.Cl);
        }

        [TestMethod]
        public void ShouldInitStruct()
        {
            var faker = new Faker();
            var dto = faker.Create<CycleDepClass1>();
            Assert.IsNotNull(dto.S1);
            Assert.AreNotEqual(dto.S1.X, default(int));
        }

        [TestMethod]
        public void ShouldInitClassWithInner()
        {
            var faker = new Faker();
            var dto = faker.Create<ClassWithInner>();
            Assert.IsNull(dto.dict);
            Assert.AreNotEqual(dto.str, default);
            Assert.IsNotNull(dto.NoInner);
        }

        [TestMethod]
        public void ShouldInitList()
        {
            var faker = new Faker();
            var dto = faker.Create<ClassWithNoInner>();
            Assert.AreNotEqual(dto.list[0], default);
        }

        [TestMethod]
        public void ShouldNotInitListWithCycle()
        {
            var faker = new Faker();
            var dto = faker.Create<CycleDepClass1>();
            Assert.AreEqual(dto.List[0], default);
        }

        [TestMethod]

        public void ShouldNotInitSysClass()
        {
            var faker = new Faker();
            var dto = faker.Create<System.Xml.Serialization.XmlSerializer>();
            Assert.AreEqual(dto, default);
        }

        private class PrivateCtr
        {
            private PrivateCtr()
            {

            }
        }

        [TestMethod]
        public void ShouldHandlePrivateCtr()
        {
            var faker = new Faker();
            var dto = faker.Create<PrivateCtr>();
            Assert.AreEqual(dto, default);
        }
    }
}
