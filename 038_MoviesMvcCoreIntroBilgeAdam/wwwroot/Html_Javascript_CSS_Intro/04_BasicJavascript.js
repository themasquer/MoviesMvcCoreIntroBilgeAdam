function ucgenDoldur(karakter, satirSayisi, tersten = false) {
    var sonuc;
    var sonucArray = new Array();
    for (var i = 0; i < satirSayisi; i++) {
        sonuc = "";
        for (var j = 0; j <= i; j++) {
            sonuc += karakter;
        }
        sonucArray[i] = sonuc;
    }
    sonuc = "";
    if (tersten) {
        for (var i = sonucArray.length - 1; i >= 0; i--) {
            sonuc += sonucArray[i] + "\n";
        }
    }
    else {
        for (var i = 0; i < sonucArray.length; i++) {
            sonuc += sonucArray[i] + "\n";
        }
    }
    return sonuc;
}

function onLoadIleMyTextAreaDoldur(karakter, satirSayisi) {
    var myTextArea = document.getElementById('mytextareaid');
    var sonuc = ucgenDoldur(karakter, satirSayisi);
    myTextArea.value = sonuc;
}

var terstenGlobal = true;

function onClickIleMyTextAreaDoldur(karakter, satirSayisi) {
    var myTextArea = document.getElementsByName('mytextareaname')[0];
    var sonuc = ucgenDoldur(karakter, satirSayisi, terstenGlobal);
    myTextArea.value = sonuc;
    var body = document.getElementsByTagName("body")[0];
    if (terstenGlobal)
        body.style.backgroundColor = "blue";
    else
        body.style.backgroundColor = "yellow";
    terstenGlobal = !terstenGlobal;
}

function inputlaraGoreUcgenDoldur() {
    var karakterTextBox = document.getElementById("karakter");
    var karakter = karakterTextBox.value;
    var satirSayisi = document.getElementById("satirsayisi").value;
    var tersten = document.getElementById("tersten").checked;
    var sonuc = document.getElementById("sonuc");
    sonuc.value = ucgenDoldur(karakter, satirSayisi, tersten);
    if (!tersten)
        document.getElementById("bilgi").innerText = satirSayisi + " satır üzerinden üçgen " + karakter + " ile düz yazdırıldı.";
    else
        document.getElementById("bilgi").innerText = satirSayisi + " satır üzerinden üçgen " + karakter + " ile tersten yazdırıldı.";
}