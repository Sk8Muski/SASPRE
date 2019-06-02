﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;

namespace Capa_Datos
{
    public class CD_Cultivos
    {
        private CD_ConexionBD conexion = new CD_ConexionBD();
        MySqlDataReader leer;
        DataTable tablaCultivos = new DataTable();
        MySqlCommand comando;
        public DataTable MostrarCultivos(String cargo,String usuario)
        {
            comando = new MySqlCommand();
            comando.Connection = conexion.AbrirConexion();
            if(cargo == "Admin")
            {
                comando.CommandText = "MostrarCultivos";
            }
            else
            {
                comando.CommandText = "MostrarCultivosUsuario";
                comando.Parameters.AddWithValue("_Usuario_Cultivo", usuario);
            }
            comando.CommandType = CommandType.StoredProcedure;
            leer = comando.ExecuteReader();
            tablaCultivos.Load(leer);
            conexion.CerrarConexion();
            return tablaCultivos;
        }
        public void AgregarCultivo(String Usuario_Cultivo,String Cultivo, String Fecha_Plantado,String Fecha_Cosecha,String Cantidad,String Estado)
        {
            comando = new MySqlCommand();
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "AgregarCultivos";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("_Usuario_Cultivo", Usuario_Cultivo);
            comando.Parameters.AddWithValue("_Cultivo",Cultivo);
            comando.Parameters.AddWithValue("_Fecha_Plantado",Fecha_Plantado);
            comando.Parameters.AddWithValue("_Fecha_Cosecha",Fecha_Cosecha);
            comando.Parameters.AddWithValue("_Cantidad",Cantidad);
            comando.Parameters.AddWithValue("_Estado",Estado);
            comando.ExecuteNonQuery();
            comando.Parameters.Clear();
            conexion.CerrarConexion();
        }
        public void EliminarCultivo(String IDCultivo)
        {
            comando = new MySqlCommand();
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "EliminarCultivos";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("_IDCultivo",IDCultivo);
            comando.ExecuteNonQuery();
            comando.Parameters.Clear();
            conexion.CerrarConexion();
        }
    }
}
