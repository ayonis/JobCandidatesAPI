Overview
The Candidate API provides an endpoint to manage candidate information, including adding, updating, retrieving, and deleting candidate records. 
This README will guide you through setting up, using, and testing the API.

Features
 - Add new candidate
 - Update existing candidate
 - Retrieve candidate by ID
 - Delete candidate by ID
 - Get a list of all candidates
   
Technologies Used
 - .NET Core
 - Entity Framework Core
 - Moq (for unit testing)
   
Prerequisites
 - .NET Core SDK
 - SQL Server (or any other database supported by Entity Framework Core)

Setup
 1- Clone the repository:
    - git clone https://github.com/ayonis/JobCandidatesAPI.git
    - cd JobCandidatesAPI
    
 2- Set up the database:
    Ensure your database is set up and configured correctly in the appsettings.json file.

 3- Run migrations:
    dotnet ef database update

 4- Run the application:
    dotnet run

Endpoints
    Add a New Candidate
        URL: /api/candidates
        Method: POST
        Request Body
          {
                "Email": "example@gmail.com",
                "First_name": "John",
                "Last_name": "Doe",
                "Phone_number": "+123456789",
                "Time_interval_call": "Anytime",
                "LinkedIn_profile_URL": "https://www.linkedin.com/in/johndoe",
                "GitHub_profile_URL": "https://github.com/johndoe",
                "comment": "No comment"
            }

    Update an Existing Candidate
        URL: /api/candidates/{id}
        Method: PUT
        Request Body:
          {
              "Email": "example@gmail.com",
              "First_name": "John",
              "Last_name": "Doe",
              "Phone_number": "+123456789",
              "Time_interval_call": "Anytime",
              "LinkedIn_profile_URL": "https://www.linkedin.com/in/johndoe",
              "GitHub_profile_URL": "https://github.com/johndoe",
              "comment": "Updated comment"
          }

   Retrieve Candidate by ID
        URL: /api/candidates/{id}
        Method: GET
        Response:       
          {
              "Email": "example@gmail.com",
              "First_name": "John",
              "Last_name": "Doe",
              "Phone_number": "+123456789",
              "Time_interval_call": "Anytime",
              "LinkedIn_profile_URL": "https://www.linkedin.com/in/johndoe",
              "GitHub_profile_URL": "https://github.com/johndoe",
              "comment": "No comment"
          }

  Delete Candidate by ID
        URL: /api/candidates/{id}
        Method: DELETE
        
  Get List of All Candidates
        URL: /api/candidates
        Method: GET
        Response:
    
          [
              {
                  "Email": "example@gmail.com",
                  "First_name": "John",
                  "Last_name": "Doe",
                  "Phone_number": "+123456789",
                  "Time_interval_call": "Anytime",
                  "LinkedIn_profile_URL": "https://www.linkedin.com/in/johndoe",
                  "GitHub_profile_URL": "https://github.com/johndoe",
                  "comment": "No comment"
              },
              ...
          ]
          
Unit Tests
  The project includes unit tests to ensure the correctness of the implemented features.

Running Unit Tests
  1- Navigate to the test project directory: 
    cd Test_Task.Tests.
  2. Run the tests:
    dotnet test

Contact
   For any inquiries or issues, please contact me via email at abdelrhmansayedyounis@gmail.com.

Conclusion
Thank you for using the Candidate API! Feel free to contribute or raise issues on the GitHub repository.





    
