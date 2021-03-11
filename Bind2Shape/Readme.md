# Bind2Shp

Este programa de consola carga una tabla de c�digos, una base de datos y un archivo .BIND y lo traduce a Shapefile.
Asume que las geometr�as del archivo .bind tienen enlaces de base de datos a una base de datos Access.

El exportador de Shapefiles de Digi3D.NET tiene la particularidad de que requiere que se pase como ruta un archivo .shp 
como archivo de salida, aunque ese archivo quiz�s nunca se cree porque no haya geometr�as que se almacenen en dicha capa.


Ejemplo de c�mo llamar al programa:

```
Bind2Shp tablacodigos.tab.xml basedatos.mdb archivo.bind shapefiles\prueba.shp
```

Este programa almacenar� los shapefiles en la subcarpeta _shapefiles_ (seg�n se ha indicado en la l�nea de comandos anterior). El programa no crea esa subcarpeta, de manera que debe de existir.