/**
 * This set of functions allows for simple form
 * and pagination updates through AJAX.
 * 
 * This works through "data-" attributes,
 * which makes it modular. Any page that needs
 * this only needs to annotate the components
 * as needed.
 */
(function ($) {
    'use strict';

    /*
     * For users without JS enabled, the links
     * in the tabs originally have the link
     * to the page.
     * 
     * Here we switch those urls
     * to the tab pane's id.
     */
    $('[data-afsm-switchto]').each(function () {
        var $a = $(this);

        $a.attr('href', $a.data('afsm-switchto'));
    });

    /*
     * Wire-up an event to the form 'submit' event,
     * for forms that should be submitted through AJAX.
     */
    $('form[data-afsm-ajax="true"]').submit(function (e) {
        var $form = $(this);

        // Submit the form asynchronously.
        // replace the original content
        // with the new data.
        $.ajax({
            url: $form.attr('action'),
            data: $form.serialize(),
            type: $form.attr('method')
        })
        .done(function (data) {
            var $data = $(data);

            $($form.parents('div[data-afsm-id]').data('afsm-id')).replaceWith($data);

            // Add a slight highlight to the new content.
            $data.find('.afsm-dohighlight').addClass('afsm-highlighted');
        });

        // Prevent the form from self-submitting.
        e.preventDefault();
        return false;
    });

    /**
     * Wire-up an event to the 'click' event
     * on pagination links that should be loaded
     * asynchronously.
     */
    $('[data-afsm-ajax="true"]').on('click', '.afsm-pagination a', function (e) {
        var $a = $(this);

        $.ajax({
            url: $a.attr('href'),
            // Link will have the information that is sent by the server, for users without JS.
            // data: $('form[data-afsm-ajax="true"]').serialize(),
            type: 'get'
        })
        .done(function (data) {
            var $data = $(data);

            $($a.parents('div[data-afsm-id]').data('afsm-id')).replaceWith($data);

            $data.find('.afsm-dohighlight').addClass('afsm-highlighted');
        });

        e.preventDefault();
        return false;
    });

    /**
     * Asynchronously load the contents in a tab.
     */
    $('[data-afsm-load]').each(function () {
        var $div = $(this);

        $.ajax({
            type: 'get',
            url: $div.data('afsm-load')
        })
        .done(function (data) {
            $div.replaceWith(data);
        });
    });
}($));