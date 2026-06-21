Project Coding Guidelines & Architectural Standards
Logging Policy:

Use global Middleware for HTTP request/response logging.

Use Services for business logic events and errors only.

Keep Repositories clean of logging; rely on global Exception handling for infrastructure errors.

Use structured logging with placeholders for all logs.

Architectural Standards:

Maintain "Clean Architecture" principles: Decouple business logic (Services) from data access (Repositories) and presentation (Controllers).

Dependency Injection: Always inject dependencies through constructors. Use interfaces for all injected services.

Code Style: Follow standard C# naming conventions (PascalCase for methods/classes, camelCase for local variables).

Documentation: Keep comments minimal; write self-documenting code instead. Use XML documentation (///) only for public API methods.

Error Handling: Do not swallow exceptions. Let them bubble up to the Middleware unless specific recovery logic is required.