# 🎬 MovieBooker Web API

This is an ASP.NET Core 8.0 Web API application for managing cinema showtimes, seat reservations, and user accounts. 
The app allows user registration, movie browsing, seat booking, and integration with Stripe for payments. 
It's preconfigured with an In-Memory database for easy testing but can easily be switched to SQL Server.

## 📌 Features

- ✅ **Authentication and Authorization**  
  Users must **register and log in** using **bearer tokens**.  
  Authentication is implemented using **Microsoft Identity** for simplified management of users, roles, and passwords,  
  while **JWT bearer tokens** are used to securely authorize access to protected API endpoints.

- 🖼️ **Image Uploads**  
  **Cloudinary** is used to store and serve images:
  - user profile pictures
  - actor photos
  - movie posters

- 🎞️ **Movie Management (Admin only)**  
  The database includes ten movies that are **inactive by default**.  
  - Only **admin** users can access endpoints to **activate movies**.  
  - A movie must be **active** in order to:
    - add a **poster** to it  
    - link **actors** to that movie  
    - create a **showtime** for it  

- 🕒 **Showtimes (Movie Screenings)**  
  - A showtime represents a scheduled movie screening with start and end times.
  - Users can book seats for specific showtimes.
  - Reservations are paid through **Stripe** integration.

- 🧪 **Postman Collection**  
  - A **Postman collection** is included with all API endpoints for easy testing.

- 🗃️ **In-Memory or SQL Server Database**  
  - The app uses an **In-Memory database** by default for easier testing and setup.  
  - To use **SQL Server**, simply set `UseInMemoryDatabase` to `false` in `appsettings.Development.json`.

## 🚀 Technologies Used

- ASP.NET Core Web API
- Entity Framework Core
- Microsoft Identity
- Cloudinary
- Stripe
- AutoMapper
- FluentValidation
- In-Memory Database / SQL Server
- Postman

## ⚙️ Getting Started

1. Clone the repository:
   git bash - git clone https://github.com/igao94/MovieBooker

2. Run the app using Visual Studio or the .NET CLI.
