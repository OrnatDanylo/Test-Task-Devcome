Bonjure!!!

# Test-Task-Devcome
Test Task From Devcome

This is test task from Devcome

Its simple WepApi app. It Realize MS SQL DB CRUD operation using .Net application.

It has couple of routse:
 - / GET: Users *All users view
 - / GET: Users/Details/5 *User with id=5 view
 - / GET: Users/Create *Create new user view
 - / POST: Users/Create *Post for create new user
 - / POST: Users/Edit/5 *Post for edit user
 - / POST: Users/Delete/5 *Post for delete user
 
 - / GET: Orders *All orders view
 - / GET: Orders/Details/5 *Order with id=5 view
 - / GET: Orders/Create *Create new order view
 - / POST: Orders/Create *Post for create new order
 - / POST: Orders/Edit/5 *Post for edit order
 - / POST: Orders/Delete/5 *Post for delete order
 
 
To connect your database you must open ./TestTaask/appsettings.json :
  find "ConnectionStrings" and edit parameters
 "DefaultConnection": "Server=YOURSERVER;Database=YOURDATABASE;Integrated Security=true;Encrypt=false;"
 
There is the two tables DB structure 

Table 1: Users 
Fields
   UserID INT NOT NULL - Autoincrement, Primary Key
   Login VARCHAR(20) NOT NULL - Uniq user name
   Password VARCHAR(50)
   FirstName VARCHAR(40)
   LastName VARCHAR(40)
   DateOfBirth DATE
   Gender VARCHAR(1) - M/F

Table 2: Orders
Fields
    OrderID  INT NOT NULL - Autoincrement, Primary Key
    UserID INT NOT NULL - Foreight Key to Users table
    OrderDate  DATETIME 
    OrderCost  MONEY
    ItemsDescription  VARCHAR(1000)
    ShippingAddress  VARCHAR(1000) 


To stars just compile the project
