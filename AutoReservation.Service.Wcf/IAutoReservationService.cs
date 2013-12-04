using AutoReservation.Common.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoReservation.Service.Wcf
{
    interface IAutoReservationService
    {
        List<AutoDto> GetAutos();
        void AddAuto(AutoDto auto);
        void RemoveAuto(AutoDto auto);
        void UpdateAuto(AutoDto modified, AutoDto original);
        AutoDto GetAutoById(int id);

        List<KundeDto> GetKunden();
        void AddKunde(KundeDto kunde);
        void RemoveKunde(KundeDto kunde);
        void UpdateKunde(KundeDto modified, KundeDto original);
        KundeDto GetKundeById(int id);

        List<ReservationDto> GetReservationen();
        void AddReservation(ReservationDto reservation);
        void RemoveReservation(ReservationDto reservation);
        void UpdateReservation(ReservationDto modified, ReservationDto original);
        ReservationDto GetReservationByNr(int resNr);

    }
}
