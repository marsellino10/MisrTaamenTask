Project Folder Structure

Controllers:	Contains all the API controllers (OrdersController, CustomersController). Each controller defines endpoints for handling business operations like create order, get order by ID, update order status.
-------------------------

Models: Holds all entity classes representing database tables (Customer, Product, Order, OrderProduct). Defines relationships and fields.
------------------------

Data: Includes the AppDbContext for Entity Framework Core and database seeding logic used to initialize sample data.
-----------------------

Configurations:	Contains configuration setup files (model constraints, relationships, etc.) if used via Fluent API.
----------------------

Validators:	Holds all FluentValidation rules (CustomerValidator) for validating incoming requests before saving to the database.
---------------------

Check Samples folder to show response of different requests
