EAuction Web APP <br />
Include : <br />
  <br />
Net 6.0 <br />
EF Core <br />
MongoDb  <br />
RabbitMQ <br />
Redis <br />
Ocelot <br />
Docker <br />
Microservices <br />
DDD <br />
MediatR/CQRS <br />
Repository <br />
Generic Repository  <br />
Load balancers <br />
Ip Rate Limiting <br />
Caching <br />
Pipeline Behaviours(Cross Cutting Concerns) <br />
Exception Handlers <br />
Swagger <br />

Implemented Micro Services architecture. Api Gateway and Web Api. <br />
Created all Docker Files and managed by Docker Compose. <br />
 <br />
• Api Gateway <br />
Implemented ocelot for routing <br />
Implemented Ip Rate Limiting <br />
Implemented Redis for caching <br />
 <br />
• Order Api <br />
DDD ,MediatR/CQRS ,Generic Repository pattern. <br />
Implemented Cross Cutting Concerns <br />
	- PerformanceBehaviour <br />
	- UnhandledExceptionBehaviour <br />
	- ValidationBehaviour <br />
Implemented AutoMapper. <br />
Implemented EF Core and seed initial data,also implemented Migration Manager. <br />
Implemented Redis for caching. <br />
Implemented RabbitMQ as Consumer. <br />
Configured Load Balancing. <br />

• Product Api <br />
Repository pattern. <br />
Exception handler. <br />
Implemented MongoDB and seed initial data. <br />
Implemented Redis for caching. <br />
 <br />
• Sourcing Api <br />
Repository pattern. <br />
Exception handler. <br />
Implemented MongoDB and seed initial data. <br />
Implemented AutoMapper. <br />
Implemented Redis for caching. <br />
Implemented RabbitMQ as Producer. <br />
