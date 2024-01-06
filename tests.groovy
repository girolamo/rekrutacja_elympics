def runTest(String expectedJson, String responseJson int testNumber) {
    println "Performing ${testNumber} test."

    def expected = new groovy.json.JsonSlurper().parseText(expectedJson)
    def response = new groovy.json.JsonSlurper().parseText(responseJson)

    response.each { it.remove('creationTime') }

    if (expected != response) {
        println "Test number ${testNumber} failed."
        error("JSON are not the same! Expected: ${expectedJson}, recieved: ${responseJson}")
    } else {
        println "Test number ${testNumber} passed."
    }
}

return this