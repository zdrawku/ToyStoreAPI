# Toy Store API

This **Toy Store API** is a RESTful Web API built with **ASP.NET Core** to manage a collection of toys for a fictional toy store. The API allows users to view, search, and filter toys by various criteria like category, price range, name, and ID. The project demonstrates routing, service-based architecture, Swagger for API documentation, and simple data handling with JSON files.

## Features
- **Get Operations**: Fetch details about toys, filtered by category, price range, name, or ID.
- **Data Source**: Static data stored in JSON files to mimic real-world toy store data (50 toy records).
- **Service-Oriented Architecture**: Clean separation of concerns, with business logic encapsulated in a dedicated service class.
- **Swagger Integration**: Easy-to-use Swagger UI for API exploration and testing.
- **Category Enumeration**: Toys are grouped into 4 categories (0-12 months, 1-3 years, 3-5 years, 5+ years).
- **ASP.NET Core**: Built with ASP.NET Core Web API, following best practices for scalability and performance.

## Technologies Used
- **ASP.NET Core 8**
- **C#**
- **Swagger/OpenAPI for API Documentation**
- **Newtonsoft.Json** for JSON deserialization
- **Dependency Injection** for service management

## API Endpoints
- `/api/toys` - Get all toys
- `/api/toys/category/{category}` - Get toys by category
- `/api/toys/price?from={price}&to={price}` - Get toys in a specific price range
- `/api/toys/name/{name}` - Search toys by name
- `/api/toys/{id}` - Get toy details by ID

## Setup Instructions
1. Clone this repository:
   ```bash
   git clone https://github.com/your-username/toy-store-api.git
   ```
2. Navigate to the project folder:
   ```bash
   cd toy-store-api
   ```
3. Run the application:
   ```bash
   dotnet run
   ```
4. Open [https://localhost:5001/swagger](https://localhost:5001/swagger) to explore the API using Swagger UI.

## Contributions
Feel free to fork this project and submit pull requests. Contributions, issues, and feature requests are welcome!
