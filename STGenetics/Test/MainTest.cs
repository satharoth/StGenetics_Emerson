using Bogus;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using STGenetics.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace STGenetics.Test
{
    [TestClass]
    public class MainTest
    {

        [TestMethod]
        public void TestMethod1()
        {
            Assert.AreEqual("1", "1");
        }

        [TestMethod]
        public void TestMethod2()
        {
            var mock = new Mock<IAnimals>();
            
            var i = 0;
            for (i = 0; i < 5; i++)
            {
                var animals = new Faker<Animals>()
                    .RuleForType(typeof(string), c => c.Random.Word())
                    .RuleForType(typeof(int), c => c.Random.Number(1, 2))
                    .RuleForType(typeof(double), c => c.Random.Double(1000, 9000));
                mock.Setup(x => x.CreateAnimals(animals)).ReturnsAsync(animals);
                Console.WriteLine(animals.Generate().Dump());
            }
            Assert.AreEqual(5, i);
        } 
    }
}
