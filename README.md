# E-Commerce Project

## Overview
This is an e-commerce web application built with ASP.NET Core. The application provides a platform for users to browse products, add them to the cart, and place orders. Admin users can manage products, categories, and view orders.

## Features
- User registration and authentication
- Product browsing with categories
- Shopping cart functionality
- Order management
- payment integration
- Admin dashboard for managing products and orders

## Technologies Used
- ASP.NET Core
- Entity Framework Core
- SQL Server
- Bootstrap
- JavaScript (jQuery)
- Stripe Payment integration

## Setup Instructions
### Prerequisites
- [.NET Core SDK](https://dotnet.microsoft.com/download)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)

### Steps to Run the Project
1. **Clone the repository**:
   ```sh
   git clone https://github.com/your-username/e-commerce-project.git
   cd e-commerce-project
2. **Update the database connection string in appsettings.json:**
"ConnectionStrings": {
    "DefaultConnection": "Server=your_server_name;Database=your_database_name;User Id=your_username;Password=your_password;"
}
3. **Apply migrations and update the database:**
4. **Run the application:**
   **Usage**
# User Features
- Browse Products: View the list of products and their details.
- Add to Cart: Add products to the shopping cart.
- Place Orders: Checkout and place orders.
- View Orders: View order history and details.

# Admin Features
- Manage Products: Add, edit, and delete products.
- Manage Categories: Add, edit, and delete categories.
- View Orders: View all orders and update order statuses.
- Controll Users : Lock Account and Unlock User Account

# Contributing
- Fork the repository
- Create a new branch (git checkout -b feature-branch)
- Commit your changes (git commit -am 'Add some feature')
- Push to the branch (git push origin feature-branch)
- Create a new Pull Request
