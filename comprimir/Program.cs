using Digi21.DigiNG.IO.BinDouble;

if (args.Length < 2)
{
    Console.Error.WriteLine("Se esperaban parámetros: [archivo de entrada] [archivo de salida]");
    return;
}

if (!File.Exists(args[0]))
{
    Console.Error.WriteLine($"El archivo {args[0]} no existe");
    return;
}


using var entrada = new BinDouble(args[0]);
using var salida = new BinDouble(args[1], () => entrada.Wkt);

Console.WriteLine($"Comprimiendo archivo {args[0]}...");
foreach (var geometría in entrada)
{
    if (geometría.Deleted)
        continue;

    salida.Add(geometría.Clone());
}

Console.WriteLine("Trabajo finalizado");
