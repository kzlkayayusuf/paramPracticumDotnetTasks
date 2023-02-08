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
