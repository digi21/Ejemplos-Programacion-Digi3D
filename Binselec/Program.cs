using System;
using System.Linq;
using System.IO;
using Digi21.DigiNG.IO.BinDouble;
using Digi21.Utilities;

if (args.Length < 2)
{
    Console.Error.WriteLine("Error, no has indicado suficientes parámetros.");
    Console.Error.WriteLine("El formato es el siguiente: ExtraerEntidadesDeArchivos [archivo .bin a generar] [codigo 1]...[código n].");
    return;
}

var di = new DirectoryInfo(Directory.GetCurrentDirectory());
var archivos = di.GetFiles("*.bind");

using var archivoSalida = new BinDouble(args[0]);
foreach (var archivo in archivos)
{
    using var archivoEntrada = new BinDouble(archivo.Name);
    Console.WriteLine($"Analizando el archivo: {archivo.Name}");

    var entidades = from entidad in archivoEntrada
        where entidad.TieneAlgúnCódigo(args.Skip(1))
        select entidad;

    foreach (var entidad in entidades)
        archivoSalida.Add(entidad.Clone());
}