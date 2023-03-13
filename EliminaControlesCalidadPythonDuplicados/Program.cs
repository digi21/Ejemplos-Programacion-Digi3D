using System.Xml;

Console.WriteLine("Eliminar controles de calidad Python duplicados.");

if (args.Length < 2)
{
    Console.Error.WriteLine("Se necesitan parámetros: [ruta a la tabla a modificar] [ruta de la tabla de códigos a crear con las modificaciones]");
    return;
}

var digiTab = new XmlDocument();
digiTab.Load(args[0]);

var root = digiTab.DocumentElement;
if (root is null)
    return;

foreach (XmlNode nodo in root.SelectNodes("/digitab/codes/code/quality_control"))
{
    var palabras = nodo.InnerText.Split(new[] {'\n', '\r'}, StringSplitOptions.RemoveEmptyEntries);
    if (palabras.Length == 1)
        continue;

    var palabrasFiltradas = palabras.Distinct().ToArray();

    if (palabrasFiltradas.Length == palabras.Length)
        continue;

    nodo.InnerText = string.Join('\n', palabrasFiltradas); ;
}

digiTab.Save(args[1]);