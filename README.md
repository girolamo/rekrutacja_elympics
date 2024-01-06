Pre-Requirments in order to run e2e tests:
- Docker
- Docker-compose
- k8s
- up and runing metrics-server on k8s
- Helm
- Git

Steps to follow:
1. First clone code repository, by running
git clone https://github.com/girolamo/rekrutacja_elympics.git

2. Navigate in your shell to project root directory
3. run '''Docker-compose build''' in order to build all required containers localy
4. Create repository on localhost by runig '''docker run -d -p 5001:5000 --restart=always --name registry registry:2''' . It will help k8s correctly pull image
5. Tag previously created images.  
    5.1 Run '''docker tag rekrutacja_elympics-aspnet_for_elympics:latest localhost:5001/rekrutacja_elympics-aspnet_for_elympics:latest'''
    5.2 Run '''docker tag rekrutacja_elympics-golang_for_elympics:latest localhost:5001/rekrutacja_elympics-golang_for_elympics:latest'''
6. Push both images to local repository
    5.1 Run '''docker push localhost:5001/rekrutacja_elympics-aspnet_for_elympics:latest'''
    5.2 Run '''docker push localhost:5001/rekrutacja_elympics-golang_for_elympics:latest'''
7. Start deployment by runnin '''helm install e2e e2e/''' 

Post
1. After whole process - uninstall deployment by running "helm uninstall e2e"