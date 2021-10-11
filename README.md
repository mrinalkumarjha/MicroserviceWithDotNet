# MicroserviceWithDotNet

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

docker-compose -f docker-compose.yml -f docker-compose.override.yml up --build

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



