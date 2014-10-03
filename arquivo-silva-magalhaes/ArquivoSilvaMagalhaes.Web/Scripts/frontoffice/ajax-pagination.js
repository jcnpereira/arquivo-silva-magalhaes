(function ($) {
    'use strict';
    //function toggleFade($element) {
    //    $element.toggleClass('in');
    //}


    $('[data-afsm-switchto]').each(function () {
        var $a = $(this);

        $a.attr('href', $a.data('afsm-switchto'));
    });

    $('form[data-afsm-ajax="true"]').submit(function (e) {
        var $form = $(this);

        $.ajax({
            url: $form.attr('action'),
            data: $form.serialize(),
            type: $form.attr('method')
        })
        .done(function (data) {
            var $data = $(data);

            $($form.parents('div[data-afsm-id]').data('afsm-id')).replaceWith($data);

            $data.find('.afsm-dohighlight').addClass('afsm-highlighted');
        });

        e.preventDefault();
        return false;
    });

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