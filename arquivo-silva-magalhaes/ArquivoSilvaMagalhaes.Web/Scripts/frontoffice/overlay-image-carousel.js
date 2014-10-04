/**
 * This set of functions allows for a simple
 * and elegant overlay that is used to show
 * large versions of images.
 * 
 * This works with a Bootstrap carousel, to
 * show multiple images.
 * 
 * To be modular, it works with data- attributes.
 */
(function ($) {
    /**
     * Wire-up an event to show the overlay
     * when the user click on an image that can show
     * an overlay.
     * 
     * The event is wired-up to the body because the image
     * elements can be destroyed through AJAX updates.
     */
    $('body').on('click', '[data-afsm-show-overlay="true"]', function (e) {
        var $link = $(e.target);

        if (!$link.data('afsm-show-overlay')) {
            return;
        }

        var $target = $($link.data('afsm-target'));
        var imgId = $link.data('afsm-imageid');

        $target.find('.item').each(function () {
            var $item = $(this);
            if ($item.data('afsm-imageid') === imgId) {
                $item.addClass('active');
            } else {
                $item.removeClass('active');
            }
        });

        $target.removeClass('hidden');
        $('body').addClass('afsm-body-noscroll');

        // This timeout is to give time to the DOM
        // and browser to get up to speed.
        window.setTimeout(function () {
            $target.addClass('in');
        }, 100);

        // Prevent the browser from navigating elsewhere.
        e.preventDefault();
        return false;
    });

    /**
     * Wire-up an event to a button that closes the overlay.
     */
    $('[data-afsm-close-overlay]').click(function () {
        var $overlay = $($(this).data('afsm-close-overlay'));
        $overlay.removeClass('in');

        /**
         * This timeout has the same value as the timeout
         * from the 'fade' transition from Bootstrap.
         */
        window.setTimeout(function () {
            $overlay.addClass('hidden');
            $('body').removeClass('afsm-body-noscroll');
        }, 150);
    });
}(window.jQuery));