function OnBeginAjax() {
    var overlay = '<div class="overlay" style="background: rgba(255, 255, 255, 0.5); top:0; left:0; z-index:3000; position: fixed; width: 100%; height: 100%"> <div class="sk-spinner sk-spinner-chasing-dots" style="margin: auto; position: absolute; top: 0; left: 0; right: 0; bottom: 0"> <div class="sk-dot1"></div><div class="sk-dot2"></div></div></div>';
    $('body').append(overlay);
}

function OnModalBeginAjax() {
    var overlay = '<div class="overlay" style="background: rgba(255, 255, 255, 0.5); top:0; left:0; z-index:3000; position: fixed; width: 100%; height: 100%"> <div class="sk-spinner sk-spinner-chasing-dots" style="margin: auto; position: absolute; top: 0; left: 0; right: 0; bottom: 0"> <div class="sk-dot1"></div><div class="sk-dot2"></div></div></div>';
    $('body').append(overlay);
}

function OnCompleteAjax() {
    $('.overlay').remove();
}

function OnFailure() {
    swal({
        type: "error",
        title: "",
        text: 'Đã có lỗi xảy ra'
    });
}

function AlertError(error) {
    swal({
        type: "error",
        title: "",
        text: error
    });
}

function ConfirmPopup(title, text, callFunc) {
    swal({
        title: title,
        text: text,
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "Xác nhận",
        cancelButtonText: "Bỏ qua",
        closeOnConfirm: true
    },
        function () {
            callFunc();
        }
    );
}

function OnSuccessAjax(result) {
    if (result.Code != null && result.Code != 0) {
        swal({
            type: "error",
            title: "",
            text: result.Message
        });
    } else if (result.Code == 0) {

        if (result.Message != null && result.Message.length > 0) {
            swal({
                type: "success",
                title: result.Message
            },
                function (isConfirm) {
                    if (isConfirm && result.Url != null && result.Url.length > 0) {
                        window.location.href = result.Url;
                    }
                    if (result.callFunc) {
                        result.callFunc();
                    }


                });
        } else if (result.Url != null && result.Url.length > 0) {
            window.location.href = result.Url;
        } else if (result.callFunc) {
            result.callFunc();
        }
    }
    $('.overlay').remove();
}

$.validator.addMethod('requiredif',
    function (value, element, parameters) {
        var id = '#' + parameters['dependentproperty'];
        var name = parameters['dependentproperty'];
        // get the target value (as a string, 
        // as that's what actual value will be)
        var targetvalue = parameters['targetvalue'];
        targetvalue =
          (targetvalue == null ? '' : targetvalue).toString();

        // get the actual value of the target control
        // note - this probably needs to cater for more 
        // control types, e.g. radios
        var control = $(id);
        var controltype = control.attr('type');
        var actualvalue =
            controltype === 'checkbox' ?
            control.prop('checked').toString() :
            controltype === 'radio' ?
            $('[name=' + name + ']:checked').val() :
            control.val();

        // if the condition is true, reuse the existing 
        // required field validator functionality
        if (targetvalue === actualvalue)
            return $.validator.methods.required.call(
              this, value, element, parameters);

        return true;
    }
);

$.validator.unobtrusive.adapters.add(
    'requiredif',
    ['dependentproperty', 'targetvalue'],
    function (options) {
        options.rules['requiredif'] = {
            dependentproperty: options.params['dependentproperty'],
            targetvalue: options.params['targetvalue']
        };
        options.messages['requiredif'] = options.message;
    }
);

function locdau(str) {
    str = str.toLowerCase();
    str = str.replace(/à|á|ạ|ả|ã|â|ầ|ấ|ậ|ẩ|ẫ|ă|ằ|ắ|ặ|ẳ|ẵ/g, "a");
    str = str.replace(/è|é|ẹ|ẻ|ẽ|ê|ề|ế|ệ|ể|ễ/g, "e");
    str = str.replace(/ì|í|ị|ỉ|ĩ/g, "i");
    str = str.replace(/ò|ó|ọ|ỏ|õ|ô|ồ|ố|ộ|ổ|ỗ|ơ|ờ|ớ|ợ|ở|ỡ/g, "o");
    str = str.replace(/ù|ú|ụ|ủ|ũ|ư|ừ|ứ|ự|ử|ữ/g, "u");
    str = str.replace(/ỳ|ý|ỵ|ỷ|ỹ/g, "y");
    str = str.replace(/đ/g, "d");
    str = str.replace(/!|@|\$|%|\^|\*|\(|\)|\+|\=|\<|\>|\?|\/|,|\.|\:|\'| |\"|\&|\#|\[|\]|~/g, "-");
    str = str.replace(/-+-/g, "-"); //thay thế 2- thành 1-
    str = str.replace(/^\-+|\-+$/g, "");//cắt bỏ ký tự - ở đầu và cuối chuỗi
    return str;
}

$(function () {
    InitSelect2();
    $('.input-group.month').datetimepicker({
        locale: 'vi',
        format: 'MM/YYYY',
        extraFormats: ["MM/YYYY"],
        defaultDate: null
    });

    $('.input-group.date').datetimepicker({
        locale: 'vi',
        format: 'DD/MM/YYYY',
        extraFormats: ["DD/MM/YYYY"],
        defaultDate: null
    });
    

    $("#SearchForm").submit();

    $("#btReset").click(function () {
        $("input[type=text]").val("");
        $("select").val("");
        $("textarea").val("");
        $("select").val('').trigger('change');
        $("#btSearch").click();
    });
});

function InitSelect2() {
    $('.select2').css('width', '100%');
    $('.select2').select2({
    });
}

jQuery.fn.ForceNumericOnly =
        function () {
            return this.each(function () {
                $(this).keydown(function (e) {
                    var key = e.charCode || e.keyCode || 0;
                    return (
                        key == 8 ||
                        key == 9 ||
                        key == 13 ||
                        key == 46 ||
                        key == 110 ||
                        key == 190 ||
                        (key >= 35 && key <= 40) ||
                        (key >= 48 && key <= 57) ||
                        (key >= 96 && key <= 105));
                });
            });
        };


function InitSortTable (){
    $('#datatable').dataTable({ "paging": false,"info": false, "searching" : false});
    $('#datatable-keytable').DataTable({keys: true});
    $('#datatable-responsive').DataTable();
    $('#datatable-colvid').DataTable({
        "dom": 'C<"clear">lfrtip',
        "colVis": {
            "buttonText": "Change columns"
        }
    });
    $('#datatable-scroller').DataTable({
        ajax: "../plugins/datatables/json/scroller-demo.json",
        deferRender: true,
        scrollY: 380,
        scrollCollapse: true,
        scroller: true
    });
    var table = $('#datatable-fixed-header').DataTable({fixedHeader: true});
    var table = $('#datatable-fixed-col').DataTable({
        scrollY: "300px",
        scrollX: true,
        scrollCollapse: true,
        paging: false,
        fixedColumns: {
            leftColumns: 1,
            rightColumns: 1
        }
    });
}