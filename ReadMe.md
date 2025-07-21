# Automated Task Scheduling System (ATSS)

[![Build Status](https://img.shields.io/badge/build-passing-brightgreen)](https://github.com/your-org/ATSS/actions)  
[![License](https://img.shields.io/badge/license-MIT-blue)](LICENSE)  
[![Coverage](https://img.shields.io/badge/coverage-95%25-yellow)](https://github.com/your-org/ATSS/actions)

---

## 📖 Project Overview

**Automated Task Scheduling System (ATSS)** helps logistics and service organizations (e.g., FedEx Express Canada) automatically generate daily work schedules. ATSS considers:

- Employee availability windows  
- Gender-based constraints  
- Historical rotation rules  
- Task-specific business rules  

By automating these constraints, ATSS improves fairness, consistency, and operational efficiency.

---

## 🚀 Key Features

- **Rule-based Scheduling**  
- **Configurable Constraints** (availability, gender balance, rotation)  
- **Web UI** for data entry and schedule visualization (ASP.NET WebForms)  
- **REST API** backend for headless integrations  
- **Extensible Architecture** (Service → Repository → Controller layers)  

---

## 🛠️ Tech Stack

- **Backend**: .NET 6 (C#), Entity Framework Core  
- **Frontend**: ASP.NET WebForms, Bootstrap  
- **Testing**: MSTest, NUnit, Postman  
- **Mocking**: Moq, In-Memory EF Provider  
- **CI/CD**: GitHub Actions  
- **Database**: SQL Server (local, staging, production)  

---

## 📥 Installation

1. **Clone the repo**  
   ```bash
   git clone https://github.com/your-org/ATSS.git
   cd ATSS
