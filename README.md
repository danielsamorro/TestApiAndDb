# TestApiAndDb

* Na pasta src está o código fonto da API.

A aplicação é uma API em .NET 6 que utiliza uma base de dados in memory para propósito de testabilidade.
A aplicação está configurada para utilizar Swagger para facilitar o uso de seus endpoints.
Foram implementados alguns testes unitários por questões de exemplo.

A aplicação utiliza o pattern mediator para envio de comandos para serem processados em seus respectivos handlers.
A aplicação utiliza o pattern Unit of Work para centralizar e sincronizar o processamentos dos dados da base SQL.

* Na pasta db está o script SQL 
* Na pasta Conseitual está a resposta junto do diagrama sobre microsserviços.
