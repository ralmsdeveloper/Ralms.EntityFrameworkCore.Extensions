
Ralms.EntityFrameworkCore.Extensions for EntityFramework Core
 
##  Supports: 
DateDiff & Query Hints (Only SQL Server)


## Example of use DateDiff

 ```csharp
 var list = db
    .People
    .Where(p => EFCore.DateDiff(DatePart.day, DateTime.Now, p.Birthday) < 50)
    .ToList();  
```

## Example of use Ralms.EntityFrameworkCore.Extensions


 ```csharp

 // Enable Extension
 protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
{
    optionsBuilder
        .UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=SampleExtension;Integrated Security=True;")
        .RalmsExtendFunctions();
}

// Sample Use
var list = db
    .People
    .WithHint(SqlServerHints.NOLOCK)
    .Take(1)
    .ToList(); 

var list = db
    .People
    .WithHint(SqlServerHints.UPDLOCK)
    .Take(1)
    .ToList();
```
 ```sql
SELECT [b].[Id], [b].[Date], [b].[Name]
FROM [Blogs] AS [b] WITH (UPDLOCK) 
```


## Example of use ToSql
 * New extension to design the SQL output


 ```csharp
 var sql = _db
    .Blogs
    .Where(p => EFCore.DateDiff(DatePart.day, DateTime.Now, p.Date) < 50)
    .ToSql();
```

**Output SQL**

```SQL
SELECT [p].[Id], [p].[Date], [p].[Name]
FROM [Blogs] AS [p]
WHERE DATEDIFF(day, GETDATE(), [p].[Date]) < 50
```
