
$(function () {
    $('[id$=btnSaveRequest]').attr('disabled', 'disabled');
    $("[name='acceptterms']").bootstrapSwitch();
    $('#PreferredDate').datetimepicker({
        minDate: moment().add(1, 'days'),
        showToday: true,
        format: "DD/MM/YYYY",
        language: 'en',
        pickTime: false
    }).on("dp.hide", function (e) {
        $(this).val(e.date.format("L"));
    });

    $('#PreferredTime').datetimepicker({
        minDate: moment(),
        showToday: true,
        language: 'en',
        pickDate: false
    }).on("dp.hide", function (e) {
        $(this).val(e.date.format("HH:mm:ss"));
    });
});


$('input[name="acceptterms"]').on('switchChange.bootstrapSwitch', function (event, state) {
    if (state) {
        $('[id$=btnSaveRequest]').removeAttr("disabled");
    }
    else {
        $('[id$=btnSaveRequest]').attr('disabled', 'disabled');
    }
});

var enterDataController = function ($scope) {
    $scope.validate = function (valid) {
        
        if (!valid) {
            $scope.submitted = true;
            return false;
        }
    }
}
function RaiseToast() {
    toastr.options = {
        "closeButton": false,
        "debug": false,
        "positionClass": "toast-top-center",
        "onclick": null,
        "showDuration": "300",
        "hideDuration": "1000",
        "timeOut": "5000",
        "extendedTimeOut": "1000",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut"
    }
    toastr.success('Your request is submitted. We will call you with possible lowest quote for requested service');
    setTimeout(function () {
        window.location = "/index.aspx";
    }, 5000);
}
