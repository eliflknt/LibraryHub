# LibraryHub

LibraryHub, kütüphane yönetim süreçlerini API üzerinden gerçekleştirmek amacıyla geliştirilmiş bir Web API projesidir.

## Projenin Amacı

LibraryHub ile kütüphane içerisindeki kitap, kategori, üye ve ödünç alma işlemlerinin yönetilmesi amaçlanmıştır.

Proje kapsamında:

- Kitap yönetimi
- Kategori yönetimi
- Üye yönetimi
- Ödünç alma işlemleri
- Ceza işlemleri
- Kullanıcı kimlik doğrulama
- Rol tabanlı yetkilendirme
- Raporlama işlemleri

gerçekleştirilmektedir.

## Kullanılan Teknolojiler

- C#
- .NET
- ASP.NET Core Web API
- Entity Framework Core
- Microsoft SQL Server
- JWT
- AutoMapper
- FluentValidation
- Serilog
- Swagger
- xUnit
- Git / GitHub

## Proje Mimarisi

Proje katmanlı mimari yapısına göre geliştirilmiştir.

### LibraryHub.Domain

Projenin temel entity ve enum yapılarını içerir.

### LibraryHub.Application

DTO, servis, interface, validation ve mapping işlemlerini içerir.

### LibraryHub.Infrastructure

Entity Framework Core, DbContext, Repository, veritabanı yapılandırmaları ve servis implementasyonlarını içerir.

### LibraryHub.WebApi

API controller'larını, middleware yapılarını, JWT işlemlerini ve uygulamanın başlangıç konfigürasyonlarını içerir.

### LibraryHub.IntegrationTests

API içerisindeki temel entegrasyon testlerini içerir.

## Kurulum

Projeyi çalıştırmak için öncelikle repository klonlanmalıdır.

```bash
git clone https://github.com/eliflknt/LibraryHub.git

Proje klasörüne geçilir:

cd LibraryHub

Gerekli NuGet paketleri restore edilir:

dotnet restore

Proje build edilir:

dotnet build
Veritabanı

Proje Microsoft SQL Server kullanmaktadır.

Migration işlemlerini uygulamak için:

dotnet ef database update --project LibraryHub.Infrastructure --startup-project LibraryHub.WebApi
Uygulamayı Çalıştırma

Web API projesini çalıştırmak için:

dotnet run --project LibraryHub.WebApi

Uygulama çalıştırıldıktan sonra Swagger arayüzü üzerinden API endpointleri test edilebilir.

Swagger

Swagger, API endpointlerinin görüntülenmesi ve test edilmesi amacıyla kullanılmaktadır.

Swagger üzerinden:

GET
POST
PUT
DELETE

işlemleri test edilebilir.

Ayrıca JWT tabanlı kimlik doğrulama ve yetkilendirme işlemleri de API üzerinde kullanılmaktadır.

Testler

Projede entegrasyon testleri bulunmaktadır.

Testleri çalıştırmak için:

dotnet test
Git ve GitHub

Projede kaynak kod yönetimi için Git kullanılmaktadır.

Geliştirmeler commit edilerek GitHub repository'sine gönderilmektedir.

Proje Yapısı
LibraryHub
│
├── LibraryHub.Application
│   ├── DTOs
│   ├── Interfaces
│   ├── Mappings
│   ├── Results
│   ├── Services
│   └── Validators
│
├── LibraryHub.Domain
│   ├── Entities
│   └── Enums
│
├── LibraryHub.Infrastructure
│   ├── Configurations
│   ├── Migrations
│   ├── Persistence
│   ├── Repositories
│   └── Services
│
├── LibraryHub.IntegrationTests
│
└── LibraryHub.WebApi
    ├── Controllers
    ├── Middleware
    ├── Services
    ├── ViewModels
    └── Program.cs
Ekran Görüntüleri

Swagger API arayüzüne ait ekran görüntüleri bu bölümde yer alacaktır.

Geliştirici

LibraryHub projesi eğitim ve staj çalışmaları kapsamında geliştirilmiştir.