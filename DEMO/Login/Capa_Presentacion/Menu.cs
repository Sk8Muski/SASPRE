﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using System.IO.Compression;
using System.IO;
using Newtonsoft.Json;
using System.Net;
using System.Globalization;
using Capa_Negocio;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace Capa_Presentacion
{
   
    public partial class Menu : Form
    {
        WebBrowser navegador = new WebBrowser();

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

        Label[] labelsDia, labelsFecha;

        private const String MON = "Monday";
        private const String TUE = "Tuesday";
        private const String WED = "Wednesday";
        private const String THU = "Thursday";
        private const String FRI = "Friday";
        private const String SAT = "Saturday";
        private const String SUN = "Sunday";

        private const String LUN = "Lunes";
        private const String MAR = "Martes";
        private const String MIE = "Miercoles";
        private const String JUE = "Jueves";
        private const String VIE = "Viernes";
        private const String SAB = "Sabado";
        private const String DOM = "Domingo";


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
            catch (Exception a)
            {
                MessageBox.Show("Ha ocurrido un error " + a.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
            catch (Exception a)
            {
                MessageBox.Show("Ha ocurrido un error " + a.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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


        DateTime fecha_hora;
        public Menu()
        {
            InitializeComponent();
            m_aeroEnabled = false;
            fecha_hora = DateTime.Now;
            labelsDia = new Label[] { labelDiaHoyNombre, labelDia1, labelDia2, labelDia3, labelDia4 };
            labelsFecha = new Label[] { labelHoy, labelFecha1, labelFecha2, labelFecha3, labelFecha4 };
        }

        //METODO PARA ABRIR FORM DENTRO DE PANEL-----------------------------------------------------
        public void AbrirFormEnPanel<Forms>() where Forms : Form, new()
        {
            try
            {
                if(btnDatAtmos == true)
                {
                    panelDerecho.Visible = false;
                    btnDatAtmos = false;
                }
                else
                {
                    panelDerecho.Visible = true;
                }
                Form formulario;
                formulario = myPanel1.Controls.OfType<Forms>().FirstOrDefault();

                //si el formulario/instancia no existe, creamos nueva instancia y mostramos
                if (formulario == null)
                {
                    formulario = new Forms();
                    formulario.TopLevel = false;
                    formulario.FormBorderStyle = FormBorderStyle.None;
                    formulario.Dock = DockStyle.Fill;
                    myPanel1.Controls.Add(formulario);
                    myPanel1.Tag = formulario;
                    formulario.Show();
                    formulario.BringToFront();
                    formulario.Opacity = .5;
                    //  formulario.FormClosed += new FormClosedEventHandler(CloseForms);
                }
                else
                {

                    //si la Formulario/instancia existe, lo traemos a frente
                    formulario.BringToFront();

                    //Si la instancia esta minimizada mostramos
                    if (formulario.WindowState == FormWindowState.Minimized)
                    {
                        formulario.WindowState = FormWindowState.Normal;
                    }

                }
            }
            catch (Exception a)
            {
                MessageBox.Show("Ha ocurrido un error " + a.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void CerrarFormEnPanel<Forms>() where Forms : Form, new()
        {
            try
            {
                Form formulario = new Forms();
                formulario = myPanel1.Controls.OfType<Forms>().FirstOrDefault();

                if (!(formulario == null))
                {

                    formulario.Close();
                }
            }
            catch (Exception a)
            {
                MessageBox.Show("Ha ocurrido un error " + a.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }
        private void pictureBox4_Click(object sender, EventArgs e)
        {
            try
            {
                Application.Exit();
            }
            catch (Exception a)
            {
                MessageBox.Show("Ha ocurrido un error " + a.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        CN_DatosClimaMes _DatosClimaMes = new CN_DatosClimaMes();

        private void Menu_Load(object sender, EventArgs e)
        {
            try
            {
                if(HayInternet() == true)
                {
                    navegador.ScriptErrorsSuppressed = true;

                    navegador.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(this.datos_cargados);
                    navegador.Navigate("https://www.google.com/search?q=clima+ciudad+mante&rlz=1C1NHXL_esMX696MX697&oq=clima+ciudad+mante&aqs=chrome..69i57j69i60l2j0l3.4208j1j7&sourceid=chrome&ie=UTF-8");
                    timerClima.Start();

                    PrivilegioUsuario();
                    labelFechaCompletaHoy.Text = DateTime.Now.ToLongDateString();
                    // Hago el ciclo para agregar hasta 7 días
                    for (int i = 1; i <= 5; i++)
                    {
                        // Este metodo solo pone en los labels el día que está en fecha_hora
                        if (i != 1)
                            SetDateTime(labelsDia[i - 1], fecha_hora);
                        PonerFechas(labelsFecha[i - 1], fecha_hora);
                        // Cambia el DateTime fecha_hora a un día después.
                        fecha_hora = fecha_hora.AddDays(1);
                    }
                    MostrarInformacionClima();
                    panelDerecho.BackColor = Color.FromArgb(0, 0, 0, 0);
                    _DatosClimaMes.AgregarDiario(DateTime.Now.ToString("yy-MM-dd"));
                    bunifuFlatButton1_Click(null, e);
                }
                else
                {
                    MessageBox.Show("Compruebe su conexion a internet, no tendrá todas las funcionalidades");
                }

            }
            catch (MySqlException ex)
            {

            }
            catch (Exception a)
            {
                MessageBox.Show("Ha ocurrido un error " + a.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        private void MostrarInformacionClima()
        {
            MostrarTemperaturaMaxima();
            MostrarTemperaturaMinima();
            MostrarPrecipitaciones();
            MostrarDescripcionDia();
            MostrarInformacionHoy();
        }
        private void datos_cargados(object sender, EventArgs e)
        {
            try
            {
                lblCentigrados.Text = navegador.Document.GetElementById("wob_tm").InnerText + "° Centigrados";

                labelClimaHoy.Text = navegador.Document.GetElementById("wob_tm").InnerText + "° C";
                foreach (HtmlElement etiqueta in navegador.Document.All)
                {
                    if (etiqueta.GetAttribute("Classname").Contains("vk_gy vk_sh wob-dtl"))
                    {

                        ktf.Kuto scrapper = new ktf.Kuto(etiqueta.InnerText);
                        //precipitaciones: 
                        lblPrecipitacionmm.Text = scrapper.Extract("precipitaciones: ", "Humedad:").ToString();
                        lblEstado.Text = scrapper.Extract("Humedad: ", ".").ToString();

                    }
                }
            }
            catch (Exception)
            {
            }
            
        }
        private void MostrarInformacionHoy()
        {
            /*var temperaturaHoy = ScrapperCN.GetTemperaturaHoy();
            //MessageBox.Show(temperaturaHoy.ToString());
            var precipitacion = ScrapperCN.GetPrecipitation()["dia1"];
            var humedad = GetHumedad(precipitacion);
            var valorPrecipitacion = GetPrecipitacion(precipitacion);

            lblCentigrados.Text = temperaturaHoy.ToString() + "° Centigrados";
            lblPrecipitacionmm.Text = valorPrecipitacion;
            lblEstado.Text = humedad;*/

        }

        private void MostrarTemperaturaMaxima()
        {
            var maxTemperature = ScrapperCN.GetMaxTemperature();

            labelHoyMax.Text = maxTemperature["dia1"] + "°C";
            labelMax1.Text = maxTemperature["dia2"] + "°C";
            labelMax2.Text = maxTemperature["dia3"] + "°C";
            labelMax3.Text = maxTemperature["dia4"] + "°C";
            labelMax4.Text = maxTemperature["dia5"] + "°C";
        }

        private void MostrarTemperaturaMinima()
        {
            var minTemperature = ScrapperCN.GetMinTemperature();

            labelHoyMin.Text = minTemperature["dia1"] + "°C";
            labelMin1.Text = minTemperature["dia2"] + "°C";
            labelMin2.Text = minTemperature["dia3"] + "°C";
            labelMin3.Text = minTemperature["dia4"] + "°C";
            labelMin4.Text = minTemperature["dia5"] + "°C";
        }

        private void MostrarPrecipitaciones()
        {
            var cero = "   0%\n0.0 mm";
            labelPrecipitacionHoy.Text = GetPrecipitationDayString(1);
            if (String.IsNullOrWhiteSpace(labelPrecipitacionHoy.Text)) labelPrecipitacionHoy.Text = cero;

            labelPrecipitacion1.Text = GetPrecipitationDayString(2);
            if (String.IsNullOrWhiteSpace(labelPrecipitacion1.Text)) labelPrecipitacion1.Text = cero;

            labelPrecipitacion2.Text = GetPrecipitationDayString(3);
            if (String.IsNullOrWhiteSpace(labelPrecipitacion2.Text)) labelPrecipitacion2.Text = cero;

            labelPrecipitacion3.Text = GetPrecipitationDayString(4);
            if (String.IsNullOrWhiteSpace(labelPrecipitacion3.Text)) labelPrecipitacion3.Text = cero;

            labelPrecipitacion4.Text = GetPrecipitationDayString(5);
            if (String.IsNullOrWhiteSpace(labelPrecipitacion4.Text)) labelPrecipitacion4.Text = cero;
        }

        private string GetPrecipitationDayString(int day)
        {
            string result = "";
            var gap = "   ";

            var precipitationsInformation = ScrapperCN.GetPrecipitation();

            var precipitationDay = precipitationsInformation[$"dia{day}"];

            if (precipitationDay.Contains('%'))
            {
                var precipitationDayInformation = precipitationDay.Split(' ');

                var precipitationPercentageDay = precipitationDayInformation[0];
                if (String.IsNullOrEmpty(precipitationPercentageDay)) precipitationPercentageDay = "0%";

                var precipitationMmDay = precipitationDayInformation[1];
                if (String.IsNullOrWhiteSpace(precipitationMmDay)) precipitationMmDay = "0.0 mm";

                result += gap + precipitationPercentageDay + "\n\r" + precipitationMmDay + " mm";

                return result;
            }

            result += precipitationDay;

            return result;
        }

        private string GetHumedad(string precipitacion)
        {
            if (precipitacion.Contains('%'))
            {
                var precipitationDayInformation = precipitacion.Split(' ');

                var humedad = precipitationDayInformation[0].ToString();

                var result = humedad;

                return result;
            }

            return "";
        }

        private string GetPrecipitacion(string precipitacion)
        {

            if (precipitacion.Contains('%'))
            {
                var precipitationDayInformation = precipitacion.Split(' ');

                var valorPrecipitacion = precipitationDayInformation[1];

                var result = valorPrecipitacion + " mm";

                return result;
            }
            return "";
        }

        private string descripcionDia1;
        private string descripcionDia2;
        private string descripcionDia3;
        private string descripcionDia4;
        private string descripcionDia5;

        private void MostrarDescripcionDia()
        {
            var descriptions = ScrapperCN.GetDescription();

            var infoDay1 = descriptions["dia1"].Split(':');
            var infoDay2 = descriptions["dia2"].Split(':');
            var infoDay3 = descriptions["dia3"].Split(':');
            var infoDay4 = descriptions["dia4"].Split(':');
            var infoDay5 = descriptions["dia5"].Split(':');

            this.picClimaHoy.Image = picClimaActual.Image =  ObtenerImagenDesdeCodigo(infoDay1[0], 1);
            this.picClima1.Image = ObtenerImagenDesdeCodigo(infoDay2[0], 2);
            this.picClima2.Image = ObtenerImagenDesdeCodigo(infoDay3[0], 3);
            this.picClima3.Image = ObtenerImagenDesdeCodigo(infoDay4[0], 4);
            this.picClima4.Image = ObtenerImagenDesdeCodigo(infoDay5[0], 5);

            descripcionDia1 = lblDescripcion.Text = infoDay1[1];
            descripcionDia2 = infoDay2[1];
            descripcionDia3 = infoDay3[1];
            descripcionDia4 = infoDay4[1];
            descripcionDia5 = infoDay5[1];

        }

        private void PonerFechas(Label lbl, DateTime datetime)
        {
            try
            {
                lbl.Text = datetime.ToString("M");
            }
            catch (Exception a)
            {
                MessageBox.Show("Ha ocurrido un error " + a.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        private void SetDateTime(Label lbl, DateTime datetime)
        {
            try
            {
                lbl.Text = TranslateDay(datetime.DayOfWeek.ToString());
                //lbl.Text = "Fecha: " + datetime.ToShortDateString() + ", Hora: " + datetime.ToLongTimeString();
            }
            catch (Exception a)
            {
                MessageBox.Show("Ha ocurrido un error " + a.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        private String TranslateDay(String day)
        {
            try
            {

                if (day.Equals(MON)) return LUN;
                if (day.Equals(TUE)) return MAR;
                if (day.Equals(WED)) return MIE;
                if (day.Equals(THU)) return JUE;
                if (day.Equals(FRI)) return VIE;
                if (day.Equals(SAT)) return SAB;
                if (day.Equals(SUN)) return DOM;
                if (day.Equals(MON)) return LUN;
                if (day.Equals(MON)) return LUN;
            }
            catch (Exception a)
            {
                MessageBox.Show("Ha ocurrido un error " + a.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            return $"{day} NOT A DAY";

        }

        bool mnuExpanded = false;
        private void MouseDetect_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                //if (!bunifuTransition1.IsCompleted) return;
                if (myPanel2.ClientRectangle.Contains(PointToClient(Control.MousePosition)))
                {
                    if (!mnuExpanded)
                    {
                        mnuExpanded = true;
                        myPanel2.Width = 250;
                    }
                }
                else
                {
                    if (mnuExpanded)
                    {
                        mnuExpanded = false;
                        //   myPanel2.Visible = false;
                        myPanel2.Width = 45;
                        myPanel2.Visible = true;
                        // bunifuTransition1.ShowSync(myPanel2);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("¿Desea cerrar el programa?", "Finalizar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Application.Exit();
                }
            }
            catch (Exception ex)
            {

            }

        }

        Thread th;
        private void bunifuFlatButton3_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            Login log = new Login();
            log.Show();
            Cursor.Current = Cursors.Default;
            this.Hide();
        }
        private void bunifuFlatButton5_Click(object sender, EventArgs e)
        {
            AbrirFormEnPanel<HistorialDePlagas>();
            panelClima.Visible = false;
            lblTemp.Visible = true;
            lblCentigrados.Visible = true;
            lblHumedad.Visible = true;
            lblEstado.Visible = true;
            lblPrecipitacion.Visible = true;
            lblPrecipitacionmm.Visible = true;
        }
        private void bunifuFlatButton6_Click(object sender, EventArgs e)
        {
            AbrirFormEnPanel<EstadisticasDePlagas>();
            panelClima.Visible = false;
            lblTemp.Visible = true;
            lblCentigrados.Visible = true;
            lblHumedad.Visible = true;
            lblEstado.Visible = true;
            lblPrecipitacion.Visible = true;
            lblPrecipitacionmm.Visible = true;
        }
        private void bunifuFlatButton7_Click(object sender, EventArgs e)
        {
            AbrirFormEnPanel<AdministrarCultivos2>();
            panelClima.Visible = false;
            lblTemp.Visible = true;
            lblCentigrados.Visible = true;
            lblHumedad.Visible = true;
            lblEstado.Visible = true;
            lblPrecipitacion.Visible = true;
            lblPrecipitacionmm.Visible = true;
        }
        private void bunifuFlatButton8_Click(object sender, EventArgs e)
        {
            AbrirFormEnPanel<GenerarReportes>();
            lblTemp.Visible = true;
            panelClima.Visible = false;
            lblCentigrados.Visible = true;
            lblHumedad.Visible = true;
            lblEstado.Visible = true;
            lblPrecipitacion.Visible = true;
            lblPrecipitacionmm.Visible = true;
        }
        private void bunifuFlatButton10_Click(object sender, EventArgs e)
        {
            AbrirFormEnPanel<ConfiguracionGeneral>();
            lblTemp.Visible = true;
            panelClima.Visible = false;
            lblCentigrados.Visible = true;
            lblHumedad.Visible = true;
            lblEstado.Visible = true;
            lblPrecipitacion.Visible = true;
            lblPrecipitacionmm.Visible = true;
        }
        private void panelDerecho_Paint(object sender, PaintEventArgs e)
        {

        }
        private void lblEstado_Click(object sender, EventArgs e)
        {

        }
        public void bunifuFlatButton4_Click(object sender, EventArgs e)
        {
            pictureBox1.Visible = true;
            pictureBox2.Visible = true;
            CerrarFormEnPanel<AdministrarCultivos2>();
            CerrarFormEnPanel<ConfiguracionGeneral>();
            CerrarFormEnPanel<EstadisticasDePlagas>();
            CerrarFormEnPanel<GenerarReportes>();
            CerrarFormEnPanel<HistorialDePlagas>();
            CerrarFormEnPanel<AdministrarCultivos>();
            CerrarFormEnPanel<AdministrarCultivosEditar>();
            CerrarFormEnPanel<ConfiguracionGeneralAgregar>();
            CerrarFormEnPanel <FromUsuarioABC>();
            CerrarFormEnPanel<Fertilizantes>();
            CerrarFormEnPanel<Datos_Atmosfericos>();
            CerrarFormEnPanel<Cosechas>();
            panelDerecho.Visible = true;
            panelClima.Visible = true;
            lblTemp.Visible = false;
            lblCentigrados.Visible = false;
            lblHumedad.Visible = false;
            lblEstado.Visible = false;
            lblPrecipitacion.Visible = false;
            lblPrecipitacionmm.Visible = false;
            pictureBox1.Visible = true;
            pictureBox2.Visible = true;
        }
        private void myPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
        private void PrivilegioUsuario()
        {
            if (Program.cargo != "Admin")
            {
                btnAdministrarUsuarios.Visible = false;
            }
        }
        private void bunifuFlatButton10_Load(object sender, EventArgs e)
        {

        }
        async void GetRequestHora()
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                String url = "https://smn.cna.gob.mx/webservices/index.php?method=3";
                using (HttpClient client = new HttpClient())
                using (HttpResponseMessage response = await client.GetAsync(url)) //obtener una variable con la info del url
                using (var content = await response.Content.ReadAsStreamAsync()) //obtener la info del archivo
                using (var descomprimido = new GZipStream(content, CompressionMode.Decompress)) //descomprimir el archivo
                {
                    if (response.IsSuccessStatusCode)
                    {
                        StreamReader reader = new StreamReader(descomprimido);
                        String data = reader.ReadLine();
                        var listInfo = JsonConvert.DeserializeObject<List<Ciudad>>(data);
                        foreach (var info in listInfo)
                        {
                            if (info.CityId.Equals("MXTS2043") && info.HourNumber == 0)
                            {
                                labelClimaHoy.Text = info.TempC.ToString() + "° C";
                            }
                        }
                    }
                }
                Cursor = Cursors.Default;
            }
            catch (Exception a)
            {
                MessageBox.Show("Ha ocurrido un error " + a.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        async void GetRequestDia()
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                String url = "https://smn.cna.gob.mx/webservices/index.php?method=1";
                using (HttpClient client = new HttpClient())
                using (HttpResponseMessage response = await client.GetAsync(url)) //obtener una variable con la info del url
                using (var content = await response.Content.ReadAsStreamAsync()) //obtener la info del archivo
                using (var descomprimido = new GZipStream(content, CompressionMode.Decompress)) //descomprimir el archivo
                {
                    if (response.IsSuccessStatusCode)
                    {
                        StreamReader reader = new StreamReader(descomprimido);
                        String data = reader.ReadLine();
                        var listInfo = JsonConvert.DeserializeObject<List<CiudadDia>>(data);
                        var iteracion = 0;
                        var diasSiguientes = false;
                        foreach (var info in listInfo)
                        {
                            if (info.CityId.Equals("MXTS2043") && DateTime.ParseExact(info.LocalValidDate.Substring(0, 8), "yyyyMMdd", CultureInfo.InvariantCulture).ToLongDateString().Equals(DateTime.Now.ToLongDateString()))
                            {
                                if (iteracion == 0)
                                {
                                    labelFechaCompletaHoy.Text = DateTime.ParseExact(info.LocalValidDate.Substring(0, 8), "yyyyMMdd", CultureInfo.InvariantCulture).ToLongDateString();
                                    labelHoy.Text = DateTime.Now.ToString("m");
                                    labelHoyMax.Text = info.HiTempC + "°";
                                    labelHoyMin.Text = info.LowTempC + "°";
                                    labelPrecipitacionHoy.Text = info.ProbabilityOfPrecip + "%";
                                    picClimaHoy.Image = vectorClima(info.SkyText, 0);
                                    iteracion++;
                                    diasSiguientes = true;
                                }
                            }
                            else if (info.CityId.Equals("MXTS2043") && diasSiguientes == true)
                            {
                                if (iteracion == 1)
                                {
                                    labelFecha1.Text = DateTime.Now.ToString("m");
                                    labelFecha1Max.Text = info.HiTempC + "°";
                                    labelFecha1Min.Text = info.LowTempC + "°";
                                    labelPrecipitacion1.Text = info.ProbabilityOfPrecip + "%";
                                    picClima1.Image = vectorClima(info.SkyText, 0);
                                    iteracion++;
                                }
                                else if (iteracion == 2)
                                {
                                    labelFecha2.Text = DateTime.Now.ToString("m");
                                    labelFecha2Max.Text = info.HiTempC + "°";
                                    labelFecha2Min.Text = info.LowTempC + "°";
                                    labelPrecipitacion2.Text = info.ProbabilityOfPrecip + "%";
                                    picClima2.Image = vectorClima(info.SkyText, 0);
                                    iteracion++;
                                }
                                else if (iteracion == 3)
                                {
                                    labelFecha3.Text = DateTime.Now.ToString("m");
                                    labelMax3.Text = info.HiTempC + "°";
                                    labelMin3.Text = info.LowTempC + "°";
                                    labelPrecipitacion3.Text = info.ProbabilityOfPrecip + "%";
                                    picClima3.Image = vectorClima(info.SkyText, 0);
                                    iteracion++;
                                }
                                else if (iteracion == 4)
                                {
                                    labelFecha4.Text = DateTime.Now.ToString("m");
                                    labelFecha4Max.Text = info.HiTempC + "°";
                                    labelFecha4Min.Text = info.LowTempC + "°";
                                    labelPrecipitacion4.Text = info.ProbabilityOfPrecip + "%";
                                    picClima4.Image = vectorClima(info.SkyText, 0);
                                    iteracion++;
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception a)
            {
                MessageBox.Show("Ha ocurrido un error " + a.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }


        private void ObtenerDias()
        {


        }
        private void timerClima_Tick(object sender, EventArgs e)
        {
            /*if (Convert.ToInt32(DateTime.Now.Minute.ToString()) == 0 && Convert.ToInt32(DateTime.Now.Second.ToString()) == 0)
            {
                //GetRequestHora();
            }*/
            navegador.Navigate("https://www.google.com/search?q=clima+ciudad+mante&rlz=1C1NHXL_esMX696MX697&oq=clima+ciudad+mante&aqs=chrome..69i57j69i60l2j0l3.4208j1j7&sourceid=chrome&ie=UTF-8");
        }

        public Image vectorClima(String texto, int panel)
        {
            try
            {
                // Nublado -> d400
                if (texto.Equals("d400") && panel == 0)
                {
                    return Vectores.Images[11];
                }
                else if (texto.Equals("Parcialmente nublado / Viento") && panel == 0)
                {
                    return Vectores.Images[15];
                }
                // Parcialmente nublado --> d200
                else if (texto.Equals("d200") && panel == 0)
                {
                    return Vectores.Images[15];
                }
                else if (texto.Equals("Aguaceros en la mañana") && panel == 0)
                {
                    return Vectores.Images[2];
                }
                // Soleado -> d000
                else if (texto.Equals("d000") && panel == 0)
                {
                    return Vectores.Images[17];
                }
                else if (texto.Equals("Nubes por la mañana / Sol por la tarde") && panel == 0)
                {
                    return Vectores.Images[15];
                }
                else if (texto.Equals("Lluvia en la mañana") && panel == 0)
                {
                    return Vectores.Images[2];
                }
                // Mayormente soleado / Viento --> d100
                else if (texto.Equals("d100") && panel == 0)
                {
                    return Vectores.Images[17];
                }
                else if (texto.Equals("Tormentas por la tarde") && panel == 0)
                {
                    return Vectores.Images[2];
                }
                else if (texto.Equals("Tormentas aisladas") && panel == 0)
                {
                    return Vectores.Images[2];
                }
                else if (texto.Equals("Tormentas dispersas") && panel == 0)
                {
                    return Vectores.Images[2];
                }
                else if (texto.Equals("Tormentas") && panel == 0)
                {
                    return Vectores.Images[2];
                }
                else if (texto.Equals("Tormentas en la mañana") && panel == 0)
                {
                    return Vectores.Images[2];
                }
                // Aguaceros por la tarde --> d210
                else if (texto.Equals("d210") && panel == 0)
                {
                    return Vectores.Images[2];
                }
                else if (texto.Equals("Aguaceros") && panel == 0)
                {
                    return Vectores.Images[2];
                }
                else if (texto.Equals("Algunos aguaceros") && panel == 0)
                {
                    return Vectores.Images[2];
                }
                else if (texto.Equals("Lluvia débil por la tarde") && panel == 0)
                {
                    return Vectores.Images[2];
                }
                else if (texto.Equals("Lluvia por la tarde") && panel == 0)
                {
                    return Vectores.Images[2];
                }
                // Aguaceros y tormentas por la tarde --> d240
                else if (texto.Equals("d240") && panel == 0)
                {
                    return Vectores.Images[2];
                }
                else if (texto.Equals("Soleado / Viento") && panel == 0)
                {
                    return Vectores.Images[17];
                }
                else if (texto.Equals("Aguaceros y tormentas") && panel == 0)
                {
                    return Vectores.Images[2];
                }
                else if (texto.Equals("Lluvia") && panel == 0)
                {
                    return Vectores.Images[2];
                }
                else if (texto.Equals("Nublado / Viento") && panel == 0)
                {
                    return Vectores.Images[11];
                }
                // Mayormente nublado / viento -> d300
                else if (texto.Equals("d300") && panel == 0)
                {
                    return Vectores.Images[11];
                }
                else if (texto.Equals("Nubes por la mañana / Sol por la tarde / Viento") && panel == 0)
                {
                    return Vectores.Images[15];
                }
                else if (texto.Equals("Tormentas aisladas / Viento") && panel == 0)
                {
                    return Vectores.Images[2];
                }
                else
                {
                    return null;
                }
            }
            catch (Exception a)
            {
                MessageBox.Show("Ha ocurrido un error " + a.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return null;
            }
            /*         
            Nublado
            Parcialmente nublado / Viento
            Parcialmente nublado
            Aguaceros en la mañana
            Soleado
            Nubes por la mañana / Sol por la tarde
            Lluvia en la mañana
            Mayormente soleado / Viento
            Tormentas por la tarde
            Tormentas aisladas
            Tormentas dispersas
            Tormentas
            Tormentas en la mañana
            Aguaceros por la tarde
            Aguaceros
            Algunos aguaceros
            Lluvia débil por la tarde
            Lluvia por la tarde
            Aguaceros y tormentas por la tarde
            Soleado / Viento
            Aguaceros y tormentas
            Lluvia
            Nublado / Viento
            Mayormente nublado/ Viento
            Nubes por la mañana / Sol por la tarde / Viento
            Tormentas aisladas / Viento
            */
        }

        public Image ObtenerImagenDesdeCodigo(String texto, int dayPictureBox)
        {
            try
            {
                // case 1 = Primer panel, este debe ser blanco de perefencia, sino color.
                // case 2 y 3 = Segundo y tercer panel, estos deben ser negros.
                // case 4 y 5 = Cuarto y quinto panel, estos a color de preferencia, sino blanco.
                switch (dayPictureBox)
                {
                    case 1:
                        // Parcialmente nuboso, posibilidad de tormentas y lluvia | Tormentas
                        if (texto.Equals("d240") || texto.Equals("n240")) return Vectores.Images[18];
                        else if (texto.Equals("d440") || texto.Equals("n440")) return Vectores.Images[18];

                        // Algunas nubes | Algunas nubes
                        else if (texto.Equals("d100") || texto.Equals("n100")) return Vectores.Images[14];
                        else if (texto.Equals("d200") || texto.Equals("n200")) return Vectores.Images[14];

                        // Parcialmente nublado, lluvia ligera
                        if (texto.Equals("d210") || texto.Equals("n210")) return Vectores.Images[0];
                        else if (texto.Equals("d220") || texto.Equals("n220")) return Vectores.Images[0];

                        // Nublado
                        else if (texto.Equals("d300") || texto.Equals("n300")) return Vectores.Images[9];

                        // Despejado
                        else if (texto.Equals("d000") || texto.Equals("n000")) return Vectores.Images[16];

                        // Nublado, lluvia ligera
                        if (texto.Equals("d310") || texto.Equals("n310")) return Vectores.Images[3];
                        else if (texto.Equals("d320") || texto.Equals("3220")) return Vectores.Images[3];

                        break;
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                        // Parcialmente nuboso, posibilidad de tormentas y lluvia | Tormentas
                        if (texto.Equals("d240") || texto.Equals("n240")) return Vectores.Images[20];
                        else if (texto.Equals("d440") || texto.Equals("n440")) return Vectores.Images[20];

                        // Algunas nubes | Algunas nubes
                        else if (texto.Equals("d100") || texto.Equals("n100")) return Vectores.Images[15];
                        else if (texto.Equals("d200") || texto.Equals("n200")) return Vectores.Images[15];

                        // Parcialmente nublado, lluvia ligera
                        if (texto.Equals("d210") || texto.Equals("n210")) return Vectores.Images[2];
                        else if (texto.Equals("d220") || texto.Equals("n220")) return Vectores.Images[2];

                        // Nublado
                        else if (texto.Equals("d300") || texto.Equals("n300")) return Vectores.Images[11];

                        // Despejado
                        else if (texto.Equals("d000") || texto.Equals("n000")) return Vectores.Images[17];

                        // Nublado, lluvia ligera
                        if (texto.Equals("d310") || texto.Equals("n310")) return Vectores.Images[5];
                        else if (texto.Equals("d320") || texto.Equals("3220")) return Vectores.Images[5];

                        break;
                }
                
            }
            catch(Exception a)
            {
                MessageBox.Show("Ha ocurrido un error " + a.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return null;
            }
            return null;
        }

        private void btnAdministrarUsuarios_Click(object sender, EventArgs e)
        {
            try
            {
                AbrirFormEnPanel<FromUsuarioABC>();
                lblTemp.Visible = true;
                panelClima.Visible = false;
                lblCentigrados.Visible = true;
                lblHumedad.Visible = true;
                lblEstado.Visible = true;
                lblPrecipitacion.Visible = true;
                lblPrecipitacionmm.Visible = true;
            }
            catch (Exception a)
            {
                MessageBox.Show("Ha ocurrido un error " + a.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void btnAdministrarCosechas_Click(object sender, EventArgs e)
        {
            panelDerecho.Visible = true;
            AbrirFormEnPanel<Cosechas>();
            panelClima.Visible = false;
            lblTemp.Visible = true;
            lblCentigrados.Visible = true;
            lblHumedad.Visible = true;
            lblEstado.Visible = true;
            lblPrecipitacion.Visible = true;
            lblPrecipitacionmm.Visible = true;
        }
        bool btnDatAtmos;

        private void btnFertilizantes_Click(object sender, EventArgs e)
        {
            AbrirFormEnPanel<Fertilizantes>();
            panelClima.Visible = false;
            lblTemp.Visible = true;
            lblCentigrados.Visible = true;
            lblHumedad.Visible = true;
            lblEstado.Visible = true;
            lblPrecipitacion.Visible = true;
            lblPrecipitacionmm.Visible = true;
        }

        private void picClimaHoy_MouseHover(object sender, EventArgs e)
        {
            var toolTip = new ToolTip();
            toolTip.SetToolTip(picClimaHoy, descripcionDia1);
        }

        private void picClima1_MouseHover(object sender, EventArgs e)
        {
            var toolTip = new ToolTip();
            toolTip.SetToolTip(picClima1, descripcionDia2);
        }

        private void picClima2_MouseHover(object sender, EventArgs e)
        {
            var toolTip = new ToolTip();
            toolTip.SetToolTip(picClima2, descripcionDia3);
        }

        private void picClima3_MouseHover(object sender, EventArgs e)
        {
            var toolTip = new ToolTip();
            toolTip.SetToolTip(picClima3, descripcionDia4);
        }

        private void picClima4_MouseHover(object sender, EventArgs e)
        {
            var toolTip = new ToolTip();
            toolTip.SetToolTip(picClima4, descripcionDia5);
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            btnDatAtmos = true;
            panelDerecho.Visible = false;
            AbrirFormEnPanel<Datos_Atmosfericos>();
            panelClima.Visible = false;
            
            //lblTemp.Visible = true;
            //lblCentigrados.Visible = true;
            //lblHumedad.Visible = true;
            //lblEstado.Visible = true;
            //lblPrecipitacion.Visible = true;
            //lblPrecipitacionmm.Visible = true;
            //Datos_Atmosfericos datos = new Datos_Atmosfericos();
            //datos.Visible = true;

        }

        private void bunifuFlatButton1_Click_1(object sender, EventArgs e)
        {
            AbrirFormEnPanel<CalculoDePlagas>();
            lblTemp.Visible = true;
            panelClima.Visible = false;
            lblCentigrados.Visible = true;
            lblHumedad.Visible = true;
            lblEstado.Visible = true;
            lblPrecipitacion.Visible = true;
            lblPrecipitacionmm.Visible = true;
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
    }
}
