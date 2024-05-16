using Digi21.DigiNG.DigiTab;
using Digi21.DigiNG.IO.BinDouble;
using Digi21.DigiNG.IO.Dwg;

if (args.Length < 4)
{
    Console.Error.WriteLine(
        "BindADwg [archivo original] [archivo a crear] [tabla de códigos]");
    return;
}


var rutaArchivoOriginal = args[0];
var rutaArchivoCrear = args[1];
var rutaTablaCodigos = args[2];

using var digiTab = DigiTab.Load(rutaTablaCodigos);

using var archivoEntrada = new BinDouble(rutaArchivoOriginal, new DatabaseConnectionProperties(), digiTab);

using var archivoSalida = new Dwg(
    rutaArchivoCrear,
    new DwgParameters
    {
        Version = DwgVersion.Ac_2010
    }, 
    digiTab, () => archivoEntrada.Wkt);

foreach (var geometria in archivoEntrada)
{
    archivoSalida.Add(geometria.Clone());
    Console.Write("+");
}
