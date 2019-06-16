﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;
using System.Windows.Forms;

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
            try
            {
                comando = new MySqlCommand();
                comando.Connection = conexion.AbrirConexion();
                if (cargo == "Admin")
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
            }
            catch (Exception )
            {
                MessageBox.Show("ADVERTENCIA", "Error al mostrar cultivos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            return tablaCultivos;
        }
        public void AgregarCultivo(String Usuario_Cultivo,String Cultivo, String Fecha_Plantado,String Fecha_Cosecha,String Cantidad,String Estado)
        {
            try
            {
                comando = new MySqlCommand();
                comando.Connection = conexion.AbrirConexion();
                comando.CommandText = "AgregarCultivos";
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("_Usuario_Cultivo", Usuario_Cultivo);
                comando.Parameters.AddWithValue("_Cultivo", Cultivo);
                comando.Parameters.AddWithValue("_Fecha_Plantado", Fecha_Plantado);
                comando.Parameters.AddWithValue("_Fecha_Cosecha", Fecha_Cosecha);
                comando.Parameters.AddWithValue("_Cantidad", Cantidad);
                comando.Parameters.AddWithValue("_Estado", Estado);
                comando.ExecuteNonQuery();
                comando.Parameters.Clear();
                conexion.CerrarConexion();
            }
            catch (Exception )
            {
                MessageBox.Show("ADVERTENCIA", "Error al agregar cultivo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        public void EliminarCultivo(String IDCultivo)
        {
            try
            {
                comando = new MySqlCommand();
                comando.Connection = conexion.AbrirConexion();
                comando.CommandText = "EliminarCultivos";
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("_IDCultivo", IDCultivo);
                comando.ExecuteNonQuery();
                comando.Parameters.Clear();
                conexion.CerrarConexion();
            }
            catch (Exception  )
            {
                MessageBox.Show("ADVERTENCIA", "Error al eliminar cultivo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public void EditarCultivo(String IDCultivo,String Estado)
        {
            try
            {
                comando = new MySqlCommand();
                comando.Connection = conexion.AbrirConexion();
                comando.CommandText = "EditarCultivos";
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("_IDCultivo", IDCultivo);
                comando.Parameters.AddWithValue("_Estado", Estado);
                comando.ExecuteNonQuery();
                comando.Parameters.Clear();
                conexion.CerrarConexion();
            }
            catch (Exception)
            {
                MessageBox.Show("ADVERTENCIA", "Error al editar cultivo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}
