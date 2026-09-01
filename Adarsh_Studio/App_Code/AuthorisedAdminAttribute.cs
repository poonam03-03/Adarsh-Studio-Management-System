using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Adarsh_Studio.App_Code
{
    public class AuthorisedAdminAttribute:Attribute,IAuthorizationFilter

    {
        public void OnAuthorization(AuthorizationFilterContext filterContext)
        {
            if (filterContext.HttpContext.Session.GetString("aid") != null)
            {
                Console.WriteLine("Valid User");
            }

            else
            {
                filterContext.Result = new RedirectResult("/Adarsh/Login");
            }
        }
    }
}
