(function ($) {
    $('[data-afsm-show-overlay="true"]').click(function (e) {
        var $link = $(e.target);
        var url = $link.data('afsm-large-image-url');
        var $target = $($link.data('afsm-target'));
        var description = $link.data('afsm-description');

        $target.css('background-image', 'url(' + url + ')');
        $target.find('p:first').text(description);

        $target.removeClass('hidden');
        $('body').addClass('afsm-body-noscroll');

        e.preventDefault();
        return false;
    });

    $('[data-afsm-close-overlay]').click(function (e) {
        $($(this).data('afsm-close-overlay')).addClass('hidden');

        $('body').removeClass('afsm-body-noscroll');
    });
}(window.jQuery));