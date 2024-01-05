pipeline {
    agent any

    stages {
        stage('Build') {
            steps {
                checkout Git
                script {
                
                sh 'echo build'
                sh 'docker-compose -f docker-compose.yml build'
        }
            }
        }

        stage('Test') {
            steps {
                sh 'echo test'
                sh 'docker-compose up -d'

                sh 'docker-compose down'
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
            sh 'echo post'
        }
    }
}
