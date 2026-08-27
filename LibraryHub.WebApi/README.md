# LibraryHub

LibraryHub, kitap, üye, kategori ve ödünç alma işlemlerini yönetmek amacıyla geliştirilmiş bir kütüphane yönetim sistemi REST API projesidir.

## 🎯 Projenin Amacı

LibraryHub ile;

- Kitap kayıtları yönetilebilir.
- Kategoriler yönetilebilir.
- Kütüphane üyeleri yönetilebilir.
- Kitap ödünç alma ve iade işlemleri gerçekleştirilebilir.
- Gecikmiş iadeler için ceza oluşturulabilir.
- Kullanıcı kimlik doğrulama ve rol bazlı yetkilendirme işlemleri uygulanabilir.
- Kitaplar sayfalama ve arama özellikleriyle listelenebilir.

## 🏗️ Mimari

Proje, katmanlı mimari yaklaşımı kullanılarak geliştirilmiştir.

```text
LibraryHub
│
├── LibraryHub.Domain
│   ├── Entities
│   ├── Enums
│   └── Common
│
├── LibraryHub.Application
│   ├── DTOs
│   ├── Interfaces
│   ├── Services
│   ├── Validators
│   ├── Mappings
│   └── Results
│
├── LibraryHub.Infrastructure
│   ├── Configurations
│   ├── Migrations
│   ├── Persistence
│   ├── Repositories
│   └── Services
│
├── LibraryHub.WebApi
│   ├── Controllers
│   ├── Middleware
│   └── Program.cs
│
├── LibraryHub.IntegrationTests
│
└── LibraryHub.UnitTests
Katmanlar

Domain

Uygulamanın temel entity, enum ve ortak sınıflarını içerir.

Application

İş kuralları, servisler, DTO'lar, validasyonlar, interface'ler ve sonuç modellerini içerir.

Infrastructure

Veritabanı işlemleri, Entity Framework Core yapılandırmaları, repository'ler ve altyapı servislerini içerir.

WebApi

HTTP isteklerini karşılayan controller'ları ve API yapılandırmasını içerir.

IntegrationTests

API ve uygulamanın farklı bileşenlerinin birlikte çalışmasını test eder.

UnitTests

Uygulamadaki bağımsız iş mantıklarının testlerini içerir.

🛠️ Kullanılan Teknolojiler
C#
.NET
ASP.NET Core Web API
Entity Framework Core
SQL Server
AutoMapper
FluentValidation
JWT
Swagger / OpenAPI
xUnit
📋 Gereksinimler

Projeyi çalıştırmadan önce aşağıdakilerin kurulu olması gerekir:

.NET SDK
SQL Server
Visual Studio
🚀 Kurulum
1. Projeyi Klonlama
git clone https://github.com/eliflknt/LibraryHub.git
2. Proje Klasörüne Girme
cd LibraryHub
3. Veritabanı Yapılandırması

LibraryHub.WebApi projesindeki yapılandırma dosyasından SQL Server bağlantı bilgisini kendi ortamınıza göre düzenleyin.

4. Veritabanını Oluşturma

Entity Framework Core migration'larını kullanarak veritabanını oluşturun:

dotnet ef database update
5. Projeyi Çalıştırma
dotnet run --project LibraryHub.WebApi
📖 API Dokümantasyonu

LibraryHub, Swagger / OpenAPI desteğine sahiptir.

Uygulama çalıştırıldıktan sonra Swagger arayüzü üzerinden API endpoint'leri incelenebilir ve test edilebilir.

Swagger üzerinden;

Books
Categories
Members
Loans
Users
Reports

endpoint'lerine erişilebilir.

Örnek API Kullanımı

Kitapları listelemek için:

GET /api/books

Belirli bir kitabı getirmek için:

GET /api/books/{id}

Kitap oluşturmak için:

POST /api/books
Content-Type: application/json

Ödünç alma işlemi için:

POST /api/loans/borrow
Content-Type: application/json

Kitap iade işlemi için:

POST /api/loans/return/{loanId}

Endpoint'ler Swagger arayüzü üzerinden incelenerek test edilebilir.

🔐 Kimlik Doğrulama ve Yetkilendirme

API'de JWT tabanlı kimlik doğrulama kullanılmaktadır.

Yetkilendirme gerektiren endpoint'lerde JWT Bearer token kullanılır.

📚 Temel İş Kuralları
Kitap Ödünç Alma
Sadece aktif üyeler kitap ödünç alabilir.
Rafta bulunmayan kitap ödünç alınamaz.
Bir üye aynı anda en fazla 3 aktif kitap ödünç alabilir.
Aynı kitap aynı üyeye aynı anda tekrar verilemez.
Ödenmemiş cezası bulunan üye yeni kitap ödünç alamaz.
Ödünç süresi 14 gündür.
Kitap İade
Daha önce iade edilmiş bir kitap tekrar iade edilemez.
İade sırasında kitap stoğu artırılır.
Gecikme durumunda ceza oluşturulur.
Günlük gecikme cezası 2 TL'dir.
🧪 Testler

Projede unit test ve integration test projeleri bulunmaktadır.

Testleri çalıştırmak için:

dotnet test
📁 Proje Yapısı
Proje	Açıklama
LibraryHub.Domain	Entity, enum ve ortak domain yapıları
LibraryHub.Application	İş mantığı, servisler, DTO ve validasyonlar
LibraryHub.Infrastructure	Veritabanı ve repository işlemleri
LibraryHub.WebApi	REST API
LibraryHub.IntegrationTests	Entegrasyon testleri
LibraryHub.UnitTests	Birim testleri
📸 Ekran Görüntüleri
Swagger API

Swagger arayüzü üzerinden API endpoint'leri görüntülenebilir ve test edilebilir.

Swagger ekran görüntüsü buraya eklenecektir.

Proje Yapısı

LibraryHub katmanlı mimari yapısı Visual Studio Solution Explorer üzerinden görüntülenebilir.

Proje yapısı ekran görüntüsü buraya eklenecektir.

👩‍💻 Geliştirici

Elif Alkanat

LibraryHub, yazılım geliştirme ve staj süreci kapsamında geliştirilmiştir.