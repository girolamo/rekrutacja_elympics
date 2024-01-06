def runTest(String expectedJson, int testNumber) {
    println "Performing ${testNumber} test."

    def expected = new groovy.json.JsonSlurper().parseText(expectedJson)

    def responseJson = sh(script: "curl -s -X POST http://localhost:8888/api/numbers", returnStdout: true).trim()
    def response = new groovy.json.JsonSlurper().parseText(responseJson)

    response.each { it.remove('creationTime') }

    if (expected != response) {
        println "Test number ${testNumber} failed."
        error("JSON are not the same! Expected: ${expectedJson}, recieved: ${responseJson}")
    } else {
        println "Test number ${testNumber} passed."
    }

    return true
}