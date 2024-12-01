UrlShortener
This project is a simple URL Shortener web application built with ASP.NET Core and Entity Framework Core (EF Core). 
The application allows users to generate shortened URLs for long hyperlinks, store them in a SQLite database, and 
redirect users to the original URL when the shortened URL is accessed.

Features
1 - Users can input a long URL, and the application generates a unique 5-character short code.
The shortened URL is displayed as https://www.focusmr.de/{shortCode}.
2 - When the shortened URL is clicked, the application retrieves the corresponding long URL from the database and redirects the user.
3 - The original URLs and their corresponding short codes are stored in a SQLite database.

Project Structure
Controller:
HomeController.cs: Contains the main logic for generating short codes and handling redirection.
* It contains 2 main functions:  
Create(Http Post): It creates 5 letter long string (url shortener) and save it along original url in sqLite Db after checking if we have any original address to shorten. 
RedirectToOriginal(Http Get) : It brings the original Url from Db and redirects the shortened Url to the original one.
* It also consists of a function to check whether our input is being saved in the DB.
DebugUrls(Http Get)  : Its checks the saved entries in the Database that are made.  

Models:
ShortenedUrl.cs: Represents the URL data structure (original URL and short code).

Views:
Index.cshtml: The main page for entering URLs and displaying results.

Database:
SQLite database for storing URLs and their corresponding short codes.
