using System.Web.Mvc;

namespace InternetCoast.Infrastructure.UI.MVC
{
    public class BaseController : Controller
    {
        public UserProfile UserProfile
        {
            get
            {
                if (Session["UserProfile"] == null)
                    Session["UserProfile"] = new UserProfile();

                var userProfile = (UserProfile)Session["UserProfile"];

                return userProfile;
            }
            
            set { SetUserProfile(value); }
        }

        public void SetUserProfile(UserProfile data)
        {
            Session["UserProfile"] = data;
        }

        public InternetCoast.Infrastructure.Data.EF.Context.UiContext UiContext
        {
            get
            {
                if (Session["UiContext"] == null)
                    Session["UiContext"] = new InternetCoast.Infrastructure.Data.EF.Context.UiContext();

                var uiContext = (InternetCoast.Infrastructure.Data.EF.Context.UiContext)Session["UiContext"];

                return uiContext;
            }

            set { SetUiContext(value); }
        }

        public void SetUiContext(InternetCoast.Infrastructure.Data.EF.Context.UiContext data)
        {
            Session["UiContext"] = data;
        }
    }
}
