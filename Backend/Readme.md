
# Business Cards - ProgressSoft Task

The BusinessCards solution is a .NET Core-based application using Onion Architecture. It is designed to manage business cards with features like creating, retrieving,  deleting, exporting, and importing business cards. The solution separates concerns across layers such as Domain, Repository, Service, and Presentation, promoting clean architecture and scalability.

## Project Structure
* ####  **BusinessCardsProject**
    * **Controllers**
        * BusinessCardController.cs: Manages the API endpoints for handling business card operations (create, list, get by ID, delete, filter, export, import).
    * **appsettings.json**: Holds the configuration settings for the application.
    * **Program.cs**: Entry point of the application.
* #### **Domain**
    * **AutoMapper**
        * **AutoMapperConfiguration.cs**: Configuration for mapping between BusinessCard and BusinessCardViewModel.
    * **Models**
        * **BusinessCard.cs**: The business card entity model with properties such as Id, Name, Email, Gender, etc.
    * **ViewModels**
        * **BusinessCardViewModel.cs**: ViewModel for handling business card data in requests and responses.
        * **ExportFormatViewModel.cs**: ViewModel for handling export format selections.
        * **StandardResponse.cs**: A generic wrapper for API responses.
* #### **Repository**
    * **Constants**
        * **CommonConstants.cs**: Holds constant values used across the repository layer.
    * **Repository**
        * **IRepositorySQL.cs**: Defines the interface for SQL repository methods.
        * **RepositorySQL.cs**: Implementation of the repository interface for SQL operations.
* #### Services
    * **Interfaces**
        * **IBusinessCard.cs**: Service interface defining business card-related methods.
        * **IExportService.cs**: Interface for exporting business card data in various formats.
        * **IFileService.cs**: Interface for handling file uploads and processing.
    * Services
        * **BusinessCardServices.cs**: Implements business card-related services.
        * **ExportService.cs**: Implements the export functionality for business card data.
        * **FileService.cs**: Handles file-related operations like uploads.
    * Unit of Work
        * **IUnitOfWork.cs**: Interface for unit of work pattern.
        * **UnitOfWork.cs**: Manages transactional consistency across repositories.
    * **ServiceRegistration.cs**: Registers services and dependencies into the DI container.
* #### Tests
    * **Import Tests**: Verify that business card data can be imported correctly from CSV and XML files, ensuring proper repository calls are made.
    * **Export Tests**: Ensure that business card data can be exported to CSV and XML formats and handle cases where no data exists.
    * **Edge Case Tests**: Check that appropriate responses are returned when no data is available for export.


## Features

* **Onion Architecture**: Separation of concerns with well-defined layers.
* **CRUD Operations**: Full support for Create, Read, Update, and Delete operations on business cards.
* **Filter and Export Functionality**: Filter and export business card data in various formats (CSV, XML, etc.).
* **File Upload**: Allows uploading of files (such as images) associated with business cards.
* **Unit of Work Pattern**: Ensures transactional integrity across repository operations.
* **AutoMapper**: Simplifies object mapping between models and view models.

## Prerequisites
* **.NET Core SDK**: Ensure that you have .NET Core 5.0 or above installed.
* **AutoMapper**: AutoMapper is required for object-to-object mapping.
* **SQL Database**: A SQL-based database is needed for repository operations.
* **Swagger**: Swagger may be used to test the API endpoints.
## API Endpoints

The application provides several API endpoints to manage business card data. Below are the available endpoints:

### Create Business Card
- **POST** `/api/businesscard/create`
- **Request**: Form data for `BusinessCardViewModel` including optional `PhotoFile` upload.
- **Response**: Success or failure message.

### List Business Cards
- **POST** `/api/businesscard/list`
- **Response**: Returns a list of all business cards.

### Get Business Card by ID
- **POST** `/api/businesscard/get`
- **Request**: `IdViewModel` with the `Id` of the business card.
- **Response**: Details of the business card with the specified `Id`.

### Delete Business Card
- **POST** `/api/businesscard/delete`
- **Request**: `IdViewModel` with the `Id` of the business card to be deleted.
- **Response**: Success or failure message.

### Filter Business Cards
- **POST** `/api/businesscard/filter`
- **Request**: `BusinessCardFilter` with criteria for filtering (e.g., `Name`, `Email`, `Phone`, `Gender`).
- **Response**: Returns a list of business cards matching the filter criteria.

### Export Business Cards
- **POST** `/api/businesscard/export`
- **Request**: `ExportFormatViewModel` with the format (e.g., `csv`, `xml`).
- **Response**: File download of the exported business cards in the specified format.

### Import Business Cards
- **POST** `/api/businesscard/import`
- **Request**: `ImportFormatViewModel` for uploading a file with business card data.
- **Response**: Success or failure message.
## Example Request

To create a new business card, you can send a `POST` request to `/api/businesscard/create` with a form data payload:

```json
{
  "name": "Abdallah",
  "gender": "Male",
  "dateOfBirth": "1997-10-12",
  "email": "admin@abdallah.com",
  "phone": "96279123456",
  "address": "Amman",
  "photoFile": null
}
```
## Testing

The solution contains unit tests under the **Tests** project. To run the tests, use the following command:

```bash
dotnet test
```## License

MIT License

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.


