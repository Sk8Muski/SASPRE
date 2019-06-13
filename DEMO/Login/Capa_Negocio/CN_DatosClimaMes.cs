﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_Datos;

namespace Capa_Negocio
{
    public class CN_DatosClimaMes
    {
        private CD_DatosClimaMes _DatosClimaMes = new CD_DatosClimaMes();
        System.Data.DataTable tablaDatosClimaMes = new System.Data.DataTable();

        public void InsertarDatosClimaMes(String Estacion,String Fecha_Local,String Fecha_UTC, String Direccion_de_Viento, String Direccion_de_Rafaga,
            String Rapidez_de_Viento, String Rapidez_de_Rafaga, String Temperatura, String Humedad_Relativa, String Presion_Atmosferica,
            String Precipitacion, String Radiacion_Solar)
        {
            _DatosClimaMes.InsertarDatosClimaMes(Estacion,Fecha_Local,Fecha_UTC,Direccion_de_Viento, Direccion_de_Rafaga,
                Rapidez_de_Viento,Rapidez_de_Rafaga,Temperatura,
                Humedad_Relativa,Presion_Atmosferica,Precipitacion,Radiacion_Solar);
        }

        public System.Data.DataTable MostrarDatosClimaMes()
        {
            tablaDatosClimaMes = _DatosClimaMes.MostrarDatosClimaMes();
            return tablaDatosClimaMes;
        }
    }
}
