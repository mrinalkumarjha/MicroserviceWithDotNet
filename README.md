# MicroserviceWithDotNet

# How to run app in local pc.
	1: install docker for desktop in window.
	2: Run docker command from powershell: Right click on docker compose > open in terminal. -d is deattache mode , means background.
		docker-compose -f .\docker-compose.yml -f .\docker-compose.override.yml up -d

# Some important docker command:
	1: When there is change in docker file like you added a refence of one app run docker-compose with -- build:
			
			docker-compose -f docker-compose.yml -f docker-compose.override.yml up --build

	2: To stop  docker container: following command will stop container but images will be saved in hard disk.
			
			docker-compose -f .\docker-compose.yml -f .\docker-compose.override.yml down

	3: To check docker is running or not using power shell use command
	
			docker ps
	
	4: To list existing image : docker images

# API ENDPOINTS:

	Catalog API : http://localhost:8000/swagger/index.html
	
	Basket API	: http://localhost:8002/swagger/index.html
					It use redis db.
	
	Discount API: http://localhost:8003/swagger/index.html
	
	Discount GRPC: http://localhost:8004						: it use http2 protocol so it has not ui.
	
	Portainer:    http://localhost:9000/#!/init/admin
					id	 : admin
					pass : admin1234
	
	PgAdmin:	  http://localhost:5050/browser/
					servername : pgServer
					hostname:discountdb
					uid:admin
					pass:admin1234
					port : 5432
	pGAdmin UI cred: 		PGADMIN_DEFAULT_EMAIL=admin@test.com
					 - PGADMIN_DEFAULT_PASSWORD=admin1234

    RabbitMQ Dashboard: http://localhost:15672
					uid: guest
					pass: guest


# Debug app in docker container.
	Set docker compose as startup project. then run in vs. it will be debuged from container.


1: Setting up mongodb in docker
	For this your docker for desktop app should be running in machine.
	GOTO hub.docker.com in browser
	search for mongo image
	select image from list

	now we need to pull this image in local system  .. for this
	right click in solution and click on open in terminal .. this will bring powershell window.

	# To check docker is running or not using power shell use command docker ps

	copy command from docker hub (docker pull mongo) and run inside powershell

	once image is pulled run following command to run mongo.

#	docker run -d -p 27017:27017 --name shopping-mongo mongo
	To verify mongo has started or not run docker ps command.

