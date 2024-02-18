using CustomerBalance.Core.ViewModels;
using CustomerBalance.Server.Data;
using CustomerBalance.Server.Extensions;
using CustomerBalance.Server.Models;
using CustomerBalance.Server.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
        b => b.MigrationsAssembly("CustomerBalance.Api")),
    contextLifetime: ServiceLifetime.Transient,
    optionsLifetime: ServiceLifetime.Transient);

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
    {
        //Sign in options
        options.SignIn.RequireConfirmedAccount = false;
        options.SignIn.RequireConfirmedEmail = false;
        options.SignIn.RequireConfirmedPhoneNumber = false;
        //Password options
        options.Password.RequireDigit = true;
        options.Password.RequiredLength = 6;
        options.Password.RequireLowercase = true;
        options.Password.RequireNonAlphanumeric = true;
        options.Password.RequireUppercase = true;
        options.Password.RequiredUniqueChars = 1; //check if rule can apply without conflicting the existing data
        //Lockout options
        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
        options.Lockout.MaxFailedAccessAttempts = 5;
        options.Lockout.AllowedForNewUsers = true;
        //User options
        options.User.AllowedUserNameCharacters =
            "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
        options.User.RequireUniqueEmail = true;
        //Email Confirmation options
        //options.Tokens.EmailConfirmationTokenProvider = "CustomEmailConfirmation";
    })
    .AddEntityFrameworkStores<AppDbContext>()
    .Services
    .AddSqlServerRepository<Customer>()
    .AddSqlServerRepository<Transaction>();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(b =>
        b.WithOrigins("*")
            .AllowAnyMethod()
            .AllowAnyHeader());
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}
app.UseHsts();

// Migrate database
await using (var scope = app.Services.CreateAsyncScope())
{
    var dataContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await dataContext.Database.MigrateAsync();
}

app.UseCors(b => b
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());
app.UseHttpsRedirection();

app.MapPost("/api/customer", async (IBaseCrud<Customer> repo, Customer data) =>
    {
        data.DateCreated = DateTime.Now;
        return await repo.AddAsync(data);

    }).WithName("AddCustomer")
    .WithOpenApi();

app.MapGet("/api/customer", async (IBaseCrud<Customer> repo) =>
        await repo.GetAllAsync()).WithName("GetCustomers")
    .WithOpenApi();

app.MapGet("/api/customer/{id}", async (IBaseCrud<Customer> repo, Guid? id) =>
        await repo.GetByIdAsync(id)).WithName("GetCustomer")
    .WithOpenApi();

app.MapPut("/api/customer/{id}", async (IBaseCrud<Customer> repo, Customer data) =>
        await repo.UpdateAsync(data)).WithName("UpdateCustomer")
    .WithOpenApi();

app.MapDelete("/api/customer/{id}", async (IBaseCrud<Customer> repo, Guid? id) =>
        await repo.DeleteAsync(id)).WithName("DeleteCustomer")
    .WithOpenApi();

app.MapPost("/api/transaction", async (IBaseCrud<Transaction> repo, Transaction data) =>
    {
        data.DateCreated = DateTime.Now;
        return await repo.AddAsync(data);
    }).WithName("AddTransaction")
    .WithOpenApi();

app.MapPost("/api/get-transaction", async (IBaseCrud<Transaction> repo, IBaseCrud<Customer> crepo, [FromBody]ReportModel model) =>
    {
        var transactions = await repo.GetAllAsync(e =>
            (model.StartDate.HasValue ? e.Date!.Value.Date >= model.StartDate.Value.Date : (e.Date.HasValue || !e.Date.HasValue)) &&
            (model.EndDate.HasValue ? e.Date!.Value.Date <= model.EndDate.Value.Date : (e.Date.HasValue || !e.Date.HasValue)) &&
            (model.CustomerId.HasValue ? e.CustomerId == model.CustomerId : (e.CustomerId.HasValue || !e.CustomerId.HasValue))
        );

        if (transactions.Success)
        {
            foreach (var item in transactions.Data!)
            {
                var customer = await crepo.GetByIdAsync(item?.CustomerId);
                if (customer.Success)
                {
                    item!.Customer = customer.Data;
                }
            }
        }

        return transactions;
    }).WithName("GetTransactions")
    .WithOpenApi();

app.Run();
