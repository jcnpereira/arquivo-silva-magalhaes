(function ($) {
    $('form[data-afsm-ajax="true"]').submit(function (e) {
        var $form = $(this);

        $.ajax({
            url: $form.attr('action'),
            data: $form.serialize(),
            type: $form.attr('method')
        })
        .done(function (data) {
            $($form.parents('div[data-afsm-id]').data('afsm-id')).replaceWith(data);
        });

        e.preventDefault();
        return false;
    });

    $('[data-afsm-ajax="true"]').on('click', '.afsm-pagination a', function (e) {
        var $a = $(this);

        $.ajax({
            url: $a.attr('href'),
            data: $('form[data-afsm-ajax="true"]').serialize(),
            type: 'get'
        })
        .done(function (data) {
            $($a.parents('div[data-afsm-id]').data('afsm-id')).replaceWith(data);
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