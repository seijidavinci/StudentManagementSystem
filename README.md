# Student Management System

A complete CRUD (Create, Read, Update, Delete) application with separate Frontend and Backend.

## Project Overview

This is a full-stack web application for managing student records. It demonstrates:
- CREATE - Add new students
- READ - View all students and search by ID
- UPDATE - Edit student information
- DELETE - Remove students from database

## Architecture

### Backend (StudentAPI)
- Language: C# (.NET 10)
- Framework: ASP.NET Core Web API
- Database: SQLite
- Port: http://localhost:5279

### Frontend (StudentFrontend)
- Technologies: HTML5, CSS3, JavaScript (ES6)
- Design: Responsive, modern UI with gradient theme
- Communication: Fetch API for REST calls

## Project Structure
```
StudentManagementSystem/
│
├── StudentAPI/                 # Backend (C# Web API)
│   ├── Controllers/
│   │   └── StudentsController.cs
│   ├── Data/
│   │   └── DatabaseHelper.cs
│   ├── Models/
│   │   └── Student.cs
│   ├── Program.cs
│   └── students.db            # SQLite database
│
└── StudentFrontend/           # Frontend (HTML/CSS/JS)
    ├── index.html
    ├── style.css
    └── script.js
```

## How to Run

### Prerequisites
- .NET SDK 6.0 or higher
- Any modern web browser (Chrome, Firefox, Safari, Edge)

### Step 1: Run the Backend
```bash
cd StudentAPI
dotnet restore
dotnet run
```

The backend will start at: http://localhost:5279

### Step 2: Run the Frontend

Simply open StudentFrontend/index.html in your web browser:
```bash
cd StudentFrontend
open index.html
```

Or double-click the index.html file.

## Features

### Backend API Endpoints

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | /api/students | Get all students |
| GET | /api/students/{id} | Get student by ID |
| POST | /api/students | Create new student |
| PUT | /api/students/{id} | Update student |
| DELETE | /api/students/{id} | Delete student |

### Frontend Features

- Student Form - Add/Edit student information
- Data Table - Display all students with sorting
- Edit Button - Load student data into form for editing
- Delete Button - Remove student with confirmation
- Refresh Button - Reload student list
- Responsive Design - Works on desktop and mobile

## Database Schema

Students Table:
```sql
CREATE TABLE Students (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Name TEXT NOT NULL,
    Email TEXT NOT NULL,
    Course TEXT NOT NULL,
    Age INTEGER NOT NULL
);
```

## Technologies Used

### Backend
- C# 10
- ASP.NET Core Web API
- Microsoft.Data.Sqlite
- CORS enabled for cross-origin requests

### Frontend
- HTML5
- CSS3 (Grid Layout, Flexbox, Gradients)
- Vanilla JavaScript (Fetch API, Async/Await)
- No external frameworks or libraries

## Sample Usage

### Adding a Student
1. Fill in the form with student details
2. Click "Add Student"
3. Student appears in the table immediately

### Updating a Student
1. Click "Edit" button on any student row
2. Form populates with student data
3. Modify the information
4. Click "Update Student"

### Deleting a Student
1. Click "Delete" button on any student row
2. Confirm the deletion
3. Student is removed from database

## Configuration

### Change Backend Port
Edit StudentAPI/Properties/launchSettings.json:
```json
"applicationUrl": "http://localhost:YOUR_PORT"
```

Then update StudentFrontend/script.js:
```javascript
const API_URL = 'http://localhost:YOUR_PORT/api/students';
```

## Troubleshooting

### "Failed to fetch" Error
- Ensure backend is running on http://localhost:5279
- Check CORS is enabled in backend
- Verify API_URL in script.js matches backend port

### Database Not Creating
- Check write permissions in StudentAPI folder
- Ensure SQLite package is installed correctly

## Author

Created as a demonstration of full-stack CRUD application development.

## License

This project is open source and available for educational purposes.

## Learning Outcomes

This project demonstrates:
- RESTful API design
- Separation of concerns (Frontend/Backend)
- CRUD operations implementation
- Database integration with SQLite
- Asynchronous JavaScript
- Responsive web design
- Error handling and validation
