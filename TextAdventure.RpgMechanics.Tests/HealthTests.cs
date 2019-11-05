using System;
using NUnit.Framework;
using TextAdventure.RpgMechanics.Calculations;

namespace TextAdventure.RpgMechanics.Tests
{
    public class HealthTests
    {
        Health health;

        [SetUp]
        public void Setup()
        {
            health = new Health(100);
        }

        [NUnit.Framework.TearDown]
        public void TearDown()
        {

        }

        [Test]
        public void Test1()
        {
            health.ValueChanged += (sender, e) => Assert.Fail();
            Assert.Pass();
        }
    }
}