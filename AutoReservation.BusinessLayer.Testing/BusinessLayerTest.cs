using System;
using AutoReservation.BusinessLayer;
using AutoReservation.Dal;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace AutoReservation.BusinessLayer.Testing
{
    [TestClass]
    public class BusinessLayerTest
    {
        private AutoReservationBusinessComponent target;
        private AutoReservationBusinessComponent Target
        {
            get
            {
                if (target == null)
                {
                    target = new AutoReservationBusinessComponent();
                }
                return target;
            }
        }


        [TestInitialize]
        public void InitializeTestData()
        {
            TestEnvironmentHelper.InitializeTestData();
        }

        [TestMethod]
        public void AlwaysPass() 
        {
            Console.WriteLine("Always Pass");
            System.Diagnostics.Debug.WriteLine("Always Pass");
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void UpdateAutoTest()
        {
            string marke = "Volvo";
            var fiat = Target.GetAutoById(1);
            fiat.Marke = marke;
            var fiatOrig = Target.GetAutoById(1);
            Target.UpdateAuto(fiat, fiatOrig);
            var newFiat = Target.GetAutoById(1);

            Assert.AreEqual(marke, newFiat.Marke);
        }

        [TestMethod]
        public void AutoConcurencyTest()
        {
            var fiat1 = Target.GetAutoById(1);
            fiat1.Marke = "Volvo";
            var fiatOrig = Target.GetAutoById(1);
            var fiat2 = Target.GetAutoById(1);
            fiat2.Marke = "Audi";
            Target.UpdateAuto(fiat2, fiatOrig);

            try {
                Target.UpdateAuto(fiat1, fiatOrig);
            } catch (LocalOptimisticConcurrencyException<Auto>) {

            } catch {
                Assert.Fail("Optimistic concurrency exception fail");
            }
        }

        [TestMethod]
        public void UpdateKundeTest()
        {
            //Assert.Inconclusive("Test wurde noch nicht implementiert!");
        }

        [TestMethod]
        public void UpdateReservationTest()
        {
            //Assert.Inconclusive("Test wurde noch nicht implementiert!");
        }

    }
}
