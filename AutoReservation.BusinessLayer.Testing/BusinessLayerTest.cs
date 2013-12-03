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
            const string marke = "Volvo";
            Auto fiat = Target.GetAutoById(1);
            fiat.Marke = marke;
            Auto fiatOrig = Target.GetAutoById(1);
            Target.UpdateAuto(fiat, fiatOrig);
            Auto newFiat = Target.GetAutoById(1);

            Assert.AreEqual(marke, newFiat.Marke);
        }

        [TestMethod]
        public void AutoConcurencyTest()
        {
            Auto fiat1 = Target.GetAutoById(1);
            fiat1.Marke = "Volvo";
            Auto fiatOrig = Target.GetAutoById(1);
            Auto fiat2 = Target.GetAutoById(1);
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
            const string vorname = "Trent";
            const string nachname = "Reznor";
            Kunde kunde1 = Target.GetKundeById(1);
            kunde1.Nachname = nachname;
            kunde1.Vorname = vorname;
            Kunde kundeOrig = Target.GetKundeById(1);
            Target.UpdateKunde(kunde1, kundeOrig);

            Kunde newKunde1 = Target.GetKundeById(1);

            Assert.AreEqual(nachname, kunde1.Nachname);
            Assert.AreEqual(vorname, newKunde1.Vorname);
        }

        [TestMethod]
        public void KundeConcurencyTest()
        {
            const string vorname = "Trent";
            const string nachname = "Reznor";
            Kunde kunde1 = Target.GetKundeById(1);
            kunde1.Nachname = nachname;
            Kunde kunde2 = Target.GetKundeById(1);
            kunde2.Vorname = vorname;
            Kunde kundeOrig = Target.GetKundeById(1);
            Target.UpdateKunde(kunde2, kundeOrig);

            try {
                Target.UpdateKunde(kunde1, kundeOrig);
            } catch (LocalOptimisticConcurrencyException<Kunde>) {

            } catch {
                Assert.Fail("Wrong concurrency exception fail");
            }
        }

        [TestMethod]
        public void UpdateReservationTest()
        {
            DateTime bisDate = DateTime.Now;
            Auto newAuto = new StandardAuto();
            Reservation res1 = Target.GetReservationByNr(1);
            res1.Bis = bisDate;
            res1.Auto = newAuto;
            Reservation resOrig = Target.GetReservationByNr(1);
            Target.UpdateReservation(res1, resOrig);

            Reservation newRes = Target.GetReservationByNr(1);
            Assert.AreEqual(bisDate, newRes.Bis);
            Assert.AreEqual(newAuto, newRes.Auto);

        }

        [TestMethod]
        public void ReservationConcurrencyTest()
        {
            DateTime bisDate = DateTime.Now;
            Auto newAuto = new StandardAuto();
            Reservation res1 = Target.GetReservationByNr(1);
            res1.Bis = bisDate;
            Reservation res2 = Target.GetReservationByNr(1);
            res2.Auto = newAuto;
            Reservation resOrig = Target.GetReservationByNr(1);
            Target.UpdateReservation(res2, resOrig);

            try {
                Target.UpdateReservation(res1, resOrig);
            } catch (LocalOptimisticConcurrencyException<Reservation>) {

            } catch {
                Assert.Fail("Wrong concurrency exception fail");
            }
        }

    }
}
