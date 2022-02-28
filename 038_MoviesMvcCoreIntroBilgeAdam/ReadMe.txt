1) Entity'ler oluşturulur.
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
1.1) Seed Action -> İlk verileri doldurma, DbContext objesinin new'lenerek kullanılması
2) MoviesController -> Servislerin constructor üzerinden enjekte edilmesi
2.1) Index Action -> HTML Helpers, ViewBag, ViewData
2.2) Details Action 
2.3) Create Action -> MultiSelectList, TempData, ModelState, ActionVerbs (HttpGet, HttpPost) kullanımı, ~/Views/Shared altında hatalar için kendi oluşturduğumuz MyError.cshtml kullanımı
2.4) Edit Action -> View Scaffolding, Tag Helpers
2.5) Delete Action
3) DirectorsController -> Controller Scaffolding
3.1) Index Action -> Model üzerinden kayıt sayısı yazdırma
3.2) Details Action -> Bir model içerisinde başka bir model referansı kullanımı, _Details partial view
3.3) Create Action -> SelectListItem listesi kullanımı, DataAnnotation özelleştirmeleri, AntiForgeryToken, Client Side Validation, Section kullanımı
3.4) Edit Action
3.5) Delete Action -> ActionName kullanımı
4) ReviewsController
4.1) Index Action -> Model üzerinden tanımlanan css class'larının view'da kullanımı, Alertify js-css third party kütüphanesi kullanımı
4.2) Details Action
4.3) Create Action -> SelectList, bootstrap-datepicker js-css third party kütüphanesi kullanımı, radio button ve textarea kullanımı, _CreateEdit partial view
4.4) Edit Action
4.5) Delete Action -> Alertify js-css third party kütüphanesi kullanımı