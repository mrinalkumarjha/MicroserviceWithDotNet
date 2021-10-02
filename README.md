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


	
	










