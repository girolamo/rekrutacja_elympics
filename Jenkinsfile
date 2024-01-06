pipeline {
    agent any

    environment {
        DOCKER_COMPOSE_FILE = 'docker-compose-mocked.yml'
        CHECK_AVA_MAX_ATTEMPTS = 10
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
                        def attempt = 0
                        def maxAva = Integer.parseInt(env.CHECK_AVA_MAX_ATTEMPTS)

                        while (attempt < maxAva) {
                            echo "Checking service availability: Attempt ${attempt + 1} from ${CHECK_AVA_MAX_ATTEMPTS}..."
                            try {
                                sh "curl -s -X GET ${SERVICE_URL}"
                                return
                            } catch (Exception e) {
                                if (attempt == CHECK_AVA_MAX_ATTEMPTS - 1) {
                                    throw new Exception("Unable to connect to service after ${CHECK_AVA_MAX_ATTEMPTS} attempts.")
                                }
                                echo "Service is not yet availeble."
                                sleep time: 5, unit: 'SECONDS'
                            }
                            attempt++
                        }
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
                        def responseJson1 = sh(script: "curl -s -X POST ${SERVICE_URL}", returnStdout: true).trim()
                        tests.runTest('[{"value":10000}]', responseJson1, 1)

                        def responseJson2 = sh(script: "curl -s -X POST ${SERVICE_URL}", returnStdout: true).trim()
                        tests.runTest('[{"value":20000},{"value":10000}]', responseJson2, 2)

                        def responseJson3 = sh(script: "curl -s -X POST ${SERVICE_URL}", returnStdout: true).trim()
                        tests.runTest('[{"value":30000},{"value":20000},{"value":10000}]', responseJson3, 3)

                        def responseJson4 = sh(script: "curl -s -X POST ${SERVICE_URL}", returnStdout: true).trim()
                        tests.runTest('[{"value":40000},{"value":30000},{"value":20000}]', responseJson4, 4)

                        def responseJson5 = sh(script: "curl -s -X POST ${SERVICE_URL}", returnStdout: true).trim()
                        tests.runTest('[{"value":50000},{"value":40000},{"value":300000}]', responseJson5, 5)
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