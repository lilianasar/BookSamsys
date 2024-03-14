using BookSamsys.BLL.Services;
using BookSamsys.DAL.Context;
using BookSamsys.DAL.Repositories;
using BookSamsys.Infrastructure.Interfaces.Books;
using BookSamsys.Infrastructure.Mappings;
using FluentAssertions.Common;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); //ambiente da API

builder.Services.AddDbContext<BookContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), //instalar pacote NuGet
    options => options.MigrationsAssembly("BookSamsys.DAL")); //definir para onde vão as migrações
});

//INJECAO DE DEPENDENCIA
//Adicionar serviço e repositório
//builder.Services.AddDbContextFactory<BookContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IBookRepository, BookRepository>();

//AutoMapper - Book para BookDTO
builder.Services.AddAutoMapper(typeof(EntityToDTOMappingProfile));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
