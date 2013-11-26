using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using AutoReservation.Dal;

namespace AutoReservation.BusinessLayer
{
    public class AutoReservationBusinessComponent
    {
        public List<Auto> GetAutos()
        {
            using (var context = new AutoReservationEntities())
            {
                return context.Autos.ToList();
            }
        }
        public void AddAuto(Auto auto)
        {
            using (AutoReservationEntities context = new AutoReservationEntities())
            {
                context.Autos.Add(auto);
            }

        }

        public void DeleteAuto(Auto auto)
        {
            using (var context = new AutoReservationEntities())
            {
                context.Autos.Remove(auto);
            }
        }

        public void UpdateAuto(Auto modified, Auto original)
        {
            using (var context = new AutoReservationEntities())
            {
                try
                {
                    context.Autos.Attach(original);
                    context.Entry(original).CurrentValues.SetValues(modified);
                    context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    HandleDbConcurrencyException(context, original);
                }
            }
        }

        public void AddKunde(Kunde kunde)
        {
            using (var context = new AutoReservationEntities())
            {
                context.Kunden.Add(kunde);
            }
        }

        public void RemoveKunde(Kunde kunde)
        {
            using (var context = new AutoReservationEntities())
            {
                context.Kunden.Remove(kunde);
            }
        }

        public void UpdateKunde(Kunde modified, Kunde original)
        {
            using (var context = new AutoReservationEntities())
            {
                try
                {
                    context.Kunden.Attach(original);
                    context.Entry(original).CurrentValues.SetValues(modified);
                    context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    HandleDbConcurrencyException(context, original);
                }
            }
        }

        public List<Kunde> GetKunden()
        {
            using (var context = new AutoReservationEntities())
            {
                return context.Kunden.ToList();
            }
        }

        public void AddReservation(Reservation reservation)
        {
            using (var context = new AutoReservationEntities())
            {
                context.Reservationen.Add(reservation);
            }
        }

        public void RemoveReservation(Reservation reservation)
        {
            using (var context = new AutoReservationEntities())
            {
                context.Reservationen.Remove(reservation);
            }
        }

        public void UpdateReservation(Reservation modified, Reservation original)
        {
            using (var context = new AutoReservationEntities())
            {
                try
                {
                    context.Reservationen.Attach(original);
                    context.Entry(original).CurrentValues.SetValues(modified);
                    context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    HandleDbConcurrencyException(context, original);
                }
            }
        }

        private static void HandleDbConcurrencyException<T>(AutoReservationEntities context, T original) where T : class
        {
            var databaseValue = context.Entry(original).GetDatabaseValues();
            context.Entry(original).CurrentValues.SetValues(databaseValue);

            throw new LocalOptimisticConcurrencyException<T>(string.Format("Update {0}: Concurrency-Fehler", typeof(T).Name), original);
        }

    }
}