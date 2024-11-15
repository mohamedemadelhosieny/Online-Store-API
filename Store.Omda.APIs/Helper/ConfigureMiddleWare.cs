using Store.Omda.APIs.Middlewares;
using Store.Omda.Repository.Data.Contexts;
using Store.Omda.Repository.Data;
using Microsoft.EntityFrameworkCore;
using Store.Omda.Repository.Identity.Contexts;
using Store.Omda.Repository.Identity.DataSeed;
using Microsoft.AspNetCore.Identity;
using Store.Omda.Core.Entities.Identity;

namespace Store.Omda.APIs.Helper
{
    public static class ConfigureMiddleWare
    {
        public static async Task<IApplicationBuilder> ConfigureMiddleWareAysnc(this WebApplication app) 
        {
            using var scope = app.Services.CreateScope();

            var services = scope.ServiceProvider;

            var context = services.GetRequiredService<StoreDbContext>();
            var identityContext = services.GetRequiredService<StoreIdentityDbContext>();
            var userManager = services.GetRequiredService<UserManager<AppUser>>();
            var loggerfactory = services.GetRequiredService<ILoggerFactory>();

            try
            {
                await context.Database.MigrateAsync();

                await StoreDbContextSeed.SeedAsync(context);

                await identityContext.Database.MigrateAsync();

                await StoreIdentityDbSeeding.SeedAppUserAsync(userManager);

            }
            catch (Exception ex)
            {
                var logger = loggerfactory.CreateLogger<Program>();
                logger.LogError(ex, "Migration Error");
            }

            app.UseMiddleware<ExceptionMiddleWare>();

            // Configure the HTTP request pipeline.
            
             app.UseSwagger();
             app.UseSwaggerUI();
            

            app.UseStatusCodePagesWithReExecute("/error/{0}");

            app.UseStaticFiles();

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();   


            return app;
        }
    }
}
