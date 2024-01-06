pipeline {
    agent any

    environment {
        DOCKER_COMPOSE_FILE = 'docker-compose-mocked.yml'
    }

    stages {
        stage('Build') {
            steps {
                checkout scm
                
                script {
                    sh 'docker-compose -f ${DOCKER_COMPOSE_FILE} build'
                }
            }
        }

        stage('Setup environment') {
            steps {
                sh 'docker-compose -f ${DOCKER_COMPOSE_FILE} up -d'

                sh 'sleep 15'     
            }
        }

        stage('Test') {
            steps {
                script {
                    def tests = load 'tests.groovy'

                    
                }
            }
        }
    }

    post {
        always {
            sh 'docker-compose down'

            sh '''
                    volumes=$(docker volume ls -q | grep numbersDbVolume)
                    for volume in $volumes; do 
                        echo "Removing Docker volume: $volume"
                        docker volume rm $volume
                    done
                '''

        }
    }
}