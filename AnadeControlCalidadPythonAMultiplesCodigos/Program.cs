using System.Xml;

Console.WriteLine("Añadir control de calidad Python a múltiples códigos.");

if (args.Length < 4)
{
    Console.Error.WriteLine("Se necesitan parámetros: [ruta a la tabla a modificar] [ruta de la tabla de códigos a crear con las modificaciones] [Control de calidad a añadir] [Códigos a los que añadir la etiqueta]");
    return;
}

var digiTab = new XmlDocument();
digiTab.Load(args[0]);

var root = digiTab.DocumentElement;

var controlCalidadAnadir = args[2];

foreach (var code in args.Skip(3))
{
    var nodo = root!.SelectSingleNode($"/digitab/codes/code[@name='{code}']");
    if (nodo is null)
    {
        Console.Error.WriteLine($"No se ha localizado el código {code}");
        continue;
    }


    var controlesCalidad = nodo.SelectSingleNode("quality_control");
    if (controlesCalidad is null)
    {
        controlesCalidad = digiTab.CreateNode(XmlNodeType.Element, "quality_control", "");
        nodo.AppendChild(controlesCalidad);
    }

    if (string.IsNullOrEmpty(controlesCalidad.InnerText))
        controlesCalidad.AppendChild(digiTab.CreateCDataSection(controlCalidadAnadir));
    else
        controlesCalidad.InnerText += "\n" + controlCalidadAnadir;

}

digiTab.Save(args[1]);