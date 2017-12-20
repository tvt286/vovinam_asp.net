/**
* Theme: Zircos Admin Template
* Author: Coderthemes
* Toastr js
*/

$(function () {
    var i = -1;
    var toastCount = 0;
    var $toastlast;

    var getMessage = function () {
        var msgs = ['My name is Inigo Montoya. You killed my father. Prepare to die!',
            'Are you the six fingered man?',
            'Inconceivable!',
            'I do not think that means what you think it means.',
            'Have fun storming the castle!'
        ];
        i++;
        if (i === msgs.length) {
            i = 0;
        }

        return msgs[i];
    };

    var getMessageWithClearButton = function (msg) {
        msg = msg ? msg : 'Clear itself?';
        msg += '<br /><br /><button type="button" class="btn btn-default clear">Yes</button>';
        return msg;
    };


})


function ShowMessageNoti(msg, title) {
    var shortCutFunction = 'success';
    toastr.options = {
        closeButton: true,
        debug: false,
        newestOnTop: false,
        progressBar: false,
        positionClass: 'toast-bottom-right',
        preventDuplicates: false,
        onclick: null
    };
    toastr.options.showDuration = 300;
    toastr.options.hideDuration = 1000;
    toastr.options.timeOut = 5000;
    toastr.options.extendedTimeOut = 1000;
    toastr.options.showEasing = 'swing';
    toastr.options.hideEasing = 'linear';
    toastr.options.showMethod = 'fadeIn';
    toastr.options.hideMethod = 'fadeOut';

    var $toast = toastr[shortCutFunction](msg, title); // Wire up an event handler to a button in the toast, if it exists

    if (typeof $toast === 'undefined') {
        return;
    }
}