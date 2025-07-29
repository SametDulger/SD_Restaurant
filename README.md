# 🍽️ SD Restaurant Management System

> **⚠️ Proje Durumu: Eğitim Amaçlı**
> 
> Bu proje, modern restaurant yönetim sistemlerinin nasıl tasarlanacağını göstermek amacıyla geliştirilmiş **eğitim amaçlı bir uygulamadır**. 
> 
> **Önemli Notlar:**
> - 📚 **Eğitim ve öğrenme amaçlı** tasarlanmıştır
> - 🚧 Production ortamında kullanım için **hazır değildir**
> - 🔒 **Güvenlik önlemleri** alınması zorunludur
> - 🛡️ [SECURITY.md](./SECURITY.md) dosyasını mutlaka okuyun
> - ⚠️ Production kullanımı için ek test ve güvenlik önlemleri gereklidir

Modern restaurant, cafe ve bar işletmelerinin günlük operasyonlarını dijitalleştirmek için geliştirilmiş kapsamlı bir yönetim sistemidir.

## 🚀 **Hızlı Başlangıç**

### **Gereksinimler**
- .NET 9.0 SDK
- SQL Server
- Visual Studio 2022 veya VS Code

### **Kurulum**
```bash
# Projeyi klonlayın
git clone https://github.com/SametDulger/SD_Restaurant.git
cd SD_Restaurant

# Bağımlılıkları yükleyin
dotnet restore

# Konfigürasyon dosyalarını oluşturun
cp appsettings.Example.json SD_Restaurant.Web/appsettings.json
cp SD_Restaurant.API/appsettings.Example.json SD_Restaurant.API/appsettings.json

# Veritabanını oluşturun
cd SD_Restaurant.API
dotnet ef database update

# API'yi çalıştırın
dotnet run

# Web UI'ı çalıştırın (yeni terminal)
cd ../SD_Restaurant.Web
dotnet run

# Not: Web UI http://localhost:5000 adresinde çalışacak
# API http://localhost:5195 adresinde çalışacak
```

### **🔒 Güvenlik Konfigürasyonu**
Production kullanımı için aşağıdaki güvenlik önlemlerini mutlaka alın:

#### **⚠️ Kritik Güvenlik Önlemleri**
1. **JWT Secret Key değiştirin** - Tüm appsettings dosyalarında
2. **Veritabanı şifrelerini güçlendirin** - Docker ve connection string'lerde
3. **Environment variables kullanın** - Hassas bilgileri kod içinde saklamayın
4. **HTTPS kullanın** - Production'da mutlaka SSL sertifikası ekleyin
5. **CORS ayarlarını güncelleyin** - Gateway'deki AllowAll politikasını değiştirin
6. **Swagger'ı kapatın** - Production'da Swagger UI'ı devre dışı bırakın
7. **Default şifreleri değiştirin** - Docker'daki admin/admin123 şifrelerini güncelleyin

### **Erişim Adresleri**
- **Web UI**: http://localhost:5000
- **API**: http://localhost:5195
- **Swagger**: http://localhost:5195/swagger
- **Health Check**: http://localhost:5195/health

## 🏗️ **Proje Mimarisi**

```
SD_Restaurant.Core          # Entities, interfaces
SD_Restaurant.Application   # Business services, DTOs
SD_Restaurant.Infrastructure # Data access, repositories
SD_Restaurant.API          # Web API controllers
SD_Restaurant.Web          # MVC web interface
SD_Restaurant.Gateway      # API Gateway (Ocelot)
SD_Restaurant.Tests        # Unit & Integration tests
```

## ✨ **Özellikler**

### 📦 **Temel Modüller**
- **Ürün Yönetimi** - Kategoriler, fiyatlar, reçeteler
- **Stok Yönetimi** - Gerçek zamanlı takip, uyarılar
- **Sipariş Yönetimi** - Masa bazlı, durum takibi
- **Masa Yönetimi** - Durum, kapasite, rezervasyon
- **Müşteri Yönetimi** - Profil, geçmiş, analiz
- **Personel Yönetimi** - Bilgiler, performans
- **Rezervasyon Sistemi** - Tarih/saat bazlı
- **Ödeme Yönetimi** - Çoklu yöntem, raporlar

### 🚀 **Enterprise Özellikler**
- **Redis Caching** - Performans optimizasyonu
- **RabbitMQ** - Message queue
- **Prometheus + Grafana** - Monitoring
- **API Gateway** - Ocelot ile yönetim
- **Docker Support** - Containerization
- **Health Checks** - Sistem durumu

## 🛠️ **Teknoloji Stack**

### **Backend**
- .NET 9.0, Entity Framework Core, SQL Server
- AutoMapper, FluentValidation, Serilog
- JWT Authentication, Role-based Authorization

### **Frontend**
- ASP.NET Core MVC, Bootstrap 5
- Modern UI/UX, Responsive Design
- Interactive JavaScript, Real-time Updates

### **API & Documentation**
- RESTful API, Swagger/OpenAPI
- Comprehensive Endpoints, Health Checks

