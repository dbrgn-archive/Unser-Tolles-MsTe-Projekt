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
			Assert.Inconclusive("Test wurde noch nicht implementiert!");
		}

		[TestMethod]
		public void GetReservationByIllegalNr()
		{
			Assert.Inconclusive("Test wurde noch nicht implementiert!");
		}

		[TestMethod]
		public void InsertAutoTest()
		{
			Assert.Inconclusive("Test wurde noch nicht implementiert!");
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
