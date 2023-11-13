# Klinika

The Klinika is a web-based application developed using ASP.NET Core 7.0 MVC. This application is designed to manage patient, doctor, appointment, and medical report information. Users can add, delete, or edit these records through an intuitive web interface.

## Features

- **Patient Management**: Easily add, view, edit, and delete patient records. Capture important patient information, including name, contact details, and health card number.

- **Doctor Management**: Maintain a database of doctors, their title, and contact information. 

- **Appointment Scheduling**: Schedule appointments between patients and doctors.

- **Medical Reports**: Record and manage patient medical reports, associated with specific appointments.

  
## Installation and Setup

### Prerequisites

- .NET 7.0 SDK
- Visual Studio 
- SQL Server Express or another database server
- iTextSharp NuGet package for generating PDFs
  ```bash
  Install-Package iTextSharp
- Git

### Instructions

1. Clone the repository to your local machine:

   ```shell
   git clone https://github.com/dzhuseinspahic/Klinika.git

2. Create a SQL Server database and configure the connection string in the appsettings.json file.

3. Open the project in Visual Studio.

4. In your project directory, open a terminal window and run the following commands to set up and update the database:
- Initialize EF Core migrations (if not already done):

   ```shell
   Add-Migration -Context ProjektniZadatak.Data.KlinikaContext InitialCreate

- Apply the migrations to create the database schema
   ```shell
   Update-Datebase -Context ProjektniZadatak.Data.KlinikaContext
   
6. Build and run the application.
