# 🗑️ DagsDumps – Full Stack Dumpster Rental Platform

**DagsDumps** is a full-stack web application built to simulate and potentially power a real dumpster rental business. Designed to demonstrate full-stack software engineering skills while building a real-world product. Customers can view dumpster options, request bookings, and manage rentals. Built with a React frontend, ASP.NET Core backend, and SQL Server database.

---

## 🚀 Features

- ✅ View available dumpsters by size and pricing
- ✅ Submit rental booking requests
- ✅ Store and manage customer details
- ✅ Admin dashboard to manage bookings
- ✅ Database-backed with full CRUD support
- ✅ Clean code structure for future scalability

---

## 🛠️ Tech Stack

**Frontend**
- React (JavaScript)
- Axios for API communication
- Basic HTML/CSS (Tailwind optional)

**Backend**
- ASP.NET Core Web API (C#)
- Entity Framework Core (EF Core)

**Database**
- SQL Server (local development, Azure-ready)

---

## 📁 Project Structure

/DumpsterDash.API → ASP.NET Core API backend
├── Controllers/
├── Models/
├── Data/
└── appsettings.json

/dumpsterdash-frontend → React frontend
├── components/
├── pages/
├── api/
└── App.jsx


## 🧱 Database Schema

**Customers**
- `CustomerId`, `FullName`, `PhoneNumber`, `Email`, `Address`, `CreatedAt`

**Dumpsters**
- `DumpsterId`, `SizeYards`, `Description`, `Price`, `MaxWeightTons`, `OverageFeePerTon`, `IsAvailable`

**Bookings**
- `BookingId`, `CustomerId`, `DumpsterId`, `DropOffDate`, `PickUpDate`, `DropOffAddress`, `Status`, `TotalCost`, `CreatedAt`

**ServiceAreas** (optional)
- `ServiceAreaId`, `City`, `ZipCode`, `DeliveryFee`

---

## 🔌 API Endpoints (MVP)

- `GET /api/dumpsters` – List available dumpsters  
- `POST /api/bookings` – Create new booking  
- `GET /api/bookings/{id}` – View booking  
- `GET /api/customers/{id}/bookings` – Get bookings for a customer  
- `POST /api/customers` – Add new customer

---

## 🧪 Status

This project is currently in development.  
The backend schema, API routes, and database models are complete.  
Frontend components and full integration in progress.

---

## 🎯 Goals

- Build real-world full-stack development experience
- Create a functioning MVP to potentially launch a local business
- Strengthen resume with React + ASP.NET Core project

---

## 📈 Future Features

- Stripe/PayPal payment integration  
- SMS/email confirmations  
- Admin login & dashboard  
- Customer login & order tracking  
- Mobile-responsive layout

---

## 👤 About the Developer

Built by a software engineer based in New Jersey.  
Using this project to gain job-ready experience.

---

## 📸 Demo Coming Soon

Live demo and screenshots will be added once the MVP is deployed.
