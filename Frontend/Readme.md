# Business Card Application

This project is an Angular-based web application for managing business cards. It includes features like listing, adding, importing, and exporting business cards, all interacting with a backend API.

## Features
- **Business Card List:** View all business cards.
- **Add Business Card:** Add a new business card to the list.
- **Import Business Cards:** Upload a file (CSV, XML, or QR with JSON) to import business cards.
- **Export Business Cards:** Export business cards in different formats.
- **Delete Business Card:** Remove a business card from the system.


## Components:
- **business-card-add:** Handles the addition of a new business card.
- **business-card-import:** Handles the import of business cards from files.
- **business-card-list:** Displays the list of business cards.
- **business-card-preview:** Previews individual business cards.

## Directives:
- **drag-drop.directive.ts:** A directive for handling drag-and-drop file uploads.

## Services:
- **business-card.service.ts:** Provides methods for interacting with the backend API for business card operations.

## API Configuration
The API base URL is configured in the environment file:
```typescript
export const environment = {
  production: false,
  Api_URL: "http://localhost:15055/api/BusinessCard"
};
```
## Available API Endpoints
- **POST /list** - Retrieves the list of business cards.
- **POST /get** - Retrieves a business card by its ID.
- **POST /create** - Adds a new business card.
- **POST /delete** - Deletes a business card by its ID.
- **POST /filter** - Filters business cards.
- **POST /export** - Exports business cards in a specified format.
- **POST /import** - Imports business cards from a file.

## File Upload Size Limit
The maximum allowed file size for uploading is 1 MB. Ensure that the uploaded files (CSV, XML, or QR code JSON) comply with this limit.

## Installation and Setup

1. **Download the repository**
 
2. Install dependencies:
    ```bash
   npm install
   ```
3. Run the application:
    ```bash
   ng s -o
   ```
