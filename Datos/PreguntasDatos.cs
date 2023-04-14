using System;
using Npgsql;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MindTrivia.Models;
using MindTrivia.Datos;

namespace MindTrivia.Datos
{
    public class PreguntasDatos
    {
        public bool Guardar(PreguntasModel oPreguntas)
        {
            bool flag = false;
            var con = new Conexion();



            string spguardar = "CALL sp_guardarPreguntas('" + oPreguntas.Categoria + "',ARRAY['"
               + oPreguntas.Preguntas[0] + "','" + oPreguntas.Preguntas[1] + "','" + oPreguntas.Preguntas[2] + "','" + oPreguntas.Preguntas[3] + "','" +
                oPreguntas.Preguntas[4] + "']" + ",ARRAY['"
               + oPreguntas.RespuestasCorrectas[0] + "','" + oPreguntas.RespuestasCorrectas[1] + "','" + oPreguntas.RespuestasCorrectas[2] + "','" + oPreguntas.RespuestasCorrectas[3] + "','" +
                oPreguntas.RespuestasCorrectas[4] + "']" + ",ARRAY['"
               + oPreguntas.RespuestasIncorrectas[0] + "','" + oPreguntas.RespuestasIncorrectas[1] + "','" + oPreguntas.RespuestasIncorrectas[2] + "','" + oPreguntas.RespuestasIncorrectas[3] + "','" +
                oPreguntas.RespuestasIncorrectas[4] + "'],ARRAY['"
               + oPreguntas.RespuestasIncorrectas2[0] + "','" + oPreguntas.RespuestasIncorrectas2[1] + "','" + oPreguntas.RespuestasIncorrectas2[2] + "','" + oPreguntas.RespuestasIncorrectas2[3] + "','" +
                oPreguntas.RespuestasIncorrectas2[4] + "'],'" + oPreguntas.Material + "');";

            Console.WriteLine(spguardar);


            NpgsqlCommand com = new NpgsqlCommand(spguardar, con.OpenCon());
            com.ExecuteNonQuery();
            flag = true;
            con.CloseCon();
            return flag;
        }

        public List<PreguntasModel> Listar()
        {
            var oLista = new List<PreguntasModel>();

            var con = new Conexion();

            NpgsqlCommand com = new NpgsqlCommand("fn_ConsultarPreguntas", con.OpenCon());
            com.CommandType = CommandType.StoredProcedure;

            using (var dr = com.ExecuteReader())
            {
                while (dr.Read())
                {
                    oLista.Add(new PreguntasModel()
                    {
                        IdPregunta = Convert.ToInt32(dr["id_preguntas"]),
                        Categoria = dr["categoria"].ToString()
                    });
                }
            }
            return oLista;
        }

        public PreguntasModel Obtener(int IdPregunta)
        {
            var oPregunta = new PreguntasModel();

            var cn = new Conexion();

            NpgsqlCommand cmd = new NpgsqlCommand("sp_Obtener_Material", cn.OpenCon());
            cmd.Parameters.AddWithValue("id_preguntas", IdPregunta);
            cmd.CommandType = CommandType.StoredProcedure;

            using (var dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    //oPregunta.IdPregunta = Convert.ToInt32(dr["id_preguntas"]);
                    oPregunta.Categoria = dr["categoria"].ToString();
                    oPregunta.Material = dr["material_extra"].ToString();

                }
            }
            return oPregunta;
        }
    }
}
