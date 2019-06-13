﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Capa_Negocio;
using MySql.Data.MySqlClient;
using System.Runtime;
using System.Runtime.InteropServices;
using System.Net.Mail;

namespace Capa_Presentacion
{
    public partial class Login : Form
    {
        private CN_ABCUsuario _ABCUsuario = new CN_ABCUsuario();
        Thread th;
        private string rutadirectorio;
        String thisDay = DateTime.Now.ToLongDateString().ToString();
        private bool Drag;
        private int MouseX;
        private int MouseY;

        private const int WM_NCHITTEST = 0x84;
        private const int HTCLIENT = 0x1;
        private const int HTCAPTION = 0x2;

        private bool m_aeroEnabled;

        private const int CS_DROPSHADOW = 0x00020000;
        private const int WM_NCPAINT = 0x0085;
        private const int WM_ACTIVATEAPP = 0x001C;
        [DllImport("wininet.dll")]
        public extern static bool InternetGetConnectedState(out int Description, int ReservedValue);
        [System.Runtime.InteropServices.DllImport("dwmapi.dll")]
        public static extern int DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS pMarInset);
        [System.Runtime.InteropServices.DllImport("dwmapi.dll")]
        public static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);
        [System.Runtime.InteropServices.DllImport("dwmapi.dll")]

        public static extern int DwmIsCompositionEnabled(ref int pfEnabled);
        [System.Runtime.InteropServices.DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect,
            int nWidthEllipse,
            int nHeightEllipse
            );

        public struct MARGINS
        {
            public int leftWidth;
            public int rightWidth;
            public int topHeight;
            public int bottomHeight;
        }
        protected override CreateParams CreateParams
        {

            get
            {
                m_aeroEnabled = CheckAeroEnabled();
                CreateParams cp = base.CreateParams;
                if (!m_aeroEnabled)
                    cp.ExStyle |= 0x02000000;
                cp.ClassStyle |= CS_DROPSHADOW; return cp;
            }
        }
        private bool CheckAeroEnabled()
        {
            try
            {
                if (Environment.OSVersion.Version.Major >= 6)
                {
                    int enabled = 0; DwmIsCompositionEnabled(ref enabled);
                    return (enabled == 1) ? true : false;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("ADVERTENCIA", "Error el el login", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            return false;
        }
        protected override void WndProc(ref Message m)
        {
            try
            {
                switch (m.Msg)
                {
                    case WM_NCPAINT:
                        if (m_aeroEnabled)
                        {
                            var v = 2;
                            DwmSetWindowAttribute(this.Handle, 2, ref v, 4);
                            MARGINS margins = new MARGINS()
                            {
                                bottomHeight = 1,
                                leftWidth = 0,
                                rightWidth = 0,
                                topHeight = 0
                            }; DwmExtendFrameIntoClientArea(this.Handle, ref margins);
                        }
                        break;
                    default: break;
                }
                base.WndProc(ref m);
                if (m.Msg == WM_NCHITTEST && (int)m.Result == HTCLIENT) m.Result = (IntPtr)HTCAPTION;
            }
            catch (Exception)
            {
                MessageBox.Show("ADVERTENCIA", "Error en el login", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        private void PanelMove_MouseDown(object sender, MouseEventArgs e)
        {
            Drag = true;
            MouseX = Cursor.Position.X - this.Left;
            MouseY = Cursor.Position.Y - this.Top;
        }
        private void PanelMove_MouseMove(object sender, MouseEventArgs e)
        {
            if (Drag)
            {
                this.Top = Cursor.Position.Y - MouseY;
                this.Left = Cursor.Position.X - MouseX;
            }
        }
        private void PanelMove_MouseUp(object sender, MouseEventArgs e) { Drag = false; }

        public Login()
        {
            InitializeComponent();
            m_aeroEnabled = false;
        }

        //protected override CreateParams CreateParams
        //{
        //    get
        //    {
        //        const int CS_DROPSHADOW = 0x20000;
        //        CreateParams cp = base.CreateParams;
        //        cp.ClassStyle |= CS_DROPSHADOW;
        //        cp.ExStyle |= 0x02000000;   // WS_EX_COMPOSITED   
        //        return cp;
        //    }
        //}

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea cerrar el programa?", "Finalizar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            entrar();
        }
        private void entrar()
        {
            //if (HayInternet() == true)
            //{
            try
            {
                rutadirectorio = "C:\\SASPRE_DATOS_ATMOSFERICOS\\datos_CIUDADMANTE_" + thisDay + ".csv";
                //crear carpeta
                crear_carpeta();
                //Guardar informacion
                DownloadGamefile DGF = new DownloadGamefile();

                DGF.DescargAsincrona("https://smn.cna.gob.mx/tools/PHP/sivea/siveaEsri2/php/manejador_descargas_csv_estaciones.php?estacion=CIUDADMANTE&organismo=SMN&variable=temperatura%27&fbclid=IwAR3lT8srywft8Sy7OVAHDQ9_6ePUYm-am6ZzcN-zSsdCOVxGGMy0aa_guDQ", rutadirectorio);
                //aqui es donde te dice si ya se descargo 
                //while (DGF.DownloadCompleted == false)
                //{
                //    MessageBox.Show(DGF.DownloadCompleted.ToString());
                //}
                CN_Login _Login = new CN_Login();
                MySqlDataReader Loguear;
                Loguear = _Login.IniciarSesion(txtNickname.Text, txtContra.Text);
                if (Loguear.Read() == true)
                {
                    if (HayInternet() == true)
                    {

                    }
                    else
                    {
                        MessageBox.Show("Compruebe su conexión a internet, no tendrá todas las funcionalidades", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    Cursor.Current = Cursors.WaitCursor;
                    Program.nickname = txtNickname.Text;
                    Program.contraseña = txtContra.Text;
                    Program.cargo = Loguear["Cargo"].ToString();
                    Program.nombre = Loguear["Nombre"].ToString();
                    Program.apellidos = Loguear["Apellidos"].ToString();
                    Program.correo = Loguear["Correo"].ToString();
                    Menu mn = new Menu();
                    mn.Show();
                    Cursor.Current = Cursors.Default;
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Usuario o contraseña incorrectos", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtContra.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ha ocurrido un error " + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //} else {
            //    MessageBox.Show("Compruebe su conexión a internet", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        public bool ConexionAInternet()
        {
            int Desc;
            if (InternetGetConnectedState(out Desc, 0) == true)
                return true;
            else
                return false;
        }

        private void open()
        {
            Application.Run(new Menu());
        }

        private void Login_Load(object sender, EventArgs e)
        {
            if (HayInternet() == true)
            {

            }
            else
            {
                MessageBox.Show("Compruebe su conexión a internet", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        //Metodo para descargar archivo de datos atmosfericos
        public async void getArchivo()
        {
            try
            {
                WebClient wc = new WebClient();
                String url = "https://smn.cna.gob.mx/tools/PHP/sivea/siveaEsri2/php/manejador_descargas_csv_estaciones.php?estacion=CIUDADMANTE&organismo=SMN&variable=temperatura%27&fbclid=IwAR3lT8srywft8Sy7OVAHDQ9_6ePUYm-am6ZzcN-zSsdCOVxGGMy0aa_guDQ";
                //await Task.Run(() => { wc.DownloadFileAsync(new Uri(url), rutadirectorio); });
                Cursor.Current = Cursors.WaitCursor;
                await wc.DownloadFileTaskAsync(url, rutadirectorio);
                Cursor.Current = Cursors.Default;
                //Thread.Sleep(10000);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ha ocurrido un error con la descarga de un archivo, compruebe su conexion a internet", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        //metodo para crear carpeta donde se almacenara el documento descargado
        public void crear_carpeta()
        {
            try
            {
                string ruta = "C:\\SASPRE_DATOS_ATMOSFERICOS";
                if (!Directory.Exists(ruta))
                {
                    System.IO.Directory.CreateDirectory(ruta);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("ADVERTENCIA", "Error al crear carpeta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void txtContra_OnValueChanged(object sender, EventArgs e)
        {
            txtContra.isPassword = true;
        }

        private void linklblcontrasena_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {


            if (ConexionAInternet() == true)
            {

                pictureBox5.Location = new Point(521, 420);
                bunifuMaterialTextbox1.Location = new Point(548, 420);
                btnEnviar.Location = new Point(655, 520);
                btnCancelar.Location = new Point(521, 520);
                bunifuSeparator2.Location = new Point(520, 470);
                linklblcontrasena.Visible = false;
                txtNickname.Visible = false;
                pictureBox2.Visible = false;
                pictureBox3.Visible = false;
                txtContra.Visible = false;
                btnIngresar.Visible = false;
                label1.Visible = true;
                pictureBox5.Visible = true;
                bunifuMaterialTextbox1.Visible = true;
                btnEnviar.Visible = true;
                btnCancelar.Visible = true;
            }
            else
            {
                MessageBox.Show("Compruebe su conexión a internet", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }
        private bool HayInternet()
        {
            try
            {
                System.Net.IPHostEntry host = System.Net.Dns.GetHostEntry("www.google.com");

                return true;
            }
            catch
            {
                return false;
            }
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void txtNickname_OnValueChanged(object sender, EventArgs e)
        {

        }

        private void txtContra_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                entrar();
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        string correoPrincipal = "sistemarhvb@gmail.com";
        string contraPrincipal = "Skate1234";
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
        private void btnEnviar_Click(object sender, EventArgs e)
        {
            var email = bunifuMaterialTextbox1.Text;
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
            try
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
                MessageBox.Show("Contraseña enviada", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception a)
            {
                MessageBox.Show("ADVERTENCIA", "Error al enviar correo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            linklblcontrasena.Visible = true;
            txtNickname.Visible = true;
            pictureBox2.Visible = true;
            pictureBox3.Visible = true;
            txtContra.Visible = true;
            btnIngresar.Visible = true;
            label1.Visible = false;
            pictureBox5.Visible = false;
            bunifuMaterialTextbox1.Visible = false;
            btnEnviar.Visible = false;
            btnCancelar.Visible = false;
            bunifuSeparator2.Location = new Point(520, 533);
        }

        private void bunifuSeparator2_Load(object sender, EventArgs e)
        {

        }
    }




    public class DownloadGamefile
    {
        private volatile bool _completed;

        public void DescargAsincrona(string address, string location)
        {
            WebClient client = new WebClient();
            Uri uri = new Uri(address);
            _completed = false;

            client.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed);

            client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(DownloadProgress);
            client.DownloadFileAsync(uri, location);

        }

        public bool DownloadCompleted { get { return _completed; } }

        private void DownloadProgress(object sender, DownloadProgressChangedEventArgs e)
        {
            // Displays the operation identifier, and the transfer progress.
            Console.WriteLine("{0}    downloaded {1} of {2} bytes. {3} % complete...",
                (string)e.UserState,
                e.BytesReceived,
                e.TotalBytesToReceive,
                e.ProgressPercentage);
        }

        private void Completed(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {
                Console.WriteLine("Download has been canceled.");
            }
            else
            {
                Console.WriteLine("Download completed!");
            }

            _completed = true;
        }
    }

}
