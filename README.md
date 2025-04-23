# dotnet-sports-api

## About
This repository contains an ASP.NET Core API designed for retrieving sports related data from the ESPN API. It is built using [Fast Endpoints](https://fast-endpoints.com/) and includes Docker support.

## Prerequisites for Development
To develop and run this application, you will need the following tools and frameworks installed on your machine:

1. **Visual Studio**:
   - Download and install [Visual Studio](https://visualstudio.microsoft.com/).
   - Make sure to include the **ASP.NET and web development** workload during installation.

2. **.NET Core SDK 9.0**:
   - Download and install the [.NET Core SDK 9.0](https://dotnet.microsoft.com/download).

3. **Docker (Optional)**:
   - If you want to run the application in a containerized environment, ensure you have [Docker](https://www.docker.com/) installed and running.

## Setting Up the Development Environment
Follow these steps to set up the development environment:

1. Clone this repository:
   ```bash
   git clone https://github.com/retepz/dotnet-sports-api.git
   cd dotnet-sports-api
   ```

2. Open the project in Visual Studio:
   - Launch Visual Studio.
   - Open the `.sln` file located in the root of the repository.

3. Restore NuGet packages:
   - In Visual Studio, navigate to **Tools** -> **NuGet Package Manager** -> **Manage NuGet Packages for Solution**.
   - Restore any missing packages.

4. Build the solution:
   - Press `Ctrl+Shift+B` or go to **Build** -> **Build Solution**.

5. Run the project:
   - Press `F5` or navigate to **Debug** -> **Start Debugging** to run the application.

## Running the Application
- The application will automatically open (swagger)[https://swagger.io/] on the default local server (http://localhost:5094/swagger)[http://localhost:5094/swagger].