FROM node:18 AS build

WORKDIR /usr/src/app

COPY client/easy-rent ./

RUN npm cache clean --force
RUN npm config rm proxy
RUN npm config rm https-proxy
RUN npm cache verify

RUN npm install

RUN npm run ng build

FROM nginx:latest
COPY client/easy-rent/nginx.conf /etc/nginx/nginx.conf
COPY --from=build /usr/src/app/dist/easy-rent /usr/share/nginx/html

EXPOSE 80