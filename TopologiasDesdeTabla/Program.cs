using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Digi21.DigiNG.DigiTab;
using Digi21.DigiNG.Dtm;
using Digi21.DigiNG.IO.BinDouble;
using Digi21.DigiNG.Topology;
using Digi21.Math;

if (args.Length < 2)
{
    Console.Error.WriteLine("Error: no se han especificado suficientes parámetros.");
    Console.Error.WriteLine("El formato es: TopologiasDesdeTabla.exe [carpeta con archivos .bind] [tabla de códigos .tab.xml] [modelo digital del terreno .mdt (opcional)] [topología 1] ... [topología n]");
    Console.Error.WriteLine("Si no se indica ninguna topología se procesan todas las de la tabla de códigos.");
    Console.Error.WriteLine(@"Ejemplo: TopologiasDesdeTabla.exe C:\Datos tabla.tab.xml terreno.mdt EDIFICIO ACERA");
    return 1;
}

var carpeta = args[0];
var rutaTabla = args[1];
var argumentosOpcionales = args.Skip(2).ToList();

// El tercer argumento es el modelo digital del terreno solo si tiene extensión .mdt; en otro caso es el nombre de una topología.
string? rutaMdt = null;
if (argumentosOpcionales.Count > 0 && string.Equals(Path.GetExtension(argumentosOpcionales[0]), ".mdt", StringComparison.OrdinalIgnoreCase))
{
    rutaMdt = argumentosOpcionales[0];
    argumentosOpcionales.RemoveAt(0);
}

if (!Directory.Exists(carpeta))
{
    Console.Error.WriteLine($"La carpeta {carpeta} no existe.");
    return 1;
}

try
{
    using var tabla = DigiTab.Load(rutaTabla);

    var nombresTopologias = argumentosOpcionales.Count > 0 ? argumentosOpcionales : tabla.Topologies.Select(topologia => topologia.Name).ToList();
    if (nombresTopologias.Count == 0)
    {
        Console.Error.WriteLine("La tabla de códigos no define ninguna topología.");
        return 1;
    }

    // El modelo se abre una sola vez y se comparte entre todos los archivos y topologías.
    // Con etiqueta vacía, la colección proyecta sobre cualquier modelo que contenga el punto.
    var modelos = new DigitalTerrainModelCollection();
    using var modelo = rutaMdt is null ? null : DigitalTerrainModel.Open(rutaMdt);
    if (modelo is not null)
        modelos.Add(modelo, "");

    foreach (var rutaArchivo in Directory.EnumerateFiles(carpeta, "*.bind").OrderBy(rutaBind => rutaBind, StringComparer.OrdinalIgnoreCase))
    {
        Console.WriteLine($"Archivo {rutaArchivo}");

        try
        {
            // Se abre en modo lectura y escritura porque se le añaden los polígonos.
            using var archivo = new BinDouble(rutaArchivo, tabla);

            // Los recintos mantienen referencias a estas entidades hasta que se crean los polígonos.
            // La lista es una copia: los polígonos que se añaden al archivo no entran en el análisis de las topologías siguientes.
            var entidades = archivo.ToList();

            foreach (var nombreTopologia in nombresTopologias)
                CreaYAnadePoligonosDeTopologia(archivo, entidades, tabla, nombreTopologia, modelos);
        }
        catch (Exception excepción)
        {
            Console.Error.WriteLine($"  Se localizó el siguiente error: {excepción.Message}");
        }
    }
}
catch (Exception excepción)
{
    Console.Error.WriteLine($"Se localizó el siguiente error: {excepción.Message}");
    return 1;
}

return 0;

static void CreaYAnadePoligonosDeTopologia(BinDouble archivo, List<Digi21.DigiNG.Entities.Entity> entidades, DigiTab tabla, string nombreTopologia, DigitalTerrainModelCollection modelos)
{
    TopologyDefinition definicion;
    try
    {
        definicion = tabla.GetTopology(nombreTopologia);
    }
    catch (Exception excepción)
    {
        Console.Error.WriteLine($"  [{nombreTopologia}] No se puede obtener la topología: {excepción.Message}");
        return;
    }

    using (definicion)
    {
        var manejadoresAvisosAnalisisTopologico = new TopologyAnalysisHandlers
        {
            LineWithDoublePoints = línea =>
                Console.WriteLine($"  [{nombreTopologia}] Aviso: línea con puntos dobles al principio o al final, cerca de {FormateaCoordenadasXY(línea.Center)}"),
            EntityWithFewerThanTwoVertices = entidad =>
                Console.WriteLine($"  [{nombreTopologia}] Aviso: punto o línea con menos de dos vértices, cerca de {FormateaCoordenadasXY(entidad.Center)}"),
            RingWithoutArea = líneas =>
                Console.WriteLine($"  [{nombreTopologia}] Aviso: {líneas.Count()} líneas forman un contorno que no encierra ninguna área"),
            PolygonWithoutCentroid = recinto =>
                Console.WriteLine($"  [{nombreTopologia}] Aviso: recinto sin centroide cerca de {FormateaCoordenadasXY(recinto.Polygon.Center)}"),
            DuplicatedCentroids = textos =>
                Console.WriteLine($"  [{nombreTopologia}] Aviso: un recinto contiene varios centroides: {string.Join(", ", textos.Select(textoCentroide => $"«{textoCentroide.Txt}» en {FormateaCoordenadasXY(textoCentroide.Coordinate)}"))}"),
            CentroidsWithoutPolygon = textos =>
                Console.WriteLine($"  [{nombreTopologia}] Aviso: {textos.Count()} centroides no están dentro de ningún recinto"),
            EntityNotInValidPolygon = entidad =>
                Console.WriteLine($"  [{nombreTopologia}] Aviso: entidad que no forma parte de ningún recinto válido, cerca de {FormateaCoordenadasXY(entidad.Center)}"),
        };

        try
        {
            var recintos = entidades.AnalyzeTopology(definicion, manejadoresAvisosAnalisisTopologico).ToList();

            // Los recintos sin centroide no generan polígono. Si no hay modelos, los vértices conservan su Z.
            var polígonos = recintos.CreatePolygons(definicion, modelos.Project);
            foreach (var polígono in polígonos)
            {
                archivo.Add(polígono);
                polígono.Dispose();
            }

            Console.WriteLine($"  [{nombreTopologia}] {recintos.Count} recintos, {polígonos.Count} polígonos añadidos");
        }
        catch (Exception excepción)
        {
            Console.Error.WriteLine($"  [{nombreTopologia}] Se localizó el siguiente error: {excepción.Message}");
        }
    }
}

static string FormateaCoordenadasXY(Point3D punto) => FormattableString.Invariant($"({punto.X:0.###}, {punto.Y:0.###})");
