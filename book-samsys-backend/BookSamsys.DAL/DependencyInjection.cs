using BookSamsys.BLL;
using BookSamsys.BLL.Interfaces;
using BookSamsys.BLL.Services;
using BookSamsys.DAL.Context;
using BookSamsys.DAL.Repositories;
using BookSamsys.Infrastructure.Interfaces;
using BookSamsys.Infrastructure.Mappings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookSamsys.Infrastructure {
    public static class DependencyInjection {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration) {
            //Usar DbContext
            services.AddDbContext<BookContext>(options => {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), //instalar pacote NuGet
                options => options.MigrationsAssembly("BookSamsys.DAL")); //definir para onde vão as migrações
            });

            services.AddAutoMapper(typeof(EntityToDTOMappingProfile));

            //Configurar injeção de dependência
            //Repositories
            services.AddScoped<IBookRepository, BookRepository>();

            //Services
            services.AddScoped<IBookService, BookService>();

            return services;
        }





    }
}
