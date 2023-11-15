using AccountServer.DB;
using AccountServer.Utils;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;

namespace AccountServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            IConfigurationRoot configuration = new ConfigurationBuilder()
                                                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                                                .AddJsonFile("config.json")
                                                .Build();

            string connectionString = configuration.GetConnectionString("MyConnection");

            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseMySQL(connectionString));

            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
                options.JsonSerializerOptions.DictionaryKeyPolicy = null;
            });

            builder.Services.AddSwaggerGen();

            builder.Services.AddControllersWithViews();
            builder.Services.AddMvc().AddRazorRuntimeCompilation();

            builder.Services.AddSingleton<PasswordEncryptor>();

            builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                                       .AddEnvironmentVariables();

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseHttpsRedirection();
            }

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            UseSwagger(app);

            app.UseForwardedHeaders();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }

        private static void UseSwagger(WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                options.RoutePrefix = string.Empty;
            });
        }
    }
}