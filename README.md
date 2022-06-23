# ECommerceProject
## Beginner ECommerce Project
In my project, basically Entities and Repositories are in Layered architecture.
Layers feed API and Presentation layers through services such as Domain, Application, Infrastructure.
In the web part, it consists of two parts as MVC type normal website view under Presentation and Admin under Areas folder.
What I want to do in my project is the view of an eCommerce site, categories and pages listing products, examining the details of the products, 
adding and deleting the product to the basket, user registration and login operations, editing the profile, saving the changes made in the database.
All operations have been added to the database using the Code First approach on the Visual Studio side.
## Beginner ECommerce Project Step-2
In the Admin section, the user can perform the necessary shopping transactions for the entire site, where the user is defined as Admin, Manager, Employee, and at the same time, he can switch to the Administration Panel with authorization.
While assigning authorization in this section can only be done by the Admin, adding ParentCategory, Category, Product, Property, ProductProperty, and deleting updates can be done by authorized persons who have access to the entire administration panel.
Added products are dynamically assigned to Categorized pages, and no editing is required.
The project, which I consider as a beginner level E-Commerce site, is open to development and innovations can be made as a loose couple when any design pattern is used in the developments.