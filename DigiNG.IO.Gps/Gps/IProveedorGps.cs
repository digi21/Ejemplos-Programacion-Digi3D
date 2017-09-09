using Digi21.Math;
using System;
using System.Collections.Generic;

namespace DigiNG.IO.Gps
{
    public class CoordenadasEventArgs : EventArgs
    {
        public Point3D Coordenadas { get; set; }
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
