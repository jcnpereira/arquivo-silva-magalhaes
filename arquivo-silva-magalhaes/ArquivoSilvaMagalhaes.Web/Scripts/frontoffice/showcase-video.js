(function ($) {
    $('body').addClass('afsm-body-noscroll');
    $('#afsm-close-overlay').click(function () {
        if ($('#afsm-dontshowagain').is(':checked')) {
            document.cookie = 'afsm-novideo=1';
        }

        $('#afsm-fullscreen').remove();

        $('body').removeClass('afsm-body-noscroll');
    });
}(window.jQuery));