using Digi21.DigiNG;
using Digi21.OpenGis.CoordinateSystems;
using Digi21.OpenGis.CoordinateTransformations;
using Digi21.OpenGis.Epsg;
using System;
using System.Windows.Forms;

namespace DigiNG.IO.Gps
{
    public partial class FormularioGps : Form
    {
        IProveedorGps gps;
        ICoordinateTransformation transformación;
        private double[] últimasCoordenadas = new double[3];

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
            gps.Stop();
        }

        private void Gps_Conectado(object sender, EventArgs e)
        {
            botonConectar.Text = "Desconectar";
            botonConectar.Enabled = true;
        }

        private void Gps_Satélites(object sender, EstadoEventArgs e)
        {
            BeginInvoke(new MethodInvoker(() =>
            {
                Digi21.Digi3D.Digi3D.StatusBar.Text = e.Estado;
            }));
        }

        private void Gps_Coordenada(object sender, CoordenadasEventArgs e)
        {
            BeginInvoke(new MethodInvoker(() =>
            {
                txtLongitud.Text = e.Coordenadas.X.ToString();
                txtLatitud.Text = e.Coordenadas.Y.ToString();
                txtAltitud.Text = e.Coordenadas.Z.ToString();
            }));


            últimasCoordenadas = transformación.MathTransform.Transform(new[]
            {
                e.Coordenadas.Y,
                e.Coordenadas.X,
                e.Coordenadas.Z
            });

            EnvíaEvento(false, false);
        }

        private void botonConectar_Click(object sender, EventArgs e)
        {
            if (gps.EsConectado)
            {
                gps.Stop();
                botonConectar.Text = "Conectar";
            }
            else
            {
                try
                {
                    var coordinateSystemFactory = new CoordinateSystemFactory();
                    var coordinateTransformationFactory = new CoordinateTransformationFactory();

                    transformación = coordinateTransformationFactory.CreateFrom3DCoordinateSystems(
                        CoordinateSystemAuthorityFactory.CreateGeographicCoordinateSystem(4979),
                        coordinateSystemFactory.CreateFromWkt(Digi21.DigiNG.DigiNG.Wkt),
                        SelectTransformationHelper.DialogSelectTransformation,
                        CreateVerticalTransformationHelper.DialogCreateVerticalTransformation);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Se detectó el error: {ex.Message} al intentar crear una transformación entre el sistema de referencia de coordenadas del GPS y el de la ventana de dibujo");
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

            if (últimasCoordenadas != null)
            {
                BeginInvoke(new MethodInvoker(() =>
                {
                    try
                    {
                        Digi21.DigiNG.DigiNG.SendInputDeviceEvent(
                        new Digi21.Math.Point3D(
                            últimasCoordenadas[0],
                            últimasCoordenadas[1],
                            últimasCoordenadas[2]),
                        button);
                    }
                    catch
                    { }
                }));
            }
        }
    }
}
