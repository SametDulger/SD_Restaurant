# 🍽️ SD Restaurant Management System

Modern restaurant, cafe ve bar işletmelerinin günlük operasyonlarını dijitalleştirmek için geliştirilmiş kapsamlı bir yönetim sistemidir. Bu proje, .NET 9.0 teknolojisi kullanılarak Clean Architecture prensiplerine uygun olarak tasarlanmıştır.

## 🚨 **ÖNEMLİ NOT**

Bu proje **aktif geliştirme aşamasındadır** ve henüz production ortamı için hazır değildir. Aşağıdaki durumlar göz önünde bulundurulmalıdır:

- ⚠️ **Geliştirme Aşaması**: Proje sürekli geliştirilmekte ve yeni özellikler eklenmektedir
- 🔧 **Eksik Özellikler**: Bazı modüller henüz tamamlanmamış olabilir
- 🐛 **Potansiyel Hatalar**: Geliştirme sürecinde hatalar bulunabilir
- 📊 **Test Eksikliği**: Kapsamlı test coverage henüz tamamlanmamıştır
- 🔒 **Güvenlik**: Production ortamı için ek güvenlik önlemleri gerekebilir

## 🏗️ **Proje Mimarisi**

Proje, SOLID prensiplerine ve Clean Architecture pattern'ine uygun olarak 5 katmanlı mimari kullanır:

### 📁 **Katmanlar**

| Katman | Proje | Açıklama |
|--------|-------|----------|
| **Domain** | `SD_Restaurant.Core` | Entities, interfaces, domain logic |
| **Application** | `SD_Restaurant.Application` | Business services, DTOs, validators |
| **Infrastructure** | `SD_Restaurant.Infrastructure` | Data access, repositories, external services |
| **Presentation** | `SD_Restaurant.API` | Web API controllers, middleware |
| **Web UI** | `SD_Restaurant.Web` | MVC web interface |

### 🔄 **Veri Akışı**
```
Web UI → API → Application → Infrastructure → Database
API → Application → Infrastructure → Database
```

## 🚀 **Özellikler**

### 📦 **Ürün Yönetimi**
- ✅ Ürün CRUD operasyonları
- ✅ Kategori bazlı gruplandırma
- ✅ Reçete tanımlama ve maliyet hesaplama
- ✅ Ürün arama ve filtreleme
- ✅ Fiyat yönetimi

### 📊 **Stok Yönetimi**
- ✅ Gerçek zamanlı stok takibi
- ✅ Minimum stok uyarıları
- ✅ Lokasyon bazlı stok yönetimi
- ✅ Stok maliyet hesaplama
- 🔄 Stok hareket geçmişi (geliştirme aşamasında)

### 🍽️ **Sipariş Yönetimi**
- ✅ Masa bazlı sipariş alma
- ✅ Sipariş durumu takibi (Beklemede, Hazırlanıyor, Hazır, Teslim Edildi)
- 🔄 Otomatik fiyat hesaplama (KDV, indirim - geliştirme aşamasında)
- ✅ Özel talimatlar ve notlar
- ✅ Sipariş geçmişi

### 🏠 **Masa Yönetimi**
- ✅ Masa durumu takibi (Boş, Dolu, Rezerve, Temizlik)
- ✅ Kapasite yönetimi
- ✅ Lokasyon bazlı gruplandırma
- 🔄 Masa rezervasyon sistemi (geliştirme aşamasında)

### 👥 **Müşteri Yönetimi**
- ✅ Müşteri bilgileri kaydetme
- ✅ Müşteri arama ve filtreleme
- ✅ Müşteri tipi (Bireysel, Kurumsal)
- ✅ Ziyaret sayısı ve toplam harcama takibi
- 🔄 Müşteri geçmişi (geliştirme aşamasında)

### 👨‍💼 **Personel Yönetimi**
- ✅ Personel bilgileri kaydetme
- ✅ Pozisyon ve departman bazlı gruplandırma
- ✅ Personel arama ve filtreleme
- 🔄 Personel performans takibi (geliştirme aşamasında)

### 📅 **Rezervasyon Yönetimi**
- ✅ Tarih ve saat bazlı rezervasyon
- ✅ Masa kapasitesi kontrolü
- ✅ Rezervasyon durumu takibi
- ✅ Özel istekler kaydetme
- 🔄 Rezervasyon onaylama/iptal (geliştirme aşamasında)

