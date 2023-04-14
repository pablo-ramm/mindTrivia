using System;
using Npgsql;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MindTrivia.Models;
using MindTrivia.Datos;
using System.Drawing;

namespace MindTrivia.Datos
{
    public class UsuarioDatos
    {
        public List<UsuarioModel> Listar()
        {
            var oLista = new List<UsuarioModel>();

            var con = new Conexion();

            NpgsqlCommand com = new NpgsqlCommand("fn_Consultar", con.OpenCon());
            com.CommandType = CommandType.StoredProcedure;

            using (var dr = com.ExecuteReader())
            {
                while (dr.Read())
                {
                    oLista.Add(new UsuarioModel()
                    {
                        IdUsuario = Convert.ToInt32(dr["idusuario"]),
                        Usuario = dr["usuario"].ToString(),
                        Nombre = dr["nombre"].ToString(),
                        Apellido = dr["apellido"].ToString(),
                        Email = dr["email"].ToString(),
                        Passw = dr["passw"].ToString(),
                        Pais = dr["pais"].ToString(),
                        Edad = Convert.ToInt32(dr["edad"])
                    });
                }
            }
            return oLista;
        }

        public int UsuarioUnico(UsuarioModel oUsuario)
        {
            int flag = 0;
      
            var con = new Conexion();

            NpgsqlCommand cmd = new NpgsqlCommand("fn_UsuarioUnico", con.OpenCon());
            cmd.Parameters.AddWithValue(oUsuario.Usuario);
            cmd.CommandType = CommandType.StoredProcedure;
            using (var dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    flag = Convert.ToInt32(dr["bandera"]);
                }
            }

            if (flag > 0)
            {
                Console.WriteLine("Usuario Repetido");
            }

            return flag;

        }

        public bool Guardar(UsuarioModel oUsuario)
        {
            bool flag = false;
            var con = new Conexion();



            string spguardar = "CALL sp_guardar ('" + oUsuario.Usuario + "','" + oUsuario.Nombre + "','"
                + oUsuario.Apellido + "','" + oUsuario.Email + "','" + oUsuario.Passw +
                "','" + oUsuario.Pais + "'," + oUsuario.Edad + ")";

            Console.WriteLine(spguardar);


            NpgsqlCommand com = new NpgsqlCommand(spguardar, con.OpenCon());
            com.ExecuteNonQuery();
            flag = true;
            con.CloseCon();
            return flag;
        }

        public UsuarioModel Obtener(int IdUsuario)
        {
            var oUsuario = new UsuarioModel();

            var cn = new Conexion();

            NpgsqlCommand cmd = new NpgsqlCommand("fn_Obtener", cn.OpenCon());
            cmd.Parameters.AddWithValue("idusuario", IdUsuario);
            cmd.CommandType = CommandType.StoredProcedure;

            using (var dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    oUsuario.IdUsuario = Convert.ToInt32(dr["idusuario"]);
                    oUsuario.Usuario = dr["usuario"].ToString();
                    oUsuario.Nombre = dr["nombre"].ToString();
                    oUsuario.Apellido = dr["apellido"].ToString();
                    oUsuario.Email = dr["email"].ToString();
                    oUsuario.Passw = dr["passw"].ToString();
                    oUsuario.Pais = dr["pais"].ToString();
                    oUsuario.Edad = Convert.ToInt32(dr["edad"]);
                }
            }
            return oUsuario;
        }

        public bool Editar(UsuarioModel oUsuario)
        {
            bool flag = false;
            var con = new Conexion();

            string editar = "CALL sp_Editar (" + oUsuario.IdUsuario + ",'" + oUsuario.Usuario + "','" + oUsuario.Nombre + "','"
                + oUsuario.Apellido + "'," + oUsuario.Email + "'," + oUsuario.Passw +
                "'," + oUsuario.Pais + "'," + oUsuario.Edad + ")";
            NpgsqlCommand com = new NpgsqlCommand(editar, con.OpenCon());
            com.ExecuteNonQuery();
            flag = true;
            con.CloseCon();
            return flag;
        }

        public bool Eliminar(int IdUsuario)
        {
            bool flag;
            var con = new Conexion();

            string sql = "CALL sp_Eliminar (" + IdUsuario + ")";
            NpgsqlCommand com = new NpgsqlCommand(sql, con.OpenCon());
            com.ExecuteNonQuery();
            flag = true;
            con.CloseCon();
            return flag;
        }

        public int Validar(string l_usuario, string l_passw)
        {
            int band = 0;
            var cn = new Conexion();

            NpgsqlCommand cmd = new NpgsqlCommand("fn_validar", cn.OpenCon());
            cmd.Parameters.AddWithValue("l_usuario", l_usuario);
            cmd.Parameters.AddWithValue("l_passw", l_passw);
            cmd.CommandType = CommandType.StoredProcedure;

            using (var dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    band = Convert.ToInt32(dr["bandera"]);
                }
            }
            return band;
        }
    }
}

