namespace BackendSharp.Users;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public static class UsersRotas {



    public static void AddRotasUsers(this WebApplication app)
    {
    var rotasUsers = app.MapGroup("users");
// POST METOD
    rotasUsers.MapPost(pattern: "register", handler: async (AddUserRquest request, AppDbContext context, CancellationToken ct ) => {

        var existingCpf = await context.Users.AnyAsync(user => user.Cpf == request.Cpf, ct);
        var existingEmail = await context.Users.AnyAsync(user => user.Email == request.Email, ct);
        var existingPhone = await context.Users.AnyAsync(user => user.Phone == request.Phone, ct);
        if (existingCpf || existingEmail || existingPhone) return Results.Conflict(error: "Already Existing");
        var newUser =  new User(request.Cpf,request.Name,request.Phone,request.Email,request.Password);
        var user = await context.Users.AddAsync(newUser);
        await context.SaveChangesAsync(ct);
        var UserReturn = new UserDto(newUser.Id, newUser.Cpf, newUser.Name, newUser.Phone, newUser.Email, newUser.Password);
        return Results.Ok(UserReturn);
    });
            
// GET METOD              
    rotasUsers.MapGet("", async (AppDbContext context, CancellationToken ct) => {

        var users = await context
        .Users
        .Where(user => user.Activated)
        .Select(user => new UserDto(user.Id, user.Cpf, user.Name, user.Phone, user.Email, user.Password))
        .ToListAsync(ct);
        return users;   
        });
// PUT METOD
        rotasUsers.MapPut(pattern: "{id:guid}", handler: async ( Guid id, UpdateUserRequest request, AppDbContext context, CancellationToken ct ) => {
            var user = await context.Users
            .SingleOrDefaultAsync(user => user.Id == id, ct);
            if (user == null) 
            return Results.NotFound();

             user.AtualizarUsers (request.Cpf, request.Name, request.Phone,request.Email, request.Password);
            await context.SaveChangesAsync(ct);
            return Results.Ok(new UserDto(user.Id, user.Cpf, user.Name, user.Phone,user.Email, user.Password));
        });



// DELETE METOD
rotasUsers.MapDelete(pattern: "{id}", handler: async ( Guid id, AppDbContext context, CancellationToken ct) => 
{
    var user = await context.Users
    .SingleOrDefaultAsync(user => user.Id == id, ct);
    if(user == null)
    return Results.NotFound();

    user.Disable();

    await context.SaveChangesAsync(ct);
    return Results.Ok();

});
    }

    }
