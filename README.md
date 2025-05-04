# 🏨 EventHotel - Hotel's Events Management System

A desktop application for managing hotel-related events and activities. Developed using C# and WinForms with support from Entity Framework, this system streamlines the entire process of organizing, updating, and reporting hotel events, staff, partners, sponsors, and financial information.

## 📌 Overview

EventHotel is designed to address the inefficiencies of manual event management in hotels by providing a modern, user-friendly interface for:

- Organizing and scheduling events
- Managing hotel staff, managers, and partners
- Tracking sponsors and financial reports
- Exporting and importing Excel files
- Generating analytical reports for better decision-making

## 🛠️ Tech Stack
# 🏨 EventHotel - Hotel's Events Management System

![C#](https://img.shields.io/badge/-C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)
![.NET](https://img.shields.io/badge/.NET_Framework-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![WinForms](https://img.shields.io/badge/-WinForms-blueviolet?style=for-the-badge)
![Entity Framework](https://img.shields.io/badge/-Entity_Framework-68217A?style=for-the-badge)
![SQL Server](https://img.shields.io/badge/-SQL%20Server-CC2927?style=for-the-badge&logo=microsoft-sql-server&logoColor=white)
![Visual Studio](https://img.shields.io/badge/-Visual%20Studio-5C2D91?style=for-the-badge&logo=visual-studio&logoColor=white)

<img src="https://img.icons8.com/color/96/000000/hotel.png" width="100"/>
![image](https://github.com/user-attachments/assets/8d5a8e20-67ad-4bda-b1de-35413bdbb03d)

Example Frame in Application

## ✨ Key Features

- 🔧 **Event Management:** Create, update, and delete event details.
- 👥 **Staff & Manager Management:** Manage personnel involved in events.
- 🤝 **Business Partners & Sponsors:** Store, view, and update collaborator info.
- 📊 **Statistics & Reporting:** Generate event, staff, sponsor, and finance reports.
- 📁 **Excel Support:** Import and export event and staff data.
- 🧩 **Modular Architecture:** DAO, MODEL, and UI are cleanly separated for maintainability.
- 🖥️ **User-Friendly Interface:** Designed for hotel admins with minimal training required.

## 🧱 Project Structure

```
EventHotel/
├── DAO/                # Data Access Layer
├── MODEL/              # Entity Framework data models
├── UI/                 # Windows Forms (UI)
├── Resources/          # Icons, Excel templates, etc.
├── Program.cs          # Application entry point
├── Connect.cs          # Database connection logic
└── Custom.txt          # Saved user configurations
