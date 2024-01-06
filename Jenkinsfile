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
                sh 'sleep 15'     
            }
        }

        stage('Test') {
            steps {
               sh '''expected_json='[{"value":10000}]'
                    response=$(curl -X POST http://localhost:8888/api/numbers)
                    processed_response=$(echo $response | jq -c '[.[] | {value: .value}]')
                    if [ "$processed_response" = "$expected_json" ]; then
                        echo "1. TEST PASSED"
                    else
                        echo "1. TEST FAILED"
                    fi

                    '''

                sh '''expected_json='[{"value":20000},{"value":10000}]'
                    response=$(curl -X POST http://localhost:8888/api/numbers)
                    processed_response=$(echo $response | jq -c '[.[] | {value: .value}]')
                    if [ "$processed_response" = "$expected_json" ]; then
                        echo "2. TEST PASSED"
                    else
                        echo "2. TEST FAILED"
                    fi

                    '''
                
                sh '''expected_json='[{"value":30000},{"value":20000},{"value":10000}]'
                    response=$(curl -X POST http://localhost:8888/api/numbers)
                    processed_response=$(echo $response | jq -c '[.[] | {value: .value}]')
                    if [ "$processed_response" = "$expected_json" ]; then
                        echo "3. TEST PASSED"
                    else
                        echo "3. TEST FAILED"
                    fi

                    '''

                sh '''expected_json='[{"value":40000},{"value":30000},{"value":20000}]]'
                    response=$(curl -X POST http://localhost:8888/api/numbers)
                    processed_response=$(echo $response | jq -c '[.[] | {value: .value}]')
                    if [ "$processed_response" = "$expected_json" ]; then
                        echo "4. TEST PASSED"
                    else
                        echo "4. TEST FAILED"
                    fi

                    '''

                sh '''expected_json='[{"value":50000},{"value":40000},{"value":30000}]]'
                    response=$(curl -X POST http://localhost:8888/api/numbers)
                    processed_response=$(echo $response | jq -c '[.[] | {value: .value}]')
                    if [ "$processed_response" = "$expected_json" ]; then
                        echo "5. TEST PASSED"
                    else
                        echo "5. TEST FAILED"
                    fi

                    '''    
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
