<!-- ABOUT THE PROJECT -->
## CRIANDO O PROJETO

Criando projeto Mvc.
* dotnet
```sh
dotnet new mvc -o Mvc
```
Criando a solução.
```sh
dotnet new sln
```
Criando projeto ClassLib.
```sh
dotnet new classlib -o Domain
```
```sh
dotnet new classlib -o Data
```
Vinculando Projeto a Solução.
```sh
dotnet sln add ./Mvc/Mvc.csproj
```
```sh
dotnet sln add ./Domain/Domain.csproj
```
```sh
dotnet sln add ./Data/Data.csproj
```

# Adicionando os pacotes que vamos Utlizar

Na Pasta Data (Dados)
```sh
cd Data
```
Pacotes, lembrando de verificar a versão do **.NetCore**
```sh
dotnet add package Microsoft.EntityFrameworkCore --version 2.1  
dotnet add package Microsoft.EntityFrameworkCore.SqlServer --version 2.1 
dotnet add package Microsoft.EntityFrameworkCore.Tools --version 2.1    
```

# Adicionando Referencias
```sh
cd Mvc  
```
```sh
dotnet add reference ../Domain/Domain.csproj
dotnet add reference ../Data/Data.csproj
```
```sh
cd Data  
```
```sh
dotnet add reference ../Domain/Domain.csproj
```

# Configurando Migrations

Configurar no arquivo **appsettings.json** a string de conexão. 

Criar Class de Context na pasta Data, nome do arquivo:  **ApplicationDbContext.cs**

Adicionar **DbSet** para criar a tabela referente no banco de dados

```C#
public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
{

}

public DbSet<Person> People {get; set;}
```

Na pasta Mvc dentro do arquivo **Startup.cs** Temos que configurar a class de context e a conexão com o banco de dados

```C#
public void ConfigureServices(IServiceCollection services)
{

    //Configurando class de context
    services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
    );

    services.Configure<CookiePolicyOptions>(options =>
    {
        options.CheckConsentNeeded = context => true;
        options.MinimumSameSitePolicy = SameSiteMode.None;
    });


    services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
}
```

### Comando para gerar o migration

Na pasta Data
```sh
cd Data
```
Cria o migration
```sh
dotnet ef --startup-project ../Mvc/Mvc.csproj migrations add AddPerson
```
Atualiza o banco com o migration criado
```sh
dotnet ef --startup-project ../Mvc/Mvc.csproj database update
```

# Projeto Mvc

Criar arquivo na pasta Mvc/Controller, arquivo: **PresonController.cs**

Adicionar o seguinte trecho criar acesso a class ApplicationDbContext 

```C#
private readonly ApplicationDbContext _context;

public PersonController(ApplicationDbContext context)
{
    _context = context;
}
```

### Adicionar na pasta Views 

```sh
mkdir Person
```

```sh
cd Person
```

criar arquivo **Register.cshtml** codigo esta no arquivo para mais detalhes.

criar arquivo **Index.cshtml** codigo esta no arquivo para mais detalhes.


### Criar Controller Index

No arquivo PersonController.cs adicionar o seguinte codigo para exibir uma listagem de pessoas

```C#
[HttpGet]
public IActionResult Index()
{
    var people = _context.People.ToList();
    return View(people);
}
```





