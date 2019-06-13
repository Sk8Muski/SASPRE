﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Capa_Datos
{
    public class CD_Fertilizantes
    {
        private CD_ConexionBD conexion = new CD_ConexionBD();
        MySqlDataReader leer;
        DataTable tablaCultivos = new DataTable();
        MySqlCommand comando;
        public DataTable MostrarFertilizantes()
        {
            try
            {
                comando = new MySqlCommand();
                comando.Connection = conexion.AbrirConexion();
                comando.CommandText = "MostrarFertilizantes";
                comando.CommandType = CommandType.StoredProcedure;
                leer = comando.ExecuteReader();
                tablaCultivos.Load(leer);
                conexion.CerrarConexion();
                
            }
            catch (Exception)
            {
                MessageBox.Show("ADVERTENCIA", "Error al mostrar fertilizantes", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            return tablaCultivos;
        }
    }
}
