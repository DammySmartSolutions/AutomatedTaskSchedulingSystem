# 🛠️ Automated Task Scheduling System

A web-based solution developed in **ASP.NET Web Forms** to streamline and automate employee task scheduling. This system dynamically assigns employees to daily setup tasks based on availability, role, gender restrictions, rotation rules, and task dependencies, ensuring optimal and fair distribution.

---

## 📌 Features

- ✅ Admin login and access control  
- 📅 Auto-generation of daily task schedules  
- 👥 Role-based task assignment (e.g., Cargo Handler)  
- 🔁 Rotation logic to avoid repetitive assignments  
- ⚖️ Gender-based constraints (e.g., no female on 'Trailer Unloader')  
- 🔗 Linked task assignment (equivalent tasks share the same employees)  
- 🧠 Intelligent fallback logic when employees are unavailable  
- 📊 Dashboard for task statistics and Employee Availability   
- 📄 CSV upload for bulk employee data  
- 💾 Data persistence via SQL Server + Entity Framework  

---

## 🏗️ System Architecture

- **Frontend:** ASP.NET Web Forms, Bootstrap, jQuery, DataTables, HTML, CSS 
- **Backend:** C#, Entity Framework (Database First)  
- **Database:** SQL Server  
- **Testing:** MSTest, Moq, Selenium (System Tests)  

---

## 🔍 Project Structure



```
/AutomatedTaskSchedulingSystemSolution
├── AutomatedTaskSchedulingSystem
│   └── WebPages/
│       ├── AddEmployee.aspx
│       ├── GenerateSchedule.aspx
│       └── SetupLocation.aspx
│
├── AutomatedTaskSchedulingSystem.BusinessLogic
│   ├── Services/
│   │   ├── AddEmployee.cs
│   │   ├── GenerateSchedule.cs
│   │   └── SetupLocation.cs
│   └── ViewModel/
│
├── AutomatedTaskSchedulingSystem.DataAccess
│   └── Model/
│       ├── tblSchedule.cs
│       ├── tblEmployee.cs
│       └── tblSetupLocation.cs
│
├── AutomatedTaskSchedulingSystem.UnitTest
│   └── TestingService/
│
└── AutomatedTaskSchedulingSystem.SystemTest
    └── SystemTest/


---

## 🚀 Getting Started

### 🧱 Prerequisites

- Visual Studio 2022+  
- SQL Server (Local or Express)  
- .NET Framework 4.8  

### ⚙️ Setup Instructions

1. Clone the repository  
   ```bash
   git clone https://github.com/DammySmartSolutions/AutomatedTaskSchedulingSystem.git
   ```

2. Open the solution file (`.sln`) in Visual Studio.

3. Update the database connection string in `Web.config`.

4. Restore the database backup  to create database in your SQL Server

5. Press **F5** or click **Start Debugging** to launch the application.

---

## 🧪 Testing

- **Unit Tests**: Validate logic in the service layer (e.g., schedule generation, employee uploads).
- **Integration Tests**: Ensure service and database models interact correctly.
- **System Tests (Selenium)**: Automate browser workflows like login, schedule generation, and navigation.

To run tests:
```bash
Test > Run All Tests
```

---

## 📊 Quality Metrics

| Metric         | Description                                               |
|----------------|-----------------------------------------------------------|
| **POFOD**      | Low – most failures caught in pre-deployment tests        |
| **ROCOF**      | Rare operational failures after deployment                |
| **MTBR**       | High – system is reliable across multiple daily runs      |
| **Availability** | 99%+ uptime expected during standard operations         |

---

## 👨‍💻 Contributors

- **Dominic Aseidu** – Project Planner   
- **Oluwadamilola Ademiluyi** – Backend Logic, Schedule Algorithm, Integration, Frontend & Testing  

---

## 📄 License

This project is licensed under the MIT License – see the [LICENSE](LICENSE) file for details.

---

## 🙏 Acknowledgments

- Memorial University of Newfoundland – ENGI 9874 and ENGI 9839  
- Prof. Raja Manzar Abbas for supervision and feedback  
- Selenium, Moq, MSTest teams for testing tools  
