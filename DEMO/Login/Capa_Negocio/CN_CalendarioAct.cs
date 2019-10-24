﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_Datos;

namespace Capa_Negocio
{
    public class CN_CalendarioAct
    {
        CD_CalendarioAct _Calendario = new CD_CalendarioAct();
        DataTable tablaCalendario = new DataTable();

        public DataTable MostrarCalendario(String cargo,String usuario)
        {
            tablaCalendario = _Calendario.MostrarCalendario(cargo, usuario);
            return tablaCalendario;
        }

        public void AgregarCalendario(String Usuario,String Nombre,String Descripcion,String FechaInicio,String FechaFin)
        {
            _Calendario.AgregarCalendario(Usuario,Nombre,Descripcion,FechaInicio,FechaFin);
        }

        public void EditarCalendario(String Nombre, String Descripcion, String FechaInicio, String FechaFin, String idCalendario)
        {
            _Calendario.EditarCalendario(Nombre, Descripcion, FechaInicio, FechaFin,idCalendario);
        }

        public void EliminarCalendario(String idCalendario)
        {
            _Calendario.EliminarCalendario(idCalendario);
        }
    }
}
