﻿using System;
using Capa_Negocio;
using System.Windows.Forms;
using System.Net.Mail;
using System.Linq;

namespace Capa_Presentacion
{
    public partial class Envio_Correo : Form
    {
        private CN_ABCUsuario _ABCUsuario = new CN_ABCUsuario();

        public Envio_Correo()
        {
            InitializeComponent();
        }

        string correoPrincipal = "sistemarhvb@gmail.com";
        string contraPrincipal = "Skate1234";

        private void btnEnviar_Click(object sender, EventArgs e)
        {   
            var email = txtCorreo.Text;
            var table = _ABCUsuario.ObtenerContra(email);
            var contra = "";
            var mensaje = "Su contraseña es: ";
            if (table.Rows.Count != 0)
            {
                contra = table.Rows[0][0].ToString();
                mensaje += contra;
                try
                {
                    if (EsDireccionDeCorreoValida(email))
                    {
                        EnviarCorreo(correoPrincipal, contraPrincipal, email, mensaje);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error " + ex);
                    throw;
                }
            }
            else
            {
                //fuente: , contra: 
                MessageBox.Show("Correo inexistente, ingrese valido.");
                return;
            }
            
        }

        private void EnviarCorreo(string fuente, string contraFuente, string destino, string mensaje)
        {
            var correo = new System.Net.Mail.MailMessage();
            correo.From = new System.Net.Mail.MailAddress(fuente);
            correo.To.Add(destino);
            correo.Subject = "Recuperacion de contraseña";
            correo.Body = mensaje;
            correo.IsBodyHtml = false;
            correo.Priority = System.Net.Mail.MailPriority.Normal;

            var smtp = new System.Net.Mail.SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.Credentials = new System.Net.NetworkCredential(fuente, contraFuente);

            smtp.Send(correo);

        }


        private bool EsDireccionDeCorreoValida(string address)
        {
            try
            {
                var m = new MailAddress(address);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

    }
}
