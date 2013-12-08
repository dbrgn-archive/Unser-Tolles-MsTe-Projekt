using System;
using System.Collections.Generic;
using System.ServiceModel;
using AutoReservation.Common.DataTransferObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AutoReservation.Common.Interfaces;
using AutoReservation.BusinessLayer.Testing;
using AutoReservation.Dal;
using AutoReservation.BusinessLayer;

namespace AutoReservation.Testing
{
	[TestClass]
	public abstract class ServiceTestBase
	{
		protected abstract IAutoReservationService Target { get; }

		[TestInitialize]
		public void InitializeTestData()
		{
			TestEnvironmentHelper.InitializeTestData();
		}

		[TestMethod]
		public void AutosTest()
		{
			List<AutoDto> autos = Target.GetAutos();
			Assert.IsTrue(autos.Count == 3);
			Assert.AreEqual("Fiat Punto", autos[0].Marke);
		}

		[TestMethod]
		public void KundenTest()
		{
			List<KundeDto> kunden = Target.GetKunden();
			Assert.IsTrue(kunden.Count == 4);
			Assert.AreEqual("Bargen", kunden[1].Nachname);
		}

		[TestMethod]
		public void ReservationenTest()
		{
			List<ReservationDto> reservationen = Target.GetReservationen();
			Assert.IsTrue(reservationen.Count == 3);
		}

		[TestMethod]
		public void GetAutoByIdTest()
		{
			AutoDto fiat = Target.GetAutoById(1);
			Assert.AreEqual("Fiat Punto", fiat.Marke);
			Assert.AreEqual(1, fiat.Id);
		}

		[TestMethod]
		public void GetKundeByIdTest()
		{
			KundeDto bargen = Target.GetKundeById(2);
			Assert.AreEqual("Bargen", bargen.Nachname);
			Assert.AreEqual("Danilo", bargen.Vorname);
		}

		[TestMethod]
		public void GetReservationByNrTest()
		{
			ReservationDto res1 = Target.GetReservationByNr(1);
			AutoDto auto1 = res1.Auto;
			Assert.AreEqual("Fiat Punto", auto1.Marke);

		}

		[TestMethod]
		public void GetReservationByIllegalNr()
		{
			ReservationDto res999 = Target.GetReservationByNr(999);
			Assert.IsNull(res999);
		}

		[TestMethod]
		public void InsertAutoTest()
		{
			const string marke = "Volvo";
			const int tarif = 100;
			AutoDto newAuto = new AutoDto();
			newAuto.Marke = marke;
			newAuto.Tagestarif = tarif;
			newAuto.AutoKlasse = AutoKlasse.Mittelklasse;
			Target.AddAuto(newAuto);

			AutoDto newAutoRet = Target.GetAutoById(4);
			Assert.AreEqual(marke, newAutoRet.Marke);
			Assert.AreEqual(tarif, newAutoRet.Tagestarif);
			Assert.AreEqual(AutoKlasse.Mittelklasse, newAutoRet.AutoKlasse);
		}

		[TestMethod]
		public void InsertKundeTest()
		{
			const string vorname = "Dr.";
			const string nachname = "Who";
			DateTime geburtsdatum = DateTime.Now;
			KundeDto newKunde = new KundeDto();
			newKunde.Vorname = vorname;
			newKunde.Nachname = nachname;
			newKunde.Geburtsdatum = geburtsdatum;
			Target.AddKunde(newKunde);

			KundeDto newKundeRet = Target.GetKundeById(5);
			Assert.AreEqual(vorname, newKundeRet.Vorname);
			Assert.AreEqual(nachname, newKundeRet.Nachname);
			Assert.AreEqual(geburtsdatum.ToShortDateString(), newKundeRet.Geburtsdatum.ToShortDateString());

		}

		[TestMethod]
		public void InsertReservationTest()
		{
			DateTime von = DateTime.Now;
			DateTime bis = DateTime.Now;
			ReservationDto newRes = new ReservationDto();
			AutoDto fiat = Target.GetAutoById(1);
			KundeDto chrigi = Target.GetKundeById(1);
			newRes.Auto = fiat;
			newRes.Kunde = chrigi;
			newRes.Von = von;
			newRes.Bis = bis;
			Target.AddReservation(newRes);

			ReservationDto newResRet = Target.GetReservationByNr(4);
			Assert.AreEqual(fiat.Marke, newResRet.Auto.Marke);
			Assert.AreEqual(chrigi.Vorname, newResRet.Kunde.Vorname);
			Assert.AreEqual(von.ToShortDateString(), newResRet.Von.ToShortDateString());
			Assert.AreEqual(bis.ToShortDateString(), newResRet.Bis.ToShortDateString());

		}

