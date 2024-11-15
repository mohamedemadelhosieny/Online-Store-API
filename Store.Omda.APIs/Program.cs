
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Store.Omda.APIs.Errors;
using Store.Omda.APIs.Helper;
using Store.Omda.APIs.Middlewares;
using Store.Omda.Core;
using Store.Omda.Core.Mapping.Products;
using Store.Omda.Core.Services.Contract;
using Store.Omda.Repository;
using Store.Omda.Repository.Data;
using Store.Omda.Repository.Data.Contexts;
using Store.Omda.Service.Services.Products;

namespace Store.Omda.APIs
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddDependency(builder.Configuration);

            var app = builder.Build();

            await app.ConfigureMiddleWareAysnc();

            app.Run();
        }
    }
}
