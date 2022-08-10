![](https://res.cloudinary.com/doh2fp7hz/image/upload/v1628911764/Screenshot_2021-08-14_062859_m2cec1.png)

# HealthHub

Health Hub is created as a platform that allows patients to effortlessly find doctors and book appointments online.

This is my defense project for ASP.NET Core Course at [SoftUni](https://softuni.org). (Jun-Aug 2021) 

Here is how the project works: https://www.youtube.com/watch?v=CLHitVhiHCE

Note: The project is created without any current knowledge of JavaScript, kindly disregard discrepancies related to it.

## Table of Contents
1. [Overview](https://github.com/DenitsaDey/My-Projects#overview)
2. [Built With](https://github.com/DenitsaDey/My-Projects#built-with)
3. [Application Configurations](https://github.com/DenitsaDey/My-Projects#application-configurations)
4. [Screenshots](https://github.com/DenitsaDey/My-Projects#screenshots)
5. [Bootstrap Template](https://github.com/DenitsaDey/My-Projects#bootstrap-template)
6. [Acknowledgements](https://github.com/DenitsaDey/My-Projects#screenshots#acknowledgements)

## :pencil2: Overview
&nbsp;&nbsp;&nbsp;&nbsp;**Health Hub** is a web application that connects doctors and patients. It has the following functionality:

- Guest Users can: 
  - browse doctors by clinics, specialties, city areas, insurance companies, rating, most popular, gender etc.
  - view information about each doctor such as About and Rating.
  - see Frequently Asked Questions.
- Logged Patients can:
  - book appointments using interactive datepicker.
  - receive confirmation emails upon appointment request or appointment status change
  - cancel appointments.
  - reschedule appointments.
  - can add or edit notes to the doctor.
  - can rate appointments that have been completed.  
- Doctors can:
  - confirm/cancel requested appointments; 
  - change the status of confirmed appointments to Completed, Cancelled or NoShow thus enabling the voting option for each past appointment.
- Admin can:
  - create/delete doctors, clinics, specialties, services, city areas, insurance companies. 
  - can review the appointments history.

## :hammer: Built With

- ASP.NET
- Entity Framework (EF) Core
- Microsoft SQL Server
- ASP.NET Identity System
- MVC Areas with Multiple Layouts
- Razor Pages, Sections, Partial Views
- View Components
- Repository Pattern
- Auto Мapping
- Dependency Injection
- Status Code Pages Middleware
- Exception Handling Middleware
- Sorting, Filtering, and Paging with EF Core
- Data Validation, both Client-side and Server-side
- Data Validation in the Models and Input View Models
- Custom Validation Attributes
- Responsive Design
- SendGrid
- Bootstrap
- jQuery
- Moq
- XUnit

## :gear: Application Configurations

### Seeding sample data
would happen once you run the application, including Test Accounts:
  - Patient: patient@patient.com / password: 123456
  - Doctor: doctor@doctor.com / password: 123456
  - Admin: admin@admin.com / password: 123456

However, Rating Seeding has to be done by the Admin once the rest of the data has been seeded. This can be done from the "Import Ratings" button on the home page.

## Screenshots

### Home Page

<img width="953" alt="Home page" src="https://res.cloudinary.com/doh2fp7hz/image/upload/v1628912885/Screenshot_2021-08-14_064429_elza2t.png">

### Search Doctors

<img width="953" alt="Search Doctors" src="https://res.cloudinary.com/doh2fp7hz/image/upload/v1628912885/Screenshot_2021-08-14_064653_inf6d8.png">

### Book An Appointment Page

<img width="953" alt="Book an Appointment" src="https://res.cloudinary.com/doh2fp7hz/image/upload/v1628912885/Screenshot_2021-08-14_064609_izomsu.png">

### Clinic Details

<img width="953" alt="Clinic Details" src="https://res.cloudinary.com/doh2fp7hz/image/upload/v1628912885/Screenshot_2021-08-14_064744_gqhhhf.png">

## License

This project is licensed under the [MIT License](LICENSE).

## Bootstrap Template

The project is created using Medilab Bootstrap template: https://bootstrapmade.com/demo/Medilab/

## Acknowledgments

#### Using [ASP.NET-MVC-Template](https://github.com/NikolayIT/ASP.NET-MVC-Template) developed by:
- [Nikolay Kostov](https://github.com/NikolayIT)
- [Vladislav Karamfilov](https://github.com/vladislav-karamfilov)
- [Stoyan Shopov](https://github.com/StoyanShopov)

## :v: Show your opinion

Give a :star: if you like this project!
