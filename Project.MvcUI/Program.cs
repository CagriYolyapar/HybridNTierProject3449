using Project.Bll.DependencyResolvers;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContextService();
builder.Services.AddIdentityService();
builder.Services.AddRepositoryServices();
builder.Services.AddManagerServices();

builder.Services.AddHttpClient(); //Eger bir API consume edilecekse HTTP client tarafında oldugunuzu Middleware'e belirtmek icin bu ifadeyi kullanmalısınız...


builder.Services.AddDistributedMemoryCache(); //Eger kompleks yapılarla session'da calısacaksanız dagıtık memori sistemi en saglıklısıdır

builder.Services.AddSession(x =>
{
    x.IdleTimeout = TimeSpan.FromDays(1);
    x.Cookie.HttpOnly = true;
    x.Cookie.IsEssential = true;
});

builder.Services.ConfigureApplicationCookie(x =>
{
    x.AccessDeniedPath = "/Home/SignIn";
    x.LoginPath = "/Home/SignIn";
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=SignIn}/{id?}");

app.Run();
