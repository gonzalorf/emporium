# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

Emporium is a .NET 9 e-commerce backend application built using Clean Architecture with Domain-Driven Design (DDD) principles. It uses Azure Cosmos DB for data persistence and implements CQRS patterns with MediatR.

## Architecture

The solution follows Clean Architecture with these layers:

- **Emporium.API**: Web API layer using Carter for minimal APIs, JWT authentication
- **Emporium.Application**: Application layer with CQRS commands/queries, validation, and DTOs
- **Emporium.Domain**: Domain layer with entities, value objects, domain events, and business rules
- **Emporium.Infrastructure**: Infrastructure layer with Cosmos DB repositories and services

### Key Patterns

- **Domain-Driven Design**: Rich domain models with business logic encapsulated in entities
- **CQRS**: Commands and queries separated using MediatR mediator pattern
- **Unit of Work**: Transactional batch operations for Cosmos DB using custom UnitOfWork implementation
- **Domain Events**: Event-driven architecture with domain events raised by aggregates
- **Repository Pattern**: Abstracted data access through repository interfaces

## Common Development Commands

### Build and Run
```bash
# Build entire solution
dotnet build src/Emporium.sln

# Run the API
dotnet run --project src/Backend/Emporium.API

# Run in watch mode for development
dotnet watch --project src/Backend/Emporium.API
```

### Testing
Check for test projects - none currently exist in the solution structure.

## Key Technologies

- **.NET 9**: Target framework
- **Carter**: Minimal API framework for endpoint definitions
- **MediatR**: Command/Query mediator pattern
- **Mapster**: Object mapping
- **FluentValidation**: Input validation
- **Azure Cosmos DB**: NoSQL database
- **JWT Bearer**: Authentication
- **Swagger/OpenAPI**: API documentation

## Domain Model

### Core Entities
- **Product**: Main product aggregate with variants, pricing, and provider relationships
- **Order**: Customer orders with order items
- **Provider**: Product suppliers
- **Variant**: Product variant definitions (size, color, etc.)
- **Stock**: Inventory management

### Value Objects
All entities use strongly-typed IDs inheriting from `TypedIdValueBase` (e.g., `ProductId`, `OrderId`).

### Base Classes
- `Entity<TIdType>`: Base entity with domain events, soft delete, versioning
- `AuditableEntity<TIdType>`: Adds created/updated/deleted audit fields
- `DomainEventBase`: Base for all domain events

## Cosmos DB Configuration

The application uses a single Cosmos DB container with partition key `/id`. Container settings are configured in:
- Connection details: `appsettings.json` under `Cosmos` section
- Container setup: `Emporium.Infrastructure.DependencyInjection.AddInfrastructure()`

### Unit of Work
Cosmos DB operations are batched using `UnitOfWork` class that ensures:
- All operations in same partition key for transactional consistency
- Optimistic concurrency using ETags
- Domain event handling (currently commented out)

## Development Notes

- **Global Usings**: Both Domain and Application projects use global usings
- **Nullable Reference Types**: Enabled across all projects
- **ImplicitUsings**: Enabled for cleaner code
- **Authentication**: JWT bearer tokens configured in Program.cs
- **API Documentation**: Swagger enabled in development environment

## Configuration

Key configuration sections in `appsettings.json`:
- `Cosmos`: Database connection and container settings
- `Jwt`: JWT authentication settings (Key, Issuer, Audience)

## Endpoint Definition

APIs are defined using Carter modules in the `Endpoints` folder, following minimal API patterns.