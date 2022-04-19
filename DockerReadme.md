# What is Docker ?
	Docker is container technology: A tool for creating and managing container. docker is not for only webserver or webapp. we also use docker for console app where
	we input and get output.
	

# What is container ?
	Container in sofware is a standardized unit of software. A package of code and dependencies to run that code(eg NodeJs code + the NodeJs runtime)

	Support for container is built into modern operating system.
	Docker simplifies the creation and management of such containers.


# Benifits of container, why containers ?
	The same container always yield the exact same application and execution behaviour. No Matter where or by whom it might be executed.

	best example is picknik basket. it is one king of container which has everything which is needed in picknik like food, dishes.
	we can carry this basket anywhere and party. also anyone can take this basket from use and enjoy picknik in same way as we would have enjoyed.

	Another example is lets say we have developed one application in node v12 and when we deploy that on another remote environment there is nodejs8 installed.
	so there is huge possibility that some api of out code fail to execute. This is where container concept work. so if we want to smooth deployment on 
	different environment(Development, Qa, Production) container technology help us to manage these version related issue. we can lock our version inside container which will work on any environment.

	Even in single machine lets say one project in node 8 and one in node 12 for any reason. so switching between these is big hassle, time consuming and bad experiance too.


# Install docker on window: download docker desktop and installed. for older window docker toolbox is alternative to docker desktop.

	in command line write docker if you dont get any error it means docker is installed.

	if you cant install can use playground : https://labs.play-with-docker.com/


# Docker tools and Building blocks

	Docker Engine
	Docker Desktop
	Docker Hub
	Docker Compose
	Kubernetes
	VS code

# Some extension of vs code for docker

	Docker of microsoft
	prettier


# First docker container

	create a simple node js or any app and add Dockerfile inside it and add configuration in it like below.
	
	FROM node:14

	WORKDIR /app

	COPY package.json .

	RUN npm install

	COPY . .

	EXPOSE 3000

	CMD [ "node", "app.mjs" ]


	NOW RUN DOCKER BUILD .   command from terminal . it will create a image for this project
	
	
	
# Images vs Container

	container is running unit of software while image is blueprint / templates for container
	Its image which contains code + require tools / runtime that is require to run a code. and its a container that execute that code.
	We create image with all instruction at once and based on that we can create as many container we want.
	Image is sharable package with instruction written once. container will be concrete running instance of any software.
	
	so we run containers that is based on some image. multiple container can be created based on one image.
	
	by using run command or opening interactive terminal we create instance of images. this create concrete container which is based on images. if you run docker run node 2 times it will create
	2 seperate container.  you can check it with docker ps -a
	
	when two container is created from one image it dosent mean code is copied in all two container. container dont hold any code. container is one extra thin layer
	which has instruction how to run appp.
	
	
	
# Finding / creating image
	
	First way is that we use an existing image from dockerhub.
	
	to run any image from command: docker run node(image name) . this will download image if not found in local.
	
	List all container: docker ps -a (ps stands for processes) with -a it shows all processes docker created for us.
	
	Expose node terminal to hosting machina: docker run -it node
	
	TO quit node session of container: ctrl + c
	
	
	# SECOND WAY IS TO CREATE OWN IMAGE, custome image:
	
	To create own image we need to go to project folder > create new file (Dockerfile). This is special name which will be identified by docker.
	
	This docker file will consist all instruction which we want to run when we will build image.
	
	It starts with FROM all caps
	
	# FROM node(node is name of image which is available either on docker hub or in local)
	
	Now we need to set working directory so that command will not run in root dir. instead it will run in working dir
	
	WORKDIR /app
	
	Next we need to give instruction of files which will go into image
	
	# COPY . /app  or (COPY . ./    --if workdir is set)  (first dot says all the files and subdirectories) and /app is space inside image where file will be copied.
	
	Then any command if we want to execute like npm install in case of node app
	
	# RUN npm install  (By default command run inside root directory if workdir is not set. since we want to execute npm install inside /app so we set workdir as /app).
	
	Now since docker image has isolated environment which cant be accessible from outside container. we need to expose port of our app from container.
	
	# EXPOSE 80  (it will expose 80 port to machine which will run container)
	
	Next we need to add command which will be executed when container will be started.
	
	# CMD ["node" "server.js"]  (we can use RUN also here to run node but it will run also when image will be created, but we want to run it only when container will be created).
	
	complete docker file which we created is as follows.
	
	FROM node

	WORKDIR /app

	COPY . /app

	RUN npm instsll

	EXPOSE 80

	CMD ["node" "server.js"]  
		
	
	
