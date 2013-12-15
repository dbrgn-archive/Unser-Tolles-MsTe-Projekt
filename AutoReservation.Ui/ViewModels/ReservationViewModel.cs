using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using AutoReservation.Common.DataTransferObjects;

namespace AutoReservation.Ui.ViewModels
{
    public class ReservationViewModel : ViewModelBase
    {
        private List<AutoDto> autos;
        public List<AutoDto> Autos
        {
            get
            {
                if (autos == null)
                {
                    autos = new List<AutoDto>();
                }
                return autos;
            }
        }

        private List<KundeDto> kunden;
        public List<KundeDto> Kunden
        {
            get
            {
                if (kunden == null)
                {
                    kunden = new List<KundeDto>();
                }
                return kunden;
            }
        }
        

        private readonly List<ReservationDto> reservationenOriginal = new List<ReservationDto>();
        private ObservableCollection<ReservationDto> reservationen;
        public ObservableCollection<ReservationDto> Reservationen
        {
            get
            {
                if (reservationen == null)
                {
                    reservationen = new ObservableCollection<ReservationDto>();
                }
                return reservationen;
            }
        }

        private ReservationDto selectedReservation;
        public ReservationDto SelectedReservation
        {
            get { return selectedReservation; }
            set
            {
                if (selectedReservation != value)
                {
                    SendPropertyChanging(() => SelectedReservation);
                    selectedReservation = value;
                    SendPropertyChanged(() => SelectedReservation);
                }
            }
        }

        #region Load-Command

        private RelayCommand loadCommand;

        public ICommand LoadCommand
        {
            get
            {
                if (loadCommand == null)
                {
                    loadCommand = new RelayCommand(
                        param => Load(),
                        param => CanLoad()
                    );
                }
                return loadCommand;
            }
        }

        protected override void Load()
        {
            Reservationen.Clear();
            reservationenOriginal.Clear();
            foreach (ReservationDto reservation in Service.GetReservationen())
            {
                Reservationen.Add(reservation);
                reservationenOriginal.Add((ReservationDto)reservation.Clone());
            }
            SelectedReservation = Reservationen.FirstOrDefault();
            Autos.Clear();
            Autos.AddRange(Service.GetAutos());
            Kunden.Clear();
            Kunden.AddRange(Service.GetKunden());
        }

        private bool CanLoad()
        {
            return Service != null;
        }

        #endregion

        #region Save-Command

        private RelayCommand saveCommand;

        public ICommand SaveCommand
        {
            get
            {
                if (saveCommand == null)
                {
                    saveCommand = new RelayCommand(
                        param => SaveData(),
                        param => CanSaveData()
                    );
                }
                return saveCommand;
            }
        }

        private void SaveData()
        {
            foreach (ReservationDto reservation in Reservationen)
            {
                if (reservation.ReservationNr == default(int))
                {
                    Service.AddReservation(reservation);
                }
                else
                {
                    ReservationDto original = reservationenOriginal.FirstOrDefault(ao => ao.ReservationNr == reservation.ReservationNr);
                    Service.UpdateReservation(reservation, original);
                }
            }
            Load();
        }

        private bool CanSaveData()
        {
            if (Service == null)
            {
                return false;
            }

            StringBuilder errorText = new StringBuilder();
            foreach (ReservationDto reservation in Reservationen)
            {
                string error = reservation.Validate();
                if (!string.IsNullOrEmpty(error))
                {
                    errorText.AppendLine(reservation.ToString());
                    errorText.AppendLine(error);
                }
            }

            ErrorText = errorText.ToString();
            return string.IsNullOrEmpty(ErrorText);
        }

        #endregion

        #region New-Command

        private RelayCommand newCommand;

        public ICommand NewCommand
        {
            get
            {
                if (newCommand == null)
                {
                    newCommand = new RelayCommand(
                        param => New(),
                        param => CanNew()
                    );
                }
                return newCommand;
            }
        }

        private void New()
        {
            Reservationen.Add(new ReservationDto());
        }

        private bool CanNew()
        {
            return Service != null;
        }

        #endregion

        #region Delete-Command

        private RelayCommand deleteCommand;

        public ICommand DeleteCommand
        {
            get
            {
                if (deleteCommand == null)
                {
                    deleteCommand = new RelayCommand(
                        param => Delete(),
                        param => CanDelete()
                    );
                }
                return deleteCommand;
            }
        }

        private void Delete()
        {
            Service.RemoveReservation(SelectedReservation);
            Load();
        }

        private bool CanDelete()
        {
            return
                SelectedReservation != null &&
                SelectedReservation.ReservationNr != default(int) &&
                Service != null;
        }

        #endregion

    }
}