(function ($) {
    $('body').on('click', '[data-afsm-show-overlay="true"]', function (e) {
    // $('[data-afsm-show-overlay="true"]').click(function (e) {
        var $link = $(e.target);

        if (!$link.data('afsm-show-overlay')) {
            return;
        }

        var url = $link.data('afsm-large-image-url');
        var $target = $($link.data('afsm-target'));
        var description = $link.data('afsm-description');
        
        $target.css('background-image', 'url(' + window.escape(url) + ')');
        
        if (description) {
            $target.find('p:first').text(description);
        } else {
            $('.afsm-overlay-header').addClass('hidden');
        }


        $target.removeClass('hidden');
        $('body').addClass('afsm-body-noscroll');

        e.preventDefault();
        return false;
    });

    $('[data-afsm-close-overlay]').click(function () {
        $($(this).data('afsm-close-overlay')).addClass('hidden');

        $('body').removeClass('afsm-body-noscroll');
    });
}(window.jQuery));