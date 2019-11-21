﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Capa_Presentacion
{
    public partial class FRM_FiltroCultivos : Form
    {
        public FRM_FiltroCultivos()
        {
            InitializeComponent();
        }

        private void btn_crear_reporte_Click(object sender, EventArgs e)
        {
            AdministrarCultivos cultivos = new AdministrarCultivos();
            cultivos.MostrarCultivos();
            cultivos.filtrar_y_exportar(cmb_cultivo.Text, cmb_plantado.Text, cmb_cosecha.Text);
        }

        private void FRM_FiltroCultivos_Load(object sender, EventArgs e)
        {
            AdministrarCultivos cultivos = new AdministrarCultivos();
            cultivos.MostrarCultivos();
            String[] cultivo = cultivos.obtenercultivo();
            String[] plantado = cultivos.obtenerfplantado();
            String[] cosecha = cultivos.obtenerfcosecha();
            cmb_cultivo.Items.Add("Todos");
            cmb_cosecha.Items.Add("Todos");
            cmb_plantado.Items.Add("Todos");
            for (int i = 0; i < cultivo.Length; i++)
            {
                cmb_cultivo.Items.Add(cultivo[i]);
                cmb_plantado.Items.Add(plantado[i]);
                cmb_cosecha.Items.Add(cosecha[i]);
            }
           
            cmb_cultivo.Text = "Todos";
            cmb_cosecha.Text = "Todos";
            cmb_plantado.Text = "Todos";
          

        }
    }
}
