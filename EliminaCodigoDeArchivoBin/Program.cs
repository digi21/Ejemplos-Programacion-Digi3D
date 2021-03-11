using System;
using Digi21.DigiNG.IO.BinDouble;

namespace EliminaCodigoDeArchivoBin
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
                Console.Error.WriteLine("El formato es: EliminaCodigoDeArchivoBin.exe [tabla de códigos] [archivo de entrada] [archivo de salida] [código a eliminar]");
                Console.Error.WriteLine("Ejemplo: EliminaCodigoDeArchivoBin.exe digiBCA.Tab.xml 29091711.bin salida.bin 201");
                return;
            }

            try
            {
                using (var archivoEntrada = new BinDouble(args[1]))
                {
                    using (var archivoSalida = new BinDouble(args[2], false))
                    {
                        foreach (var entidad in archivoEntrada)
                        {
                            var clonada = entidad.Clone();

                            // Eliminamos todos los códigos de la entidad clonada
                            clonada.Codes.Clear();

                            // Añadimos únicamente los códigos cuyo nombre no coincida con el pasado por parámetros
                            foreach (var código in entidad.Codes)
                            {
                                if (código.Name == args[3])
                                    continue;

                                clonada.Codes.Add(código);
                            }

                            if (entidad.Codes.Count == 0)
                                continue; // No almacenamos la entidad, pues al quitarle el código indicado por parámetros se ha quedado sin códigos. Era el único que tenía.

                            archivoSalida.Add(clonada);
                            Console.Write("*");
                        }
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
