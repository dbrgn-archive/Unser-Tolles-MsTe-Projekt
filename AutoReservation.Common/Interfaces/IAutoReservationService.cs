using AutoReservation.Common.DataTransferObjects;
using System.Collections.Generic;
using System.ServiceModel;

namespace AutoReservation.Common.Interfaces
{
	[ServiceContract(Namespace = "http://localhost:7876/AutoReservationService")]
	public interface IAutoReservationService
	{
		[OperationContract]
		List<AutoDto> GetAutos();

		[OperationContract]
		void AddAuto(AutoDto auto);

		[OperationContract]
		void RemoveAuto(AutoDto auto);

		[OperationContract]
		[FaultContract(typeof(AutoDto))]
		void UpdateAuto(AutoDto modified, AutoDto original);

		[OperationContract]
		AutoDto GetAutoById(int id);

		[OperationContract]
		List<KundeDto> GetKunden();

		[OperationContract]
		void AddKunde(KundeDto kunde);

		[OperationContract]
		void RemoveKunde(KundeDto kunde);

		[OperationContract]
		[FaultContract(typeof(KundeDto))]
		void UpdateKunde(KundeDto modified, KundeDto original);

		[OperationContract]
		KundeDto GetKundeById(int id);

		[OperationContract]
		List<ReservationDto> GetReservationen();

		[OperationContract]
		void AddReservation(ReservationDto reservation);

		[OperationContract]
		void RemoveReservation(ReservationDto reservation);

		[OperationContract]
		[FaultContract(typeof(ReservationDto))]
		void UpdateReservation(ReservationDto modified, ReservationDto original);

		[OperationContract]
		ReservationDto GetReservationByNr(int resNr);
	}
}