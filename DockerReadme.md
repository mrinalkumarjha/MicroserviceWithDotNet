# What is Docker ?
Docker is container technology: A tool for creating and managing container.

> What is container ?
Container in sofware is a standardized unit of software. A package of code and dependencies to run that code(eg NodeJs code + the NodeJs runtime)

Support for container is built into modern operating system.
Docker simplifies the creation and management of such containers.


> Benifits of container, why containers ?
The same container always yield the exact same application and execution behaviour. No Matter where or by whom it might be executed.

best example is picknik basket. it is one king of container which has everything which is needed in picknik like food, dishes.
we can carry this basket anywhere and party. also anyone can take this basket from use and enjoy picknik in same way as we would have enjoyed.

Another example is lets say we have developed one application in node v12 and when we deploy that on another remote environment there is nodejs8 installed.
so there is huge possibility that some api of out code fail to execute. This is where container concept work. so if we want to smooth deployment on 
different environment(Development, Qa, Production) container technology help us to manage these version related issue. we can lock our version inside container which will work on any environment.

Even in single machine lets say one project in node 8 and one in node 12 for any reason. so switching between these is big hassle, time consuming and bad experiance too.


> Install docker on window: download docker desktop and installed. for older window docker toolbox is alternative to docker desktop.

in command line write docker if you dont get any error it means docker is installed.

if you cant install can use playground : https://labs.play-with-docker.com/


> Docker tools and Building blocks

Docker Engine
Docker Desktop
Docker Hub
Docker Compose
Kubernetes
VS code

> Some extension of vs code for docker

Docker of microsoft
prettier


> First docker container

	create a simple node js or any app and add Dockerfile inside it and add configuration in it like below.
	
	FROM node:14

	WORKDIR /app

	COPY package.json .

	RUN npm install

	COPY . .

	EXPOSE 3000

	CMD [ "node", "app.mjs" ]


	NOW RUN DOCKER BUILD .   command from terminal . it will create a image for this project
	
	
	
> Images vs Container

	container is running unit of software while image is blueprint / templates for container
	Its image which contains code + require tools / runtime that is require to run a code. and its a container that execute that code.
	We create image with all instruction at once and based on that we can create as many container we want.
	Image is sharable package with instruction written once. container will be concrete running instance of any software.
	
	so we run containers that is based on some image. multiple container can be created based on one image.
	
	by using run command or opening interactive terminal we create instance of images. this create concrete container which is based on images. if you run docker run node 2 times it will create
	2 seperate container.  you can check it with docker ps -a
	
	
	
> Finding / creating image
	
	First way is that we use an existing image from dockerhub.
	
	to run any image from command: docker run node(image name) . this will download image if not found in local.
	
	List all container: docker ps -a (ps stands for processes) with -a it shows all processes docker created for us.
	
	Expose node terminal to hosting machina: docker run -it node
	
	TO quit node session of container: ctrl + c
	
	
	SECOND WAY IS TO CREATE OWN IMAGE, custome image:
	
	To create own image we need to go to project folder > create new file (Dockerfile). This is special name which will be identified by docker.
	
	
	
	
	
	
	
	
	

	

