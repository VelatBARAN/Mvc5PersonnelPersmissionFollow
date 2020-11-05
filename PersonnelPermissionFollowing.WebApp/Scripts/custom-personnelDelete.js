$(function () {

        //$('#modalPermission').on('show.bs.modal', function (e) {  // sayfa show old. anda , function daki kodlar çalışır

        //    var perId = $(e.relatedTarget); // tıklanan nesneyi yakalama
        //    personnelid = perId.data("id"); // tıklanan nesnedeki data-id attributenin id si kesilerek alınır.

        //    $("#modal_body").load("/Personnel/PersonnelPermissionModal/" + personnelid); // body alanı controller deki action u yükleme
        //});

    $("#personnelTable").on("click", ".personnelDelete", function () {
        var btn = $(this);
        bootbox.confirm("Personeli silmek istediğinize emin misiniz?", function (result) {
            if (result) {
                var btnId = btn.data("id");
                $.ajax({
                    method: "Post",
                    //dataType: "Json",
                    url: "/Personnel/Delete",
                    data: { id: btnId }
                }).done(function (data) {
                    if (!data.hasError) {
                        alert(data.Message);
                        btn.parent().parent().parent().parent().parent().parent().remove();
                    } else if (data.hasError) {
                        alert(data.Message);
                    } else if (data.results) {
                        alert(data.Message);
                    }
                }).fail(function () {
                    alert('sunucu ile bağlantı kurulurken hata oluştu.')
                });
            }
        });

    });

    $("#assignedTaskPersonnelTable").on("click", ".assignedTaskPersonnelDelete", function () {
        var btn2 = $(this);
        bootbox.confirm("Görev atanan personeli silmek istediğinize emin misiniz?", function (result) {
            if (result) {
                var btnId2 = btn2.data("id");
                $.ajax({
                    method: "Post",
                    //dataType: "Json",
                    url: "/AssigningTaskOfPersonnel/Delete",
                    data: { id: btnId2 }
                }).done(function (data) {
                    if (!data.hasError) {
                        alert(data.Message);
                        btn2.parent().parent().parent().parent().parent().parent().remove();
                    } else if (data.hasError) {
                        alert(data.Message);
                    } else if (data.results) {
                        alert(data.Message);
                    }
                }).fail(function () {
                    alert('sunucu ile bağlantı kurulurken hata oluştu.')
                });
            }
        });
    });

    $("#personnelPermissionTable").on("click", ".personnelPermissionDelete", function () {
        var btn3 = $(this);
        bootbox.confirm("Personele verilen izni silmek istediğinize emin misiniz?", function (result) {
            if (result) {
                var btnId3 = btn3.data("id");
                $.ajax({
                    method: "Post",
                    //dataType: "Json",
                    url: "/Personnel/PersonnelPermissionDelete",
                    data: { id: btnId3 }
                }).done(function (data) {
                    if (!data.hasError) {
                        alert(data.Message);
                        btn3.parent().parent().parent().parent().parent().parent().remove();
                    } else if (data.hasError) {
                        alert(data.Message);
                    } else if (data.results) {
                        alert(data.Message);
                    }
                }).fail(function () {
                    alert('sunucu ile bağlantı kurulurken hata oluştu.')
                });
            }
        });

    });

    $("#userTable").on("click", ".userDelete", function () {
        var btn4 = $(this);
        bootbox.confirm("Kullanıcıyı silmek istediğinize emin misiniz?", function (result) {
            if (result) {
                var btnId4 = btn4.data("id");
                $.ajax({
                    method: "Post",
                    //dataType: "Json",
                    url: "/User/Delete",
                    data: { id: btnId4 }
                }).done(function (data) {
                    if (!data.hasError) {
                        alert(data.Message);
                        btn4.parent().parent().parent().parent().parent().parent().remove();
                    } else if (data.hasError) {
                        alert(data.Message);
                    } else if (data.results) {
                        alert(data.Message);
                    }
                }).fail(function () {
                    alert('sunucu ile bağlantı kurulurken hata oluştu.')
                });
            }
        });

    });
});