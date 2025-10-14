Of course. Here is a more polished and professional version of your README file. It includes a better structure, badges, and more engaging formatting.

-----

\<div align="center"\>

# 🌍 EcoTrack: Carbon Footprint Calculator

**Measure your environmental impact, gain actionable insights, and join a community dedicated to sustainability.**

\</div\>

EcoTrack is a modern web application built with ASP.NET Core MVC designed to help users measure, understand, and reduce their personal carbon footprint. By logging daily activities like travel, food consumption, and energy use, users get actionable insights and can engage in friendly competition to promote environmental sustainability.

-----

## Table of Contents

  - [Key Features](https://www.google.com/search?q=%23key-features-)
  - [Technology Stack](https://www.google.com/search?q=%23technology-stack-)
  - [Setup Instructions](https://www.google.com/search?q=%23setup-instructions-)
  - [Team & Contributions](https://www.google.com/search?q=%23team--contributions-)

-----

## Key Features ✨

  * **👤 User Authentication & Management:** Secure registration, login, logout, and profile editing using Cookie Authentication.
  * **📝 Dynamic Activity Logging:** A comprehensive system for logging activities like Travel, Food, Electricity, and Waste.
  * **🔬 Automatic Carbon Calculation:** Uses a database of scientific emission factors to instantly convert user activities into a CO₂ equivalent.
  * **📊 Personalized User Dashboard:** A central hub for each user to view a summary of their recent activities, personal rankings, and achievements.
  * **📈 Activity Statistics:** A feature allowing users to view their total emissions and activity count over custom date ranges.
  * **💡 Personalized Suggestions:** An intelligent system that analyzes user habits and provides tailored tips to help reduce their environmental impact.
  * **🏆 Gamification & Badges:** An achievement system that awards users with badges for reaching specific milestones (e.g., "Green Commuter," "Veggie Lover").
  * **🏅 Competitive Leaderboards:** Dynamic weekly and monthly leaderboards that rank users, encouraging friendly competition.

-----

## Technology Stack 🛠️

  * **Backend:** C\#, ASP.NET Core MVC, .NET 8
  * **Database:** Entity Framework Core 8, SQLite
  * **Architecture:** Repository Pattern, Unit of Work, Service Layer
  * **Frontend:** Razor Views, HTML, CSS, JavaScript
  * **Libraries:** AutoMapper, BCrypt.Net

-----

## Setup Instructions 🚀

Follow these steps to get the project running on your local machine.

### Prerequisites

  * .NET 8 SDK
  * VS Code or Visual Studio
  * EF Core Command-Line Tools:
    ```bash
    dotnet tool install --global dotnet-ef
    ```

### How to Run

1.  **Clone the Repository**

    ```bash
    git clone <your-repository-url>
    cd EcoTrack_MVC
    ```

2.  **Apply Database Migrations**
    This command creates your local SQLite database (`.db` file) and sets up all the necessary tables.

    ```bash
    cd src/EcoTrack.WebMvc
    dotnet ef database update
    ```

3.  **Run the Application**
    From the same project directory (`src/EcoTrack.WebMvc`):

    ```bash
    dotnet run
    ```

    The application will now be running. Open your browser and navigate to the address shown in the terminal (e.g., `http://localhost:5192`).

-----

## Team & Contributions 🤝

This project was a collaborative effort by Archi Patel and Henil Suchak.

| Contributor | Responsibilities |
| :--- | :--- |
| **Archi Patel** | Focused on gamification, user engagement, and data visualization features.<br>• **Personalized Suggestions**<br>• **Leaderboards & Background Service**<br>• **Badge & Achievement System**<br>• **Dashboard UI/UX Design** |
| **Henil Suchak** | Focused on the core application architecture, data management, and user systems.<br>• **User Authentication & Profile Editing**<br>• **Activity Logging System**<br>• **Emission Factor & Calculation Logic**<br>• **Activity Statistics Feature**<br>• **Initial Project & Database Architecture** |