### 💳 **Ödeme Yönetimi**
- ✅ Çoklu ödeme yöntemi (Nakit, Kredi Kartı, Online)
- ✅ Ödeme durumu takibi
- ✅ Tarih bazlı ödeme raporları
- 🔄 Fatura oluşturma (geliştirme aşamasında)

## 🛠️ **Teknoloji Stack**

### **Backend**
- **.NET 9.0** - Modern .NET platformu
- **Entity Framework Core** - ORM framework
- **SQL Server** - Veritabanı (SQLite'dan geçiş yapıldı)
- **AutoMapper** - Object mapping
- **FluentValidation** - Input validation
- **Serilog** - Structured logging

### **Frontend**
- **ASP.NET Core MVC** - Web interface
- **Bootstrap 5** - CSS framework
- **jQuery** - JavaScript library
- **Font Awesome** - Icon library

### **API & Documentation**
- **ASP.NET Core Web API** - RESTful API
- **Swagger/OpenAPI** - API documentation
- **Health Checks** - System monitoring

## 📋 **Sistem Gereksinimleri**

### **Geliştirme Ortamı**
- .NET 9.0 SDK
- Visual Studio 2022 veya VS Code
- SQL Server Express (MSI\SQLEXPRESS)
- Git

### **Production Ortamı**
- .NET 9.0 Runtime
- SQL Server (Standard/Enterprise)
- IIS veya Kestrel
- Windows Server 2019+

## 🚀 **Kurulum ve Çalıştırma**

### **1. Projeyi Klonlayın**
```bash
git clone https://github.com/SametDulger/SD_Restaurant.git
cd SD_Restaurant
```

### **2. Bağımlılıkları Yükleyin**
```bash
dotnet restore SD_Restaurant.sln
```

### **3. Veritabanını Hazırlayın**
```bash
# SQL Server'ın çalıştığından emin olun
# Veritabanı otomatik oluşturulacaktır
```

### **4. API'yi Çalıştırın**
```bash
dotnet run --project SD_Restaurant.API
```

### **5. Web UI'ı Çalıştırın**
```bash
dotnet run --project SD_Restaurant.Web
```

### **6. Erişim Adresleri**
- **API**: http://localhost:5195
- **Swagger**: http://localhost:5195/swagger
- **Health Check**: http://localhost:5195/health
- **Web UI**: http://localhost:5224

## 📚 **API Endpoints**

### **Ürünler** `/api/products`
- `GET` - Tüm ürünleri listele
- `GET /{id}` - Ürün detayı
- `GET /category/{categoryId}` - Kategoriye göre ürünler
- `GET /search` - Ürün arama
- `GET /low-stock` - Düşük stok ürünleri
- `GET /{id}/cost` - Ürün maliyeti
- `POST` - Yeni ürün ekle
- `PUT /{id}` - Ürün güncelle
- `DELETE /{id}` - Ürün sil

### **Kategoriler** `/api/categories`
- `GET` - Tüm kategorileri listele
- `GET /{id}` - Kategori detayı
- `POST` - Yeni kategori ekle
- `PUT /{id}` - Kategori güncelle
- `DELETE /{id}` - Kategori sil

### **Siparişler** `/api/orders`
- `GET` - Tüm siparişleri listele
- `GET /{id}` - Sipariş detayı
- `GET /table/{tableId}` - Masaya göre siparişler
- `GET /status/{status}` - Duruma göre siparişler
- `GET /date-range` - Tarih aralığına göre siparişler
- `GET /{id}/total` - Sipariş toplam tutarı
- `POST` - Yeni sipariş oluştur
- `POST /{id}/process` - Sipariş işle
- `PUT /{id}` - Sipariş güncelle
- `DELETE /{id}` - Sipariş sil

### **Stoklar** `/api/stocks`
- `GET` - Tüm stokları listele
- `GET /{id}` - Stok detayı
- `GET /low-stock` - Düşük stok uyarıları
- `GET /location/{location}` - Lokasyona göre stoklar
- `GET /product/{productId}` - Ürüne göre stoklar
- `GET /check-availability` - Stok müsaitlik kontrolü
- `POST` - Yeni stok ekle
- `PUT /{id}` - Stok güncelle
- `PUT /update-quantity` - Stok miktarı güncelle
- `DELETE /{id}` - Stok sil

### **Masalar** `/api/tables`
- `GET` - Tüm masaları listele
- `GET /{id}` - Masa detayı
- `GET /status/{status}` - Duruma göre masalar
- `GET /location/{location}` - Lokasyona göre masalar
- `POST` - Yeni masa ekle
- `PUT /{id}` - Masa güncelle
- `PUT /{id}/status` - Masa durumu güncelle
- `DELETE /{id}` - Masa sil

### **Müşteriler** `/api/customers`
- `GET` - Tüm müşterileri listele
- `GET /{id}` - Müşteri detayı
- `GET /search` - Müşteri ara
- `POST` - Yeni müşteri ekle
- `PUT /{id}` - Müşteri güncelle
- `DELETE /{id}` - Müşteri sil

### **Personel** `/api/employees`
- `GET` - Tüm personeli listele
- `GET /{id}` - Personel detayı
- `GET /position/{position}` - Pozisyona göre personel
- `GET /department/{department}` - Departmana göre personel
- `POST` - Yeni personel ekle
- `PUT /{id}` - Personel güncelle
- `DELETE /{id}` - Personel sil

### **Rezervasyonlar** `/api/reservations`
- `GET` - Tüm rezervasyonları listele
- `GET /{id}` - Rezervasyon detayı
- `GET /date/{date}` - Tarihe göre rezervasyonlar
- `GET /table/{tableId}` - Masaya göre rezervasyonlar
- `POST` - Yeni rezervasyon oluştur
- `PUT /{id}` - Rezervasyon güncelle
- `DELETE /{id}` - Rezervasyon sil

### **Ödemeler** `/api/payments`
- `GET` - Tüm ödemeleri listele
- `GET /{id}` - Ödeme detayı
- `GET /order/{orderId}` - Siparişe göre ödemeler
- `GET /method/{paymentMethod}` - Ödeme yöntemine göre ödemeler
- `GET /date-range` - Tarih aralığına göre ödemeler
- `POST` - Yeni ödeme ekle
- `PUT /{id}` - Ödeme güncelle
- `DELETE /{id}` - Ödeme sil

### **Reçeteler** `/api/recipes`
- `GET` - Tüm reçeteleri listele
- `GET /{id}` - Reçete detayı
- `GET /product/{productId}` - Ürüne göre reçeteler
- `GET /ingredient/{ingredientId}` - Malzemeye göre reçeteler
- `GET /product/{productId}/ingredient/{ingredientId}` - Ürün ve malzemeye göre reçete
- `POST` - Yeni reçete ekle
- `PUT /{id}` - Reçete güncelle
- `DELETE /{id}` - Reçete sil

## 🗄️ **Veritabanı Şeması**

### **Ana Varlıklar**
- **Products** - Ürün bilgileri ve fiyatları (malzemeler de ürün olarak saklanır)
- **Categories** - Ürün kategorileri
- **Recipes** - Ürün reçeteleri ve malzemeleri (Product ↔ Product ilişkisi)
- **Stocks** - Stok bilgileri ve lokasyonları
- **Tables** - Masa bilgileri ve durumları
- **Orders** - Sipariş ana bilgileri
- **OrderItems** - Sipariş kalemleri
- **Customers** - Müşteri bilgileri
- **Employees** - Personel bilgileri
- **Reservations** - Rezervasyon bilgileri
- **Payments** - Ödeme bilgileri

### **İlişkiler**
- Ürün ↔ Kategori (Many-to-One)
- Ürün ↔ Reçete (One-to-Many) - Ürünün malzemeleri
- Malzeme ↔ Reçete (One-to-Many) - Malzeme olarak kullanılan ürünler
- Ürün ↔ Stok (One-to-Many)
- Ürün ↔ Sipariş Kalemi (One-to-Many)
- Masa ↔ Sipariş (One-to-Many)
- Müşteri ↔ Sipariş (One-to-Many)
- Personel ↔ Sipariş (One-to-Many)
- Sipariş ↔ Ödeme (One-to-Many)
- Masa ↔ Rezervasyon (One-to-Many)
- Müşteri ↔ Rezervasyon (One-to-Many)

## 🔧 **Konfigürasyon**

### **Veritabanı Bağlantısı**
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=MSI\\SQLEXPRESS;Database=SD_Restaurant;Trusted_Connection=true;TrustServerCertificate=true;MultipleActiveResultSets=true"
  }
}
```

### **Logging Konfigürasyonu**
```json
{
  "Serilog": {
    "MinimumLevel": {
      "Default": "Information",
      "Override": {
        "Microsoft": "Warning",
        "System": "Warning"
      }
    },
    "WriteTo": [
      {
        "Name": "Console"
      },
      {
        "Name": "File",
        "Args": {
          "path": "Logs/log-.txt",
          "rollingInterval": "Day",
          "retainedFileCountLimit": 7
        }
      }
    ]
  }
}
```

## 📊 **Örnek Kullanım Senaryoları**

### **1. Yeni Ürün Ekleme**
```json
POST /api/products
{
  "name": "Çilekli Kokteyl",
  "description": "Taze çileklerle hazırlanmış özel kokteyl",
  "price": 45.00,
  "unit": "bardak",
  "categoryId": 1,
  "isRecipe": true
}
```

### **2. Sipariş Oluşturma**
```json
POST /api/orders
{
  "tableId": 1,
  "customerId": 1,
  "employeeId": 1,
  "notes": "Acil sipariş",
  "orderItems": [
    {
      "productId": 1,
      "quantity": 2,
      "specialInstructions": "Buzsuz"
    }
  ]
}
```

### **3. Stok Güncelleme**
```json
PUT /api/stocks/update-quantity
{
  "productId": 1,
  "location": "Depo",
  "quantity": 50
}
```

## 🧪 **Test ve Geliştirme**

### **Swagger UI**
API'yi test etmek için Swagger UI kullanın:
```
http://localhost:5195/swagger
```

### **Health Check**
Sistem durumunu kontrol edin:
```
http://localhost:5195/health
```

### **Log Dosyaları**
Uygulama logları şu konumda bulunur:
```
SD_Restaurant.API/Logs/
```

## 🔮 **Gelecek Özellikler**

### **Planlanan Geliştirmeler**
- 📱 **Mobil Uygulama** - iOS/Android native uygulamalar
- 📊 **Raporlama Modülü** - Detaylı analiz ve raporlar
- 🔔 **Bildirim Sistemi** - Email/SMS bildirimleri
- 💳 **Online Ödeme** - Stripe/PayPal entegrasyonu
- 📈 **Dashboard** - Gerçek zamanlı metrikler
- 🔐 **Kullanıcı Yönetimi** - Role-based access control
- 📱 **QR Kod Sistemi** - Masa QR kodları
- 🍽️ **Menü Yönetimi** - Dinamik menü sistemi

### **Teknik İyileştirmeler**
- 🧪 **Unit Tests** - Kapsamlı test coverage
- 🔒 **Authentication** - JWT token authentication
- 🚀 **Performance** - Caching ve optimization
- 📦 **Docker** - Containerization
- ☁️ **Cloud Ready** - Azure/AWS deployment

## 🤝 **Katkıda Bulunma**

Bu proje açık kaynak olarak geliştirilmektedir. Katkıda bulunmak için:

1. **Fork** yapın
2. **Feature branch** oluşturun (`git checkout -b feature/AmazingFeature`)
3. **Commit** yapın (`git commit -m 'Add some AmazingFeature'`)
4. **Push** yapın (`git push origin feature/AmazingFeature`)
5. **Pull Request** oluşturun

### **Geliştirme Kuralları**
- Clean Code prensiplerine uyun
- SOLID prensiplerini takip edin
- Unit test yazın
- Documentation güncelleyin
- Code review sürecine katılın

## 📞 **İletişim ve Destek**

- **Issues**: GitHub Issues kullanarak hata bildirin
- **Discussions**: GitHub Discussions'da soru sorun
- **Email**: Proje sahibi ile iletişim için GitHub Issues kullanın

## 📝 **Lisans**

Bu proje MIT lisansı altında lisanslanmıştır. Detaylar için [LICENSE](LICENSE) dosyasına bakın.

## 🔄 **Sürüm Geçmişi**

### **v1.0.0** (Geliştirme Aşaması)
- ✅ Temel CRUD operasyonları
- ✅ Katmanlı mimari
- ✅ SQL Server entegrasyonu
- ✅ Swagger dokümantasyonu
- ✅ AutoMapper entegrasyonu
- ✅ FluentValidation
- ✅ Serilog logging
- ✅ Health checks
- ✅ MVC Web interface
- 🔄 Geliştirme devam ediyor...

---

**⚠️ Uyarı**: Bu proje geliştirme aşamasındadır. Production ortamında kullanmadan önce kapsamlı test yapılması önerilir. 