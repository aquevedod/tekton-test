# ProductApi - Sistema de Gestión de Productos

## 📋 Descripción

API REST desarrollada en .NET 9.0 para la gestión de productos, implementando patrones de arquitectura modernos y principios SOLID.

## 🏗️ Arquitectura y Patrones Utilizados

### **Patrones de Arquitectura**
- **Clean Architecture**: Separación en capas (Domain, Application, Infrastructure, API)
- **CQRS (Command Query Responsibility Segregation)**: Separación de operaciones de lectura y escritura
- **Mediator Pattern**: Implementado con MediatR para desacoplar componentes
- **Repository Pattern**: Abstracción de la capa de datos
- **Unit of Work**: Gestión de transacciones y contexto de datos

### **Principios SOLID Aplicados**
- **Single Responsibility**: Cada clase tiene una responsabilidad específica
- **Open/Closed**: Extensible sin modificar código existente
- **Liskov Substitution**: Interfaces bien definidas
- **Interface Segregation**: Interfaces específicas por funcionalidad
- **Dependency Inversion**: Dependencias hacia abstracciones

### **Tecnologías y Librerías**
- **.NET 9.0**: Framework más reciente
- **Entity Framework Core**: ORM para persistencia
- **MediatR**: Implementación del patrón Mediator
- **FluentValidation**: Validaciones de entrada
- **Serilog**: Logging estructurado
- **Swagger/OpenAPI**: Documentación de la API
- **AutoMapper**: Mapeo entre objetos

## 🚀 Funcionalidades

### **Operaciones CRUD**
- ✅ **POST** `/api/products` - Crear producto
- ✅ **PUT** `/api/products/{id}` - Actualizar producto
- ✅ **GET** `/api/products/{id}` - Obtener producto por ID

### **Características Especiales**
- 📊 **Logging de Performance**: Registro de tiempo de respuesta en archivo
- 💾 **Caché de Estados**: Diccionario de estados con TTL de 5 minutos
- 🎯 **Servicio de Descuentos**: Integración con servicio externo
- ✅ **Validaciones**: Validaciones robustas con FluentValidation

## 📁 Estructura del Proyecto

```
ProductApi/
├── ProductApi/                    # Capa de Presentación (API)
│   ├── Controllers/              # Controladores REST
│   ├── Program.cs               # Configuración de la aplicación
│   └── ProductApi.csproj        # Proyecto principal
├── ProductApi.Application/        # Capa de Aplicación
│   ├── Interfaces/              # Contratos de servicios
│   ├── Products/
│   │   ├── Commands/           # Comandos CQRS
│   │   ├── Queries/            # Consultas CQRS
│   │   └── DTOs/               # Objetos de transferencia
│   └── ProductApi.Application.csproj
├── ProductApi.Domain/            # Capa de Dominio
│   ├── Entities/               # Entidades de dominio
│   └── Constants/              # Constantes del dominio
└── ProductApi.Infrastructure/    # Capa de Infraestructura
    ├── Persistence/            # Configuración de EF Core
    ├── Repositories/           # Implementaciones de repositorios
    ├── Caching/               # Servicios de caché
    └── ExternalServices/      # Servicios externos
```

## 🛠️ Instalación y Configuración

### **Prerrequisitos**
- .NET 9.0 SDK
- Visual Studio 2022 o VS Code
- Git

### **Pasos para Levantar el Proyecto**

1. **Clonar el repositorio**
   ```bash
   git clone https://github.com/tu-usuario/tekton-test.git
   cd tekton-test
   ```

2. **Restaurar dependencias**
   ```bash
   dotnet restore
   ```

3. **Ejecutar la aplicación**
   ```bash
   cd ProductApi
   dotnet run
   ```

4. **Acceder a la documentación**
   - Swagger UI: `https://localhost:7001/swagger`
   - API Base URL: `https://localhost:7001/api`

### **Configuración de Base de Datos**
- **Tipo**: Base de datos en memoria (Entity Framework InMemory)
- **Configuración**: Automática al iniciar la aplicación
- **Persistencia**: Los datos se mantienen durante la sesión

## 📊 Modelo de Datos

### **Entidad Product**
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

### **DTO de Respuesta**
```csharp
public class ProductDto
{
    public int ProductId { get; set; }
    public string Name { get; set; }
    public string StatusName { get; set; }  // "Active" o "Inactive"
    public int Stock { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public decimal Discount { get; set; }   // Porcentaje de descuento
    public decimal FinalPrice { get; set; } // Precio final calculado
}
```

## 🔧 Endpoints de la API

### **Crear Producto**
```http
POST /api/products
Content-Type: application/json

{
  "name": "Producto Ejemplo",
  "status": 1,
  "stock": 100,
  "description": "Descripción del producto",
  "price": 99.99
}
```

### **Obtener Producto**
```http
GET /api/products/{id}
```

### **Actualizar Producto**
```http
PUT /api/products/{id}
Content-Type: application/json

{
  "productId": 1,
  "name": "Producto Actualizado",
  "status": 1,
  "stock": 50,
  "description": "Nueva descripción",
  "price": 89.99
}
```

## 📝 Logging

- **Archivo**: `Logs/requests.txt`
- **Formato**: `[Timestamp] HTTP_METHOD /path responded STATUS_CODE in TIME ms`
- **Ejemplo**: `[2024-01-15 10:30:45] GET /api/products/123 responded 200 in 150 ms`

## 🧪 Pruebas

### **Ejecutar Pruebas Unitarias**
```bash
dotnet test
```

### **Cobertura de Pruebas**
- Validaciones de entrada
- Lógica de negocio
- Integración con servicios externos

## 🔒 Validaciones

### **Reglas de Validación**
- **Name**: Obligatorio, máximo 100 caracteres
- **Status**: Debe ser 0 o 1
- **Stock**: No puede ser negativo
- **Description**: Obligatorio, máximo 500 caracteres
- **Price**: Debe ser mayor a 0

### **Códigos de Respuesta HTTP**
- **200 OK**: Operación exitosa
- **201 Created**: Producto creado exitosamente
- **204 No Content**: Producto actualizado exitosamente
- **400 Bad Request**: Datos de entrada inválidos
- **404 Not Found**: Producto no encontrado
- **500 Internal Server Error**: Error interno del servidor

## 🚀 Despliegue

### **Docker**
```bash
docker build -t productapi .
docker run -p 8080:80 productapi
```

### **Azure/AWS**
- Configurar variables de entorno
- Configurar base de datos persistente
- Configurar logging centralizado

## 🤝 Contribución

1. Fork el proyecto
2. Crear una rama para tu feature (`git checkout -b feature/AmazingFeature`)
3. Commit tus cambios (`git commit -m 'Add some AmazingFeature'`)
4. Push a la rama (`git push origin feature/AmazingFeature`)
5. Abrir un Pull Request

## 📄 Licencia

Este proyecto está bajo la Licencia MIT - ver el archivo [LICENSE](LICENSE) para detalles.

## 📞 Contacto

- **Desarrollador**: Noli Quevedo
- **Email**: arnoldqd@gmail.com
- **LinkedIn**: https://www.linkedin.com/in/arnoldqd/

---

**Nota**: Este proyecto está diseñado como una demostración de arquitectura limpia y patrones de diseño modernos en .NET.