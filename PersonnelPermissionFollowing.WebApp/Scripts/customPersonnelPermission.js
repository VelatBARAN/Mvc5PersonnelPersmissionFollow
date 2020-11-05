$("#btnPermission").click(function () {
    var a = $(this);
    var aexitdate = a.data("exitdate");
    if (aexitdate != null) {
        alert("Bu personelin iş çıkışı verilmiş");
    } else {
        alert("Sunucu ile bağlantı sağlanamadı.")
    }
});