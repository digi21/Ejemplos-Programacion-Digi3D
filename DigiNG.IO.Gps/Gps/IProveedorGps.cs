using Digi21.Math;
using System;
using System.Collections.Generic;

namespace DigiNG.IO.Gps
{
    // Superficie respecto a la que el GPS mide la altitud.
    public enum ReferenciaAltitud
    {
        Elipsoide,
        Geoide,
        Desconocida
    }

    public class CoordenadasEventArgs : EventArgs
    {
        // Longitud y latitud en grados WGS 84 y altitud en metros.
        public Point3D Coordenadas { get; set; }
        public ReferenciaAltitud Altitud { get; set; }
    }

    public class EstadoEventArgs : EventArgs
    {
        public string Estado { get; set; }
    }
    public interface IProveedorGps
    {
        void Start();
        void Stop();

        bool EsConectado { get; }
        event EventHandler Conectado;
        event EventHandler<CoordenadasEventArgs> Coordenada;
        event EventHandler<EstadoEventArgs> Estado;
    }
}
