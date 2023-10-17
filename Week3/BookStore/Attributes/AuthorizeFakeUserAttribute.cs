using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

// AuthorizeFakeUserAttribute: Sahte kullanıcı yetkilendirme.
// Kullanıcı girişi olup olmadığını kontrol eder.

// AuthorizeFakeUserAttribute: Fake user authorization attribute. Checks whether a user is logged in or not.

namespace BookStore.Attributes
{
    public class AuthorizeFakeUserAttribute : TypeFilterAttribute
    {
        public AuthorizeFakeUserAttribute() : base(typeof(AuthorizeFakeUserFilter))
        {
        }
    }

    public class AuthorizeFakeUserFilter : IAuthorizationFilter
    {

        // OnAuthorization: Bu metod, herhangi bir action'a erişim öncesi çalışır ve kullanıcının oturum açıp açmadığını kontrol eder.
        // Eğer oturum açılmamışsa, UnauthorizedResult döndürülür.
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;
            if (!user.Identity.IsAuthenticated)
            {
                context.Result = new UnauthorizedResult(); //Yetkisiz Kullanıcı - Unauthorized User
            }
        }

    }
}
