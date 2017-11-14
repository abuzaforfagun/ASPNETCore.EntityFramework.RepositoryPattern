# ASPNETCore.EntityFramework.RepositoryPattern
Description
Example of using Repository Pattern in ASP .NET Core with Entity Framework 

Why Repository Patter?
Repository Pattern helps you to decouple your code. It will separate your data access layer from your business logic layer. So at further anytime if you want to change the database technology then you have to change the repository only. No need to touch other code.

What I have done
Added Employee cass with three properties. Add IEmployeeRepository interface with Add, Remove and Get method declieartion and implement this using EmployeeRepository class.
Add IUnitOfWorkRepository interface with Complete method declearation. And implement this with UnitOfWorkRepository to save all the changes.
Do the dependency injection in Stratup.cs.
Test the code in Index method inside HomeController.cs
