package main

import (
	"net/http"
	"github.com/gin-gonic/gin"
	"math/rand"
	"time"
)

type jsonResponse struct {
    RandomNumber int `json:"randomNumber"`
}

func getRandomNumber(cx *gin.Context) {
	rand.Seed(time.Now().UnixNano())
	randomNumber := rand.Intn(1000000)

	response := jsonResponse{
        RandomNumber: randomNumber,
    }

	cx.IndentedJSON(http.StatusOK, response)
}

func main() {
	router := gin.Default()
	router.GET("api/randomNumber", getRandomNumber)
	router.Run("0.0.0.0:8081")
}