# 🎬 Movie API – ASP.NET Core 7 (JWT Authentication)

This is a fully functional **Movie REST API** built using **ASP.NET Core 7**.

It provides user authentication, movie management, and a favorites system, all secured with **JWT Authentication**.

---

## 🚀 Technologies Used
- **ASP.NET Core 7 Web API**  
- **Entity Framework Core 7**  
- **SQL Server**  
- **JWT Authentication**  
- **Swagger / OpenAPI**  
- **C# .NET**  

---

## 📌 Features

### 🔐 Authentication
- Register a new user  
- Login with JWT token  
- Secure endpoints with `[Authorize]`  

### 🎬 Movie Management
- Add new movies  
- Edit movies  
- Delete movies  
- Get all movies  

### ⭐ Favorites System
- Add movies to favorites  
- Remove movies from favorites  
- Get a user’s favorite movies  

---

## 📂 Project Structure
MovieAPI/
│── Controllers/
│── Models/
│── Data/
│── Program.cs
│── appsettings.json
│── README.md 

---

## ▶ How to Run the Project

1. Update your **SQL Server connection** in `appsettings.json`.  

Example connection string:
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER;Database=MovieDb;Trusted_Connection=True;"
}
```
2.Apply EF Core migrations:
```
dotnet ef migrations add InitialCreate
dotnet ef database update
```
3.Run the API:
```
dotnet run
```
4.Open Swagger UI in your browser:
```
https://localhost:7001/swagger/index.html
```
## 👨‍💻 Developer
**Ahmed Khaled**  
ASP.NET Backend Developer  
GitHub: [https://github.com/ahmeddkhaledd12](https://github.com/ahmeddkhaledd12)  

