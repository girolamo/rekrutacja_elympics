pipeline {
    agent any

    environment {
        DOCKER_COMPOSE_FILE = 'docker-compose-mocked.yml'
        CHECK_AVAILABILITY_MAX_ATTEMPTS = 10
        SERVICE_URL = "http://localhost:8888/api/numbers"
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
                
                script {
                    def checkServiceAvailability = { ->
                        
                    }

                    try {
                        checkServiceAvailability()
                    } catch (Exception e) {
                        println "Error: ${e.message}"
                        throw e
                    }
                }
            }
        }

        stage('Test') {
            steps {
                script {
                    def tests = load 'tests.groovy'

                    def testsPassed = true
                    try {
                        def responseJson1 = sh(script: "curl -s -X POST http://localhost:8888/api/numbers", returnStdout: true).trim()
                        tests.runTest('[{"value":10000}]', responseJson1, 1)

                        def responseJson2 = sh(script: "curl -s -X POST http://localhost:8888/api/numbers", returnStdout: true).trim()
                        tests.runTest('[{"value":20000},{"value":10000}]', responseJson2, 2)

                        def responseJson3 = sh(script: "curl -s -X POST http://localhost:8888/api/numbers", returnStdout: true).trim()
                        tests.runTest('[{"value":30000},{"value":20000},{"value":10000}]', responseJson3, 3)

                        def responseJson4 = sh(script: "curl -s -X POST http://localhost:8888/api/numbers", returnStdout: true).trim()
                        tests.runTest('[{"value":40000},{"value":30000},{"value":20000}]', responseJson4, 4)

                        def responseJson5 = sh(script: "curl -s -X POST http://localhost:8888/api/numbers", returnStdout: true).trim()
                        tests.runTest('[{"value":50000},{"value":40000},{"value":30000}]', responseJson5, 5)
                    }
                    catch (Exception e) {
                        testsPassed = false
                        println "Error: ${e.message}"
                        throw e
                    }

                    if (!testsPassed) {
                        error "One or more tests failed"
                    }
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