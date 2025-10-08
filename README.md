# 🏡 Luxury Property API

![.NET](https://img.shields.io/badge/.NET-9.0-512BD4?logo=dotnet&logoColor=white)
![C#](https://img.shields.io/badge/-C%23-239120?logo=c-sharp&logoColor=white)
![MongoDB](https://img.shields.io/badge/-MongoDB-47A248?logo=mongodb&logoColor=white)
![Clean Architecture](https://img.shields.io/badge/-Clean%20Architecture-007ACC?logo=arch-linux&logoColor=white)
![Swagger](https://img.shields.io/badge/-Swagger-85EA2D?logo=swagger&logoColor=black)
![Docker](https://img.shields.io/badge/-Docker-2496ED?logo=docker&logoColor=white)
![TypeScript](https://img.shields.io/badge/-TypeScript-007ACC?logo=typescript&logoColor=white)
![Nunit](https://img.shields.io/badge/-NUnit-007ACC?logo=nunit&logoColor=white)
![Moq](https://img.shields.io/badge/-Moq-007ACC?logo=moq&logoColor=white)

---

## 📘 Overview

**LuxuryPropertyAPI** is a RESTful API built with **.NET 9**, **MongoDB**, and follows the **Clean Architecture** pattern.  
It manages **luxury real estate properties**, supporting filters, pagination, and image management.

The architecture promotes **separation of concerns**, making the code **scalable**, **testable**, and **maintainable**.

---

## 🧩 Project Structure (Clean Architecture)

src/

├── LuxuryProperty.API → Presentation layer (controllers, swagger, startup)

├── LuxuryProperty.Application → Application layer (business logic, DTOs, services)

├── LuxuryProperty.Domain → Core entities, interfaces, and shared models

└── LuxuryProperty.Infrastructure → Database context and repository implementations

### Layers explained:

- **Domain:**  
  Contains entities like `Property`, `PropertyImage`, and interfaces (`IPropertyRepository`, `IPropertyService`).

- **Application:**  
  Holds use cases, filters, and business logic — e.g., `PropertyService`.

- **Infrastructure:**  
  Contains MongoDB integration, repository implementations, and seeders.

- **API:**  
  Handles HTTP endpoints, dependency injection, and request routing.

---

## ⚙️ Prerequisites

Make sure you have installed:

| Tool                                                                      | Minimum Version | Description                           |
| ------------------------------------------------------------------------- | --------------- | ------------------------------------- |
| [.NET SDK](https://dotnet.microsoft.com/download)                         | 9.0             | Required to build and run the project |
| [MongoDB](https://www.mongodb.com/try/download/community)                 | 6.0+            | NoSQL database                        |
| [Docker](https://www.docker.com/) _(optional)_                            | Latest          | Run MongoDB easily                    |
| [Insomnia](https://insomnia.rest/) or [Postman](https://www.postman.com/) | Any             | API testing tool                      |

---

## 🚀 Getting Started

### 1️⃣ Clone the repository

```bash
git clone git@github.com:jhon-millionluxury/backend-test.git
cd backend-test
```

### 2️⃣ Configure environment variables

The application uses a `.env` file to configure MongoDB connection parameters.  
An example file is already included in the repository as `.env.example`.

Create a new file at `src/LuxuryProperty.API` folder called `.env` and copy the contents of `.env.example` into it.

### Example

```bash
MONGO_URI=mongodb://localhost:27017
MONGO_DBNAME=luxurypropertydb
```

### 3️⃣ Database seeding configuration

This project includes a default data seeders that inserts demo data into the database.

The seeding logic is located in:

```
src/LuxuryProperty.Infrastructure/Data/PropertySeeder.cs
src/LuxuryProperty.Infrastructure/Data/OwnerSeeder.cs
src/LuxuryProperty.Infrastructure/Data/PropertyImagesSeeder.cs
src/LuxuryProperty.Infrastructure/Data/PropertyTraceSeeder.cs
```

### 🚫 Disable seeding

If you don’t want to run the seeders, simply comment out the seeding section in `Program.cs`:

```csharp
// using (var scope = app.Services.CreateScope())
// {
//   var dbContext = scope.ServiceProvider.GetRequiredService<MongoDbContext>();

//   var ownersSeeder = new OwnerSeeder(dbContext);
//   await ownersSeeder.SeedAsync();

//   var propertiesSeeder = new PropertySeeder(dbContext, scope.ServiceProvider.GetRequiredService<ILogger<PropertySeeder>>());
//   await propertiesSeeder.SeedAsync();

//   var propertyImagesSeeder = new PropertyImagesSeeder(dbContext, scope.ServiceProvider.GetRequiredService<ILogger<PropertyImagesSeeder>>());
//   await propertyImagesSeeder.SeedAsync();

//   var propertyTraceSeeder = new PropertyTraceSeeder(dbContext, scope.ServiceProvider.GetRequiredService<ILogger<PropertyTraceSeeder>>());
//   await propertyTraceSeeder.SeedAsync();
// }
```

##### 💡 Tip: You can re-enable seeding anytime to repopulate your MongoDB with default data.

### 4️⃣ (Optional) Run MongoDB using Docker

If you don’t have MongoDB installed locally, you can run it with Docker:

```bash
docker run -d --name mongodb -p 27017:27017 mongo:latest
```

Access it at:
👉 http://localhost:8081

### 5️⃣ Run the API

Once the .env file is set up and MongoDB is running, start the API:

```bash
cd src/LuxuryProperty.API
dotnet run
```

or run using `F5` in Visual Studio.

Check the console output for the URL where the API is running. For example:

👉 http://localhost:PORT

Replace `PORT` with the actual port number.

### 6️⃣ Verify with Swagger

After the app starts, open Swagger UI in your browser:

👉 http://localhost:PORT/swagger

Replace `PORT` with the actual port number.

From here you can:

- Browse and test all available endpoints
- Check model schemas
- Send requests directly to the API

### 7️⃣ Test with Insomnia or Postman

##### Example request

```bash
GET http://localhost:PORT/api/Property
```

Replace `PORT` with the actual port number.

With filters:

```bash
GET http://localhost:PORT/api/Property?name=Luxury&page=1&pageSize=10
```

Replace `PORT` with the actual port number.

##### Example response

```json
{
  "items": [
    {
      "idProperty": "1",
      "name": "Luxury Villa",
      "address": "Miami",
      "price": 1000000,
      "images": [{ "file": "villa1.jpg", "enabled": true }]
    }
  ],
  "totalCount": 25,
  "totalPages": 3,
  "currentPage": 1
}
```

---

## 🧪 Testing

The project includes unit tests for the `PropertyService` class.

To run the tests, open the `tests/LuxuryProperty.Tests` project in Visual Studio and run the tests.

```bash
cd tests/LuxuryProperty.Tests
dotnet test
```

---

## 🧱 Technologies Used

- .NET 9 — Core framework
- MongoDB — NoSQL database
- Clean Architecture — Scalable design pattern
- Dependency Injection — Built-in DI container
- Swagger / OpenAPI — Documentation & testing
- Moq + NUnit — Unit testing
- Docker — Optional containerized environment

---

## 📝 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
