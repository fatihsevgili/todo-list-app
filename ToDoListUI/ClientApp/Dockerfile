﻿########################
### build environment ###
#########################

# base image
FROM node:10.15.1 as builder

# install chrome for protractor tests
# RUN wget -q -O - https://dl-ssl.google.com/linux/linux_signing_key.pub | apt-key add -
# RUN sh -c 'echo "deb [arch=amd64] http://dl.google.com/linux/chrome/deb/ stable main" >> /etc/apt/sources.list.d/google.list'
# RUN apt-get update && apt-get install -yq google-chrome-stable

ARG BUILD_ENV=""

ARG ENV_FILE_NAME=environment-${BUILD_ENV}.ts

RUN echo "Env File Name:" $ENV_FILE_NAME
# set working directory
RUN mkdir /usr/src/app
WORKDIR /usr/src/app



# add `/usr/src/app/node_modules/.bin` to $PATH
ENV PATH /usr/src/app/node_modules/.bin:$PATH

# install and cache app dependencies
COPY package.json /usr/src/app/package.json
COPY package-lock.json /usr/src/app/package-lock.json
RUN npm install --loglevel verbose
RUN npm install -g @angular/cli@7.3.3  --unsafe

# add app
COPY . /usr/src/app

# run tests
# RUN ng test --watch=false

# generate build
RUN npm run "build-prod"

##################
### production ###
##################

# base image
FROM nginx:1.13.9-alpine

# copy artifact build from the 'build environment'
COPY --from=builder /usr/src/app/dist /usr/share/nginx/html

# expose port 80
EXPOSE 80

# run nginx
CMD ["nginx", "-g", "daemon off;"]
