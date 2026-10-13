using System;
using Digi21.DigiNG.DigiTab;
using Digi21.DigiNG.IO.BinDouble;

if (args.Length < 2)
{
    Console.Error.WriteLine("Error: No se han especificado parámetros.");
    Console.Error.WriteLine("El formato es: ListarArchivosReferenciaEnBinD.exe [tabla de códigos] [archivo bind]");
    Console.Error.WriteLine("Ejemplo: ListarArchivosReferenciaEnBinD.exe digiBCA.Tab.xml 29091711.bind");
    return;
}

var tabla = DigiTab.Load(args[0]);

using var bin = new BinDouble(args[1], tabla);
foreach (var reference in bin.ReferenceFiles)
{
    Console.WriteLine(reference);
}