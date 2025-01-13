## **ToDo List**
A simple and efficient task management tool, designed to help you stay organized and boost productivity. Built with C# and .NET Core, offering a smooth and seamless experience.

## **About the project**
The **ToDo List** application is designed to help you manage your daily tasks in a simple, fast, and efficient way. Whether for work, study, or personal life, this tool will help you stay organized and make your routine easier.

This project consists of an API developed with .NET Core for task management. The API allows users to register by providing their name, email, and password. After registration, users can create, edit, list, and delete tasks. Each task can include a title, description, due date, and status (in progress, or completed).

The API supports the SQL Server, providing a robust and optimized solution for data storage. The application's design follows Domain-Driven Design (DDD) and SOLID principles, aiming for a modular and sustainable architecture. Data validation is performed using FluentValidation, ensuring that all data inputs meet the required criteria.

To ensure the system's quality and reliability, unit and integration tests were implemented using xUnit. Dependency injection has been implemented to ensure code modularity and facilitate testing and maintenance.

Other technologies and practices used in the development of this project include Entity Framework for object-relational mapping and JWT & Refresh Tokens for secure authentication. Version control is managed through Git and GitFlow, ensuring organization and efficiency in development.

With that, ToDo List is not just a task list but an efficient, secure, and well-structured application to help you keep your life organized.

Some of the key technologies used to build this project include

- **C#**: The main programming language for the backend logic.  
- **.NET Core**: The framework for building fast and scalable web applications.  
- **Entity Framework**: Facilitates database integration, ensuring efficient task storage.
- **SQL Server**: A optimized database to manage the querys.

## **Getting Started**

### **Requirements**:
- Visual Studio 2022+ or Visual Studio Code.
- Windows 10+ or Linux/MacOS with [.NET SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0) installed.
- SQL Server installed.

### **Instalation**:
**1. Clone the repository.** Clone the project repository to your local machine using the following command:
```bash
    git clone git@github.com:oIthalo/to-do-list.git
```

### **Usage instructions**:
**1. Enter the project.** Navigate into the project directory to access all the project files:
```bash
    cd t0-do-list
```
**2. Restore the packages.** Restore all the necessary NuGet packages that the project depends on:
```
    dotnet restore
```
**3. Configuring appsettings** Fill in the information in the file bellow:
```bash
    appsetings.Delevopment.json
```
**4. Run the project.** Open the localhost:
```
    https://localhost:7186
```

## **API Endpoints**

### **USER CONTROLLER**:
| **Endpoint**         | **Method** | **Route**                         | **Request Body**                                      | **Response**                |
|----------------------|------------|-----------------------------------|------------------------------------------------------|-----------------------------|
| **Register**         | `POST`     | `/user/register`                  | `{ "name": "string", "email": "string", "password": "string" }` | `{ "name": "string", "tokens": { "accessToken": "string", "refreshToken": "string" } }` |
| **Login**            | `POST`     | `/user/login`                     | `{ "email": "string", "password": "string" }`         | `{ "name": "string", "tokens": { "accessToken": "string", "refreshToken": "string" } }` |
| **Update**           | `POST`     | `/user/update`                    | `{ "name": "string", "email": "string" }`             | `204 No Content`            |
| **Profile**          | `GET`      | `/user/profile`                   | `No request body`                                                 | `{ "name": "string", "email": "string" }` |
| **Change Password**  | `POST`     | `/user/change-password`           | `{ "password": "string", "newPassword": "string" }`   | `204 No Content`            |
| **Delete**           | `DELETE`   | `/user/delete`                    | `No request body`                                                | `204 No Content`            |

<br>

### **TASK CONTROLLER**
| **Endpoint**          | **Method** | **Route**                | **Request Body**                                      | **Response**                                               |
|-----------------------|------------|--------------------------|-------------------------------------------------------|------------------------------------------------------------|
| **Create Task**       | `POST`     | `/tasks/create`           | `{ "title": "string", "description": "string" }`      | `{ "id": "string", "title": "string", "description": "string", "createdOn": "2025-01-11T11:00:29.129Z", "status": 0 }` |
| **Get All Tasks**     | `GET`      | `/task/get-all`           | `No request body`                                                   | `{ "tasks": [ { "id": "string", "title": "string", "description": "string", "createdOn": "2025-01-11T11:02:21.133Z", "status": 0 } ] }` |
| **Get Task by ID**    | `GET`      | `/task/get/{id}`          | `No request body`                                                   | `{ "id": "string", "title": "string", "description": "string", "createdOn": "2025-01-11T11:03:30.714Z", "status": 0 }` |
| **Update Task**       | `PUT`      | `/task/update/{id}`       | `{ "title": "string", "description": "string" }`      | `204 No Content`                                           |
| **Change Task Status**| `PUT`    | `/task/change-status/{id}`| `{ "status": 0 }`                                     | `204 No Content`                                           |
| **Delete Task**       | `DELETE`   | `/task/delete/{id}`       | `No request body`                                                   | `204 No Content`                                           |

<br>

### **TOKEN CONTROLLER**
| **Endpoint**        | **Method** | **Route**               | **Request Body**                        | **Response**                                               |
|---------------------|------------|-------------------------|-----------------------------------------|------------------------------------------------------------|
| **Refresh Token**   | `POST`     | `/token/refresh-token`   | `{ "refreshToken": "string" }`          | `{ "accessToken": "string", "refreshToken": "string" }`     |

## **License**
Feel free to use this project for study and learning. However, distribution or commercialization is not permitted.
