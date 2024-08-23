using System.ComponentModel.DataAnnotations;

namespace Rac.Web.Models
{
    public class LoginViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
