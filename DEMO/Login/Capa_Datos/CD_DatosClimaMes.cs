﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Data;
using System.Windows.Forms;

namespace Capa_Datos
{
    public class CD_DatosClimaMes
    {
        private CD_ConexionBD conexion = new CD_ConexionBD();
        MySqlCommand comando = new MySqlCommand();
        DataTable tablaDatosClimaMes = new DataTable();
        MySqlDataReader leer;

        public void InsertarDatosClimaMes(String Estacion,String Fecha_Local,String Fecha_UTC, String Direccion_de_Viento, String Direccion_de_Rafaga,
            String Rapidez_de_Viento, String Rapidez_de_Rafaga, String Temperatura, String Humedad_Relativa, String Presion_Atmosferica,
            String Precipitacion, String Radiacion_Solar)
        {
            try
            {
                comando = new MySqlCommand();
                comando.Connection = conexion.AbrirConexion();
                comando.CommandText = "InsertarDatosClimaMes";
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("_Estacion", Estacion);
                comando.Parameters.AddWithValue("_Fecha_Local", Fecha_Local);
                comando.Parameters.AddWithValue("_Fecha_UTC", Fecha_UTC);
                comando.Parameters.AddWithValue("_Direccion_del_Viento", Direccion_de_Viento);
                comando.Parameters.AddWithValue("_Direccion_de_Rafaga", Direccion_de_Rafaga);
                comando.Parameters.AddWithValue("_Rapidez_de_Viento", Rapidez_de_Viento);
                comando.Parameters.AddWithValue("_Rapidez_de_Rafaga", Rapidez_de_Rafaga);
                comando.Parameters.AddWithValue("_Temperatura", Temperatura);
                comando.Parameters.AddWithValue("_Humedad_Relativa", Humedad_Relativa);
                comando.Parameters.AddWithValue("_Presion_Atmosferica", Presion_Atmosferica);
                comando.Parameters.AddWithValue("_Precipitacion", Precipitacion);
                comando.Parameters.AddWithValue("_Radiacion_Solar", Radiacion_Solar);
                comando.ExecuteNonQuery();
                comando.Parameters.Clear();
                conexion.CerrarConexion();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ha ocurrido un error " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        public DataTable MostrarDatosClimaMes()
        {
            comando = new MySqlCommand();
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "MostrarDatosClimaMes";
            comando.CommandType = CommandType.StoredProcedure;
            leer = comando.ExecuteReader();
            tablaDatosClimaMes.Load(leer);
            conexion.CerrarConexion();
            return tablaDatosClimaMes;
        }
        public void AgregarDiario(String fecha)
        {
            comando = new MySqlCommand();
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "agregardiarios";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("_Fecha",fecha);
            comando.ExecuteNonQuery();
            comando.Parameters.Clear();
            conexion.CerrarConexion();
        }
    }
}