## 📊 **Veritabanı Şeması**

### **Ana Varlıklar**
- **Products** - Ürünler ve kategoriler
- **Ingredients** - Malzemeler ve reçeteler
- **Stocks** - Stok takibi ve lokasyonlar
- **Tables** - Masa yönetimi
- **Orders** - Siparişler ve kalemler
- **Customers** - Müşteri bilgileri
- **Employees** - Personel yönetimi
- **Reservations** - Rezervasyon sistemi
- **Payments** - Ödeme işlemleri
- **Users & Roles** - Kullanıcı yönetimi

## 🔧 **Konfigürasyon**

### **Connection String**
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=SD_Restaurant_Dev;User Id=sa;Password=YourStrong@Passw0rd;TrustServerCertificate=true"
  }
}
```

### **Docker ile Çalıştırma**
```bash
# Tüm servisleri başlat
docker-compose up -d

# Sadece API ve Web
docker-compose up -d api1 web1

# Erişim Adresleri
# Web UI: http://localhost:80 (Nginx Load Balancer)
# API Gateway: http://localhost:5001
# Grafana: http://localhost:3000 (admin/admin123)
# Prometheus: http://localhost:9090
# RabbitMQ Management: http://localhost:15672 (admin/admin123)

# ⚠️ GÜVENLİK: Default şifreleri değiştirin!
# - Grafana: admin/admin123
# - RabbitMQ: admin/admin123
# - SQL Server: sa/YourStrong@Passw0rd
```

## 📚 **API Endpoints**

### **Temel Endpoints**
- `GET /api/products` - Ürün listesi
- `GET /api/orders` - Sipariş listesi
- `GET /api/tables` - Masa listesi
- `GET /api/customers` - Müşteri listesi
- `GET /api/stocks` - Stok durumu

### **Swagger UI**
API'yi test etmek için: http://localhost:5195/swagger

### **Docker ile Erişim**
Docker kullanıyorsanız:
- **Web UI**: http://localhost:80 (Nginx Load Balancer)
- **API Gateway**: http://localhost:5001
- **Swagger**: http://localhost:5001/swagger

## 🧪 **Test ve Geliştirme**

### **Health Check**
```
http://localhost:5195/health
```

### **Log Dosyaları**
```
SD_Restaurant.API/Logs/
```

## 🤝 **Katkıda Bulunma**

1. Fork yapın
2. Feature branch oluşturun
3. Değişikliklerinizi commit edin
4. Pull Request gönderin

## 📞 **İletişim**

- **Issues**: GitHub Issues
- **Discussions**: GitHub Discussions
- **Email**: GitHub üzerinden iletişim

## 📝 **Lisans**

MIT License - Detaylar için [LICENSE](LICENSE) dosyasına bakın.

---

**SD Restaurant Management System** - Modern restaurant yönetimi için geliştirilmiş kapsamlı çözüm.

---

## ⚠️ **Yasal Uyarı ve Sorumluluk Reddi**

Bu proje **eğitim ve öğrenme amaçlı** geliştirilmiş bir örnek uygulamadır. Proje şu anda geliştirme aşamasındadır ve aşağıdaki durumlar söz konusudur:

### **Proje Durumu**
- 🚧 **Geliştirme Aşamasında**: Proje aktif olarak geliştirilmektedir
- 🔄 **Sürekli Güncelleme**: Özellikler ve API'ler değişebilir
- 🐛 **Potansiyel Hatalar**: Test edilmemiş özellikler bulunabilir
- 📚 **Eğitim Amaçlı**: Production kullanımı için tasarlanmamıştır

### **Kullanım Uyarıları**
- ⚠️ **Production Kullanımı**: Bu projeyi production ortamında kullanmadan önce kapsamlı test yapın
- 🔒 **Güvenlik**: Ek güvenlik önlemleri ve audit gerekebilir
- 📊 **Performans**: Yüksek trafikli ortamlar için optimize edilmemiştir
- 🛡️ **Veri Güvenliği**: Hassas veriler için ek şifreleme ve güvenlik katmanları ekleyin

### **🔒 Güvenlik Uyarıları**
- **Default Şifreler**: Docker'daki admin/admin123 şifrelerini mutlaka değiştirin
- **JWT Anahtarları**: Tüm appsettings dosyalarındaki JWT secret key'leri güncelleyin
- **CORS Politikaları**: Gateway'deki AllowAll CORS politikasını production için kısıtlayın
- **Swagger UI**: Production'da Swagger'ı devre dışı bırakın
- **Environment Variables**: Hassas bilgileri environment variable'lara taşıyın
- **HTTPS**: Production'da mutlaka SSL sertifikası kullanın

### **Katkıda Bulunma**
Bu proje açık kaynak olarak geliştirilmektedir. Katkıda bulunurken:
- Hata raporları için GitHub Issues kullanın
- Öneriler için GitHub Discussions'ı tercih edin
- Pull Request'lerde detaylı açıklama yapın

**Not**: Bu projeyi kullanarak oluşabilecek herhangi bir zarardan proje geliştiricileri sorumlu değildir.

