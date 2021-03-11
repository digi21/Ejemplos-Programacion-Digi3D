using Digi21.Digi3D;
using Digi21.DigiNG.Plugin.Command;
using System;
using Digi21.DigiNG.Plugin.Menu;

namespace DigiNG.IO.Gps
{
    [Command(Name ="gps_clasico", Description ="Activa el panel de GPS")]
    [CommandInMenu("GPS Clásico", MenuItemGroup.WindowGroup1)]
    public class GpsClasico : Command
    {
        public GpsClasico()
        {
            this.Initialize += ConectaGps_Initialize;
        }

        private void ConectaGps_Initialize(object sender, EventArgs e)
        {
            Digi3D.Panes.Add(new FormularioGps(new ProveedorDotSpatial()), PaneDock.Left, false);
            Dispose();
        }
    }
}
