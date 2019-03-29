/**
 * This set of functions
 * initializes the showcase video
 * for the index page.
 * 
 * The video is initialized through JS
 * because it can only be removed through JS.
 */
(function ($, document) {
    // Show the video.
    $('#afsm-fullscreen').removeClass('hidden');

    // Add a class to the body to hide any scroll
    // bars.
    $('body').addClass('afsm-body-noscroll');

    // Wire-up an event to the close button
    // to close the video.
    // 
    // If the user checked the button to not
    // show the video again (I should've made sure
    // it wouldn't be even shown ONCE!), a cookie
    // is set to prevent the server from doing
    // such a horrible thing again.
    $('#afsm-close-overlay').click(function () {
        if ($('#afsm-dontshowagain').is(':checked')) {
            document.cookie = 'afsm-novideo=1';
        }

        $('#afsm-fullscreen').remove();

        $('body').removeClass('afsm-body-noscroll');
    });
}($, document));