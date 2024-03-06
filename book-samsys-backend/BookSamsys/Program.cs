using BookSamsys.DAL.Repositories.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); //ambiente da API

//Usar DbContext
builder.Services.AddDbContext<LivroContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), //instalar pacote NuGet
    options => options.MigrationsAssembly("BookSamsys.DAL")); //definir para onde vão as migrações

});

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
