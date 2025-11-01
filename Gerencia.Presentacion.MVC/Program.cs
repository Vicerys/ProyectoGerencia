var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient<Gerencia.Presentacion.MVC.Services.Empleado_Api>((sp, http) =>
{
    var cfg = sp.GetRequiredService<IConfiguration>();
    var baseUrl = cfg["Api:BaseUrl"] ?? "http://localhost:5110//";
    http.BaseAddress = new Uri(baseUrl);
})
    .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
    {
        ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
    });

builder.Services.AddHttpClient<Gerencia.Presentacion.MVC.Services.EmpleadoProyecto_Api>((sp, http) =>
{
    var cfg = sp.GetRequiredService<IConfiguration>();
    var baseUrl = cfg["Api:BaseUrl"] ?? "http://localhost:5110//";
    http.BaseAddress = new Uri(baseUrl);
});

builder.Services.AddHttpClient<Gerencia.Presentacion.MVC.Services.Equipo_Api>((sp, http) =>
{
    var cfg = sp.GetRequiredService<IConfiguration>();
    var baseUrl = cfg["Api:BaseUrl"] ?? "http://localhost:5110//";
    http.BaseAddress = new Uri(baseUrl);
});

builder.Services.AddHttpClient<Gerencia.Presentacion.MVC.Services.Proceso_Api>((sp, http) =>
{
    var cfg = sp.GetRequiredService<IConfiguration>();
    var baseUrl = cfg["Api:BaseUrl"] ?? "http://localhost:5110//";
    http.BaseAddress = new Uri(baseUrl);
});

builder.Services.AddHttpClient<Gerencia.Presentacion.MVC.Services.Proyecto_Api>((sp, http) =>
{
    var cfg = sp.GetRequiredService<IConfiguration>();
    var baseUrl = cfg["Api:BaseUrl"] ?? "http://localhost:5110//";
    http.BaseAddress = new Uri(baseUrl);
});

builder.Services.AddHttpClient<Gerencia.Presentacion.MVC.Services.Tarea_Api>((sp, http) =>
{
    var cfg = sp.GetRequiredService<IConfiguration>();
    var baseUrl = cfg["Api:BaseUrl"] ?? "http://localhost:5110//";
    http.BaseAddress = new Uri(baseUrl);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
