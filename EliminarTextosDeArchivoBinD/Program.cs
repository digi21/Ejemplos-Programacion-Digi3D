using Digi21.DigiNG.Entities;
using Digi21.DigiNG.IO.BinDouble;

if (args.Length < 4 || args.Length % 2 != 0)
{
    Console.Error.WriteLine(
        "EliminarTextosDeArchivoBinD [archivo original] [archivo a crear] [codigo 1] [texto a eliminar 1] ... [codigo n] [texto a eliminar n]");
    return;
}

var rutaArchivoOriginal = args[0];
var rutaArchivoCrear = args[1];

var archivoEntrada = new BinDouble(rutaArchivoOriginal);
var archivoSalida = new BinDouble(rutaArchivoCrear, () => archivoEntrada.Wkt);

var diccionario = new Dictionary<string, List<string>>();
for (var i = 2; i < args.Length; i+=2)
{
    if(!diccionario.ContainsKey(args[i]))
        diccionario[args[i]] = new();

    diccionario[args[i]].Add(args[i + 1]);
}

foreach (var geometria in archivoEntrada)
{
    if (EliminarGeometria(geometria, diccionario))
    {
        Console.Write("-");
        continue;
    }

    archivoSalida.Add(geometria.Clone());
    Console.Write("+");
}


bool EliminarGeometria(Entity geometria, Dictionary<string, List<string>> diccionario)
{
    if (geometria is not ReadOnlyText texto)
        return false;

    foreach (var codigo in diccionario)
    {
        if (texto.Codes.All(c => 0 != string.Compare(c.Name, codigo.Key, StringComparison.CurrentCultureIgnoreCase)))
            continue;

        if (codigo.Value.Any(textoAEliminar => 0 == string.Compare(texto.Txt, textoAEliminar, StringComparison.CurrentCultureIgnoreCase)))
        {
            return true;
        }
    }

    return false;
}