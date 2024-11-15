using Microsoft.EntityFrameworkCore;
using Store.Omda.Core.Services.Contract;
using Store.Omda.Core;
using Store.Omda.Repository;
using Store.Omda.Repository.Data.Contexts;
using Store.Omda.Service.Services.Products;
using Store.Omda.Core.Mapping.Products;
using Microsoft.AspNetCore.Mvc;
using Store.Omda.APIs.Errors;
using Store.Omda.Core.Repositories.Contract;
using Store.Omda.Repository.Repositories;
using StackExchange.Redis;
using Store.Omda.Core.Mapping.Baskets;
using Store.Omda.Service.Services.Caches;
using Store.Omda.Repository.Identity.Contexts;
using Microsoft.AspNetCore.Identity;
using Store.Omda.Core.Entities.Identity;
using Store.Omda.Service.Services.Token;
using Store.Omda.Service.Services.Users;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Store.Omda.Core.Mapping.Auth;
using Store.Omda.Core.Mapping.Orders;
using Store.Omda.Service.Services.Orders;
using Store.Omda.Service.Services.Basket;
using Store.Omda.Service.Services.Payment;

namespace Store.Omda.APIs.Helper
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDependency(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddBuiltInService();
            services.AddIdentityService();
            services.AddSwaggerService();
            services.AddDbContextService(configuration);
            services.AddUserDefinesService();
            services.AddAutoMapperService(configuration);
            services.ConfigureInvalidModelStateResponse();
            services.AddRedisService(configuration);
            services.AddAuthenticationService(configuration);

            return services;
        }
        private static IServiceCollection AddBuiltInService(this IServiceCollection services) 
        {
            services.AddControllers();

            return services;
        }

        private static IServiceCollection AddSwaggerService(this IServiceCollection services) 
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            return services;
        }


        private static IServiceCollection AddDbContextService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddDbContext<StoreIdentityDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("IdentityConnection"));
            }); 

            return services;
        }

        private static IServiceCollection AddUserDefinesService(this IServiceCollection services)
        {
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ICacheService, CacheService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IBasketService, BasketService>();
            services.AddScoped<IPaymentService, PaymentService>();
            
            services.AddScoped<IBasketRepository, BasketRepository>();
            

            return services;
        }

        private static IServiceCollection AddAutoMapperService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(M => M.AddProfile(new ProductProfile(configuration)));
            services.AddAutoMapper(M => M.AddProfile(new BasketProfile()));
            services.AddAutoMapper(M => M.AddProfile(new AuthProfile()));
            services.AddAutoMapper(M => M.AddProfile(new OrderProfile(configuration)));

            return services;
        }

        private static IServiceCollection ConfigureInvalidModelStateResponse(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (actionContext) =>
                {
                    var errors = actionContext.ModelState.Where(p => p.Value.Errors.Count() > 0)
                                             .SelectMany(p => p.Value.Errors)
                                             .Select(e => e.ErrorMessage)
                                             .ToArray();


                    var response = new ApiValidationErrorResponse()
                    {
                        errors = errors
                    };

                    return new BadRequestObjectResult(response);
                };
            });

            return services;
        }

        private static IServiceCollection AddRedisService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IConnectionMultiplexer>((serviceProvider) =>
            {
                var connection = configuration.GetConnectionString("Redis");

                return ConnectionMultiplexer.Connect(connection);
            }
            ); 

            return services;
        }

        private static IServiceCollection AddIdentityService(this IServiceCollection services)
        {
            services.AddIdentity<AppUser, IdentityRole>()
                    .AddEntityFrameworkStores<StoreIdentityDbContext>();

            return services;
        }
        private static IServiceCollection AddAuthenticationService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme= JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme= JwtBearerDefaults.AuthenticationScheme;
            }
            ).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
                };
            });

            return services;
        }

    }
}
