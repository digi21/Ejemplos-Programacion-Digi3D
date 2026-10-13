# Digi21.IO.Gps

Extensión de Digi3D.AI para utilizar GPSs como dispositivos de entrada.

Para la conexión con GPSs antiguos, se utiliza el paquete NuGet DotSpatial.Positioning

## No compila con .NET 10

El proyecto sigue en `net8.0-windows` con los paquetes `Digi21.DigiNG` y `Digi21.DigiNG.Plugin` 24.0.0, y no compila con ninguna versión de los paquetes:

- `FormularioGps.cs` usa los espacios de nombres `Digi21.OpenGis.CoordinateSystems`, `Digi21.OpenGis.CoordinateTransformations` y `Digi21.OpenGis.Epsg` para transformar las coordenadas del GPS (EPSG:4979) al sistema de referencia de la ventana de dibujo. Los paquetes 26.0.0 no contienen ninguna API de transformación de coordenadas, y Digi3D.AI para .NET 10 no instala ensamblados .NET con esos espacios de nombres. Los paquetes `Digi21.OpenGis` y `Digi21.Epsg` 22.0.0 de nuget.org son ensamblados de referencia para .NET Framework 4.6.
- `ProveedorLocation.cs` usa `System.Device.Location`, que solo existe en .NET Framework. Se puede sustituir por `Windows.Devices.Geolocation` con `TargetFramework` `net10.0-windows10.0.19041.0`.

Para compilar el ejemplo hace falta un paquete 26.0.0 con una API .NET de transformación de coordenadas, y que Digi3D.AI instale su implementación.
