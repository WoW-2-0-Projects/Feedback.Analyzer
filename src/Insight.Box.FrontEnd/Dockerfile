# Step 1: Build the Vue.js application
FROM node:lts-alpine as build-stage

WORKDIR /app
COPY package*.json ./
RUN npm install
COPY . .
RUN npm run build

# Step 2: Set up Nginx to serve the app
FROM nginx:stable-alpine as production-stage

# Copy the built app from the previous stage
COPY --from=build-stage /app/dist /usr/share/nginx/html

# Custom Nginx configuration
RUN rm /etc/nginx/conf.d/default.conf
COPY nginx.conf /etc/nginx/conf.d

EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]