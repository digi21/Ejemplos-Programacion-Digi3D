using Digi21.Math;
using System;
using System.Device.Location;

namespace DigiNG.IO.Gps
{
    public class ProveedorLocation : IProveedorGps
    {
        private GeoCoordinateWatcher watcher = new GeoCoordinateWatcher(GeoPositionAccuracy.High);

        public ProveedorLocation()
        {
            watcher.PositionChanged += Watcher_PositionChanged;
            watcher.StatusChanged += Watcher_StatusChanged;
        }

        private void Watcher_StatusChanged(object sender, GeoPositionStatusChangedEventArgs e)
        {
            Estado?.Invoke(this, new EstadoEventArgs()
            {
                Estado = $"Estado del GPS: {e.Status}"
            });
        }

        private void Watcher_PositionChanged(object sender, GeoPositionChangedEventArgs<GeoCoordinate> e)
        {
            Coordenada?.Invoke(this, new CoordenadasEventArgs()
            {
                Coordenadas = new Point3D(e.Position.Location.Longitude, e.Position.Location.Latitude, e.Position.Location.Altitude)
            });
        }

        public bool EsConectado => watcher.Status == GeoPositionStatus.Ready;

        public event EventHandler Conectado;
        public event EventHandler<CoordenadasEventArgs> Coordenada;
        public event EventHandler<EstadoEventArgs> Estado;

        public void Start()
        {
            bool started = watcher.TryStart(false, TimeSpan.FromMilliseconds(2000));

            if (started)
                Conectado?.Invoke(this, new EventArgs());
        }

        public void Stop()
        {
            watcher.Stop();
        }
    }
}
