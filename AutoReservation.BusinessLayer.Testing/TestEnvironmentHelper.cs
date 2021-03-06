﻿using System;
using System.Data.SqlClient;
using AutoReservation.Dal;

namespace AutoReservation.BusinessLayer.Testing
{
    public static class TestEnvironmentHelper
    {
        public static void InitializeTestData()
        {
            var type = typeof(System.Data.Entity.SqlServer.SqlProviderServices);

            using (AutoReservationEntities context = new AutoReservationEntities())
            {
                var connection = (SqlConnection)context.Database.Connection;
                var command = new SqlCommand();
                command.Connection = connection;

                connection.Open();

                DeleteAll(command);

                // Insert Autos
                SetAutoIncrementOnTable(connection, "Auto", true);
                InsertAutos(command);
                SetAutoIncrementOnTable(connection, "Auto", false);

                // Insert Kunden
                SetAutoIncrementOnTable(connection, "Kunde", true);
                InsertKunden(command);
                SetAutoIncrementOnTable(connection, "Kunde", false);

                // Insert Reservationen
                SetAutoIncrementOnTable(connection, "Reservation", true);
                InsertReservationen(command);
                SetAutoIncrementOnTable(connection, "Reservation", false);
            }
        }

        private static void InsertKunden(SqlCommand command)
        {
            command.CommandText =
                "INSERT INTO Kunde (Id, Nachname, Vorname, Geburtsdatum)" + Environment.NewLine +
                "   SELECT 1, 'Faessler', 'Chrigi', '1961-05-05 00:00:00' UNION" + Environment.NewLine +
                "   SELECT 2, 'Bargen', 'Danilo', '1980-09-09 00:00:00' UNION" + Environment.NewLine +
                "   SELECT 3, 'Furrer', 'Jonas', '1950-07-03 00:00:00' UNION" + Environment.NewLine +
                "   SELECT 4, 'Stein', 'Gertrude', '1944-11-11 00:00:00'";

            command.ExecuteNonQuery();
        }

        private static void InsertAutos(SqlCommand command)
        {
            command.CommandText =
                "INSERT INTO Auto (Id, Marke, AutoKlasse, Tagestarif, Basistarif)" + Environment.NewLine +
                "   SELECT 1, 'Fiat Punto', 2, 50, 0 UNION" + Environment.NewLine +
                "   SELECT 2, 'VW Golf', 1, 120, 0 UNION" + Environment.NewLine +
                "   SELECT 3, 'Audi S6', 0, 180, 50";

            command.ExecuteNonQuery();
        }

        private static void InsertReservationen(SqlCommand command)
        {
            command.CommandText =
                "INSERT INTO Reservation (Id, AutoId, KundeId, Von, Bis)" + Environment.NewLine +
                "   SELECT 1, 1, 1, '2013-06-03 00:00:00', '2014-01-15 00:00:00' UNION" + Environment.NewLine +
                "   SELECT 2, 2, 2, '2013-05-06 00:00:00', '2013-09-15 00:00:00' UNION" + Environment.NewLine +
                "   SELECT 3, 3, 3, '2013-04-10 00:00:00', '2013-11-06 00:00:00'";

            command.ExecuteNonQuery();
        }

        private static void DeleteAll(SqlCommand command)
        {
            command.CommandText = "DELETE FROM Reservation";
            command.ExecuteNonQuery();

            command.CommandText = "DELETE FROM Auto";
            command.ExecuteNonQuery();

            command.CommandText = "DELETE FROM Kunde";
            command.ExecuteNonQuery();
            
            command.CommandText = "DBCC CHECKIDENT('Reservation', RESEED, 0)";
            command.ExecuteNonQuery();

            command.CommandText = "DBCC CHECKIDENT('Auto', RESEED, 0)";
            command.ExecuteNonQuery();

            command.CommandText = "DBCC CHECKIDENT('Kunde', RESEED, 0)";
            command.ExecuteNonQuery();
        }

        private static void SetAutoIncrementOnTable(SqlConnection connection, string table, bool autoIncrementIsOn)
        {
            SqlCommand command = new SqlCommand
            {
                Connection = connection,
                CommandText = "SET IDENTITY_INSERT " + table + (autoIncrementIsOn ? " ON" : " OFF")
            };

            command.ExecuteNonQuery();
        }
    }
}
