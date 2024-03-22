using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using dataBaseQueries;

//Start with function visibility
//Then do return type (void if nothing)
//use static if we want it to be a standalone function, not needing to make unique instances
//of it

//EG public static int name(parameter)
//    optional ^


namespace MyApp.Namespace //this is not relevant to the model, only the class inside is

{
    public class indextwoModel : PageModel{ //This is shown in the html file @model, is is associated with that fiel
        public IActionResult OnPost(){ //iActionresult is just a general return type if you are unsure, you can throw HTML pages etc
            string nameSubmitted = Request.Form["email"]; //NAME of input, ID is for js and css obv
            string password = Request.Form["password"]; //SHOULD BE HASHIED


           connectToDB dbConnection = new connectToDB(); //type connectToDB is our class which we have to specific as the type?
           bool accountCreated = dbConnection.CreateAccount(nameSubmitted, password);

            if(accountCreated == true){
                  return Content($"<p>Hello {nameSubmitted}! Account created! </p>", "text/html"); //$ allows you to put variables into strings
            }
            else{
                 return Content($"<p>Hello {nameSubmitted}! Creation Failed </p>", "text/html"); //$ allows you to put variables into strings
            }
        }
    }
}
