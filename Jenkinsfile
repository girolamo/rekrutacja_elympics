pipeline {
    agent any

    stages {
        stage('Build') {
            steps {
                checkout scm
                
                script {
                sh 'docker-compose -f docker-compose-mocked.yml build'
        }
            }
        }

        stage('Run') {
            steps {
                sh 'docker-compose -f docker-compose-mocked.yml up -d' 
                sh 'sleep 10'     
            }
        }

        stage('Test') {
            steps {
               sh 'curl -X POST http://localhost:8888/api/numbers'
               sh 'curl -X POST http://localhost:8888/api/numbers' 
               sh 'curl -X POST http://localhost:8888/api/numbers' 
               sh 'curl -X POST http://localhost:8888/api/numbers' 
               sh 'curl -X POST http://localhost:8888/api/numbers' 
            }
        }

        stage('Cleanup') {
            steps {
                sh 'echo cleanup'
            }
        }
    }

    post {
        always {
            sh 'docker-compose down'
        }
    }
}
