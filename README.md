![SENAI_São_Paulo_logo](https://github.com/user-attachments/assets/64d5dde5-e99d-434a-ad5e-81f7dd74c1a1)

# API com MongoDB.

Este repositório tem como objetivo manter arquivos referente aos estudos efetuados em sala de aula no Senai Informática, contendo os arquivos e explicações necessárias para criação, utilização e aplicação de uma API utilizando como linguagem de programação o C# e como banco de dados o MongoDB.

## Pré-Requisitos

Instalar o banco e a ferramenta de visual do banco.

- https://www.mongodb.com/try/download/community
- https://www.mongodb.com/try/download/compass?authuser=0

## Documentação para aplicação de uma API .Net com Mongo

https://learn.microsoft.com/pt-br/aspnet/core/tutorials/first-mongo-app?view=aspnetcore-8.0&tabs=visual-studio

## Configurando API com o MongoDB

```
using MongoDB.Driver;

namespace minimalAPIMongo.Services
{
    public class MongoDbService
    {
        /// <summary>
        /// Armaena a configuração da aplicação.
        /// </summary>
        private readonly IConfiguration _configuration;
        /// <summary>
        /// Armazena uma referencia ao mongoDB.
        /// </summary>
        private readonly IMongoDatabase _database;

        /// <summary>
        /// Recebe a configuraçao da aplicação como parametro
        /// </summary>
        /// <param name="configuration">Objeto Configuration</param>
        public MongoDbService(IConfiguration configuration) 
        {
            // Atribui a configuraçao recebida em _configuration
            _configuration = configuration;

            // Obtem a string de conexão atraves do _configuration, DbConnection foi estabelecido no appsettings.json
            var connectionString = _configuration.GetConnectionString("DbConnection");

            // Cria um objeto MongoUrl que recebe como parametro a string de conexão;
            var mongoUrl = MongoUrl.Create(connectionString);

            // Cria um client MongoClient para se conectar ao MongoDb
            var mongoClient = new MongoClient(mongoUrl);

            // Obtem a referencia ao Bando de dados com o nome especifico da string de conexão.
            _database = mongoClient.GetDatabase(mongoUrl.DatabaseName);
        }

        /// <summary>
        /// Propriedade para acessaro o banco de dados.
        /// </summary>
        public IMongoDatabase GetDatabase => _database;

    }
}

```
<!--

## Screenshots

-->

## Demonstração

![Screenshot 2024-07-29 080709](https://github.com/user-attachments/assets/c1f7effc-9897-4791-afa3-bb086ee5b1b3)


## Recursos utilizados durante o desenvolvimento:

-	SO:
	-	![Windows 11](https://img.shields.io/badge/Windows%2011-%230079d5.svg?style=for-the-badge&logo=Windows%2011&logoColor=white)

-  IDEs/Editors:
   -  ![Visual Studio](https://img.shields.io/badge/Visual%20Studio-5C2D91.svg?style=for-the-badge&logo=visual-studio&logoColor=white)
   
- Banco de Dados
  -  ![MongoDB](https://img.shields.io/badge/MongoDB-%234ea94b.svg?style=for-the-badge&logo=mongodb&logoColor=white)

-  Frameworks, Platforms and Libraries
   -  ![.Net](https://img.shields.io/badge/.NET-5C2D91?style=for-the-badge&logo=.net&logoColor=white) 
-	Linguagens utilizadas:
	-	![C#](https://img.shields.io/badge/c%23-%23239120.svg?style=for-the-badge&logo=csharp&logoColor=white)

-	Navegadores Utilizados Durante desenvolvimento:
	-	![Edge](https://img.shields.io/badge/Edge-0078D7?style=for-the-badge&logo=Microsoft-edge&logoColor=white)	![Google Chrome](https://img.shields.io/badge/Google%20Chrome-4285F4?style=for-the-badge&logo=GoogleChrome&logoColor=white)	![Firefox](https://img.shields.io/badge/Firefox-FF7139?style=for-the-badge&logo=Firefox-Browser&logoColor=white) 

-	Controle de Versionamento:
	-	![Git](https://img.shields.io/badge/git-%23F05033.svg?style=for-the-badge&logo=git&logoColor=white)	![GitHub](https://img.shields.io/badge/github-%23121011.svg?style=for-the-badge&logo=github&logoColor=white)

-	Base de estudos:
	-	[![Senai]( https://img.shields.io/badge/Senai-Infromatica-red)](https://informatica.sp.senai.br/)

<!--
## Funcionalidades

- Transcrição de imagem em texto.
- Notificação de consultas canceladas
- Agendamentos
- Envio de E-mail
- Perfil de usuario
-->
    
## Autores

- [<img src="https://github.com/Lucca-gOn/vitalhubteamwork/assets/22855740/fe3ac17c-18c6-4b2e-9490-176b9099db5b" width=115><br><sub>| 🙋🏼‍♂️ Allan Rodrigues dos Santos |</sub>](https://github.com/AllanR1991)
