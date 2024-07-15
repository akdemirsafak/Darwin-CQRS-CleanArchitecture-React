using Darwin.Web.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddHttpClient<IPlanService, PlanService>(opt =>
{
    opt.BaseAddress = new Uri("https://localhost:7006");
});

builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
   
    app.UseHsts();
}

//builder.Services.AddHttpClientServices();


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
