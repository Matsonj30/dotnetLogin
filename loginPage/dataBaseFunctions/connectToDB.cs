namespace dataBaseQueries;
using MySqlConnector;
using System.Security.Cryptography;


public class connectToDB{

    public MySqlConnection Connect(){
        string connectionString = "Server=127.0.0.1; User ID=root ;Password= Password123!;Database=loginauthentication";
        //var means you are letting the compiler determine the type for you
        
        var connection = new MySqlConnection(connectionString);
        try{
            connection.Open();
            Console.WriteLine("Connected Successfully");

            return connection;
        }
        catch (Exception ex){
            Console.WriteLine("Error: " +ex.Message); //print error to console
            return null;
        }
        
    }

    //When a new account is created, we want to CreateAccount with these variables
    public bool CreateAccount(string username, string password){ //password needs to be hashed somewhere
    
      //WE SHOULD HASH HERE BUT THATS FOR ANOTHER DAY
      //______________________________________________
    //the @ makes it multiline  
        string insertQuery = $@" 
                        INSERT INTO logins (username, password_hash)
                        VALUES(@username, @password)
                        ";
            
        var connection = Connect();

        if(connection != null){
            MySqlCommand query = new MySqlCommand(insertQuery, connection);

            using(connection){ //using will ensure it is closed afterwards
            
                query.Parameters.AddWithValue("@username", username); //we do it this way to prevent injection, as this function knows how to treat any input
                query.Parameters.AddWithValue("@password", password);

                    int rowsAffected = query.ExecuteNonQuery();
                    if(rowsAffected > 0){
                        Console.WriteLine("Account Created Successfully");
                        return true;
                    }
                    else{
                        Console.WriteLine("Account Creation Failed");
                        return false;
                    }
              
      
              

            }
        } //ensure its not broken
        else{
            return false;
        }
     

    }
   


}
