namespace BackendSharp.Users;



public class User {
    public Guid Id { get; init; }
    public string Cpf { get; private set; }
    public string Name { get; private set; }
    public string Phone { get; private set; } 
    public string Email { get; private set; } 
    public string Password { get; private set; } 

    public bool Activated { get; private set; }

    public User( string cpf, string name, string phone, string email, string password ) {

        Id =  Guid.NewGuid();
        Cpf = cpf;  
        Name = name;    
        Phone = phone;
        Email = email;
        Password = password;
        Activated = true;
    }

    public void AtualizarUsers(string cpf, string name, string phone, string email, string password){
        Cpf = cpf;  
        Name = name;    
        Phone = phone;
        Email = email;
        Password = password;
    }


    public void Disable () {
        Activated = false;
    }
}  


