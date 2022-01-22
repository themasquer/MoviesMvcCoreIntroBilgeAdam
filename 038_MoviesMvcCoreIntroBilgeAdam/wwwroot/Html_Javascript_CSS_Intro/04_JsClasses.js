function bilgisayarOlusturVeGetir() {
	let bilgisayar = new Bilgisayar("ASUS", 'ROG', 3.5, 32, true, new Date("2021-12-29T15:56:03")); // objeler oluşturulurken let ile referans değişkeni tanımlanır!
	document.getElementById('bilgisayar').innerHTML = bilgisayar.bilgileriGetir();

	// var ile let arasındaki fark: var ile bir değişken tanımlanmadan ve değeri atanmadan kullanılırsa değeri undefined olurken let bu şekilde kullanıldığında hata oluşur.
	console.log(degisken1); // undefined yazacaktır
	var degisken1 = 'Değişken 1';
	console.log(degisken1); // Değişken 1 yazacaktır

	//console.log(degisken2); // hata fırlatacaktır
	//let degisken2 = 'Değişken 2';
	//console.log(degisken2); // hatadan dolayı Değişken 2 yazdıramaz
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
		return '<b>Marka:</b> ' + this.marka + '<br />' +
			'<b>Model:</b> ' + this.model + '<br />' +
			'<b>İşlemci Hızı:</b> ' + this.hiz + ' GHz<br />' +
			'<b>Hafıza:</b> ' + this.hafiza + ' GB<br />' +
			'<b>Su Soğutma:</b> ' + (this.suSogutmaliMi ? 'Evet' : 'Hayır') + '<br />' +
			//'<b>Üretim Tarihi: ' + this.uretimTarihi.toLocaleString('en-US'); // 12/29/2021, 3:56:03 PM
			'<b>Üretim Tarihi:</b> ' + this.uretimTarihi.toLocaleString('tr-TR'); // 29.12.2021 15:56:03
	}
}