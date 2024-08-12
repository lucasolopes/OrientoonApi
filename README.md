
# OrientoonApi

OrientoonApi é um projeto que visa em ser um sistema de gerenciamento para Mangás, incluindo Webtoons e Novels. O nome vem de "Orient" (cultura oriental) e "toon" (desenhos animados). Foi desenvolvido para ser intuitivo e fácil de usar, permitindo acesso eficiente às obras favoritas. Além disso, o OrientoonApi oferece ferramentas para criadores e administradores de conteúdo, facilitando a gestão de publicações e a interação com a comunidade de leitores.

## Desafios Propostos para este projeto:
* Ferramenta de pesquisa :white_check_mark:
* Documentação Swagger :white_check_mark:
* TDD 
* Arquitetura Onion
* Fluent Validation
* Compressao De imagens
* Caching
* Autenticação e Autorização
* Pre-Carregamento de capitulos
* Pub/Sub(notificação)
* Monitoramento e Logging
* Testes de Carga/Segurança
* Sistema de Backup
* Serviços de Pagamento
* Recomendação Por IA

## Pré-requisitos

Para iniciar o desenvolvimento do seu projeto utilizando ASP.NET Core 6 com MariaDB 11.4, certifique-se de ter os seguintes requisitos instalados e configurados:

### Banco de Dados

- **MariaDB 11.4:**
  - Verifique se o MariaDB está instalado e configurado corretamente na sua máquina ou servidor.
  - Tenha acesso às credenciais de conexão necessárias (nome do servidor, porta, nome do banco de dados, usuário e senha).

### Ferramentas

- **ASP.NET Core 6:**
  - Instale o SDK do ASP.NET Core 6. Você pode baixá-lo em [dot.net](https://dot.net).

### Ambiente de Desenvolvimento

- **Visual Studio 2022 (ou superior):**
  - Utilize o Visual Studio como seu ambiente de desenvolvimento integrado (IDE) para aproveitar ao máximo as funcionalidades do ASP.NET Core 6.
  - Configure o ambiente de desenvolvimento para trabalhar com C# e projetos ASP.NET Core.


## Iniciando o Projeto

Para começar a trabalhar no projeto utilizando C# e ASP.NET Core como uma Web API, siga os passos abaixo:

### Clonando e Abrindo o Projeto

1. **Clone o Repositório:**
   Abra o Git Bash ou o terminal de sua preferência e clone o projeto do repositório remoto.


2. **Abra o Projeto no Visual Studio:**
Após clonar o repositório, abra o Visual Studio.

3. **Configurações Secretas:**
- Vá para o arquivo `Secrets` e insira as seguintes configurações no bloco `ConnectionStrings` e `FileUploadPath`:
  ```json
  "ConnectionStrings": {
    "DatabaseLocal": "Server=;Port=;Database=;User=;Password=;"
  },
  "FileUploadPath": "PastaDestinoParaSalvarEstaticos"
  ```

### Executando as Migrações do Banco de Dados

1. **Abra o Console do Gerenciador de Pacotes:**
No Visual Studio, abra o Console do Gerenciador de Pacotes. Você pode encontrá-lo em `Tools -> NuGet Package Manager -> Package Manager Console`.

2. **Execute o Comando Update-Database:**
No Console do Gerenciador de Pacotes, execute o comando `Update-Database` para aplicar todas as migrações pendentes e atualizar o banco de dados conforme configurado na string de conexão

3. **Verifique o Status das Migrações:**
Após executar o comando, verifique no Console do Gerenciador de Pacotes se as migrações foram aplicadas com sucesso.
## Documentação da API

Apos inciar o projeto pode acessar a documentação através do swagger: localhost:7284/swagger/index.html

