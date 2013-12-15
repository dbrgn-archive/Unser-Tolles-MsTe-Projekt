using System.Windows.Input;
using AutoReservation.Testing;
using AutoReservation.Ui.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AutoReservation.BusinessLayer.Testing;
using System;

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
            AutoViewModel avm = new AutoViewModel();
            var relayCmd = avm.LoadCommand;
            var autos = avm.Autos;
            Assert.IsTrue(relayCmd.CanExecute(null));
            Assert.IsTrue(autos.Count == 3);
        }

        [TestMethod]
        public void KundenLoadTest()
        {
            KundeViewModel kvm = new KundeViewModel();
            var relayCmd = kvm.LoadCommand;
            var kunden = kvm.Kunden;
            Assert.IsTrue(relayCmd.CanExecute(null));
            Assert.IsTrue(kunden.Count == 4);
        }

        [TestMethod]
        public void ReservationenLoadTest()
        {
            ReservationViewModel rvm = new ReservationViewModel();
            var relayCmd = rvm.LoadCommand;
            var reservationen = rvm.Reservationen;
            Assert.IsTrue(relayCmd.CanExecute(null));
            Assert.IsTrue(reservationen.Count == 3);
        }
    }
}