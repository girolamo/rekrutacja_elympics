# E2E Tests Setup Guide

This guide provides steps to set up and run end-to-end (e2e) tests. 

## Pre-Requisites

Before running the e2e tests, ensure you have the following installed:

- Docker
- Docker-compose
- Kubernetes (k8s)
- A running metrics-server on k8s
- Helm
- Git
- Postman

## Setup Steps

1. **Clone the Code Repository**
   
   ```bash
   git clone https://github.com/girolamo/rekrutacja_elympics.git
   ```

2. **Navigate to Project Root Directory**
3. **Build Containers Locally**

Run the following command to build all required containers:
    
    ```bash
    Docker-compose build
    ```

4. **Create Local Repository**

Set up a local repository for Kubernetes to pull images:

    ```bash
    docker run -d -p 5001:5000 --restart=always --name registry registry:2
    ```

5. **Tag Images**

Tag the created images:

    Tag ASP.NET app:
    ```bash
    docker tag rekrutacja_elympics-aspnet_for_elympics:latest localhost:5001/rekrutacja_elympics-aspnet_for_elympics:latest
    ```

    Tag Golang app:
    ```bash
    docker tag rekrutacja_elympics-golang_for_elympics:latest localhost:5001/rekrutacja_elympics-golang_for_elympics:latest
    ```

6. **Push Images to Local Repository**

    Push ASP.NET app:
    ```bash
    docker push localhost:5001/rekrutacja_elympics-aspnet_for_elympics:latest
    ```

    Push Golang app:
    ```bash
    docker push localhost:5001/rekrutacja_elympics-golang_for_elympics:latest
    ```
7. **Start deployment**

    ```bash
    helm install e2e e2e/
    ```

8. **Testing the Setup**

    Now you can test app by sending POST request to the endpoin using Postman

    ```bash
    http://localhost:8888/api/Numbers
    ```

## Post-Deployment

    After completing the process, uninstall the deployment:

    ```bash
    helm uninstall e2e
    ```