namespace BackendSharp.Users;


public record AddUserRquest (string Cpf, string Name, string Phone, string Email, string Password);