using ControleDespesasCasa.Api.Data;
using ControleDespesasCasa.Api.Interfaces.Repositories;
using ControleDespesasCasa.Api.Interfaces.Services;
using ControleDespesasCasa.Api.Repositories;
using ControleDespesasCasa.Api.Services;
using Microsoft.EntityFrameworkCore;

// Programa de inicialização da API: configura serviços, dependências e pipeline HTTP.
// Comentários abaixo explicam a função de cada bloco de configuração.

var builder = WebApplication.CreateBuilder(args);

// Adiciona suporte a controllers (API controllers usando atributos e rotas convencionais)
builder.Services.AddControllers();

// Configura o DbContext da aplicação apontando para uma base SQLite definida em
// appsettings.json (chave: "DefaultConnection"). O DbContext será injetado onde
// for necessário para acessar/gerenciar entidades e a base de dados.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registra implementações concretas para os repositórios e serviços usados pela API
// usando o tempo de vida 'Scoped' (uma instância por request HTTP). Isso habilita
// injeção de dependência em controllers e outros serviços.
builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddScoped<IPersonService, PersonService>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
builder.Services.AddScoped<ITransactionService, TransactionService>();

// Serviço de relatório não tem interface; também registrado como Scoped para uso
// direto por controllers ou outros serviços.
builder.Services.AddScoped<ReportService>();

// Adiciona geração de documentação OpenAPI/Swagger para facilitar testes e
// exploração dos endpoints durante o desenvolvimento.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configura política de CORS permitindo que o frontend acesse a API. Atualmente
// está aberto (AllowAnyOrigin/AllowAnyHeader/AllowAnyMethod) para facilitar
// desenvolvimento; em produção restrinja a origem e cabeçalhos permitidos.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

// Em ambiente de desenvolvimento, habilita UI do Swagger para inspecionar e testar
// os endpoints HTTP expostos pela aplicação.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Força redirecionamento para HTTPS para proteger tráfego em produção.
app.UseHttpsRedirection();

// Aplica a política de CORS configurada anteriormente.
app.UseCors("AllowFrontend");

// Habilita middleware de autorização. Autenticação/autorizações específicas devem
// ser configuradas separadamente (ex.: JWT, Identity) quando necessário.
app.UseAuthorization();

// Mapeia controllers para rotas e inicia a aplicação.
app.MapControllers();

app.Run();