# How to create image from this dockerfile.

	To create image: DOCKER build . (. says docker file is in same directory from where we are running build). this will create an image.
	
	once image is created you can check using command(docker ps -a)

	docker ps : will list all running process.
	
	To stop container : docker stop container name
	
	Run docker image : docker run -p 3000:80 5c0ab802236c48abec5e2506e35153bf8738308ba5fdf4da9e3f749dfb7bbd56 
	
	here 3000 is port where we want top run app of local machine and 80 is port of container which is exposed.
	
	For help on any docker command: use --help 
	
	
# EXPOSE & A Little Utility Functionality
	In the last lecture, we started a container which also exposed a port (port 80).

	I just want to clarify again, that EXPOSE 80 in the Dockerfile in the end is optional. It documents that a process in the container will expose this port. But you still need to then actually expose the port with -p when running docker run. So technically, -p is the only required part when it comes to listening on a port. Still, it is a best practice to also add EXPOSE in the Dockerfile to document this behavior.

	As an additional quick side-note: For all docker commands where an ID can be used, you don't always have to copy / write out the full id.

	You can also just use the first (few) character(s) - just enough to have a unique identifier.

	So instead of

	docker run abcdefg
	you could also run

	docker run abc
	or, if there's no other image ID starting with "a", you could even run just:

	docker run a
	This applies to ALL Docker commands where IDs are needed.
		
	
	
# Docker image are readonly.

	Once you create image from DOCKER build . command image is created and it is readonly so you cant change source code inside image. if you want to change 
	source code of image you will have to build in again. we will see some best elegant way to update image source code. right now use docker build . to build image
	again.
	
	so by this process new image will be created and after running this new image using (docker run -p 3000:80 ee21278214df38dd697e5bba8a7071c31790ee9938154887edbe267e3f8b50a5)
	
	Now you will be able see latest changes you would have made in source code.
	
# Understanding image layer.

	each instruction in dockerfile is layer. when we create image from docker build . it is cached and after changing source code if we again run docker build
	it checks for cached version if there is no change then it takes from cache.
	once any layer in between changes all subsequent layer is executed.
	
	ex if we change source code only but did not made any change in package.json , copy ./app and all subsequent layer will be executed on docker build.
	to make it more efficient we copy package.json before npm run. here are modified dockerfile.
	
	
