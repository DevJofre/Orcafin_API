# Orcafin_API

optionsBuilder.UseSqlServer("Server=localhost;Database=OrcafinDB;User Id=sa;Password=!Admin123;TrustServerCertificate=True");

Este projeto utiliza o Entity Framework Core com SQL Server para o gerenciamento de banco de dados.

.NET SDK instalado (versão compatível com o projeto)

SQL Server em execução local (ou string de conexão válida no DbContextFactory)

Ferramentas do EF Core instaladas globalmente ou como pacote de ferramentas:
dotnet tool install --global dotnet-ef

---Os Comnandos devem ser rodados dentro da pasta Orcafin.Infrastructure---

1- Compilar o projeto (opcional, mas recomendado):

  dotnet build

2-Criar uma nova migration:
Use esse comando sempre que fizer mudanças no modelo de dados (ex: novas entidades, alterações em propriedades).

  dotnet ef migrations add NomeDaMigration

3-Aplicar a migration ao banco de dados:

  dotnet ef database update

4- Dropa Banco:

  dotnet ef database drop

#Resumo da ordem de implementação

1- Camada Domain:	Definir entidades e interfaces
   -- User.cs(Entities), IUserRepository.cs(Interfaces) --

2- Camada Application: Corrigir namespace e implementar lógica
    -- UserService.cs(Service), UserResponse.cs(Dto), IUserService.cs(Interfaces) --

3- Camada Infrastructure: Implementar interfaces e configurar acesso a dados
    -- UserRepository.cs(Repository), OrcafinDbContext(Context), UserConfiguration.cs(Configuration) --

4- Camada WebApi: Criar endpoints e injetar serviços
    -- UsersController.cs(Controller) --

