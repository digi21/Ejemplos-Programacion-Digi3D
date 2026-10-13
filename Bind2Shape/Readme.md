# Bind2Shape

Este programa de consola carga una tabla de códigos, una base de datos y un archivo .BIND y lo traduce a Shapefile.
Asume que las geometrías del archivo .bind tienen enlaces de base de datos a una base de datos Access.
Las geometrías con la marca de borrado no se exportan.

## Requisitos

- SDK de .NET 10.
- Digi3D.AI para .NET 10 instalado en el equipo, para ejecutar el programa.
- El proveedor OLE DB `Microsoft.ACE.OLEDB.16.0` de 64 bits.

El proyecto referencia los paquetes `Digi21.DigiNG`, `Digi21.DigiNG.IO.BinDouble` y `Digi21.DigiNG.IO.Shp` 26.0.0.

## Uso

```
Bind2Shape.exe [tabla de códigos] [base de datos.mdb] [archivo bin a traducir] [archivo .shp de salida]
```

El cuarto argumento es la ruta de un archivo .shp. El exportador de Shapefiles de Digi3D.AI crea los shapefiles en la
carpeta de ese archivo, uno por capa, aunque el archivo indicado puede no llegar a crearse si ninguna geometría se
almacena en esa capa.

Ejemplo de cómo llamar al programa:

```
Bind2Shape tablacodigos.tab.xml basedatos.mdb archivo.bind shapefiles\prueba.shp
```

Este programa almacena los shapefiles en la subcarpeta _shapefiles_ (según se ha indicado en la línea de comandos anterior). El programa no crea esa subcarpeta, de manera que tiene que existir.
