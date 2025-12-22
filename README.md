# 1. Create root folder
```sh
mkdir WebAppCleanArch
cd WebAppCleanArch
```

# 2. Setup global json
```sh
dotnet new globaljson --sdk-version 8.0.414
```

# 3. Create empty solution
```sh
dotnet new sln -n WebAppCleanArch
```

# 3. Create source folder
```sh
mkdir src
cd src
```

# 4. Create Class Libraries (.NET 8)
```sh
dotnet new classlib -n WebAppCleanArch.Domain -f net8.0 -o WebAppCleanArch.Domain
dotnet new classlib -n WebAppCleanArch.Application -f net8.0 -o WebAppCleanArch.Application
dotnet new classlib -n WebAppCleanArch.Infrastructure -f net8.0 -o WebAppCleanArch.Infrastructure
```

# 5. Create MVC Web App (.NET 8)
```sh
dotnet new mvc -n WebAppCleanArch.Web -f net8.0 -o WebAppCleanArch.Web
```

# 6. Add projects to solution
```sh
dotnet sln ../WebAppCleanArch.sln add \
  WebAppCleanArch.Domain/WebAppCleanArch.Domain.csproj \
  WebAppCleanArch.Application/WebAppCleanArch.Application.csproj \
  WebAppCleanArch.Infrastructure/WebAppCleanArch.Infrastructure.csproj \
  WebAppCleanArch.Web/WebAppCleanArch.Web.csproj
```


# Application → Domain
```
dotnet add src/WebAppCleanArch.Application/WebAppCleanArch.Application.csproj \
  reference src/WebAppCleanArch.Domain/WebAppCleanArch.Domain.csproj
```

# Infrastructure → Application
```sh
dotnet add src/WebAppCleanArch.Infrastructure/WebAppCleanArch.Infrastructure.csproj \
  reference src/WebAppCleanArch.Application/WebAppCleanArch.Application.csproj
```

# Web → Infrastructure
```sh
dotnet add src/WebAppCleanArch.Web/WebAppCleanArch.Web.csproj \
  reference src/WebAppCleanArch.Infrastructure/WebAppCleanArch.Infrastructure.csproj
```

# Migration
```sh
dotnet ef migrations add InitialCreate \
  --project src/WebAppCleanArch.Infrastructure \
  --startup-project src/WebAppCleanArch.Web \
  --context ApplicationDbContext \
  --output-dir Persistence/Migrations


dotnet ef database update -p src/WebAppCleanArch.Infrastructure -s src/WebAppCleanArch.Web -c ApplicationDbContext
```