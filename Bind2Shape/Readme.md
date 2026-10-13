# Bind2Shape

Este programa de consola carga una tabla de códigos, una base de datos y un archivo .BIND y lo traduce a Shapefile.
Asume que las geometrías del archivo .bind tienen enlaces de base de datos a una base de datos Access.

El cuarto argumento es la ruta de un archivo .shp. El exportador de Shapefiles de Digi3D.AI crea los shapefiles en la
carpeta de ese archivo, uno por capa, aunque el archivo indicado puede no llegar a crearse si ninguna geometría se
almacena en esa capa.

Ejemplo de cómo llamar al programa:

```
Bind2Shape tablacodigos.tab.xml basedatos.mdb archivo.bind shapefiles\prueba.shp
```

Este programa almacena los shapefiles en la subcarpeta _shapefiles_ (según se ha indicado en la línea de comandos anterior). El programa no crea esa subcarpeta, de manera que tiene que existir.
