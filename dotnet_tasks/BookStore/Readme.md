# Dotnet BookStore Project

* FluentValidation'ı kullanabilmiz için öncelikle kütüphaneyi paket olarak uygulamamıza eklememiz gerekir.

```.NET CLI
dotnet add package FluentValidation
```

Fluent Validation Kütüphanesi ile yapılabilecek hazır validasyonların tamamını [burdan](https://docs.fluentvalidation.net/en/latest/built-in-validators.html) inceleyebilirsiniz.

Inceleme Önerisi: Fluent Validation Kütüphanesi ile ilgili daha detay bilgi için [tıklayınız](https://docs.fluentvalidation.net/en/latest/installation.html).

* Kesin bir kural olmamakla birlikte middleware ler standart olarak Use prefix'i ile başlar.

Okuma Önerisi: Middleware ile igili data detaylı bilgi sahibi olabilmek için [buraya](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/middleware/?view=aspnetcore-5.0) ve [buraya](https://www.gencayyildiz.com/blog/asp-net-core-2de-middleware-yapisi-ve-kullanimi/) tıklayınız.

* Json Serialize

```.NET CLI
dotnet add package Newtonsoft.Json
```

## ÖDEV - Projeye Author Controller ve Servislerin Eklenmesi

1. Projeye kitapların yazarları için Author controller'ı ekleyiniz. Bu controller ile aşağıdaki işlemlerin gerçeklenebilmesi gerekmektedir.

    * Yazar Ekleme
    * Yazar Bilgileri Güncelleme
    * Yazar Silme
    * Tüm Yazarları Listeleme
    * Spesifik Bir Yazarın Bilgilerini Getirme

2. Yazar Bilgileri:

    * Ad
    * Soyad
    * Doğum Tarihi

3. Kitap - Yazar - Tür entity ilişkilerini kurunuz. Bir kitabın yalnızca bir yazarı olabilir varsayımında bulunabilirsiniz.

    * Kitabı yayında olan bir yazar silinememeli. Öncelikle kitap silinmeli, daha sonra yazar silinebilir.

4. Author için model ve dto'ları ekleyiniz. Controller metotları entity'leri input veya output olarak kullanmamalı.

5. Author entity model map'lerini Auto Mapper kullanarak yazınız.

6. Author servisleri için Fluent Validation kullanarak validation sınıflarını yazınız. Kuralları uygun gördüğünüz şekilde belirleyebilirsiniz.

7. Servisler içerisinden anlamlı hata mesajları fırlatılmalı.
