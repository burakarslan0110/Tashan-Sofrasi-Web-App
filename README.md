# Taşhan Sofrası Restoran Yönetim Projesi
![TashanSofrasi](https://github.com/user-attachments/assets/a18b8a1f-b0cb-4140-a436-32c2fae0dd4b)
## Proje Açıklaması

**Taşhan Sofrası**, bir restoran senaryosu üzerinde rezervasyon ve sipariş süreçlerinin kolayca yönetilebilmesi için geliştirmiş olduğum bir sistemdir. Kullanıcılar, restorana giriş yaparak masalardaki QR kodu okutup menüdeki ürünleri inceleyebilir, sepetlerine ekleyebilir ve masaya sipariş verebilirler. Ayrıca kullanıcılar rezervasyon oluşturma, mesaj gönderme imkânına da sahip.

Admin panelinde ise, SignalR teknolojisi kullanılarak siparişler, rezervasyonlar  ve mesajlar gerçek zamanlı olarak görüntülenebilir. Bu sayede yöneticiler; anlık olarak masalara yapılan siparişleri tamamlayabilir ve çeşitli istatistiksel verileri inceleyebilir. Bununla beraber sitenin ön yüz kısmındaki verileri büyük ölçüde admin paneliyle dinamik olarak özelleştirmek mümkün. 

## Teknik Detaylar

Bu projeyi **ASP.NET Core 8.0** ve **SignalR** teknolojileri kullanarak geliştirdim. Tüm CRUD işlemleri API katmanı üzerinden gerçekleştirildi ve MVC tarafında dinamik bir yapıya entegre edildi. Veritabanı olarak MSSQL Server üzerinde Code-First olarak **Entity Framework Core** kullanıldı. Proje, sürdürülebilirlik ve modülerlik açısından **N katmanlı mimariye (Entity, DataAccess, Business, DTO, API, UI)** ve SOLID prensiplerine uygun olarak geliştirdim. Bu açıdan CRUD işlemlerini merkezileştirmek için Generic Repository tasarım desenini de uyguladım. Ayrıca AutoMapper üzerinden **DTO katmanı** sayesinde veriler daha güvenli bir şekilde işlemiş oldum. Son olarak Docker ile projeyi tamamen konteyner ortamına taşıdım. Tüm teknik detayları ise şu şekilde sıralayabilirim:

### 📌 **Kullanılan Teknolojiler**

- **ASP.NET Core 8.0**
- **ASP.NET Core Web API**
- **SignalR**
- **Entity Framework Code First**
- **MSSQL Server**
- **Docker**
- **Identity**
- **Fluent Validation**
- **AutoMapper**
- **QRCoder**
- **SkiaSharp**
- **HTML, CSS, JavaScript**
- **Bootstrap**
- **Ajax**

### 📌 Öne Çıkan Özellikler

- **Rezervasyon yapma**
- **QR Kod ile ilgili masaya sipariş verme**
- **Mesaj gönderme**
- **Kupon kodu sistemi**
- **SignalR ile anlık sipariş yönetimi**
- **SignalR ile anlık rezervasyon yönetimi**
- **SignalR ile bildirim sistemi**
- **SignalR ile anlık istatistikler**
- **Ürünlerde CRUD işlemleri**
- **Kategorilerde CRUD işlemleri**
- **İndirimli ürün tanımlama**
- **Sitedeki diğer veriler için CRUD işlemleri**
- **Masaya ait QR kod oluşturma**
- **Identity ile hesap işlemleri**

Projeyi lokalinizde ayağa kaldırmak için projeyi sisteminize klonlayıp TashanSofrasi.sln dosyasını Visual Studio IDE üzerinden Docker compose ile çalıştırmanız yeterlidir. 

```
git clone https://github.com/burakarslan0110/Tashan-Sofrasi-Web-App.git
```

Eğer IDE kullanmaksızın projeyi ayağa kaldırmak istiyorsanız **DockerComposeNonVS** dosyasının bulunduğu dizine terminal üzerinden geçip aşağıdaki komutu çalıştırın. Ardından [https://localhost:8083](https://localhost:8083) adresinden projeyi inceleyebilirsiniz. API katmanına ise Swagger üzerinden [https://localhost:8081](https://localhost:8081) adresiyle erişebilirsiniz.

```
docker compose up -d
```
Admin paneline giriş adresi [https://localhost:8083/Admin](https://localhost:8083/Admin)

Kayıt olarak hesap oluşturabilirsiniz.

## Proje Görselleri

### WEB UI Tarafı / Ana Sayfa

#### Slider:
![TashanSlider](https://github.com/user-attachments/assets/2ce297c0-21ac-444e-a67b-ee1fe4cbcede)
#### İndirimli Ürünler:
![TashanDiscount](https://github.com/user-attachments/assets/d5484b51-d910-4b4c-88a7-0194f11f313c)
#### Menü:
![TashanMenu](https://github.com/user-attachments/assets/eb710e41-e433-4ea7-adea-1aff1da78d03)
#### Hakkımızda:
![TashanAbout](https://github.com/user-attachments/assets/71619a7b-1489-4f13-a472-5f2847c85e28)
#### Rezervasyon:
![TashanBooking](https://github.com/user-attachments/assets/1310457a-672f-40d2-8933-7893ac347f01)
#### Referanslar:
![TashanTestimonial](https://github.com/user-attachments/assets/a37f47f3-9d4f-4fc7-9eb1-d0bf62cd74e0)
#### Footer:
![TashanFooter](https://github.com/user-attachments/assets/1a2f883e-afde-40fe-b100-0a861c6c1905)
#### Hakkımızda Sayfası:
![TashanContact](https://github.com/user-attachments/assets/06948bd7-0759-4bf1-8e1e-02b51d735d00)
#### Sepet:
![TashanBasket](https://github.com/user-attachments/assets/01297d12-b1f3-4d16-98c8-c7c1e3eba1b7)

### WEB UI Tarafı / Giriş Ekranı
![TashanLogin](https://github.com/user-attachments/assets/e7b1c57b-4774-464e-a08f-7c3fdaae5333)
### WEB UI Tarafı / Kayıt Ol Ekranı
![TashanRegister](https://github.com/user-attachments/assets/d1b5294f-7f4c-4ffe-89ac-c586cb57a9c9)

### WEB UI Tarafı / Admin Panel

#### İstatistikler:
![TashanSt](https://github.com/user-attachments/assets/f6ec2ad6-dfee-4df7-bb1b-e57eafc587aa)
#### Sipariş Masa Durum:
![TashanTableStatus](https://github.com/user-attachments/assets/e3d3c060-f3e9-40fc-abdc-41746e48b023)
#### Sipariş Detayları:
![TashanOrderStatus](https://github.com/user-attachments/assets/358f09ad-131e-4df9-a151-1d64180a186d)
#### Kategoriler:
![TashanKategori](https://github.com/user-attachments/assets/3f2d70fc-df46-49bf-8a23-8e748fa90b9b)
#### Ürünler:
![TashanProducts](https://github.com/user-attachments/assets/9d39582b-442b-415e-95d3-cbd748696f34)
#### Rezervasyon ve Bildirimler:
![TashanBookingAdmin](https://github.com/user-attachments/assets/909be789-0c8e-4bc1-ad16-53fcd75ec79b)
#### QR Kod Oluşturma:
![TashanQR](https://github.com/user-attachments/assets/7668a11f-70b2-4b02-88ac-8511be4214f8)
#### Slider:
![TashanSliderAdmin](https://github.com/user-attachments/assets/6792a9a4-028b-4b01-bc5a-8ade3a95496d)
#### İndirimler:
![TashanAdminDiscount](https://github.com/user-attachments/assets/70badead-b625-4f16-afc4-65b6080c5a2e)
#### Hakkımızda:
![TashanAdminAbout](https://github.com/user-attachments/assets/2e444aeb-1ff6-4e30-b051-26dfddc4a913)
#### Referanslar:
![TashanAdminTestimonial](https://github.com/user-attachments/assets/946f2b6a-3a60-454c-8bbb-60414eb9f1dc)
#### Footer:
![TashanAdminFooter](https://github.com/user-attachments/assets/f92e9906-aa99-42dd-ad03-cf8404fd08fa)
#### Mesajlar:
![TashanAdminMessagge](https://github.com/user-attachments/assets/3c186010-a8c6-42bf-bfb3-8ec9b1b7c3b2)
### API Tarafı / Swagger
![Ekran görüntüsü_16-12-2024_153415_localhost](https://github.com/user-attachments/assets/c9a39bd1-e221-4632-94b8-d61e693c20cf)
![Ekran görüntüsü_16-12-2024_153459_localhost](https://github.com/user-attachments/assets/23a37e68-86ff-4fbf-a55f-9925b42a1b59)

