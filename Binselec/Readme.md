# Binselec

Este programa de consola genera un archivo BIND con las geometrías de todos los archivos .BIND localizados en el directorio activo y que tengan alguno de los códigos pasados por parámetros. Las geometrías con la marca de borrado no se copian.

## Requisitos

- SDK de .NET 10.
- Digi3D.AI para .NET 10 instalado en el equipo, para ejecutar el programa.

El proyecto referencia los paquetes `Digi21.DigiNG`, `Digi21.DigiNG.IO.BinDouble`, `Digi21.DigiNG.Plugin` y `Digi21.Utilities` 26.0.0.

## Uso

Los parámetros son `binselec.exe [archivo .bind a crear] [código 1] [código 2] .... [código n]`

Ejemplo: `binselec.exe curvado.bind 020400 020200`
