# BindADwg

Este programa de consola convierte un archivo `.bind` a un archivo DWG de AutoCAD 2010.

## Requisitos

- SDK de .NET 10.
- Digi3D.AI para .NET 10 instalado en el equipo, para compilar y para ejecutar el programa.

## Referencia a Digi21.DigiNG.IO.Dwg

El ensamblado `Digi21.DigiNG.IO.Dwg` no tiene paquete NuGet. El proyecto referencia el ensamblado que instala Digi3D.AI:

```
<carpeta de instalación>\Digi21.DigiNG.IO.Dwg\26.0.0\Digi21.DigiNG.IO.Dwg.dll
```

La propiedad MSBuild `Digi3DInstallDir` indica la carpeta de instalación. Si no se asigna, el proyecto toma el primero de estos valores que no esté vacío:

1. La variable de entorno `DIGI3D_INSTALLDIR`.
2. El valor `InstallDir` de la clave de registro `HKEY_LOCAL_MACHINE\SOFTWARE\Digi21\Digi3D.NET\App\Configuration`, que escribe el instalador de Digi3D.AI.
3. `C:\Program Files\Digi21.net\Digi3D.AI\`, la carpeta de instalación por defecto.

Para compilar contra otra instalación, asigna la propiedad en la línea de órdenes:

```
dotnet build BindADwg\BindADwg.csproj -p:Digi3DInstallDir="D:\Digi3D.AI"
```

Si el archivo no existe, la compilación termina con el error `No se encuentra <ruta>`.

La referencia tiene `Private` a `false`, así que `dotnet build` no copia `Digi21.DigiNG.IO.Dwg.dll` a la carpeta de salida. Al ejecutarse, el programa carga el ensamblado de la carpeta de instalación de Digi3D.AI, igual que los ensamblados de los paquetes `Digi21.DigiNG.*`.

## Uso

```
BindADwg.exe [archivo original] [archivo a crear] [tabla de códigos]
```

## Ejemplo

```
BindADwg.exe a.bind a.dwg Digi.tab.xml
```
