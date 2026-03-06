var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


// Add the middleware to return requests for CSS, JS, HTML and other static content from wwwroot.
// NOTE: Ensure it is added before "app.UseRouting()"!
app.UseStaticFiles();      

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();          // Serves library/package assets (generated during the build & bundling process)

app.MapRazorPages()
   .WithStaticAssets();

app.Run();
