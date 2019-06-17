using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using MorozovWPF.Models;

namespace MorozovWPF.Helpers {
    public class Tables {
        public const string Consumers = "CONSUMERS";
        public const string Services  = "Services";
        public const string Orders    = "Orders";
        public const string Auth      = "Auth";
    }

    class DatabaseHelper {
        private const string DbFileName = "Morozov.db";

        private static SQLiteConnection connection;

        private static SQLiteConnection Connection => connection ?? (connection = new SQLiteConnection(
                                                                  @"DataSource = " +
                                                                  Path.GetDirectoryName(System.Reflection.Assembly
                                                                      .GetExecutingAssembly()
                                                                      .Location) +
                                                                  '\\'           + DbFileName +
                                                                  "; Version = 3"));


        public static bool Authorize(string userName, string password) {
            if (userName == "Morozov" && password == "Kirill") {
                return true;
            }
            else {
                return false;
            }
        }

        public static List<T> GetFullData<T>(string table)
            where T : DataModel<T>, new() {
            if (Connection.State == ConnectionState.Closed) {
                Connection.Open();
            }
            DataTable resultTable = new DataTable();

            SQLiteCommand    cmd          = new SQLiteCommand("SELECT * FROM " + table, Connection);
            SQLiteDataReader sqliteReader = cmd.ExecuteReader();
            resultTable.Load(sqliteReader);
            sqliteReader.Close();

            var consumers = new List<T>();

            foreach (DataRow row in resultTable.Rows) {
                consumers.Add(new T().FromDataRow(row));
            }

            return consumers;
        }

        public static bool AddRow<T>(string table, T data)
            where T : DataModel<T>, new() {
            try {
                SQLiteDataHelper dataHelper = new SQLiteDataHelper(data);
                SQLiteCommand cmd = new SQLiteCommand(
                    $"INSERT INTO {table} {dataHelper.FieldsList} VALUES {dataHelper.ParametersList}",
                    Connection);
                cmd.Parameters.AddRange(dataHelper.Parameters);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch {
                return false;
            }

        }

        public static bool UpdateRow<T>(string table, T data)
            where T : DataModel<T>, new() {
            try {
                SQLiteDataHelper dataHelper = new SQLiteDataHelper(data);
                SQLiteCommand cmd = new SQLiteCommand(
                    $"UPDATE {table} {dataHelper.FieldsList} VALUES {dataHelper.ParametersList} WHERE ID={data.ID}",
                    Connection);
                cmd.Parameters.AddRange(dataHelper.Parameters);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch {
                return false;
            }


        }

        public static bool DeleteRow<T>(string table, T data)
            where T : DataModel<T>, new() {
            try {
                SQLiteDataHelper dataHelper = new SQLiteDataHelper(data);
                SQLiteCommand cmd = new SQLiteCommand(
                    $"DELETE FROM {table} WHERE ID={data.ID}",
                    Connection);
                cmd.Parameters.AddRange(dataHelper.Parameters);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch {
                return false;
            }


        }
    }
}