# TopologiasDesdeTabla

Este programa de consola crea polígonos a partir de las topologías definidas en una tabla de códigos, como hace la orden
`TOPOLOGIA_A_POLIGONO` de Digi3D.AI, y los añade a cada archivo `.bind` de una carpeta.

Para cada archivo `.bind` y cada topología, el programa:

1. Analiza las entidades del archivo con `AnalyzeTopology` y escribe por consola los avisos del análisis (líneas con puntos
   dobles, contornos sin área, recintos sin centroide, centroides duplicados o sin recinto, y entidades que no forman parte
   de ningún recinto válido).
2. Crea los polígonos de los recintos con `CreatePolygons`. Si la topología tiene una regla de coordenada Z que proyecta
   sobre un modelo digital del terreno, usa el `.mdt` indicado. Los recintos sin centroide no generan polígono.
3. Añade los polígonos al archivo `.bind`, que se abre en modo lectura y escritura, y escribe por consola el número de
   recintos y de polígonos.

El análisis de cada topología usa las entidades que había en el archivo al abrirlo: los polígonos añadidos para una
topología no forman parte del análisis de las topologías siguientes. El programa conserva los polígonos existentes y
añade los polígonos que genera en cada ejecución.

## Requisitos

- SDK de .NET 10.
- Digi3D.AI para .NET 10 instalado en el equipo, para ejecutar el programa.
- Los paquetes `Digi21.DigiNG` 26.2.0, `Digi21.DigiNG.Topology` 26.1.0, `Digi21.DigiNG.Dtm` 26.0.0 y
  `Digi21.DigiNG.IO.BinDouble` 26.0.1. Contienen ensamblados de referencia: al ejecutarse, el programa carga los
  ensamblados de la carpeta de instalación de Digi3D.AI.

`Digi21.DigiNG.Dtm` abre el modelo digital del terreno con las DLL de MDTopX de la carpeta de instalación de Digi3D.AI.

## Uso

```
TopologiasDesdeTabla.exe [carpeta con archivos .bind] [tabla de códigos .tab.xml] [modelo digital del terreno .mdt (opcional)] [topología 1] ... [topología n]
```

- El tercer argumento se interpreta como modelo digital del terreno si su extensión es `.mdt`; en otro caso se interpreta
  como el nombre de una topología. Sin modelo, los polígonos conservan la Z de los vértices originales.
- Si no se indica ninguna topología, el programa procesa todas las de la tabla de códigos.
- El modelo digital del terreno se abre una sola vez y se usa para todos los archivos y topologías.
- El programa procesa los archivos `*.bind` de la carpeta indicada, sin recorrer sus subcarpetas.

Ejemplos:

```
TopologiasDesdeTabla C:\Datos tabla.tab.xml
TopologiasDesdeTabla C:\Datos tabla.tab.xml terreno.mdt EDIFICIO ACERA
```

El programa libera con `Dispose` cada polígono después de añadirlo al archivo, cada definición de topología al terminar
`CreaYAnadePoligonosDeTopologia`, y la tabla de códigos y el modelo digital del terreno al terminar de procesar los
archivos.
