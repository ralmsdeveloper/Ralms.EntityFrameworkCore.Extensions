
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
var list = db
    .People
    .WithNoLock()
    .Take(1)
    .ToList(); 
```
 
