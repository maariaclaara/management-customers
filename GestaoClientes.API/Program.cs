using GestaoClientes.Aplicacao.Abstracoes;
using GestaoClientes.Aplicacao.Clientes.Criar;
using GestaoClientes.Aplicacao.Clientes.ObterPorId;
using GestaoClientes.Infraestrutura.Repositorios;

var builder = WebApplication.CreateBuilder(args);

// Controllers
builder.Services.AddControllers();

// DI - Handlers (Aplicação)
builder.Services.AddScoped<CriaClienteCommandHandler>();
builder.Services.AddScoped<ObtemClientePorIdQueryHandler>();

// DI - Repositório (Infraestrutura)
builder.Services.AddSingleton<IClienteRepository, ClienteRepositoryEmMemoria>();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
