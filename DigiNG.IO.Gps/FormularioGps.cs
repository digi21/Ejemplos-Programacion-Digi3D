using Digi21.CoordinateSystems;
using Digi21.DigiNG;
using Digi21.Math;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace DigiNG.IO.Gps
{
    public partial class FormularioGps : Form
    {
        IProveedorGps gps;
        CoordinateSystem sistemaVentana;
        readonly Dictionary<ReferenciaAltitud, CoordinateTransformation> transformaciones = new Dictionary<ReferenciaAltitud, CoordinateTransformation>();
        private Point3D? últimasCoordenadas;

        public FormularioGps(IProveedorGps gps)
        {
            InitializeComponent();

            this.gps = gps;
            gps.Coordenada += Gps_Coordenada;
            gps.Estado += Gps_Satélites;
            gps.Conectado += Gps_Conectado;
            Digi21.DigiNG.DigiNG.DestroyingWindow += DigiNG_DestroyingWindow;
        }

        private void DigiNG_DestroyingWindow(object sender, EventArgs e)
        {
            Desconecta();
        }

        private void Gps_Conectado(object sender, EventArgs e)
        {
            BeginInvoke(new MethodInvoker(() =>
            {
                botonConectar.Text = "Desconectar";
                botonConectar.Enabled = true;
            }));
        }

        private void Gps_Satélites(object sender, EstadoEventArgs e)
        {
            BeginInvoke(new MethodInvoker(() =>
            {
                Digi21.Digi3D.Digi3D.StatusBar.Text = e.Estado;
            }));
        }

        // Los proveedores lanzan los eventos desde otros hilos. Las transformaciones se crean y se usan en el hilo
        // principal: TransformationOptions.Interactive muestra los cuadros de diálogo de Digi3D.AI.
        private void Gps_Coordenada(object sender, CoordenadasEventArgs e)
        {
            BeginInvoke(new MethodInvoker(() =>
            {
                txtLongitud.Text = e.Coordenadas.X.ToString();
                txtLatitud.Text = e.Coordenadas.Y.ToString();
                txtAltitud.Text = e.Coordenadas.Z.ToString();

                var transformación = Transformación(e.Altitud);
                if (transformación == null)
                    return;

                try
                {
                    últimasCoordenadas = transformación.Transform(e.Coordenadas);
                }
                catch (CoordinateSystemException ex)
                {
                    Digi21.Digi3D.Digi3D.StatusBar.Text = ex.Message;
                    return;
                }

                EnvíaEvento(false, false);
            }));
        }

        private CoordinateTransformation Transformación(ReferenciaAltitud referencia)
        {
            if (transformaciones.TryGetValue(referencia, out var existente))
                return existente;

            // EPSG:4979 es WGS 84 con altura elipsoidal; EPSG:4326 + EPSG:5773 es WGS 84 con altitud sobre el
            // geoide EGM96. WithEastNorthAxes: los proveedores dan la longitud antes que la latitud.
            if (referencia == ReferenciaAltitud.Desconocida)
                Digi21.Digi3D.Digi3D.StatusBar.Text = "El GPS no indica la referencia de la altitud: se toma como altura elipsoidal";

            try
            {
                var origen = referencia == ReferenciaAltitud.Geoide
                    ? CoordinateSystem.FromEpsg(4326, 5773)
                    : CoordinateSystem.FromEpsg(4979);

                var transformación = CoordinateTransformation.Create(origen.WithEastNorthAxes(), sistemaVentana, TransformationOptions.Interactive);
                transformaciones[referencia] = transformación;
                return transformación;
            }
            catch (Exception ex) when (ex is CoordinateSystemException || ex is InvalidOperationException)
            {
                Desconecta();
                MessageBox.Show($"Se detectó el error: {ex.Message} al intentar crear una transformación entre el sistema de referencia de coordenadas del GPS y el de la ventana de dibujo");
                return null;
            }
        }

        private void Desconecta()
        {
            gps.Stop();
            botonConectar.Text = "Conectar";
            botonConectar.Enabled = true;

            foreach (var transformación in transformaciones.Values)
                transformación.Dispose();
            transformaciones.Clear();
        }

        private void botonConectar_Click(object sender, EventArgs e)
        {
            if (gps.EsConectado)
            {
                Desconecta();
            }
            else
            {
                try
                {
                    sistemaVentana = CoordinateSystem.FromWkt(Digi21.DigiNG.DigiNG.Wkt);
                }
                catch (Exception ex) when (ex is CoordinateSystemException || ex is ArgumentException)
                {
                    MessageBox.Show($"Se detectó el error: {ex.Message} al leer el sistema de referencia de coordenadas de la ventana de dibujo");
                    return;
                }

                botonConectar.Enabled = false;
                gps.Start();
            }
        }


        private void botonDato_CheckedChanged(object sender, EventArgs e)
        {
            EnvíaEvento(false, false);
        }

        private void botonTentativo_Click(object sender, EventArgs e)
        {
            botonDato.Checked = false;
            EnvíaEvento(true, false);
        }

        private void botonCancelar_Click(object sender, EventArgs e)
        {
            botonDato.Checked = false;
            EnvíaEvento(false, true);
        }

        private void EnvíaEvento(bool tentativo, bool cancelar)
        {
            InputDeviceButton button = InputDeviceButton.None;

            if (tentativo)
                button = InputDeviceButton.Snap;
            else if (cancelar)
                button = InputDeviceButton.Cancel;
            else if (botonDato.Checked)
                button = InputDeviceButton.Data;

            if (últimasCoordenadas is Point3D coordenadas)
            {
                BeginInvoke(new MethodInvoker(() =>
                {
                    try
                    {
                        Digi21.DigiNG.DigiNG.SendInputDeviceEvent(coordenadas, button);
                    }
                    catch
                    { }
                }));
            }
        }
    }
}
