using System.Xml;

Console.WriteLine("Añadir etiquetas a códigos en una tabla de códigos.");

if (args.Length < 4)
{
    Console.Error.WriteLine("Se necesitan parámetros: [ruta a la tabla de códigos sin etiquetas] [ruta de la tabla de códigos a crear con etiquetas] [Etiqueta a añadir] [Códigos a los que añadir la etiqueta]");
    return;
}

var digiTab = new XmlDocument();
digiTab.Load(args[0]);

var root = digiTab.DocumentElement;

var etiquetaAñadir = args[2];

foreach (var code in args.Skip(3))
{
    var nodo = root!.SelectSingleNode($"/digitab/codes/code[@name='{code}']");

    var etiquetasNodo = nodo?.Attributes?.GetNamedItem("tags");
    if (etiquetasNodo == null) continue;

    var clonEtiquetas = etiquetasNodo.Clone();
    if (clonEtiquetas.InnerText.Length != 0)
        clonEtiquetas.InnerText = etiquetaAñadir + "," + clonEtiquetas.InnerText;
    else
        clonEtiquetas.InnerText = etiquetaAñadir;

    var clon = nodo!.Clone();
    clon.Attributes!.RemoveNamedItem("tags");
    clon.Attributes.SetNamedItem(clonEtiquetas);

    nodo.ParentNode!.ReplaceChild(clon, nodo);
}

digiTab.Save(args[1]);