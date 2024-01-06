FROM golang:1.21

WORKDIR /app

COPY go.* ./
RUN go mod download

COPY *.go ./

RUN go build -o /random-number-api

EXPOSE 8081

CMD ["/random-number-api"]