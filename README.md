# URL Shortening Service

A simple RESTful API that allows users to shorten long URLs. The API provides endpoints to create, retrieve, update, and delete short URLs. It also provides statistics on the number of times a short URL has been accessed.

# Running the .NET 6 API
Follow these steps to run the API project on your local machine:

## Prerequisites
1. NET 6 SDK: Make sure the .NET 6 SDK is installed on your machine. 

2. IDE or Editor: You can use an Integrated Development Environment (IDE) like Visual Studio or a code editor like Visual Studio Code with the C# extension.

## Steps to Run the Project
1. **Clone the repository** containing the project:
```
git clone https://github.com/labr1younes/URL-Shortening-Service.git
cd URL-Shortening-Service
```
2. **Open the project** with Visual Studio or your preferred IDE. 
3. **Configure the database connection :**
- Open "appsettings.json" file and change the "DefaultConnection" string to :
```
"DefaultConnection": "<Write your connection string here>"
```
- example : 
```
Data Source=SQL_Server_url;Initial Catalog=database;Integrated Security=True;Trust Server Certificate=True
```
- **Open "YURL_ShortenerContext.cs"** file and and add the same connection string inside :
```
 optionsBuilder.UseSqlServer("<Write your connection string here>") 
```
4.**Install necessary packages**: Run the following commands in the terminal to install Entity Framework Core packages:
```
install-package Microsoft.EntityFrameworkCore -version 6.0.33
install-package Microsoft.EntityFrameworkCore.SqlServer -version 6.0.33
install-package Microsoft.EntityFrameworkCore.Tools -version 6.0.33
```
5. **Create and apply the database migration**:
- Create an initial migration :
```
dotnet ef migrations add InitialCreate
```
- Apply the migration to your database :
```
dotnet ef database update
```
6. **Run the application**: Now you can run the application using:
```
dotnet run
```
