using Digi21.Math;
using DotSpatial.Positioning;
using System;
using System.Collections.Generic;

namespace DigiNG.IO.Gps
{
    public class ProveedorDotSpatial : IProveedorGps
    {
        private NmeaInterpreter nmeaInterpreter = new NmeaInterpreter();
        private double altitud;

        public bool EsConectado => nmeaInterpreter.IsRunning;

        public ProveedorDotSpatial()
        {
            nmeaInterpreter.IsFilterEnabled = false;
            nmeaInterpreter.AltitudeChanged += (_, e) => altitud = e.Distance.Value;
            nmeaInterpreter.PositionChanged += PositionChanged;
            nmeaInterpreter.SatellitesChanged += (_, e) => Estado?.Invoke(this, new EstadoEventArgs() { Estado = $"Número de satélites: {e.Satellites.Count}" });
            Devices.DeviceDetected += DevicesDeviceDetected;
        }

        public event EventHandler<CoordenadasEventArgs> Coordenada;
        public event EventHandler<EstadoEventArgs> Estado;
        public event EventHandler Conectado;

        public void Start()
        {
            Devices.BeginDetection();
        }

        public void Stop()
        {
            if (nmeaInterpreter.IsRunning)
                nmeaInterpreter.Stop();

            nmeaInterpreter.Device?.Close();
            if( Devices.IsDetectionInProgress )
                Devices.Undetect();
        }

        private void DevicesDeviceDetected(object sender, DeviceEventArgs e)
        {
            nmeaInterpreter.Start(e.Device);
            Conectado?.Invoke(this, new EventArgs());
        }

        private void PositionChanged(object sender, PositionEventArgs e)
        {
            Coordenada?.Invoke(this, new CoordenadasEventArgs()
            {
                Coordenadas = new Point3D(
                    e.Position.Longitude.ToRadians().Value * 180 / Math.PI,
                    e.Position.Latitude.ToRadians().Value * 180 / Math.PI,
                    altitud)
            });
        }

    }
}
