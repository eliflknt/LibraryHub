# LibraryHub Veri Sözlüğü

## Member

| Kolon | Veri Tipi | Kısıt |
|-------|-----------|-------|
| Id | int | Primary Key |
| FullName | varchar(100) | NOT NULL |
| Email | varchar(100) | NOT NULL, UNIQUE |
| Phone | varchar(20) | NULL |
| MembershipDate | date | NOT NULL |
| IsActive | boolean | NOT NULL |

## Category

| Kolon | Veri Tipi | Kısıt |
|-------|-----------|-------|
| Id | int | Primary Key |
| Name | varchar(100) | NOT NULL, UNIQUE |
| Description | varchar(255) | NULL |

## Author

| Kolon | Veri Tipi | Kısıt |
|-------|-----------|-------|
| Id | int | Primary Key |
| FullName | varchar(100) | NOT NULL |
| Biography | text | NULL |

## Book

| Kolon | Veri Tipi | Kısıt |
|-------|-----------|-------|
| Id | int | Primary Key |
| ISBN | varchar(20) | NOT NULL, UNIQUE |
| Title | varchar(150) | NOT NULL |
| PublishYear | int | NULL |
| StockQuantity | int | NOT NULL |
| ShelfQuantity | int | NOT NULL |
| CategoryId | int | Foreign Key, NOT NULL |

## BookAuthor

| Kolon | Veri Tipi | Kısıt |
|-------|-----------|-------|
| BookId | int | Foreign Key |
| AuthorId | int | Foreign Key |

## Loan

| Kolon | Veri Tipi | Kısıt |
|-------|-----------|-------|
| Id | int | Primary Key |
| BookId | int | Foreign Key, NOT NULL |
| MemberId | int | Foreign Key, NOT NULL |
| BorrowDate | date | NOT NULL |
| DueDate | date | NOT NULL |
| ReturnDate | date | NULL |
| Status | varchar(20) | NULL |

## Fine

| Kolon | Veri Tipi | Kısıt |
|-------|-----------|-------|
| Id | int | Primary Key |
| LoanId | int | Foreign Key, NOT NULL |
| Amount | decimal | NOT NULL |
| IsPaid | boolean | NOT NULL |