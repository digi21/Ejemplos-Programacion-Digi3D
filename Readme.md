# Ejemplos de programación de Digi3D.AI

Este repositorio contiene ejemplos que demuestran cómo programar con las API de programación .NET de Digi3D.AI.

La documentación de cada ejemplo está en la [ayuda de Digi3D.AI](https://github.com/digi21/docs/blob/main/src/digi3d-ai/programacion/.net/ejemplos/README.md).

## Requisitos

- SDK de .NET 10 (Visual Studio 2026, o `dotnet build` con el SDK 10).
- Digi3D.AI para .NET 10 instalado en el equipo, para ejecutar las aplicaciones de consola y para compilar `BindADwg`. Las utilidades de tablas de códigos no lo necesitan.

Los proyectos usan `net10.0` y los paquetes NuGet `Digi21.*` de la versión 26.0.0. Los paquetes `Digi21.DigiNG.*` solo contienen ensamblados de referencia: al ejecutarse, el programa carga los ensamblados de la carpeta de instalación de Digi3D.AI. Ningún proyecto referencia ya el paquete `Digi21.Digi3D.App`. La página [Migración a .NET 10](https://github.com/digi21/docs/blob/main/src/digi3d-ai/programacion/.net/migracion-a-net-10.md) explica los cambios respecto a los paquetes 24.0.0.

## Compilar

Compila cada proyecto por separado. Por ejemplo, desde la carpeta del repositorio:

```
dotnet build comprimir\comprimir.csproj -c Release -o C:\Herramientas\comprimir
```

La solución `Ejemplos-programacion-digi3d.sln` no compila completa, porque `DigiNG.IO.Gps` no compila con .NET 10 (ver su `Readme.md`).

## Ejemplos

| Ejemplo | Tipo | Qué hace |
|---|---|---|
| `AnadeEtiquetaAMultiplesCodigosTablaCodigos` | Utilidad de tablas de códigos | Añade una etiqueta a varios códigos de una tabla de códigos. |
| `AnadeControlCalidadPythonAMultiplesCodigos` | Utilidad de tablas de códigos | Añade un control de calidad a varios códigos de una tabla de códigos. |
| `EliminaControlesCalidadPythonDuplicados` | Utilidad de tablas de códigos | Elimina los controles de calidad repetidos de cada código. |
| `comprimir` | Aplicación de consola | Elimina de un archivo `.bind` las entidades borradas. |
| `Binselec` | Aplicación de consola | Copia a un archivo nuevo las entidades de los `.bind` de la carpeta actual que tienen alguno de los códigos indicados. |
| `EliminaCodigoDeArchivoBin` | Aplicación de consola | Copia un archivo `.bind` quitando un código a todas las entidades. |
| `EliminarTextosDeArchivoBinD` | Aplicación de consola | Copia un archivo `.bind` sin los textos que tienen un código y un contenido determinados. |
| `ListarArchivosReferenciaEnBinD` | Aplicación de consola | Muestra los archivos de referencia de un archivo `.bind`. |
| `MuestraAtributosBBDDGeometriasArchivoBinD` | Aplicación de consola | Muestra los atributos de base de datos de las entidades de un archivo `.bind`. |
| `Bind2Shape` | Aplicación de consola | Convierte un archivo `.bind` con atributos de base de datos a Shapefile. |
| `BindADwg` | Aplicación de consola | Convierte un archivo `.bind` a DWG. Referencia el ensamblado `Digi21.DigiNG.IO.Dwg` de la instalación de Digi3D.AI (ver su `Readme.md`). |
| `DigiNG.IO.Gps` | Extensión | Añade las órdenes `GPS` y `GPS_CLASICO`, que usan un receptor GPS como dispositivo de entrada. No compila con .NET 10. |

Las aplicaciones de consola no tienen en cuenta las entidades con la marca de borrado (`Deleted`).
