FROM node:alpine as build

WORKDIR /usr/src/app

COPY package.json package-lock.json ./

RUN npm install
COPY . .
RUN npm run build

EXPOSE 80