function bilgisayarOlusturVeGetir1() {
	let bilgisayar = new Bilgisayar("ASUS", 'ROG', 3.5, 32, true, new Date("2021-12-29T15:56:03")); // objeler oluşturulurken let ile referans değişkeni tanımlanır!
	document.getElementById('bilgisayar1').innerHTML = bilgisayar.bilgileriGetir();

	// var ile let arasındaki fark: var ile bir değişken tanımlanmadan ve değeri atanmadan kullanılırsa değeri undefined olurken let bu şekilde kullanıldığında hata oluşur.
	console.log(degisken1); // undefined yazacaktır
	var degisken1 = 'Değişken 1';
	console.log(degisken1); // Değişken 1 yazacaktır

	//console.log(degisken2); // hata fırlatacaktır
	//let degisken2 = 'Değişken 2';
	//console.log(degisken2); // hatadan dolayı Değişken 2 yazdıramaz
}

function bilgisayarOlusturVeGetir2() {
    var marka = document.getElementById("marka");
    var model = document.getElementById("model");
    var hiz = document.getElementById("hiz");
    var hafiza = document.getElementById("hafiza");
    var suSogutmaliMi = document.getElementById("susogutmalimi");
    var uretimTarihi = document.getElementById("uretimtarihi");
	let bilgisayar = new Bilgisayar(marka.value, model.value, parseFloat(hiz.value), parseInt(hafiza.value), suSogutmaliMi.checked, new Date(uretimTarihi.value + " 00:00:00"));
    document.getElementById('bilgisayar2').innerHTML = bilgisayar.bilgileriGetir();
}

class Bilgisayar {
	constructor(marka, model, hiz, hafiza, suSogutmaliMi, uretimTarihi) {
		this.marka = marka;
		this.model = model;
		this.hiz = hiz;
		this.hafiza = hafiza;
		this.suSogutmaliMi = suSogutmaliMi;
		this.uretimTarihi = uretimTarihi;
	}

	bilgileriGetir() {
		return '<b><i>Marka:</i></b> ' + this.marka + '<br />' +
			'<b><i>Model:</i></b> ' + this.model + '<br />' +
			'<b><i>İşlemci Hızı:</i></b> ' + this.hiz + ' GHz<br />' +
			'<b><i>Hafıza:</i></b> ' + this.hafiza + ' GB<br />' +
			'<b><i>Su Soğutma:</i></b> ' + (this.suSogutmaliMi ? 'Evet' : 'Hayır') + '<br />' +
			//'<b><i>Üretim Tarihi:</i></b> ' + this.uretimTarihi.toLocaleString('en-US'); // 12/29/2021, 3:56:03 PM
			'<b><i>Üretim Tarihi:</i></b> ' + this.uretimTarihi.toLocaleString('tr-TR'); // 29.12.2021 15:56:03
	}
}