		[TestMethod]
		public void UpdateAutoTest()
		{
			const string marke = "Volvo";
			AutoDto newAuto = Target.GetAutoById(1);
			newAuto.Marke = marke;
			AutoDto origAuto = Target.GetAutoById(1);
			Target.UpdateAuto(newAuto, origAuto);

			AutoDto newAutoRet = Target.GetAutoById(1);
			Assert.AreEqual(marke, newAutoRet.Marke);

		}

		[TestMethod]
		public void UpdateKundeTest()
		{
			const string vorname = "Elchbert";
			KundeDto newKunde = Target.GetKundeById(1);
			newKunde.Vorname = vorname;
			KundeDto origKunde = Target.GetKundeById(1);
			Target.UpdateKunde(newKunde, origKunde);

			KundeDto newKundeRet = Target.GetKundeById(1);
			Assert.AreEqual(vorname, newKundeRet.Vorname);
		}

		[TestMethod]
		public void UpdateReservationTest()
		{
			AutoDto auto = Target.GetAutoById(3);
			ReservationDto newRes = Target.GetReservationByNr(1);
			newRes.Auto = auto;
			ReservationDto origRes = Target.GetReservationByNr(1);
			Target.UpdateReservation(newRes, origRes);

			ReservationDto newResRet = Target.GetReservationByNr(1);
			Assert.AreEqual(auto.Marke, newResRet.Auto.Marke);
		}

		[TestMethod]
		public void UpdateAutoTestWithOptimisticConcurrency()
		{
			const string marke = "Volvo";
			const int tarif = 1000;
			AutoDto newAuto1 = Target.GetAutoById(1);
			newAuto1.Marke = marke;
			AutoDto newAuto2 = Target.GetAutoById(1);
			newAuto2.Tagestarif = tarif;
			AutoDto origAuto = Target.GetAutoById(1);
			Target.UpdateAuto(newAuto2, origAuto);

			try
			{
				Target.UpdateAuto(newAuto1, origAuto);
			}
			catch (LocalOptimisticConcurrencyException<Auto>)
			{
				
			}
			catch
			{
				Assert.Fail("Wrong Concurrency Exception, Auto");
			}
		}

		[TestMethod]
		public void UpdateKundeTestWithOptimisticConcurrency()
		{
			const string vorname = "Elchbert";
			const string nachname = "Gubbler";
			KundeDto newKunde1 = Target.GetKundeById(1);
			newKunde1.Vorname = vorname;
			KundeDto newKunde2 = Target.GetKundeById(1);
			newKunde2.Nachname = nachname;
			KundeDto origKunde = Target.GetKundeById(1);
			Target.UpdateKunde(newKunde2, origKunde);

			try
			{
				Target.UpdateKunde(newKunde1, origKunde);
			}
			catch (LocalOptimisticConcurrencyException<Kunde>)
			{
			
			} 
			catch
			{
				Assert.Fail("Wrong Concurrency Exception, Kunde");
			}
		
		}

		[TestMethod]
		public void UpdateReservationTestWithOptimisticConcurrency()
		{
			Assert.Inconclusive("Test wurde noch nicht implementiert!");
		}

		[TestMethod]
		public void DeleteKundeTest()
		{
			KundeDto kunde = Target.GetKundeById(1);
			Target.RemoveKunde(kunde);
			Assert.AreEqual(3, Target.GetKunden().Count);
		}

		[TestMethod]
		public void DeleteAutoTest()
		{
			AutoDto auto = Target.GetAutoById(1);
			Target.RemoveAuto(auto);
			Assert.AreEqual(2, Target.GetAutos().Count);	
		}

		[TestMethod]
		public void DeleteReservationTest()
		{
			ReservationDto res = Target.GetReservationByNr(1);
			Target.RemoveReservation(res);
			Assert.AreEqual(2, Target.GetReservationen().Count);
		}
	}
}
