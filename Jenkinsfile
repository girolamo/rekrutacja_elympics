pipeline {
    agent any

    stages {
        stage('Build') {
            steps {
                checkout scm
                
                script {
                sh 'docker-compose -f docker-compose.yml build'
        }
            }
        }

        stage('Run') {
            steps {
                sh 'docker-compose up -d'      
            }
        }

        stage('Test') {
            steps {
               sh 'curl -X POST -H \'Content-Type: application/json\' http://localhost:8081' 
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
