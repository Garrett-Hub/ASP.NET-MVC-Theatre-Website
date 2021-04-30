# ASP.NET MVC Theatre Website
This is a collection of code that I contributed for a new website being built for a theatre in Portland.  We created this site using code first ASP.NET MVC along with the Entity Framework.  We used Azure DevOps to manage the project.

For this project I built out the [Production](#production-model) and [ProductionPhoto](#productionphoto-model) models.  Both of these models had full CRUD functionality as well as some other features.  I configured these models to have a One-To-Many relationship in the database, since each ProductionPhoto has an associated Production it's linked to.  After building both models I seeded some productions with photos to the database.

I also created a special type of Identity User role called a [Production Photographer](#productionphotographer-user) that is responsible for managing ProductionPhotos.

---
## Production Model
I created the Production model for the website which keeps track of all the productions the theatre puts on.  I implemented basic CRUD functionality and then styled every page to fit with the chosen theme for the website.  The index page uses boostrap cards to display the productions and has a search bar with pagination.

![Production Index](production-index.png)
*Index page for Production Model with search and pagination*

Each Production requires a Default Photo, so the user is also required to upload a new Production Photo when creating a Production.  Productions and Production Photos are configured with a One-To-Many relationship and the uploaded photo is set as the Production's "Default Photo" when it's created.

![Production Create](production-create.png)
*Creating productions along with a new default Production Photo with preview*

---
## ProductionPhoto Model
I also created the model for keeping track of Production Photos.  The user can upload a photo which is then converted to a `byte[]` and saved to the database.

![Production Photo Index](photos-index.png)
*Index page for Production Photos using bootstrap cards. The Upload, Edit, and Delete buttons are only visible while logged in as a Production Photographer.*

---
## ProductionPhotographer User
This project is using Identity to manage its users. I was tasked with creating a special user role called a "Production Photographer." This type of user would be responsible for managing ProductionPhotos.

CRUD functionality for the ProductionPhoto model was restricted unless logged in as a ProductionPhotographer. To do this I created a custom `[Authorize]` attribute that redirects users to a Restricted Access page if they tried to access ProductionPhoto CRUD pages while not logged in as a ProductionPhotographer.

```
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
```

For testing and development I also implemented a quick login button to login as a Production Photographer that showed up in the bottom right corner of every page when the user was not logged into an account.  