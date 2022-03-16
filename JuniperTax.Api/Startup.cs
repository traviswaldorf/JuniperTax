using JuniperTax.Extensions;

namespace JuniperTax.Api
{
    public static class Startup
    {
        public static WebApplication BuildApp(string[]? args = null, Action<IServiceCollection>? serviceRegistrations = null)
        {
            if (args == null)
            {
                args = new string[] { };
            }

            var builder = WebApplication.CreateBuilder(args);

            builder.Configuration.AddEnvironmentVariables();

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddJuniperTax();

            if (serviceRegistrations != null)
            {
                builder.WebHost.ConfigureServices(serviceRegistrations);
            }

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            return app;
        }
    }
}
