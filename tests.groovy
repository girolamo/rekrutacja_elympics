def runTest(String expectedJson, int testNumber) {
    sh script: '''
        response=$(curl -X POST http://localhost:8888/api/numbers)
        processed_response=$(echo $response | jq -c '[.[] | {value: .value}]')
        if [ "$processed_response" = "$''' + expectedJson + '''" ]; then
            echo "''' + testNumber + '''. TEST PASSED"
        else
            echo "''' + testNumber + '''. TEST FAILED: Oczekiwano ''' + expectedJson + ''', otrzymano $processed_response"
            exit 1
        fi
    ''', returnStatus: false
}

return this