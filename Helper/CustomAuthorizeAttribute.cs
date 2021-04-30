using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace System.Web.Mvc
{

    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (!this.Roles.Split(',').Any(filterContext.HttpContext.User.IsInRole))
            {
                // If User's role isn't allowed, redirect to restricted page
                filterContext.Result = new ViewResult
                {
                    ViewName = "~/Views/Shared/Restricted.cshtml"
                };
            }
            else
            {
                base.HandleUnauthorizedRequest(filterContext);
            }
        }
    }
}