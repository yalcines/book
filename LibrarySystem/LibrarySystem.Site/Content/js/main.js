var token = '';//tokenControl
$(document).on('submit', 'form[data-submit=true]', function (e) { // Form Post tarafında injection tarzi işler için kullanıyorum. Dönen Funciton degeri ile ilgili function posttan dönen değer ile çalışıyor

    $(":submit").prop('disabled', false);
    e.preventDefault();
    var form = $(this);
    var url = $(form).attr('action');// form post controller yolu
    var data = $(form).serialize();// serilize işlemi
    if ($(form).valid()) { // form vali mi
        $(form).find(':submit').attr("disabled", true);// butonu pasife alıyorum
 
        $.ajax({
            method: 'POST',
            url: url,
            data: data,
            headers: { '__RequestVerificationToken': token }, //token kontrolu için geçerli değeri gönderiyorum
            success: function (data) {

                $(form).find(':submit').attr("disabled", false);// butonu aktife alıyorum
                if (data.value) {// dönen value değeri true ise
                    if (data.jqueryFunc != '') {// çalıştırılacak function değeri boş değil ise
                        var functionName = data.jqueryFunc.split('(')[0]; // fonksiyon adını al
                        var params = data.jqueryFunc.split('(')[1]; // fonksiyona gidecek değerleri al
                        params = params.substring(0, params.length - 1);
                        window[functionName](params);// fonksiyonu değer vererek çalıştır.
                    }
                }
                
            }
        });
    }
});
$(document).ready(function () {

    if ($('[name=__RequestVerificationToken]')) {
        token = $('[name=__RequestVerificationToken]').val();// tokenı formda al
    }
});