# 	Catalog api architecture diagram.
![image](https://user-images.githubusercontent.com/3676282/135683643-994e1184-8352-4690-935c-613e43d86797.png)


# Install mogo db nuget packge(MongoDB.Driver version 2.13) into catalog api to connect to mongo db


# Catalog.API

	Data Layer : 

# Containerize catalog microservice with mongodb using docker compose.
	With docker compose you can make multiple container defination in single file and run all with the help of single
	command.
	Here we are going to add docker compose file for catalog microservice.
	Right click on catalog microservice project file > Add > Container Orchestor Support.
	From new window select Docker Compose and press OK.
	Select os as linux.
	Once you click ok. docker-compose in solution root
	and dockerfile inside Catalog.API will be created. and that will automatically set as startup project.

	The purpose of creating dockerfile is when we ask docker to extract image to our catalog microservice 
	it will search for file name DockerFile inside project and this will work our project as per setting in it.

	Basically dockerfile consist two part . first part is to build application and second part is to publishing and run app.

# Docker composr.yml
	it holds services with image and dockerfile path.

# Add mongo db  database  to docker compose file.

	add following in docker-compose.yml

			catalogdb:
				image:mongo


# Test catalog service in docker env:
	Right click on docker compose > open in terminal
	first stop existing mongo image by following command

	docker ps to list image
	it wil give container id

	then docker stop first 4 char of containerid  : it will stop existing image
	now remove that image
	docker rm 00b7

	docker images : to check if old image exists

#	Now we will run our docker yml file which contain our catalog service and mongo db image.

	run command in powershell:

	docker-compose -f .\docker-compose.yml -f .\docker-compose.override.yml up -d


# Run mogodb client for gui purpose
	docker run -d -p 3000:3000 mongoclient/mongoclient
	this will download mongo client image and run it

	check if image available using docker ps command

	once it is available run it in browser : localhost:3000



# Debugging docker compose in vs for catalog api.
	Once docker support is added now if we will biuld application from vs it will build docker container also.
	click on run Docker Compose

	if you get following error , stop all docker container using command 	"docker-compose -f docker-compose.yml -f docker-compose.override.yml down"

		Error	DT1001	Error response from daemon: Conflict. The container name "/catalogdb" is already in use by container.



# Some useful docker command

	
Docker Commands

Example docker hub pull :
	docker run -d --hostname swn-rabbit --name swn-rabbit -p 5672:5672 -p 15672:15672 rabbitmq:3-management

Single Container
For aspnetcore app after adding docker file -- for single container add docker file build and run = create new container
	$ docker build -t aspnetapp .
	$ docker run -d -p 8080:80 --name myapp aspnetapp

docker compose up
	This command run multiple container

Multi Container - docker-compose.yml
	docker-compose up
	docker-compose -f docker-compose.yaml -f docker-compose-infrastructure.yaml up --build
	
	When there is change in code file use docker compose with --build command .
	docker-compose -f docker-compose.yml -f docker-compose.override.yml up --build

	When there is change in docker compose file use following command.
	docker-compose -f .\docker-compose.yml -f .\docker-compose.override.yml up -d

	To stop  docker container: following command will stop container but images will be saved in hard disk.
	docker-compose -f .\docker-compose.yml -f .\docker-compose.override.yml down

2

DOCKER REMOVE ALL CONTAINER IMAGES PS -A

POWERSHELL commands

https://blog.baudson.de/blog/stop-and-remove-all-docker-containers-and-images

List all containers (only IDs)
docker ps -aq
Stop all running containers
docker stop $(docker ps -aq)
Remove all containers
docker rm $(docker ps -aq)
Remove all images
docker rmi $(docker images -q)
Remove all none images
docker system prune

-- You can also run all with copy paste

docker ps -aq
docker stop $(docker ps -aq)
docker rm $(docker ps -aq)
docker rmi $(docker images -q) -f
docker system prune


# Added BAsket.API service
		Architecture of basket api..
		
	![image](https://user-images.githubusercontent.com/75416010/136662323-ccddd8f2-36a0-4242-961f-a81f267e532d.png)


# Redis in dockr
	docker pull redis : this will pull image from server and install in local pc

	to run image 
		docker run -d -p 6379:6379 --name aspnetrun-redis redis

	To interact with docker redis in command:

		docker exec -it aspnetrun-redis /bin/bash

		after this command we will be in container and can hit redis command.

		some redis command is:
		redis-cli : this will take to redis cli
		ping - PONG

		set key value
		get key
		set name mrinal
		get name


# Dependencies of basket api:
	Microsoft.Extensions.caching.StackExchangeRedis : for communication of redis 
	install this in basket api using package manager :  Install-Package Microsoft.Extensions.caching.StackExchangeRedis

	Nutonsoft.json : for json parsing

	update existing package to new 
	Update-Package -ProjectName Basket.API

# why redis has been choosen for basket api.
	basket api will store and manage basket and basket item . so to cache basket we will use redis.
	redis is used as distributed caching.

# redis-insight : used this gui client for viewing redis db 

# Containerize Basket Microservices with Redis using Docker Compose

	we will use visual container orcastration support tool to containerize basket api.

	rightclick on basket api project> add > container orchestrator support

	select docker compose
	select linux

	this will create Dockerfile in project root and make change in existing docker-compose.

# redis:alpine : we have use this image for lightweight redis.

# Stop old redis image as we have added image in yml
	list image : docker ps
	stop image : docker stop ff23 (ff23 is id of container)

	remove from stopped list

	list stopped image: docker ps -a
	remove image : docker rm oo23



# run docker compose now

	docker-compose -f docker-compose.yml -f docker-compose.override.yml up -d

	when we make any changes in code use -- build in docker compose 
    docker-compose -f docker-compose.yml -f docker-compose.override.yml up -d  --build


# docker hub account
mrinalkumarjha
mrinal.1234
# docker playground on cloud: login with docker hub ac
https://labs.play-with-docker.com/ 
# docs: https://training.play-with-docker.com/ops-stage1/
	

# Container management with Portainer
	Portainer is container management tool. it is open source. it is useful to setup app, monitor app, deploy app.
	docs : https://docs.portainer.io/v/ce-2.9/start/intro

	add portainer to docker compose.
	  portainer:
    image: portainer/portainer-ce

	also add volume
	  portainer_data:


	add following in override file:

	  portainer:
    container_name: portainer
    restart: always
    ports:
      - "8080:8000"
      - "9000:9000"
    volumes:
      - /var/run/docker.sock:/var/run/docker.sock
      - portainer_data:/data



> Now run docker compose.. this will pull image and add container.

browse portainer : http://localhost:9000/#!/init/admin

id admin
pass: admin1234

logout and login again to see docker environment.



# Create discount service now with postgre db

Postgresql : postgre sql is open source and relational database.

install postgre in docker

search postgres in docker hub.

		POSTGRES_USER=admin
      - POSTGRES_PASSWORD=admin1234

# pgAdmin: https://www.pgadmin.org/

pgadmin is tool to manage posgresql. it has ui.

setup pgadmin4 image in docker 

		PGADMIN_DEFAULT_EMAIL=admin@test.com
      - PGADMIN_DEFAULT_PASSWORD=admin1234

run docker compose here again to setup pgadmin in docker.

# docker-compose -f docker-compose.yml -f docker-compose.override.yml up -d


# create pgadmimn server after login into http://localhost:5050/browser/
	
Add server from pg admin interface

servername : pgServer
hostname:discountdb
uid:admin
pass:admin1234
port : 5432

# Add table in pg admin
To add table goto pg interface>expand pgServer > schemas > tables> create 

or from query tools directly from tools > query tools

run following script to create coupan table :

CREATE TABLE Coupon(
		ID SERIAL PRIMARY KEY         NOT NULL,
		ProductName     VARCHAR(24) NOT NULL,
		Description     TEXT,
		Amount          INT
	);


Add seeding data to coupan table
INSERT INTO Coupon (ProductName, Description, Amount) VALUES ('IPhone X', 'IPhone Discount', 150);

INSERT INTO Coupon (ProductName, Description, Amount) VALUES ('Samsung 10', 'Samsung Discount', 100);

# package used in discount service:
NpgSql : install Npgsql version 5.X version
Dapper (orm) : install Dapper version 2.X.. This is developed by stackoverflow.


# Containerize discount service with postgre sql
Right click on discount project file > Add > Container orcastration support

select docker-compose
target os: linux

After this Docker file would be created in project dir. and docker compose will be apdated with discount services.

update docker compose and override file as per need.


# Adding migration for discount db.
after adding this coupan table will be created automatically with dapper when discount service will run.

# Adding GRPC for discount service>
gRPC usage of microservice communication : gRPC is developed by google and best for microservice communication.
	1: Synchronous backend microservice to microservice communication
	2:Polyglot environment
	3:Low latency and high throughput communication
	4:Point to point real time communication
	5: Network constraint environment

Steps to add grpc template:
Right click on discount folder > add new project > search for grpc template> add 

For grpc service we need Grpc.AspNetCore package and Protos folder for storing proto file. when you build grpc service
c# file for each proto file is created inside obj>Debug>net5.0>Protos

Add protocol buffer file inside protos folder for discount.
Right click on proto file > properties>
change  Build action to protobuff compiler
ANd grpc stub class to Server only.
	
# Add Dicount service class to implement grpc proto service method.
we can say service class in grpc project is similar to controller in api project.
create a discountService class and inherit from proto class created by visual studio.


Override methods from base class.
and add this service class into aspnet pipeline in startup class.
 ex: endpoints.MapGrpcService<DiscountService>(); 

# Implement automapper in grpc proj.

install nuget package : AutoMapper.Extensions.Microsoft.DependencyInjection
Create new folder Mapper and create mapping profile by inheriting Profile class from automapper library. and create mapping here.
inject IMapper inside DiscountService.

REgister Automapper in dependency injection container.
  services.AddAutoMapper(typeof(Startup));


# Consuming disoount grpc from basket api to check discount for product.
	Basket api is client of discount grpc service.

	To connect discount grpc service in basket api right click on Basket.API > Add > Connected Services
	inside grpc service section add reference to grpc service.

	Give the path of proto file inside file location (C:\Mrinal\Learning\MicroserviceWithDotNet\src\Services\Discount\Discount.Grpc\Protos\discount.proto)
	and select client as file to be generated. as we are consuming here grpc so we are client here so vs will automatically generate client file for us.

	Build the basket api. now you can see client generated c# file inside obj > Debug > Net5.0 > DiscountGrpc.cs.
	Inside this class we have all method available like getdiscount...

	Now to encapsulate grpc class create new class DiscountGrpcService.cs in basket.API.


# Containerize Discount Grpc Microservice with postgre sql with Docker compose
	Right click on discount Grpc -> Add -> Container Orchestrator Support
	Select Container Orchestrator as Docker-Compose
	select Os as Linux
	A docker file is created inside project Discount.Grpc
	and docker-compose.yml and override file will be updated also. we can update the settings there.

	The purpose of this docker file is when we ask docker to get images it looks for docker file inside project and get images from server and 
	create image locally.

# Add Discount grpc url config in basket api image config in docker compose override file..

	 basket.api:
    container_name: basket.api
    environment:
      - ASPNETCORE_ENVIRONMENT=Development
      - "CacheSettings.ConnectionString=basketdb:6379"
      - "GrpcSettings.DiscountUrl=discount.grpc"
    depends_on:
      - basketdb
    ports:
      - "8002:80"

	We did not added port for GrpcSettings.DiscountUrl..
	because In docker network its enough to say container name when it comes to communicate 2 containers. But for database ones, 
	it can be required additional port number explicitly.

# Build particular docker image
	
	docker-compose -f docker-compose.yml -f docker-compose.override.yml up   --build


# Building Ordering service with sql server and cqrs
	

  Dependency inversion principle:
	![image](https://user-images.githubusercontent.com/3676282/140612579-2164ef82-1832-490c-bd79-b401a28d7db8.png)

  CQRS:
	command query responsibility segregation principle. 
	![image](https://user-images.githubusercontent.com/3676282/140613739-e719723c-b806-41e3-a28c-5562531f0d22.png)

	This pattern says that we should seperate the interface of reading data and updating data. it use different database for reading and updating.

	Jason Taylor has github repository for CleanArchitecture.(https://github.com/jasontaylordev/CleanArchitecture)
	
	Clean architecture diagram 
	![image](https://user-images.githubusercontent.com/3676282/140641000-8dce57c6-34f6-41d6-bf34-6c3cb11abcb6.png)

	DOMAIN LAYER: Domain layer has no dependency
	APPLICATION LAYER: Application layer has reference of domain. It has all business logics.
		This layer is responsible for business use cases business validations.
		main folder of application layer is Contract, Feature, Behaviour
		Contract folder has all interfaces.
		Feature folder has cqrs implementation
		Behaviour folder has implementations

	INFRA LAYER: Infrastructure layer has reference of application. it has implementation 
	PRESENTATION LAYER : Ordering.API , Api layer is presentation layer it will have reference of both application and infracture layer as reference


	Application Layer: 
	


# CQRS IMPLEMENTATION WITH MEDIATOR DESIGN PATTERN
	We will use MediatR Nuget package to implement mediator pattern.
	![image](https://user-images.githubusercontent.com/3676282/140643284-9c38acfb-53f2-4a9f-9f39-72206830f7f0.png)

	Flow in project 
	
	![image](https://user-images.githubusercontent.com/3676282/140643349-c7d87e9f-aa3d-4203-a2b9-927e29e57a24.png)

	
	Install MediatR.Extensions.Microsoft.DependencyInjection inside Ordering.Application proj

	Lecture 95 is explanation of mediator pttern after implementation.

# Implementing custome exception inside Ordering.Application

	For this we have created new NotFoundException class and inherited from ApplicationException.

# Developing behaviour of ordering.
	We will use IPipelineBehaviour of Mediatr library to create preprocessor behaviour.

	request will be handled like this

	REQUEST  ==>  MediatR  ==>  Preprocessor Behaviour ==> Handle Request ==> Post processor behaviour

	MediatR library has IRequestHandler to handle request and IPipelineBehaviour interface to add preprocessing
	logic to pipeline.

	ValidationBehaviour.cs is implementation of IPipelineBehaviour

	Lecture 98 is explanation of mediator pttern after implementation.

# Creating extension method  for regestring services of Ordering.Application
	ApplicationServiceRegistration is extension class for registering services.

# Implementation presentation layer:

	since we are using mediator pattern and presentation layer will send request to mediater which is IMediatR. 
	Added OrderController with order endpoints in presentation layer.

# Develop Ordering.Infrastructure
  Here we will implement abstraction from application layer.
  install package Microsoft.EntityFrameworkCore.SqlServer (5.0.12)

  Created OrderContext and seed class. overrided SaveChangesAsync to set default value for createdby and createddate
  Created OrderRepository and RepositoryBase to implement abstraction.

  Implement email service inside infracture.
  To send email we will use SendGrid Library. and implemented in EmailService class.

# Registering ordering application and ordering infracture service inside ordering api.
	for this we have to register extension method ApplicationServiceRegistration and InfrastructureServiceRegistration
	inside ordering.Api startup.cs ConfigureServices

# Adding Migration for code first approach.
	1> set ordering.api as startup project.
	2> install some package in ordering.api for migration
		Microsoft.EntityFrameworkCore.Tools (5.0.12)  : this will help to generate codefirst migration folder.

	Now to generate migration inside infrastructure project open package Manager console. select default project 
	as Ordering.Infrastructure.

	run command : Add-Migration InitialCreate  OR Add-Migration InitialMigration -OutputDir "Persistense/Migrations"

# Applying migration automatically to sqlserver when api project runs.
	created extension of host HostExtensions.cs inside Odering.API to apply migration automatically when ordering.Api starts.

# Add Sql server image in docker compose
	check sqlserver image on https://hub.docker.com/
	or see link below for official image
	https://hub.docker.com/_/microsoft-mssql-server


	After adding image in docker compose and overriding enviroment variable in override file run docker compose in terminal.

	docker-compose -f docker-compose.yml -f docker-compose.override.yml up -d


# Containerize Ordering api Microservice with sql server using Docker compose
	Right click on Ordering.api -> Add -> Container Orchestrator Support
	Select Container Orchestrator as Docker-Compose
	select Os as Linux
	A docker file is created inside project Ordering.api
	and docker-compose.yml and override file will be updated also. we can update the settings there.

	The purpose of this docker file is when we ask docker to get images it looks for docker file inside project and get images from server and 
	create image locally.

	Use docker up

	docker-compose -f docker-compose.yml -f docker-compose.override.yml up -d

# Async microservice communication with RabbitMQ and MassTransit for cjeckout order.
	
	Diagram of checkout order communication
	![image](https://user-images.githubusercontent.com/3676282/141788130-c7e4da77-af9d-4e61-822f-a0d13f088bd5.png)


	Microservice Communication types:

	Three types of microservice communication happens
	1: Request Driven architecture:  In this approach service communicate between http or grpc. in our proj basket api communicate with discount grpc.
										We can easily determine sequence of actions. if one of the dependent service is down call cannot proceed.
	2: Event driven architecture: In this architecture services dont call each other. instead they create event and consume events from 
									Message broaker system in async way.

									Producer dosent know about consumer also consumer dont know about producer services. As a result services can deployed
									and maintained seperately.

	3: Hybrid Architecture: Depending on scenario we can use custom
	
	![image](https://user-images.githubusercontent.com/3676282/141792263-71a15b75-03b1-4612-b8be-69c8fbf6ff4a.png)

# What is RabbitMq ?
	Rabbit Mq is message quing system.	some similar service is Apache Kafka, Msmq, Microsoft Azure service bus, Kestral, ActiveMq.
	All transaction is kept in queue until consumer consume it. Following image show how transaction happens in rabbit mq.
	RabbitMQ is an open source multi-protocol messaging broker.

	![image](https://user-images.githubusercontent.com/3676282/141793643-b37c554a-71aa-4531-8a19-df65693651f2.png)
	We will use rabbitMq and masstransit to do Async communication between basket and ordering microservice.

# Main component of RabbitMq
	1: Producer: It is source of creating message. Message is data that we send in queue.
	2: Queue:  Queue is where the message is stored. all incoming message are stored in queue that is memory.
	3: Consumer: consumer is server that needs sent mesages. It is application that will receive messages and process it from queue.

	4: Exchange: Before sending message we have Exchange. Exchange decide which queue to send message. it makes decision based on routing case.

	5: Binding : Binding is link between exchnage and queues.

	In queue we have FIFO which is first in and first out. Order of processing message in queue is first in first out. following are image of rabbitMq component.

	![image](https://user-images.githubusercontent.com/3676282/141804273-6b1652c2-c873-4774-8d47-92bc5a2e99b0.png)
	

# RabbitMq Queue properties:
	1: Name: Name of queue what we have defined.
	2: Durable: This will determine the lifetime of the queue.
	3: Exclusive: This contains info whether queue will be used in some other connection or not.
	4: AutoDelete: autodel info.
	
	![image](https://user-images.githubusercontent.com/3676282/141804543-c8624381-3d84-4301-a3f4-b991b61d0160.png)

# RabbitMq Exchange Types:
	1: Direct Exchanges: One queue directly sent to one consumer.
	2: Topic Exchanges: One queue is being send to different consumer based on subject. Its a variation of publish subscribe pattern.
						If we have several consumer. Topic exchange is used to determine what kind of message they want to receive.
						we will use this in our project.

	3: Fanout Exchanges: If one queue is needed to broadcasted to multiple consumer. useful for gaming.
	4: Header Exchanges:

	Exchnage type image.
	![image](https://user-images.githubusercontent.com/3676282/141808623-a31da237-8d7a-413e-9291-65e76d8e3937.png)
	
	Topic and Fanout exchange type image.
	![image](https://user-images.githubusercontent.com/3676282/141808928-8f76757f-8ca8-4686-9d84-8be73293ded1.png)


# Adding RabbitMq into docker compose for multi container docker env:
	look into https://hub.docker.com/_/rabbitmq
	4Gb config is required
	this port "5672:5672" is for rabbitmq
	this port "15672:15672" is for rabbit mq dashboard


	inside docker compose add rabbitmq image:
	  rabbitmq:
    image: rabbitmq:3-management-alpine

	
	inside docker compose override add rabbitmq config:
	  rabbitmq:
    container_name: rabbitmq
    restart: always
    ports:
        - "5672:5672"
        - "15672:15672"


	Run docker compose to add image in docker.
	
	docker-compose -f docker-compose.yml -f docker-compose.override.yml up -d

	Once image is updated in docker we can browser rabbitmq dashboard in localhost using url

	localhost:15672
	uid: guest
	pass: guest
	guest is default id and password.


# Analysis of rabbitMq implementation.
	When basket checkout operation performed, we are going to create basket checkout event and consume this event from ordering service with 
	using rabbitMQ and messtransit.


	Masstransit is opensource message bus for dotnet ecosystem. Masstransit is useful for routing message over msmq, rabbitMq and so on.
	We will create a class lib for building event.
	
	
	![image](https://user-images.githubusercontent.com/3676282/141998882-a2f709db-006f-411f-ac5e-6812ca45ae8d.png)


	Following is image of transaction from basket to order

	![image](https://user-images.githubusercontent.com/3676282/141998755-b3361a92-bb2b-4997-b566-f971e8fc4fba.png)


# RabbitMq nuget packages
	MassTransit
	Masstransit.RabbitMQ
	Masstransit.AspNetCore  : to inject asp.net built in dependency injection

# Develop EventBus.Messages BuildingBlock class library
	since this is not microservice we will create new folder in solution BuildingBlocks

# Produce RabbitMqEvent
	Add EventBus.Messages class library into Basket service

	When we add any reference we should also modify docker file.

	install following in basket service
	MassTransit
	Masstransit.RabbitMQ
	Masstransit.AspNetCore

	Register it in startup.cs it create new bus for basket

	   // MassTransit-RabbitMQ Configuration
            services.AddMassTransit(config => {
                config.UsingRabbitMq((ctx, cfg) => {
                    cfg.Host(Configuration["EventBusSettings:HostAddress"]);
                });
            });

            services.AddMassTransitHostedService();

# Add automapper in basket api

	install nuget package : AutoMapper.Extensions.Microsoft.DependencyInjection
	this automatically install autommaper and extension method to use in di container.


	1: create BasketProfile and inherit from Profile(this is from automapper).
	2: map source destination class in constructor.
	3: Add automapper in di container inside startup class.
	4: inject Imapper in controller and map object.


# Building api gateway with ocelot an apply api routing.
	
	## Gateway routing pattern : main objective of this pattern is 
	1: Routing: Route request to multiple services using single endpoint.
	2: Data Aggregator:
	3: Protocol Abstractration
	4:Centralized Error management.
	
	![image](https://user-images.githubusercontent.com/75416010/147410361-2e50677d-abc0-4e9a-b976-33489d254611.png)
	
	## Api Gateway pattern: This is similar to facad pattern from object oriented world. it gives single entry point for multiple service.
	Api gateway sits between client application and services. it acts as a reverse proxy. authentication , authorization can also be applied here.
	We can create(segregate) multiple gateway as per client type. like for web create one gateway and one for mobile request.
	
	It is also known as BFF(Backend for frontend pattern)
	
	![image](https://user-images.githubusercontent.com/75416010/147410459-08963505-8a18-4df6-9e35-70f4ac212c89.png)
	
	Diagram of multiple gateway.
	
	![image](https://user-images.githubusercontent.com/75416010/147410648-1cd889f6-2c36-497f-b2eb-9f5a9b709a74.png)
	
	Main Features of api gateway pattern:
	
	![image](https://user-images.githubusercontent.com/75416010/147410725-56e2dd63-b7c9-4307-a84a-31afc3cb7452.png)
	
# Ocelot api gateway: Ocelot is lightweight gateway and simple to  use. it is opensource dotnet core based api gateway. As it is based on dotnet core, it is cross plateform,
	and it allow you to deploy on window , linux. ocelot works with dot net core only.
	
	
	
	![image](https://user-images.githubusercontent.com/75416010/147410794-c4fcc81a-7c77-4ea2-8aad-42c5c3c5baaa.png)

	# Authentication and authorization in ocelop api gateway
	
	![image](https://user-images.githubusercontent.com/75416010/147415393-1966ddd7-1e17-4770-8631-65105d7dec43.png)

	![image](https://user-images.githubusercontent.com/75416010/147415406-34c323b1-75d3-49d3-be6a-ffe2fb320e09.png)

	# Design of Api gateway microservice
	![image](https://user-images.githubusercontent.com/75416010/147415456-7eded822-932f-44be-9d55-57789c9e45cb.png)

	![image](https://user-images.githubusercontent.com/75416010/147415472-a957d768-f013-4baa-b88c-16193a2f533e.png)



# Create ocelot gateway
	Created new empty core project inside ApiGateways folder.

	Extended logging functionality inside program.cs
	Added Ocelot nuget package from nuget.
	Configure Ocelot in startup class.

	 Add ocelot.json to perforn routing.
	 We will add three json file. one for local, one for development. and configured ocelot.json to be used in app based on env inside program file.
	 When we create core project default environment is Development. we change change this from debug section.

	 ocelot.Development.json is used in docker environment.

	 Added routes definition inside ocelot.local.json file. 

	 Documentation url of ocelaot : https://ocelot.readthedocs.io/en/latest/introduction/gettingstarted.html

	 Downstream for internal microservices.
	 upstream for exposing api

	 GlobalConfig for base url of exposing url.


# Rate limiting in ocelot: 
	Ocelot support rate limiting of upstream request so that your downstream service donot overloaded. We need to add "RateLimitOptions" to json file.  we can also add this configuration 
	in global part of ocelot.json

	Following is code which needs to be added on ocelot.json file
	 
	"RateLimitOptions": {
        "ClientWhiteList": [],
        "EnableRateLimiting": true,
        "Period": "5s",
        "PeriodTimespan": 1,
        "Limit":  1
      }


	  After this configuration ocelot will throw http status code 429 is for too many request if rate limit exceeded by 1 in 5 second.


# Response caching in ocelot api gateway.
	For this we need to install cacheManager from nuget.  Ocelot.cache.CacheManager

	Now activate cache manager in startup class.

	Now can configure in json file.
	"FileCacheOptions": { "TtlSeconds": 30 },

# Develop shopping aggregator micro service with applying gateway aggregation pattern:
	Shopping aggregator microservice aggregate multiple client request using httpclient factory. client app send single req to api gateway.
	then send to multiple internal services then aggregate request and then send back to client app.

# Gateway aggregation pattern :
	. single request
	. multiple call to different backend system
	. Dispatche request to various backend system
	. Reduce chattiness between client and service

	using a gateway to aggregate  multiple individual request into a single request.

	This pattern is useful when client must make multiple calls to different backend system to perform an operation.

	Why we need this pattern. 

	If we dont apply this pattern client have to call directly shopping basket service then product catalog service then discount service. if any service 
	is down entire operation is failed.


	We can also solve this by chaing http call but this is anti pattern.

	So we will develop shopping aggregator microservice.


	