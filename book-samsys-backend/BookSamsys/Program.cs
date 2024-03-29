using BookSamsys.BLL.Services;
using BookSamsys.DAL.Context;
using BookSamsys.DAL.Repositories;
using BookSamsys.Infrastructure.Interfaces.Books;
using BookSamsys.Infrastructure.Mappings;
using FluentAssertions.Common;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using System.Net.Mime;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); //ambiente da API

builder.Services.AddTransient<BookContext>();
builder.Services.AddTransient<IBookRepository, BookRepository>();

//Adicionar cors -> para ser lido pelo frontend
builder.Services.AddCors(p => p.AddPolicy("corspolicy", build => {
    build.WithOrigins("http://localhost:3000").AllowAnyMethod().AllowAnyHeader();
}));



builder.Services.AddDbContext<BookContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), //instalar pacote NuGet
    options => options.MigrationsAssembly("BookSamsys.DAL")); //definir para onde v�o as migra��es
});

//INJECAO DE DEPENDENCIA
//Adicionar servi�o e reposit�rio
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

//cors
app.UseCors("corspolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
