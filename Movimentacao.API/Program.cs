using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Movimentacao.Application.Mapping;
using Movimentacao.Application.Model;
using Movimentacao.Application.Validations;
using Movimentacao.Data.Repositories;
using Movimentacao.Domain.Configs;
using Movimentacao.Domain.Interfaces;
using Movimentacao.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

ConfigureServices(builder.Services, builder.Configuration);

void ConfigureServices(IServiceCollection services, ConfigurationManager configuration)
{
    services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(PostMovimentoRequest).Assembly));

    services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

    services.AddAutoMapper(typeof(MovimentoProfile).Assembly);

    services.AddFluentValidationAutoValidation();
    services.AddValidatorsFromAssemblyContaining<MovimentoValidator>();

    services.Configure<Configs>(configuration.GetSection("Configs"));

    services.AddTransient<IProdutoCosifRepository, ProdutoCosifRepository>();
    services.AddTransient<IProdutoRepository, ProdutoRepository>();
    services.AddTransient<IMovimentoManualRepository, MovimentoManualRepository>();
}

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularDevServer", policy =>
    {
        policy.WithOrigins("*")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors("AllowAngularDevServer");

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
}

app.Run();
