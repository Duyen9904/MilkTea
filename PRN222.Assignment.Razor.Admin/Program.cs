using Microsoft.AspNetCore.Authentication.Cookies;
using PRN222.Assignment.Razor.Admin.Components;
using PRN222.Assignment.Repositories.Repositories.Interface;
using PRN222.Assignment.Repositories.Repositories;
using Microsoft.EntityFrameworkCore;
using PRN222.Assignment.Repositories.Entities;
using PRN222.Assignment.Services.CustomerService;
using PRN222.Assignment.Services.Interfaces;
using PRN222.Assignment.Services.Implementations;
using RazorPage;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder => builder
        .WithOrigins("https://localhost:7130")  // Client app's URL
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials());
});

builder.Services.AddSignalR();
builder.Services.AddDbContext<MilkTeaShopContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/login";       // Path to your login page
        options.LogoutPath = "/logout";      // Path to handle logout
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
    });

builder.Services.AddAuthorization();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IComboService, ComboService>();
builder.Services.AddScoped<IComboItemService, ComboItemService>();
builder.Services.AddScoped<IDashboardService, DashboardService>();
builder.Services.AddScoped<IMilkTeaProductService, MilkTeaProductService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderComboService, OrderComboService>();
builder.Services.AddScoped<IOrderItemService, OrderItemService>();
builder.Services.AddScoped<IOrderItemToppingService, OrderItemToppingService>();
builder.Services.AddScoped<IProductSizeService, ProductSizeService>();
builder.Services.AddScoped<ISizeService, SizeService>();
builder.Services.AddScoped<IToppingService, ToppingService>();
builder.Services.AddScoped<ITransactionService, TransactionService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAntiforgery();
app.UseCors("CorsPolicy");
app.UseAuthentication();
app.UseAuthorization();
app.MapGet("/", () => Results.Redirect("/Login"));
app.MapRazorPages();
app.MapHub<OrderHub>("/orderHub");
app.Run();
