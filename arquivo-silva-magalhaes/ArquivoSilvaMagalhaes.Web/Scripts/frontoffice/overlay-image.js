(function ($) {
    $('[data-afsm-show-overlay="true"]').click(function (e) {
        var $link = $(e.target);
        var url = $link.data('afsm-large-image-url');
        var $target = $($link.data('afsm-target'));
        var description = $link.data('afsm-description');

        $target.find('img:first').attr('src', url);
        $target.find('p:first').text(description);

        $target.removeClass('hidden');

        e.preventDefault();
        return false;
    });

    $('[data-afsm-close-overlay]').click(function (e) {
        $($(e.target).data('afsm-close-overlay')).addClass('hidden');
    })
}(window.jQuery));