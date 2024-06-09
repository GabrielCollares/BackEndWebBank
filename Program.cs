using BackendSharp.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<AppDbContext>();
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "MyCorsPolicy",
                      builder =>
                      {
                          builder.WithOrigins("https://effective-space-orbit-7jrqpvxwrjphpxq5-5173.app.github.dev", "http://localhost:5173")
                                 .AllowAnyMethod() // Permite todos os métodos, ou especifique os desejados
                                 .AllowAnyHeader(); // Permite todos os cabeçalhos, ou especifique os desejados
                      });
});

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors("MyCorsPolicy");
}

app.UseHttpsRedirection();

UsersRotas.AddRotasUsers(app);
// apply any pending migration before run app
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<AppDbContext>();
    if (context.Database.GetPendingMigrations().Any())
    {
        context.Database.Migrate();
    }
}

app.Run();

