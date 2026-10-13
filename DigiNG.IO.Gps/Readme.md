# Digi21.IO.Gps

Extensión de Digi3D.AI para utilizar GPSs como dispositivos de entrada.

La extensión añade dos órdenes, que abren un panel con las coordenadas del GPS y los botones de dato, tentativo y cancelar:

- `gps` lee la posición con la API de localización de Windows (`Windows.Devices.Geolocation`).
- `gps_clasico` lee las sentencias NMEA de un GPS conectado a un puerto serie con el paquete NuGet `DotSpatial.Positioning`.

## Requisitos

- `TargetFramework` `net10.0-windows10.0.19041.0`, necesario para `Windows.Devices.Geolocation`.
- Paquetes `Digi21.DigiNG`, `Digi21.DigiNG.Plugin` y `Digi21.CoordinateSystems` 26.0.0, y `DotSpatial.Positioning` 4.0.656 (la primera versión para .NET; la 1.9.0 es para .NET Framework).
- `Digi21DisableAssemblyResolver` a `true`, como toda extensión, y `EnableDynamicLoading` a `true` para que la compilación copie `DotSpatial.Positioning.dll` y `System.IO.Ports.dll` a la carpeta de salida.

## Transformación de coordenadas

El panel transforma las coordenadas del GPS al sistema de referencia de la ventana de dibujo con `Digi21.CoordinateSystems`. El sistema de origen depende de la referencia de la altitud que indica el GPS:

| referencia de la altitud | sistema de origen |
|---|---|
| elipsoide (`AltitudeReferenceSystem.Ellipsoid`) | EPSG:4979, WGS 84 con altura elipsoidal |
| geoide (`AltitudeReferenceSystem.Geoid` y las sentencias NMEA) | EPSG:4326 + EPSG:5773, WGS 84 con altitud sobre el geoide EGM96 |
| sin indicar (`Unspecified`, `Terrain`, `Surface`) | EPSG:4979; el panel avisa en la barra de estado de que toma la altitud como altura elipsoidal |

Los proveedores dan la longitud antes que la latitud, así que el panel usa `WithEastNorthAxes` sobre el sistema de origen. La transformación se crea con `TransformationOptions.Interactive`: si hay varias operaciones o falta una transformación vertical, Digi3D.AI muestra sus cuadros de diálogo.
