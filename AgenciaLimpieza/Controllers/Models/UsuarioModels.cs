namespace AgenciaLimpieza.Controllers.Models
{
    public class UsuarioModels
    {
        public class RegisterUser
        {
            public int UserID { get; set; }
            public string? Name { get; set; }
            public string? Email { get; set; }
            public string? Phone { get; set;}
            public string? Password { get; set;}
            public int RoleID { get; set; }
        }

        public class Session
        {
            public int ID { get; set; }
            public RegisterUser? User { get; set; }
            public string? Auth { get; set; }
        }
        public class Roles
        {
            public int RoleID { get; set; }
            public string? RoleName { get; set; }
        }
    }
}
