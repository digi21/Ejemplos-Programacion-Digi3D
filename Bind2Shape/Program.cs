using System;
using Digi21.DigiNG.DigiTab;
using Digi21.DigiNG.IO.BinDouble;
using Digi21.DigiNG.IO.Shp;

namespace Bind2Shape
{
    /// <summary>
    /// Crea un archivo de dibujo con las entidades del archivo de dibujo de entrada pero al que se ha eliminado el código pasado por parámetros.
    /// </summary>
    internal class Program
    {
        private static void Main(string[] args)
        {
            if (args.Length < 4)
            {
                Console.Error.WriteLine("Error: No se han especificado parámetros.");
                Console.Error.WriteLine("El formato es: Bind2Shape.exe [tabla de códigos] [base de datos.mdb] [archivo bin a traducir] [carpeta de salida]");
                Console.Error.WriteLine("Ejemplo: Bind2Shape.exe prueba.Tab.xml prueba.mdb prueba.bind shapefiles");
                return;
            }

            try
            {
                var tabla = DigiTab.Load(args[0]);

                var parámetrosbbdd = new DatabaseConnectionProperties
                {
                    ConnectionString =
                        $"Provider=Microsoft.ACE.OLEDB.16.0;Data Source={args[1]};Persist Security Info=False",
                    DataModel = "CATDBS"
                };

                using (var archivoEntrada = new BinDouble(args[2], parámetrosbbdd, tabla))
                using (var archivoSalida = new Shp(args[3], tabla, ()=> archivoEntrada.Wkt))
                {
                    foreach (var entidad in archivoEntrada)
                    {
                        try
                        {
                            var clonada = entidad.Clone();
                            archivoSalida.Add(clonada, archivoEntrada.GetDatabaseAttributes(entidad));
                            Console.Write("+");
                        }
                        catch (Exception excepción)
                        {
                            Console.Error.WriteLine($"Se localizó el siguiente error: {excepción.Message}");
                        }
                    }
                }
            }
            catch (Exception excepción)
            {
                Console.Error.WriteLine($"Se localizó el siguiente error: {excepción.Message}");
            }

        }

    }
}
