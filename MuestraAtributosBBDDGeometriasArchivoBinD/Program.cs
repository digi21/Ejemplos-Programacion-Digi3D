using System;
using Digi21.DigiNG.DigiTab;
using Digi21.DigiNG.IO.BinDouble;

namespace MuestraAtributosBBDDGeometriasArchivoBinD
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            if (args.Length < 3)
            {
                Console.Error.WriteLine("Error: No se han especificado parámetros.");
                Console.Error.WriteLine("El formato es: MuestraAtributosBBDDGeometriasArchivoBinD.exe [tabla de códigos] [base de datos.mdb] [archivo bin a enumerar]");
                Console.Error.WriteLine("Ejemplo: MuestraAtributosBBDDGeometriasArchivoBinD.exe prueba.Tab.xml prueba.mdb prueba.bind");
                return;
            }

            try
            {
                var tabla = DigiTab.Load(args[0]);

                var parámetrosbbdd = new DatabaseConnectionProperties
                {
                    ConnectionString = $"Provider=Microsoft.ACE.OLEDB.16.0;Data Source={args[1]};Persist Security Info=False",
                    DataModel = "CATDBS"
                };

                using (var archivoEntrada = new BinDouble(args[2], parámetrosbbdd, tabla))
                {
                    foreach (var entidad in archivoEntrada)
                    {
                        var atributos = archivoEntrada.GetDatabaseAttributes(entidad);

                        foreach(var código in entidad.Codes)
                            foreach (var clave in atributos[código.Name])
                                Console.WriteLine($"{clave.Key}: {clave.Value}");

                        Console.Write("-----------------");
                    }
                }
            }
            catch (Exception excepción)
            {
                Console.Error.WriteLine("Se localizó el siguiente error:");
                Console.Error.WriteLine(excepción.Message);
            }
        }
    }
}