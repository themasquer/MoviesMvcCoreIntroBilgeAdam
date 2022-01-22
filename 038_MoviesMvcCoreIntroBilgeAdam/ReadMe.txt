﻿1) Entity'ler oluşturulur.
2) Microsoft.EntityFrameworkCore.SqlServer ile Microsoft.EntityFrameworkCore.Tools paketleri NuGet'ten indirilir.
3) DbContext'ten türeyen Context ve içerisindeki DbSet'ler oluşturulur.
4) Context içerisindeki override edilen OnConfiguring methodunda connection string 
server=.\\SQLEXPRESS;database=BA_MoviesCore;trusted_connection=true; formatta yazılır.
5) Tools -> NuGet Package Manager -> Package Manager Console açılır ve önce add-migration v1 daha sonra 
update-database komutları çalıştırılır.
6) İstenirse ilk verileri oluşturmak için Database gibi bir controller oluşturulup içerisine Seed gibi bir action yazılarak
veritabanında ilk verilerin oluşturulması sağlanabilir.
7) Entity model dönüşümlerini gerçekleştirecek servis class'ları önce interface üzerinden methodlar tanımlanarak oluşturulur.
Tanımlanabilecek methodlar CRUD işlemlerine karşılık gelecek Query, Add, Update ve Delete methodlarıdır.
Bu aşamada entity'lere karşılık model'ler de oluşturulmalıdır. Servislerde de dependency injection için DbContext tipinde parametreli
constructor yazılır.
8) Program.cs altında IoC Container içerisinde önce DbContext için projenin Context'i tanımı,
daha sonra da service interface'leri için servis class'ları tanımları yapılır.
9) İlgili model için Controller oluşturulur, dependency injection için ilgili servisin interface'i tipinde parametreli 
constructor yazılır, daha sonra Index, Details, Create, Edit ve Delete aksiyonları yazılır.
10) Bu aksiyonlar sonucunda ilgili view'lar oluşturulur.
11) Bazı controller aksiyon'larını çağırabilmek için view'larda veya layout view'ında link'ler yazılır.
12) View'larda yapılan değişikliklerin proje çalışırken tarayıcıdan sayfanın yenilenmesi durumunda sayfaya yansıması için
NuGet'ten Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation paketi indirilir ve projede Properties -> launchSettings.json
dosyasına "ASPNETCORE_ENVIRONMENT" altına "ASPNETCORE_HOSTINGSTARTUPASSEMBLIES": "Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation"
tüm profiller için eklenir.
13) İstenirse launchSettings.json'daki profiles altında IIS Express development (DEV), _038_MoviesMvcCoreIntroBilgeAdam 
production (PROD) olarak ayarlanabilir.

Yapı:
View <-> Controller (Başlangıç noktası) <-> Service (model, entity -> context -> veritabanı)

HTML - CSS - Javascript örnekleri projede wwwroot -> HTML_Javascript_CSS_Intro klasörü altındadır.

Proje geliştirme aşamaları:
1) DatabaseController
1.1) Seed Action
2) MoviesController
2.1) Index Action -> HTML Helpers, ViewBag, ViewData
2.2) Details Action 
2.3) Create Action -> TempData, ModelState
2.4) Edit Action -> View Scaffolding, Tag Helpers
2.5) Delete Action
3) DirectorsController
3.1) Index Action
3.2) Details Action
3.3) Create Action
3.4) Edit Action
3.5) Delete Action
4) ReviewsController
4.1) Index Action
4.2) Details Action
4.3) Create Action
4.4) Edit Action
4.5) Delete Action