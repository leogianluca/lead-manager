FROM node:24.1.0 AS build
WORKDIR /app
COPY frontend/ ./
RUN npm install
RUN npm run build

FROM nginx:alpine
COPY --from=build /app/dist /usr/share/nginx/html
EXPOSE 80
