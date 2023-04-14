using System;
using Npgsql;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MindTrivia.Datos
{
    public class Conexion
    {
        NpgsqlConnection conn = new NpgsqlConnection();

        static string servidor = "localhost";
        static string bd = "proyecto";
        static string user = "postgres";
        static string pass = "12345";
        static string port = "5432";
        string CadenaConexion = "server=" + servidor + ";port=" + port + ";user id=" + user + ";password=" + pass + ";database=" + bd + ";";

        public NpgsqlConnection OpenCon()
        {
            try
            {
                conn.ConnectionString = CadenaConexion;
                conn.Open();
            }
            catch (NpgsqlException e)
            {
                string error = e.Message;
            }
            return conn;
        }

        public void CloseCon()
        {
            try
            {
                conn.Close();
            }
            catch (NpgsqlException e)
            {
                string error = e.Message;
            }
        }
    }
}
