# ApiCS

1 - To start a new project in C#. We start it by File/new project/ and adding ASP.NET core API.
2 - We added Entity Framework packages such as E.F. Tools, SQL, Core..
3 - After we created the Database (recipes) and the tables where we store them.
	- We have two ways of creating the database. We can use SQL sytanx  to create our database and its tables and scaffold the model out of it, or, do the migrations after writing the model.
	To do a migration: 
	`add-migration <NameOfMigration>`
	`update-database`

4 - On tools, we open NuGet Package manager/console and in the console we write down: 

``` scaffold-dbcontext -provider Microsoft.EntityFrameworkCore.SqlServer       -connection "Data Source = (localdb)\mssqllocaldb;Initial Catalog = <YourDB>;" -OutputDir Models -t <YourModels> -f ```


5 - In appsettings.json, paste: 
 ` "ConnectionStrings": {
    "<YourName>ConnectionString": "Data Source = (localdb)\\mssqllocaldb;Initial Catalog = <YourNameDB>;"
  },
 `

 6 - In startup.cs file, on the `public void ConfigureServices(IServiceCollection services)` method, paste: 
   `services.AddDbContext<CatDBContext>(options => options.UseSqlServer("Data Source = (localdb)\\mssqllocaldb;Initial Catalog = CatDB;"));`