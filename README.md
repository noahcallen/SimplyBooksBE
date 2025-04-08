# 📘 SimplyBooks-BE

### Project Name: **Hymnal-History**

---

## 📖 Overview

**SimplyBooks-BE** is the backend for the SimplyBooks full-stack web application. Built using **C#/.NET**, this RESTful API enables complete CRUD functionality for **Authors** and **Books**, and establishes strong relational mapping between them.

This project follows the **repository pattern** to maintain clean architecture and scalability. The project is structured into the following folders:

- **Data**: Contains context and seeded data logic (`AuthorData.cs`, `BookData.cs`, `UserData.cs`, `SimplyBooksDbContext.cs`)
- **Endpoints**: Minimal API routes for `AuthorEndpoints.cs` and `BookEndpoints.cs`
- **Interfaces**: Contract definitions for Repositories and Services
- **Models**: Data models for `Author`, `Book`, and `User`
- **Repositories**: Logic for database access in `SimplyBooksAuthorRepository.cs` and `SimplyBooksBookRepository.cs`
- **Services**: Business logic separated into `SimplyBooksAuthorService.cs` and `SimplyBooksBookService.cs`

---

## 🧠 Features

- ✅ Seeded initial data for Authors, Books, and Users
- ✅ Full **CRUD functionality** for both Authors and Books
- ✅ Cascade delete: Deleting an Author removes all their associated Books
- ✅ Entity relationships established (Book ↔ Author, Author ↔ User)
- ✅ User-specific data: Users only see their own Authors and Books
- ✅ Fully documented and tested in **Postman**
- ✅ Swagger UI integrated for API testing

---

## 📡 API Endpoints

### 🔹 **Authors**

- `GET /authors` – Get all authors
- `GET /authors/user/{userId}` – Get all authors by user
- `GET /authors/{id}` – Get author by ID with books
- `POST /authors` – Create a new author
- `PUT /authors/{id}` – Update author details
- `DELETE /authors/{id}` – Delete author and all their books

### 🔹 **Books**

- `GET /books` – Get all books
- `GET /books/user/{userId}` – Get books by user
- `GET /books/{id}` – Get book by ID with author
- `POST /books` – Create a new book
- `PUT /books/{id}` – Update book details
- `DELETE /books/{id}` – Delete book

---

## 🗺 Entity Relationship Diagram (ERD)

[Click to view ERD](https://dbdiagram.io/d/Almost-Amazon-60315ba6fcdcb6230b20bbaa?utm_source=dbdiagram_embed&utm_medium=bottom_open)

---

## 👤 User & Problem Statement

Users of the SimplyBooks frontend app need to manage their personal libraries by performing full CRUD operations on **Authors** and **Books**. Each user has their own dataset and can only view and manage their own content. The backend securely handles these relationships and operations to enable a seamless frontend experience.

---

## 🖼 Screenshots

> ![SimplyBooks Swagger Screenshot](![Swagger SimplyBooksBE](https://github.com/user-attachments/assets/357e436f-2a82-4797-ae5e-49e2fd781782)
)

---

## 🧑‍💻 Contributors

- **Noah Allen** – [GitHub Profile]([https://github.com/MJSuttles](https://github.com/noahcallen))

---

## 🎥 Loom Video Walkthrough

[Loom Video Walkthrough([https://www.loom.com/share/b60e10cd5cf948d99e3ebe9afa1fafc6](https://www.loom.com/share/3777eaabfde741e2aa63e406f03ee62b?sid=ead8d0be-e74c-4c3e-bb3c-fc6c73f081fc))

---

##Postman Link: https://hustlegrouprareapi.postman.co/workspace/HustleGroupRAREAPI-Workspace~3dce0828-ed88-4746-86fa-6d0a3066e35f/collection/31594183-b11a607b-687c-4a12-b34d-311429dbae5d?action=share&creator=31594183

## 🔌 Running the Project Locally

Make sure you have the following installed:
- .NET 8.0 SDK+
- PostgreSQL (or your preferred DBMS)
- Postman (for testing the API)
- Swagger (auto-enabled at `/swagger`)

Run the app:

```bash
dotnet ef database update
dotnet run
