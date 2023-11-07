namespace AgenciaLimpieza.Controllers.Models
{
    public class UsuarioModels
    {
        public class RegisterUser
        {
            public string? Name { get; set; }
            public string? Email { get; set; }
            public string? Phone { get; set;}
            public string? Password { get; set;}
        }
    }
}
