﻿using System.Windows.Input;
using AutoReservation.Testing;
using AutoReservation.Ui.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AutoReservation.BusinessLayer.Testing;

namespace AutoReservation.Ui.Testing
{
    [TestClass]
    public class ViewModelTest
    {
        [TestInitialize]
        public void InitializeTestData()
        {
            TestEnvironmentHelper.InitializeTestData();
        }

        [TestMethod]
        public void AutosLoadTest()
        {
            Assert.Inconclusive("Test wurde noch nicht implementiert!");
        }

        [TestMethod]
        public void KundenLoadTest()
        {
            Assert.Inconclusive("Test wurde noch nicht implementiert!");
        }

        [TestMethod]
        public void ReservationenLoadTest()
        {
            Assert.Inconclusive("Test wurde noch nicht implementiert!");
        }
    }
}