using System;
using System.Collections.Generic;
using System.ServiceModel;
using AutoReservation.Common.DataTransferObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AutoReservation.Common.Interfaces;
using AutoReservation.BusinessLayer.Testing;

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
			const int id = 4;
			const string marke = "Volvo";
			const int tarif = 100;
			AutoDto newAuto = new AutoDto();
			newAuto.Id = id;
			newAuto.Marke = marke;
			newAuto.Tagestarif = tarif;
			newAuto.AutoKlasse = AutoKlasse.Mittelklasse;
			Target.AddAuto(newAuto);

			AutoDto newAutoRet = Target.GetAutoById(id);
			Assert.AreEqual(marke, newAutoRet.Marke);
			Assert.AreEqual(tarif, newAutoRet.Tagestarif);
			Assert.AreEqual(AutoKlasse.Mittelklasse, newAutoRet.AutoKlasse);
		}

		[TestMethod]
		public void InsertKundeTest()
		{
			Assert.Inconclusive("Test wurde noch nicht implementiert!");
		}

		[TestMethod]
		public void InsertReservationTest()
		{
			Assert.Inconclusive("Test wurde noch nicht implementiert!");
		}

		[TestMethod]
		public void UpdateAutoTest()
		{
			Assert.Inconclusive("Test wurde noch nicht implementiert!");
		}

		[TestMethod]
		public void UpdateKundeTest()
		{
			Assert.Inconclusive("Test wurde noch nicht implementiert!");
		}

		[TestMethod]
		public void UpdateReservationTest()
		{
			Assert.Inconclusive("Test wurde noch nicht implementiert!");
		}

		[TestMethod]
		public void UpdateAutoTestWithOptimisticConcurrency()
		{
			Assert.Inconclusive("Test wurde noch nicht implementiert!");
		}

		[TestMethod]
		public void UpdateKundeTestWithOptimisticConcurrency()
		{
			Assert.Inconclusive("Test wurde noch nicht implementiert!");
		}

		[TestMethod]
		public void UpdateReservationTestWithOptimisticConcurrency()
		{
			Assert.Inconclusive("Test wurde noch nicht implementiert!");
		}

		[TestMethod]
		public void DeleteKundeTest()
		{
			Assert.Inconclusive("Test wurde noch nicht implementiert!");
		}

		[TestMethod]
		public void DeleteAutoTest()
		{
			Assert.Inconclusive("Test wurde noch nicht implementiert!");
		}

		[TestMethod]
		public void DeleteReservationTest()
		{
			Assert.Inconclusive("Test wurde noch nicht implementiert!");
		}
	}
}
