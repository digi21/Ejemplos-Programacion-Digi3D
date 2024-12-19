using Digi21.DigiNG.IO.BinDouble;

if (args.Length < 1)
{
    Console.Error.WriteLine("Se esperaban parámetros: [archivo a comprimir]");
    return;
}

if (!File.Exists(args[0]))
{
    Console.Error.WriteLine($"El archivo {args[0]} no existe");
    return;
}

var rutaTemporal = Path.Combine(Path.GetTempPath(), Path.ChangeExtension(Path.GetTempFileName(), "bind"));

using (var entrada = new BinDouble(args[0]))
{
    using var salida = new BinDouble(rutaTemporal, () => entrada.Wkt);

    Console.WriteLine($"Comprimiendo archivo {args[0]}...");
    foreach (var geometría in entrada)
    {
        if (geometría.Deleted)
            continue;

        salida.Add(geometría.Clone());
    }
}

File.Delete(args[0]);
File.Move(rutaTemporal, args[0]);

Console.WriteLine("Trabajo finalizado");
