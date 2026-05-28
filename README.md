# \# WFMS - Workforce Management System

# 

# A desktop data-dashboard application built in C# using the WPF framework and the Model-View-Controller (MVC) architectural pattern. It automates company timesheet ingestion, visualizes department workloads, and exports structured analytical reports.

# 

# \## Introduction

# The Workforce Management System (WFMS) is designed to process and calculate workforce effort by department. Instead of using a simple console input script, this system implements a multi-page Graphical User Interface (GUI) dashboard for a more professional end-user experience.

# 

# To ensure clean system scalability, the project strictly adheres to the \*\*Model-View-Controller (MVC)\*\* design pattern, separating the user interface elements from the core data processing algorithms. By decoupling these components, the codebase remains highly modular, organized, and straightforward to debug.

# 

# \## Tech Stack

# \- Framework \& UI: WPF (Windows Presentation Foundation) with XAML Layouts

# \- Architecture Pattern: Model-View-Controller (MVC)

# \- Core Language: C# (.NET)

# \- File Data Handling: Streams (`StreamReader` / `StreamWriter`)

# \- Data Collections: Generic lists (`List<Employee>`) and float core arrays

# 

# \## Directory Structure

# The workspace is organized into explicit architectural folders inside Visual Studio:

# ```text

# WFMS-WorkforceManagementSystem/

# │

# ├── Controllers/

# │   └── WfmsController.cs             <-- Manages application state \& triggers

# │

# ├── Views/

# │   ├── MainWindow.xaml               <-- Primary desktop shell layout

# │   ├── DataDashboardPanel.xaml       <-- Analytical results GUI panel

# │   └── FileLoadPanel.xaml            <-- Initial file ingestion view panel

# │

# ├── Models/

# │   ├── Employee.cs                   <-- Encapsulated worker profile properties

# │   ├── WeekWorkedHours.cs            <-- Standardized 5-day shift array record

# │   ├── WfmsEngine.cs                 <-- Mathematical analysis formulas

# │   ├── FileService.cs                <-- Handles physical data stream I/O

# │   └── DepartmentSummary.cs          <-- Data transport model for totals

# │

# └── TextFiles/

# &#x20;   ├── SD-TA-001-A\_OrganisationWeeklyTimesheet.csv  <-- Input Dataset

# &#x20;   └── OrganisationDepartmentTotals.txt            <-- Generated Text Report

# ```

# 

# \## Features

# \- \*\*MVC Architecture Separation:\*\* Front-end visual components, tracking managers, and mathematical data services are kept completely independent.

# \- \*\*Dynamic UI Data Dashboard:\*\* Features specialized views (`FileLoadPanel` and `DataDashboardPanel`) to walk users through uploading spreadsheets and viewing metrics.

# \- \*\*Robust Exception Containment:\*\* Ingestion operations run inside structured `try-catch` blocks to log bad text line offsets cleanly without causing desktop screen crashes.

# \- \*\*Automated Text Summary Exports:\*\* Compiles total department workloads, employee headcounts, averages, and peak performance rankings into formatted two-decimal files (`.ToString("N2")`).

# 

# \## Prerequisites

# \- .NET SDK (Compatible with your IDE assembly target)

# \- Visual Studio with the \*\*.NET Desktop Development\*\* workload selected.

# 

# \## Installation \& Running

# 1\. Extract the `WFMS-WorkforceManagementSystem` project folder.

# 2\. Open the solution folder by double-clicking the main project file in Visual Studio.

# 3\. Keep the target spreadsheets inside the `TextFiles/` directory.

# 4\. Click \*\*Start / Run\*\* to launch the desktop application interface.

# 

# \## Contributors

# \- Everson Spinola <everson\_spinola@hotmail.com>

# 

# \## Contact

# For any queries or support, please contact me at everson\_spinola@hotmail.com.



