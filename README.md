# Luxury Property API

## Requisitos

- Dotnet 9.0
- MongoDB

## Instalación

1. Clonar el repositorio
2. Abrir la carpeta `src/LuxuryProperty.API` en Visual Studio Code
3. Abrir el archivo `appsettings.json` y modificar la conexión a la base de datos
4. Abrir el archivo `.env` y modificar las variables de entorno
5. Ejecutar el proyecto
   - En Visual Studio Code, presionar `F5`
   - En la terminal, ejecutar `dotnet run`

## API

### Propiedades

#### GET /api/properties

Obtener todas las propiedades

#### GET /api/properties/{id}

Obtener una propiedad por su ID

#### POST /api/properties

Crear una nueva propiedad

#### PUT /api/properties/{id}

Actualizar una propiedad

#### DELETE /api/properties/{id}

Eliminar una propiedad
