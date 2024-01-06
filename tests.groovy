def runTest(String expectedJson, int testNumber) {
    if (!sh(script: 'command -v jq', returnStatus: true)) {
        echo 'jq is not installed. Test cannot be run.'
        return
    }

    script = """
        response=\$(curl -s -X POST http://localhost:8888/api/numbers)
        processed_response=\$(echo \$response | jq -c '[.[] | {value: .value}]')

        expected_normalized=\$(echo ${JsonOutput.toJson(expectedJson)} | jq -c .)
        response_normalized=\$(echo \$processed_response | jq -c .)

        if [ "\$response_normalized" = "\$expected_normalized" ]; then
            echo "${testNumber}. TEST PASSED"
        else
            echo "${testNumber}. TEST FAILED: Expected \$expected_normalized, Received \$response_normalized"
            exit 1
        fi
    """

    sh(script: script, returnStatus: false)
}