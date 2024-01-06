def runTest(String expectedJson, int testNumber) {
    sh script: '''
        response=$(curl -X POST http://localhost:8888/api/numbers)
        processed_response=$(echo $response | jq -c '[.[] | {value: .value}]')
        
        expected_normalized=$(echo $''' + expectedJson + ''' | jq -c .)
        response_normalized=$(echo $processed_response | jq -c .)

        if [ "$response_normalized" = "$expected_normalized" ]; then
            echo "''' + testNumber + '''. TEST PASSED"
        else
            echo "''' + testNumber + '''. TEST FAILED: Expected $expected_normalized, Recieved $response_normalized"
            exit 1
        fi
    ''', returnStatus: false
}

return this