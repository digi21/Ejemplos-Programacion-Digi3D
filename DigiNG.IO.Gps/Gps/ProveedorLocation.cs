using Digi21.Math;
using System;
using Windows.Devices.Geolocation;

namespace DigiNG.IO.Gps
{
    public class ProveedorLocation : IProveedorGps
    {
        private readonly Geolocator geolocalizador = new Geolocator { DesiredAccuracy = PositionAccuracy.High, ReportInterval = 1000 };
        private bool conectado;

        private void Geolocalizador_StatusChanged(Geolocator sender, StatusChangedEventArgs e)
        {
            Estado?.Invoke(this, new EstadoEventArgs()
            {
                Estado = $"Estado del GPS: {e.Status}"
            });
        }

        private void Geolocalizador_PositionChanged(Geolocator sender, PositionChangedEventArgs e)
        {
            var punto = e.Position.Coordinate.Point;
            Coordenada?.Invoke(this, new CoordenadasEventArgs()
            {
                Coordenadas = new Point3D(punto.Position.Longitude, punto.Position.Latitude, punto.Position.Altitude),
                Altitud = punto.AltitudeReferenceSystem switch
                {
                    AltitudeReferenceSystem.Ellipsoid => ReferenciaAltitud.Elipsoide,
                    AltitudeReferenceSystem.Geoid => ReferenciaAltitud.Geoide,
                    _ => ReferenciaAltitud.Desconocida
                }
            });
        }

        public bool EsConectado => conectado;

        public event EventHandler Conectado;
        public event EventHandler<CoordenadasEventArgs> Coordenada;
        public event EventHandler<EstadoEventArgs> Estado;

        public async void Start()
        {
            try
            {
                var acceso = await Geolocator.RequestAccessAsync();
                if (acceso != GeolocationAccessStatus.Allowed)
                {
                    Estado?.Invoke(this, new EstadoEventArgs() { Estado = $"Windows no permite acceder a la ubicación: {acceso}" });
                    return;
                }

                geolocalizador.StatusChanged += Geolocalizador_StatusChanged;
                geolocalizador.PositionChanged += Geolocalizador_PositionChanged;
                conectado = true;
                Conectado?.Invoke(this, new EventArgs());
            }
            catch (Exception ex)
            {
                Estado?.Invoke(this, new EstadoEventArgs() { Estado = $"No se ha podido acceder a la ubicación: {ex.Message}" });
            }
        }

        public void Stop()
        {
            geolocalizador.PositionChanged -= Geolocalizador_PositionChanged;
            geolocalizador.StatusChanged -= Geolocalizador_StatusChanged;
            conectado = false;
        }
    }
}
