# OrderManagement
Solution test task by Andrii Kostiuchenko

## Overview

This project implements web services for creating and managing product purchases, development on the .NET/C# platform. In the process of creating, 
reading and receiving information about products and orders, a simple interface for interacting with the API is also included.

## Architecture

The system follows **Clean Architecture**, which emphasizes separation of concerns into distinct layers. This approach is designed to ensure scalability, maintainability, and testability by isolating business logic from external frameworks and dependencies. The core of the system remains independent of implementation details, enabling flexibility and easy adaptation to future changes.

### Core Components:

- **Presentation**: Exposes the API endpoints and acts as the interface between the system and external consumers.
- **Application Layer**: Handles commands and queries, interacting with domain logic.
- **Domain Layer**: Contains business rules and logic, including entities like Order and Product.
- **Infrastructure Layer**: Manages external integrations, such as DataBase


The solution will be implemented using .NET 8 with the following structure:
```plaintext
OrderManagement.Solution/
├── src/
│   ├── OrderManagement.API            # Web API project
│   ├── OrderManagement.Application    # Application logic, DTOs, interfaces
│   ├── OrderManagement.Domain         # Domain entities and logic
│   ├── OrderManagement.Infrastructure # External services, database, etc.
│   └── OrderManagement.MVC            # Front-end technology
└── tests/
    └── OrderManagement.UnitTests
```

## Features

### 1. **Order Management**

* **Create Orders**: Supports creating new orders with multiple products in a single request.

* **Retrieve Orders**: Get a list of all orders or fetch a specific order by its ID.

* **Order Details**: Each order includes product details such as ID and name.

### 2. **Product Management**

* **Create Products**: Add new products.

* **Retrieve Products**: Fetch all products, get a product by ID, or search by product code.

* **Update Products**: Modify product details by ID.

### 3. **API Documentation & Validation**

* **Swagger Integration**: API is documented using Swagger for easy testing and interaction.

* **Validation & Error Handling**: Includes proper validation rules and meaningful error messages.

### 4. **Front-end Integration**

* **ASP.NET MVC-based UI**: A simple web interface for interacting with the API.

* **JavaScript-powered UI**: All business logic is handled using vanilla JavaScript.

* **Static File Management**: JavaScript files are stored in /wwwroot/js for easy organization.
  

## Setup 

### How to Run with Docker Compose

1. Clone the repository:
 
   ```bash
   git clone <repository-url>
   ```

2. Build and Run with Docker Compose
   ```bash
   docker-compose up 
   ```

## Ports Information

The API is running on port 5000.
The MVC application is accessible on port 8080.
