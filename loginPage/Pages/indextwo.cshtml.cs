using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using dataBaseQueries;
using System.Security.Cryptography;
using System.Text;

//Start with function visibility
//Then do return type (void if nothing)
//use static if we want it to be a standalone function, not needing to make unique instances
//of it

//EG public static int name(parameter)
//    optional ^


//MODELS -> Back end database models
//Views -> Front end served to clients
//Controllers -> "Back" end for the front end files that handle POSTs

//MVC (big projects) vs RAzor (small project)
// MVC uses views, not pages folder

//Razer pages are abled to just return other razer pages based on the logic
//MVC uses a controllers page which acts like a menu to return speicifc views

namespace MyApp.Namespace //this is not relevant to the model, only the class inside is

{
    public class indextwoModel : PageModel{ //This is shown in the html file @model, is is associated with that fiel
        public IActionResult OnPost(){ //iActionresult is just a general return type if you are unsure, you can throw HTML pages etc
            string nameSubmitted = Request.Form["email"]; //NAME of input, ID is for js and css obv
            string password = Request.Form["password"]; //SHOULD BE HASHIED HERE

           string hashedPassword = convertToSha256(password);

           connectToDB dbConnection = new connectToDB(); //type connectToDB is our class which we have to specific as the type?
           bool accountCreated = dbConnection.CreateAccount(nameSubmitted, hashedPassword);

            if(accountCreated == true){
                  return Content($"<p>Hello {nameSubmitted}! Account created! </p>", "text/html"); //$ allows you to put variables into strings
            }
            else{
                 return Content($"<p>Hello {nameSubmitted}! Creation Failed </p>", "text/html"); //$ allows you to put variables into strings
            }
        }

        static string convertToSha256(string input){
            //We have to convert string to bytes to use the hash function

            using (SHA256 sha256 = SHA256.Create()){ //ensuring proper disposal of object, it should
            //dispose on its own, this just ensures it is done quicker
                byte[] inputBytes = Encoding.UTF8.GetBytes(input); //this is an array of bytes
                byte [] hashedBytes = sha256.ComputeHash(inputBytes);

                string hashedValue = BitConverter.ToString(hashedBytes).Replace("-","").ToLower();
                //this pops out 32 hexadecimal characters, which are ofc 2 bytes each = 265 bits
                //replace dashes with blanks, then lowercase everything
                return hashedValue;
            }

        }
    }
}
