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
        public List<AutoDto> GetAutos();
        public void AddAuto(AutoDto auto);
        public void DeleteAuto(AutoDto auto);
        public void UpdateAuto(AutoDto modified, AutoDto original);
        public AutoDto GetAutoById(int id);

        public List<KundeDto> GetKunden();
        public void AddKunde(KundeDto kunde);
        public void DeleteKunde(KundeDto kunde);
        public void UpdateKunde(KundeDto modified, KundeDto original);
        public KundeDto GetKundeById(int id);

        public List<ReservationDto> GetReservationen();
        public void AddReservation(ReservationDto reservation);
        public void DeleteReservation(ReservationDto reservation);
        public void UpdateReservation(ReservationDto modified, ReservationDto original);
        public ReservationDto GetRservationByNr(int resNr);

    }
}
