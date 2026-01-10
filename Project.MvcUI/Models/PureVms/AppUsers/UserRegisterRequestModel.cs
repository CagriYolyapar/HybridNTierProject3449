using System.ComponentModel.DataAnnotations;

namespace Project.MvcUI.Models.PureVms.AppUsers
{
    //Todo : Refactor (Validation sistemi , confirm password property'si ekleyiniz)
    //Todo : SignIn icin ayrı bir RequestModel olusturun...
    public class UserRegisterRequestModel
    {
        
        public string UserName { get; set; }
        public string Password { get; set; }
       
        public string Email { get; set; }

    }
}
