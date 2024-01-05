pipeline {
    agent any

    environment {
        // Zmienne środowiskowe
    }

    stages {
        stage('Build') {
            steps {
                checkout Git
                script {
                    
                sh 'docker-compose -f docker-compose.yml build'
        }
            }
        }

        stage('Test') {
            steps {
                sh 'docker-compose up -d'
                // Uruchomienie testów integracyjnych
                sh 'docker-compose down'
            }
        }

        stage('Cleanup') {
            steps {
                // Kody do sprzątania
            }
        }
    }

    post {
        always {
            // Kody do wykonania po każdym przebiegu, np. wysyłanie powiadomień
        }
    }
}
