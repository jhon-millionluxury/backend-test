# Luxury Property API

## Requisitos

- .NET 9 SDK
- MongoDB (local o remoto)

## Configuración

1. Clonar el repositorio `git clone https://github.com/jhon-millionluxury/backend-test`
2. cd LuxuryProperty
3. Abrir la carpeta `src/LuxuryProperty.API` en Visual Studio Code
4. Crear un archivo `.env` en la carpeta `src/LuxuryProperty.API` o copiar el archivo `.env.example` y modificarlo

```
MONGO_URI=tu_uri_mongo
MONGO_DBNAME=nombre_de_la_base_de_datos
```

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

## Seeders

Para insertar datos de ejemplo, puedes ejecutar el seeder de la base de datos.

1. Abrir la carpeta `src/LuxuryProperty.API` en Visual Studio Code
2. Abrir el archivo `Program.cs`
3. Descomentar las líneas de código después de `// If you want to new data for testing, uncomment the following lines`
4. Ejecutar el proyecto
   - En Visual Studio Code, presionar `F5`
   - En la terminal, ejecutar `dotnet run`

Esto insertará unas 25 propiedades de ejemplo en la base de datos.
