$(document).ready(function () {
    (function ($) {
        $.fn.datepicker.dates['tr'] = {
            days: ["Pazar", "Pazartesi", "Salı", "Çarşamba", "Perşembe", "Cuma", "Cumartesi"],
            daysShort: ["Pz", "Pzt", "Sal", "Çrş", "Prş", "Cu", "Cts"],
            daysMin: ["Pz", "Pzt", "Sa", "Çr", "Pr", "Cu", "Ct"],
            months: ["Ocak", "Şubat", "Mart", "Nisan", "Mayıs", "Haziran", "Temmuz", "Ağustos", "Eylül", "Ekim", "Kasım", "Aralık"],
            monthsShort: ["Oca", "Şub", "Mar", "Nis", "May", "Haz", "Tem", "Ağu", "Eyl", "Eki", "Kas", "Ara"],
            today: "Bugün",
            clear: "Temizle",
            weekStart: 1,
            format: "dd/mm/yyyy"
        };
    }(jQuery));

    $('#BeginDate').mask('99/99/9999');
    $('#EndDate').mask('99/99/9999');

    $.fn.datepicker.defaults.language = 'tr';
    $('#BeginDate').datepicker({
        language: 'tr'
    });
    $('#EndDate').datepicker({
        language: 'tr'
    });
});



function CalculateReturn(val) {
    var data = JSON.parse(val);
    if (data.value == 'True') {
        $('#returnDiv').html(
            '  <h5 class="card-title">Sonuc</h5>    <p class="card-text" >Çalışma Gün Sayısı: ' + data.WorkDays + ' </br>  Ceza Tutarı: ' + data.Penalty + '  </p>');

    }
    else {
        $('#returnDiv').html('');
        alert(data.Return);
    }
}