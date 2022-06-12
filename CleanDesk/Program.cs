using CleanDesk.Core;
using static CleanDesk.Core.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMvc();
builder.Services.AddHttpContextAccessor();

// Manage Sessions
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromDays(20);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<ISecurity, Security>();
builder.Services.AddTransient<ILocation, Locations>();
builder.Services.AddTransient<IRequest, Requests>();
builder.Services.AddTransient<IRequestArea, RequestAreas>();
builder.Services.AddTransient<IFloor, Floors>();
builder.Services.AddTransient<IStatus, Status>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Login}/{id?}");

app.Run();
