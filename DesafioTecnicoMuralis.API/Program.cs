using AutoMapper;
using DesafioTecnicoMuralis.API.SwaggerExemplos;
using DesafioTecnicoMuralis.Application.Interfaces.Repository;
using DesafioTecnicoMuralis.Application.Interfaces.Service;
using DesafioTecnicoMuralis.Application.Services;
using DesafioTecnicoMuralis.Infrastructure.Configurarions;
using DesafioTecnicoMuralis.Infrastructure.Repositories;
using DesafioTecnicoMuralis.Infrastructure.Servies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => c.SchemaFilter<ClienteDtoExample>());
builder.Services.AddDbContext<AppDbContext>(option => option.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddHttpClient<ICepService, ViaCepService>();
builder.Services.AddScoped<IEnderecoRepository, EnderecoRepository>();
builder.Services.AddScoped<IContatoRepository, ContatoRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();

var mapper = app.Services.GetRequiredService<IMapper>();
mapper.ConfigurationProvider.AssertConfigurationIsValid();