# MuestraAtributosBBDDGeometriasArchivoBinD

Este programa de consola muestra todos los atributos de base de datos de las geometrías de un archivo de dibujo, salvo las que tienen la marca de borrado.
Asume que las geometrías del archivo .bind tienen enlaces de base de datos a una base de datos Access.

## Requisitos

- SDK de .NET 10.
- Digi3D.AI para .NET 10 instalado en el equipo, para ejecutar el programa.
- El proveedor OLE DB `Microsoft.ACE.OLEDB.16.0` de 64 bits.

El proyecto referencia los paquetes `Digi21.DigiNG` y `Digi21.DigiNG.IO.BinDouble` 26.0.0.

Ejemplo de cómo llamar al programa:

```
MuestraAtributosBBDDGeometriasArchivoBinD tablacodigos.tab.xml basedatos.mdb archivo.bind
```
