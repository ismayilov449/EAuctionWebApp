EAuction Web APP
Include :
 
Net 6.0
EF Core
MongoDb 
RabbitMQ
Redis
Ocelot
Docker
Microservices
DDD
MediatR/CQRS
Repository
Generic Repository 
Load balancers
Ip Rate Limiting
Caching
Pipeline Behaviours(Cross Cutting Concerns)
Exception Handlers
Swagger

Implemented Micro Services architecture. Api Gateway and Web Api.
Created all Docker Files and managed by Docker Compose.

• Api Gateway
Implemented ocelot for routing
Implemented Ip Rate Limiting
Implemented Redis for caching

• Order Api
DDD ,MediatR/CQRS ,Generic Repository pattern.
Implemented Cross Cutting Concerns
	- PerformanceBehaviour
	- UnhandledExceptionBehaviour
	- ValidationBehaviour
Implemented AutoMapper.
Implemented EF Core and seed initial data,also implemented Migration Manager.
Implemented Redis for caching.
Implemented RabbitMQ as Consumer.
Configured Load Balancing.

• Product Api
Repository pattern.
Exception handler.
Implemented MongoDB and seed initial data.
Implemented Redis for caching.

• Sourcing Api
Repository pattern.
Exception handler.
Implemented MongoDB and seed initial data.
Implemented AutoMapper.
Implemented Redis for caching.
Implemented RabbitMQ as Producer.