# DOCKER Commands:

	docker ps : list all running container
	docker ps -a : list all container which are even stopped.
	docker ps --help : to see all configuration options available for docker ps
	
	docker run -p 8000:80 image_id : create new container based on some image. it is attached mode(means we are attached to container and any console.log kind of output will appear in terminal)
	docker run -p 8000:80 -d image_id : create new container based on some image. -d  is deattached mode(means we are not attached to container and any console.log kind of output will not appear in terminal)
	docker run -p 3000:80 --rm image_id : by --rm it will automatically remove container once exit.
	docker run -p 8000:80 --name goalsapp 485b13a7c43a : by running image with --name we can name a container which will be created based on image.
	
	docker start container_name_or_id: start existing container in deattached mode by default. use this if there is not any source code change. because docker run will create new container each time.
	docker start -a container_name_or_id: start existing container in attached mode by default.
	docker stop container_name_or_id : stop running container.
	docker attach container_name_or_id : to attach container to terminal(so that user can see logs and outputs , error from server)
	docker logs container_name_or_id: to see logs of any container
	docker logs -f container_name_or_id: to keep listning and see logs of any container(similar to attach mode)
	
	docker run -it ee21278214df : To enter in interactive mode(for python script. to enter enput and expect output)
	docker start -a -i ffdbb802a596 : To start container in interactive mode. ex for python script . -a is atttched and -i means we want to input something.
	
	docker rm container_name_or_id container_name_or_id : To remove container.
	docker rm compassionate_yonath romantic_yalow affectionate_sinoussi  : remove multiple container.
	
	docker images : list all images
	docker rmi image_id image_id: to remove multiple images. it will remove image and all llayers of image. to remove image you will have to remove container first which is using image.
	docker image prune: it will remove all unused images.
	
	docker inspect 8778d77035e2: to inspect, see details like all layers, when created , which os is used , size about images.
	
	docker cp dummy/. nice_leakey:/test   : copy dummy directory to running container inside test folder. same way we can copy files from container to our machine.
	
	docker build -t goalsapp:1 .   : to give name/tag any image while building. images has repo:tag format of naming. for detail see Understanding tag section.
	
	
	
	
	
	
	
	
	
# Attaching to an already-running Container
	By default, if you run a Container without -d, you run in "attached mode".

	If you started a container in detached mode (i.e. with -d), you can still attach to it afterwards without restarting the Container with the following command:

	docker attach CONTAINER
	attaches you to a running Container with an ID or name of CONTAINER.
	

# Running python script from docker.

	script.py
				from random import randint

				min_number = int(input('Please enter the min number: '))
				max_number = int(input('Please enter the max number: '))

				if (max_number < min_number): 
				  print('Invalid input - shutting down...')
				else:
				  rnd_number = randint(min_number, max_number)
				  print(rnd_number)

	Dockerfile
				FROM python

				WORKDIR /app

				COPY . /app

				CMD ["python", "rng.py"]


	> docker build . (to create image)
	> docker run -it ee21278214df : To enter in interactive mode(for python script. to enter enput and expect output)
	> docker start -a -i ffdbb802a596 : To start container in interactive mode. ex for python script . -a is atttched and -i means we want to input something.
	
	

	
	
# Understanding tag:
	
	we can also assign name to images, but images does not have name field. it has instead Repository and Tag. by name you can set name and by tag we can se version.
	line node:12
	
	Image tag consists of two parts : 1st actual name also called Repository then Tag seperated by colon.
	
	name: tag
	
	command to set tags: docker build -t goalsapp:1 .
	
	
# Sharing images and container

	we can share images locally or from centrailized server. one who has images can create container based on that images.
	
	There are two ways of doing this.
	
	1> if someone give you dockerfile and sourcecode one can build image based on this.
	2> Share complete build image: no build is require for receipient he can use this image and create container based on this. we can push image to dockerhub to share
		globally.
		
	
# Pushing images to dockerhub.

	we can share images via dockerhub or private registry.
	
	> example of sharing on docker hub:
	
	go to docker hub
	create a repository
	copy push command from repository (docker push mrinalkumarjha/node-goal-app) in my case
	we need to run this from our local but before that we should have same name image in local including slash  (mrinalkumarjha/node-goal-app)4
	
	so re tag your image if name is not same using command : docker tag goals:1 mrinalkumarjha/node-goal-app:1
	this will create clone of old with new name provided.
	
	now run push command : docker push mrinalkumarjha/node-goal-app:1
	
	this will start pushing but gives access denied error in last. for that you need to docker login first. we need to run this command once.
	run docker login
	
	now run push command again : docker push mrinalkumarjha/node-goal-app:1
	
	after this images is there in repo.
	
	
	
	
	
