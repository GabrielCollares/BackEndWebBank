
namespace BackendSharp.Users;

public record UpdateUserRequest(string Cpf, string Name, string Phone, string Email, string Password)
{

}

