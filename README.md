
Ralms.EntityFrameworkCore.Extensions for EntityFramework Core
 
##  Supports: 
DateDiff, LazyLoad (No Virtual) & Hint (With NOLOCK) 


## Example of use DateDiff

 ```csharp
 var list = db
    .People
    .Where(p => EFCore.DateDiff(DatePart.day, DateTime.Now, p.Birthday) < 50)
    .ToList();  
```

## Example of use WithNoLock


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
    .WithNoLock()
    .Take(1)
    .ToList(); 

var list = db
    .People
    .WithNoLock(true)
    .Take(1)
    .ToList(); 

var list = db
    .People
    .WithNoLock(false)
    .Take(1)
    .ToList(); 
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
