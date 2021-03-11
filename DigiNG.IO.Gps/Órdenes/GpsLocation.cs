using Digi21.Digi3D;
using Digi21.DigiNG.Plugin.Command;
using System;
using Digi21.DigiNG.Plugin.Menu;

namespace DigiNG.IO.Gps
{
    [Command(Name = "gps", Description = "Activa el panel de GPS")]
    [CommandInMenu("GPS por API de localización", MenuItemGroup.WindowGroup1)]
    public class GpsLocation : Command
    {
        public GpsLocation()
        {
            this.Initialize += ConectaGps_Initialize;
        }

        private void ConectaGps_Initialize(object sender, EventArgs e)
        {
            Digi3D.Panes.Add(new FormularioGps(new ProveedorLocation()), PaneDock.Left, false);
            Dispose();
        }
    }
}
