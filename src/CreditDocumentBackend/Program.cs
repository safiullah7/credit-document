using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CreditDocumentBackend.Data;
using CreditDocumentBackend.Interfaces;
using CreditDocumentBackend.Repositories;
using CreditDocumentBackend.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SharedModels;

namespace CreditDocumentBackend
{
    public class Program
    {
        private static async Task SeedDatabaseAsync(ApplicationDbContext context)
        {
            // Check if there are any existing credit documents
            if (!await context.CreditDocuments.AnyAsync())
            {
                var creditDocuments = new List<CreditDocument>
        {
            // Add 10 mock credit document entries
            new CreditDocument { LoanNumber = "LN0001", CompanyName = "Bolag 1", OrganizationNumber = "123456-7890", State = DocumentState.New, Date = DateTime.Now },
            new CreditDocument { LoanNumber = "LN0002", CompanyName = "Bolag 2", OrganizationNumber = "223456-7890", State = DocumentState.UnderProcessing, Date = DateTime.Now },
            new CreditDocument { LoanNumber = "LN0003", CompanyName = "Bolag 3", OrganizationNumber = "323456-7890", State = DocumentState.OnHold, Date = DateTime.Now },
            new CreditDocument { LoanNumber = "LN0004", CompanyName = "Bolag 4", OrganizationNumber = "423456-7890", State = DocumentState.Completed, Date = DateTime.Now },
            new CreditDocument { LoanNumber = "LN0005", CompanyName = "Bolag 5", OrganizationNumber = "523456-7890", State = DocumentState.New, Date = DateTime.Now },
            new CreditDocument { LoanNumber = "LN0006", CompanyName = "Bolag 6", OrganizationNumber = "623456-7890", State = DocumentState.UnderProcessing, Date = DateTime.Now },
            new CreditDocument { LoanNumber = "LN0007", CompanyName = "Bolag 7", OrganizationNumber = "723456-7890", State = DocumentState.OnHold, Date = DateTime.Now },
            new CreditDocument { LoanNumber = "LN0008", CompanyName = "Bolag 8", OrganizationNumber = "823456-7890", State = DocumentState.Completed, Date = DateTime.Now },
            new CreditDocument { LoanNumber = "LN0009", CompanyName = "Bolag 9", OrganizationNumber = "923456-7890", State = DocumentState.New, Date = DateTime.Now },
            new CreditDocument { LoanNumber = "LN0010", CompanyName = "Bolag 10", OrganizationNumber = "1023456-7890", State = DocumentState.UnderProcessing, Date = DateTime.Now },
        };

                // Add the mock credit documents to the database
                await context.CreditDocuments.AddRangeAsync(creditDocuments);
                await context.SaveChangesAsync();
            }
        }
        public static async Task Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });

            builder.Services.AddControllers();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new() { Title = "CreditDocumentBackend", Version = "v1" });
            });

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<IScriveService, ScriveService>();
            builder.Services.AddScoped<IRepository, Repository>();
            builder.Services.AddHttpClient(); // Add this line

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseCors("AllowAllOrigins");

            app.UseAuthorization();

            app.MapControllers();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CreditDocumentBackend v1"));

            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<ApplicationDbContext>();
            await context.Database.MigrateAsync(); // Applies pending migrations
            await SeedDatabaseAsync(context); // Seed the database with mock data

            await app.RunAsync();

        }
    }
}