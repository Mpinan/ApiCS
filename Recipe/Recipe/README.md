# ApiCS

1 - To start a new project in C#. We start it by File/new project/ and adding ASP.NET core API.
2 - We added Entity Framework packages such as E.F. Tools, SQL, Core..
3 - After we created the Database (recipes) and the tables where we store them.
4 - On tools, we open NuGet Package manager/console and in the console we write down: 

``` scaffold-dbcontext -provider Microsoft.EntityFrameworkCore.SqlServer       -connection "Data Source = (localdb)\mssqllocaldb;Initial Catalog = <YourDB>;" -OutputDir Models -t <YourModels> -f ```

