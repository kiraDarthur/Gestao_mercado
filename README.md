# GestaoMercadoApp 🛒

A desktop Market Management Application developed in **C#** using **Windows Forms** and integrated with a **MySQL** database via XAMPP. This system was designed to streamline inventory controls, manage market operations, and track product data efficiently.

## 🚀 Features

- **Product Inventory Management:** Full CRUD (Create, Read, Update, Delete) capabilities for market articles.
- **Image Upload:** Attach and display specific product images directly inside the dashboard.
- **Advanced Filters:** Separated, dedicated search window supporting instantaneous queries using either a mouse click or the **Enter** key.
- **Database Architecture:** Built-in support for relational tables including products, categories (`categorias`), employees (`funcionarios`), and sales (`vendas`).

## 🛠️ Technologies Used

- **Language:** C#
- **Framework:** .NET Windows Forms
- **Database:** MySQL (via XAMPP / phpMyAdmin)
- **Driver:** MySQL Connector/NET

## 📋 Prerequisites

Before running the project, make sure you have installed:
1. [Visual Studio](https://visualstudio.microsoft.com/) (with .NET Desktop Development workload).
2. [XAMPP](https://www.apachefriends.org/) (for running Apache and MySQL local servers).

## 🔧 Database Setup

1. Open the **XAMPP Control Panel** and start both **Apache** and **MySQL**.
2. Click on **Admin** next to MySQL to open **phpMyAdmin**.
3. Create a new database named `gestao_mercado`.
4. Import your SQL tables architecture (`produtos`, `categorias`, `funcionarios`, `vendas`).

## ⚙️ How to Run

1. Clone this repository to your local machine:
   ```bash
   git clone https://github.com/kiraDarthur/Gestao_mercado.git
