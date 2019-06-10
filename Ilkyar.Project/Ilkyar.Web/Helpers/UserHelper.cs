using Ilkyar.Contracts.Entities.DTO;
using System.Web;

namespace Ilkyar.Web.Helpers
{
    public static class UserHelper
    {
        public static UserDTO CurrentUser
        {
            get
            {
                ///TODO: login işlemi devreye alındıktan sonra burası düzenlenecek
                var currentUser = new UserDTO();
                if (HttpContext.Current.Session["CurrentUser"] != null)
                    currentUser = HttpContext.Current.Session["CurrentUser"] as UserDTO;
                return currentUser;
            }
        }

        public static long UserId
        {
            get
            {
                return CurrentUser.UserId;
            }
        }

        public static int UserTypeId
        {
            get
            {
                return CurrentUser.UserTypeId;
            }
        }
            

        public static string Username
        {
            get
            {
                return CurrentUser.Username;
            }
        }

        public static string FirstLastName
        {
            get
            {
                return $"{CurrentUser.FirstName} {CurrentUser.LastName}";
            }
        }
    }
}