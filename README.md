# ProductApi - Product Management System

## 📋 Description

REST API developed in .NET 9.0 for product management, implementing modern architecture patterns and SOLID principles.

## 🏗️ Architecture and Patterns Used

### **Architecture Patterns**
- **Clean Architecture**: Layered separation (Domain, Application, Infrastructure, API)
- **CQRS (Command Query Responsibility Segregation)**: Separation of read and write operations
- **Mediator Pattern**: Implemented with MediatR to decouple components
- **Repository Pattern**: Data layer abstraction
- **Unit of Work**: Transaction and data context management

### **SOLID Principles Applied**
- **Single Responsibility**: Each class has a specific responsibility
- **Open/Closed**: Extensible without modifying existing code
- **Liskov Substitution**: Well-defined interfaces
- **Interface Segregation**: Specific interfaces per functionality
- **Dependency Inversion**: Dependencies towards abstractions

### **Technologies and Libraries**
- **.NET 9.0**: Latest framework
- **Entity Framework Core**: ORM for persistence
- **MediatR**: Mediator pattern implementation
- **FluentValidation**: Input validation
- **Serilog**: Structured logging
- **Swagger/OpenAPI**: API documentation
- **AutoMapper**: Object mapping

## 🚀 Features

### **CRUD Operations**
- ✅ **POST** `/api/products` - Create product
- ✅ **PUT** `/api/products/{id}` - Update product
- ✅ **GET** `/api/products/{id}` - Get product by ID

### **Special Features**
- 📊 **Performance Logging**: Response time logging to file
- 💾 **Status Cache**: Status dictionary with 5-minute TTL
- 🎯 **Discount Service**: Integration with external service
- ✅ **Validations**: Robust validations with FluentValidation

## 📁 Project Structure

```
ProductApi/
├── ProductApi/                    # Presentation Layer (API)
│   ├── Controllers/              # REST Controllers
│   ├── Program.cs               # App configuration
│   └── ProductApi.csproj        # Main project
├── ProductApi.Application/        # Application Layer
│   ├── Interfaces/              # Service contracts
│   ├── Products/
│   │   ├── Commands/           # CQRS Commands
│   │   ├── Queries/            # CQRS Queries
│   │   └── DTOs/               # Data Transfer Objects
│   └── ProductApi.Application.csproj
├── ProductApi.Domain/            # Domain Layer
│   ├── Entities/               # Domain entities
│   └── Constants/              # Domain constants
└── ProductApi.Infrastructure/    # Infrastructure Layer
    ├── Persistence/            # EF Core configuration
    ├── Repositories/           # Repository implementations
    ├── Caching/               # Cache services
    └── ExternalServices/      # External services
```

## 🛠️ Installation and Setup

### **Prerequisites**
- .NET 9.0 SDK
- Visual Studio 2022 or VS Code
- Git

### **Steps to Run the Project**

1. **Clone the repository**
   ```bash
   git clone https://github.com/your-user/tekton-test.git
   cd tekton-test
   ```

2. **Restore dependencies**
   ```bash
   dotnet restore
   ```

3. **Run the application**
   ```bash
   cd ProductApi
   dotnet run
   ```

4. **Access the documentation**
   - Swagger UI: `https://localhost:7001/swagger`
   - API Base URL: `https://localhost:7001/api`

### **Database Configuration**
- **Type**: In-memory database (Entity Framework InMemory)
- **Configuration**: Automatic on app start
- **Persistence**: Data is kept during the session

## 📊 Data Model

### **Product Entity**
```csharp
public class Product
{
    public int ProductId { get; set; }
    public string Name { get; set; }
    public int Status { get; set; }        // 0: Inactive, 1: Active
    public int Stock { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
}
```

### **Response DTO**
```csharp
public class ProductDto
{
    public int ProductId { get; set; }
    public string Name { get; set; }
    public string StatusName { get; set; }  // "Active" or "Inactive"
    public int Stock { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public decimal Discount { get; set; }   // Discount percentage
    public decimal FinalPrice { get; set; } // Calculated final price
}
```

## 🔧 API Endpoints

### **Create Product**
```http
POST /api/products
Content-Type: application/json

{
  "name": "Sample Product",
  "status": 1,
  "stock": 100,
  "description": "Product description",
  "price": 99.99
}
```

### **Get Product**
```http
GET /api/products/{id}
```

### **Update Product**
```http
PUT /api/products/{id}
Content-Type: application/json

{
  "productId": 1,
  "name": "Updated Product",
  "status": 1,
  "stock": 50,
  "description": "New description",
  "price": 89.99
}
```

## 📝 Logging

- **File**: `Logs/performance.txt`
- **Format**: `[Timestamp] HTTP_METHOD /path responded STATUS_CODE in TIME ms`
- **Example**: `[2024-01-15 10:30:45] GET /api/products/123 responded 200 in 150 ms`

## 🧪 Testing

### **Run Unit Tests**
```bash
dotnet test
```

### **Test Coverage**
- Input validations
- Business logic
- Integration with external services

## 🔒 Validations

### **Validation Rules**
- **Name**: Required, max 100 characters
- **Status**: Must be 0 or 1
- **Stock**: Cannot be negative
- **Description**: Required, max 300 characters
- **Price**: Must be greater than 0

### **HTTP Response Codes**
- **200 OK**: Successful operation
- **201 Created**: Product created successfully
- **204 No Content**: Product updated successfully
- **400 Bad Request**: Invalid input data
- **404 Not Found**: Product not found
- **500 Internal Server Error**: Internal server error

## 🚀 Deployment

### **Docker**
```bash
docker build -t productapi .
docker run -p 8080:80 productapi
```

## 🤝 Contribution

1. Fork the project
2. Create a branch for your feature (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## 📄 License

This project is under the MIT License - see the [LICENSE](LICENSE) file for details.

## 📞 Contact

- **Developer**: Noli Quevedo
- **Email**: arnoldqd@gmail.com
- **LinkedIn**: https://www.linkedin.com/in/arnoldqd/

---

**Note**: This project is designed as a demonstration of clean architecture and modern design patterns in .NET.