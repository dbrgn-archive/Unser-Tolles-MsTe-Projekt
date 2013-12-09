using AutoReservation.BusinessLayer;
using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Common.Interfaces;
using AutoReservation.Dal;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.ServiceModel;

namespace AutoReservation.Service.Wcf
{
	public class AutoReservationService : IAutoReservationService
	{
		private AutoReservationBusinessComponent businessComponent;

		private AutoReservationBusinessComponent getBusinessComponent()
		{
			if (businessComponent == null)
			{
				businessComponent = new AutoReservationBusinessComponent();
			}
			return businessComponent;
		}

		private static void WriteActualMethod()
		{
			Console.WriteLine("Calling: " + new StackTrace().GetFrame(1).GetMethod().Name);
		}

		public List<AutoDto> GetAutos()
		{
			AutoReservationService.WriteActualMethod();
			return this.getBusinessComponent().GetAutos().ConvertToDtos();
		}

		public void AddAuto(AutoDto auto)
		{
			AutoReservationService.WriteActualMethod();
			this.getBusinessComponent().AddAuto(auto.ConvertToEntity());
		}

		public void RemoveAuto(AutoDto auto)
		{
			AutoReservationService.WriteActualMethod();
			this.getBusinessComponent().RemoveAuto(auto.ConvertToEntity());
		}

		public void UpdateAuto(AutoDto modified, AutoDto original)
		{
			AutoReservationService.WriteActualMethod();
			try
			{
				this.getBusinessComponent().UpdateAuto(modified.ConvertToEntity(), original.ConvertToEntity());
			}
			catch (LocalOptimisticConcurrencyException<Auto> e)
			{
				FaultException faultException = new FaultException<AutoDto>( e.MergedEntity.ConvertToDto(), e.Message);
				throw faultException;
			}
			
		}

		public AutoDto GetAutoById(int id)
		{
			AutoReservationService.WriteActualMethod();
			return this.getBusinessComponent().GetAutoById(id).ConvertToDto();
		}

		public List<KundeDto> GetKunden()
		{
			AutoReservationService.WriteActualMethod();
			return this.getBusinessComponent().GetKunden().ConvertToDtos();
		}

		public void AddKunde(KundeDto kunde)
		{
			AutoReservationService.WriteActualMethod();
			this.getBusinessComponent().AddKunde(kunde.ConvertToEntity());
		}

		public void RemoveKunde(KundeDto kunde)
		{
			AutoReservationService.WriteActualMethod();
			this.getBusinessComponent().RemoveKunde(kunde.ConvertToEntity());
		}

		public void UpdateKunde(KundeDto modified, KundeDto original)
		{
			AutoReservationService.WriteActualMethod();
			try
			{
				this.getBusinessComponent().UpdateKunde(modified.ConvertToEntity(), original.ConvertToEntity());
			}
			catch (LocalOptimisticConcurrencyException<Kunde> e)
			{
				FaultException faultException = new FaultException<KundeDto>(e.MergedEntity.ConvertToDto(), e.Message);
				throw faultException;
			}
		}

		public KundeDto GetKundeById(int id)
		{
			AutoReservationService.WriteActualMethod();
			return this.getBusinessComponent().GetKundeById(id).ConvertToDto();
		}

		public List<ReservationDto> GetReservationen()
		{
			AutoReservationService.WriteActualMethod();
			return this.getBusinessComponent().GetReservationen().ConvertToDtos();
		}

		public void AddReservation(ReservationDto reservation)
		{
			AutoReservationService.WriteActualMethod();
			this.getBusinessComponent().AddReservation(reservation.ConvertToEntity());
		}

		public void RemoveReservation(ReservationDto reservation)
		{
			AutoReservationService.WriteActualMethod();
			this.getBusinessComponent().RemoveReservation(reservation.ConvertToEntity());
		}

		public void UpdateReservation(ReservationDto modified, ReservationDto original)
		{
			AutoReservationService.WriteActualMethod();
			try {
				this.getBusinessComponent().UpdateReservation(modified.ConvertToEntity(), original.ConvertToEntity());
			}
			catch (LocalOptimisticConcurrencyException<Reservation> e)
			{
				FaultException faultException = new FaultException<ReservationDto>(e.MergedEntity.ConvertToDto(), e.Message);
				throw faultException;
			}
		}

		public ReservationDto GetReservationByNr(int resNr)
		{
			AutoReservationService.WriteActualMethod();
			return this.getBusinessComponent().GetReservationByNr(resNr).ConvertToDto();
		}
	}
}