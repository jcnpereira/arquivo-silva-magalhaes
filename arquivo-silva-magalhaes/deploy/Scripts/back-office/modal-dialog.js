﻿/*
 * To be used with the modal helpers in create and edit forms
 * in the back-office.
 *
 * By @afecarv.
 */
(function ($) {
    var $e,     
        target,
        remote,
        list;
    
    $('body').on('click', '[data-afsm-toggle="modal"]', function (e) {
        $e = $(e.target);
        target = $e.data('target');
        remote = $e.data('remote');
        list = $e.data('afsm-selectlist');

        $(target + ' .modal-form').attr('action', remote);

        $(target + ' .modal-body').load(remote, function () {
            // Parse the form's contents for validation purposes, using the 
            // unobtrusive validation library.
            $.validator.unobtrusive.parse(target + ' .modal-form');

            // Show the modal dialog.
            $(target).modal('show');
        });

        e.preventDefault();
        return false;
    });

    $('body').on('submit', '.modal-form', function (e) {
        var $form = $(e.target);

        // Validate the form first.
        if ($form.valid()) {

            //var options = {
            //    type: $form.attr('method'),
            //    url: $form.attr('action'),
            //    headers: {
            //        getJson: true
            //    }
            //}

            $.post($form.attr('action'), $form.serialize(), function (data) { // data is an array { value, text }
                var $list = $(list),
                    i = 0;

                // Replace the select list's items with the new data.
                $list.empty();
                for (i; i < data.length; i++) {
                    var $option = $('<option value="' + data[i].value + '">' + data[i].text + '</option>');

                    if (data[i].selected) {
                        $option.attr('selected', '');
                    }

                     $option.appendTo($list);
                }
            });
            // Close the modal dialog.
            $(target).modal('hide');
        }
        // Prevent the form from submitting itself.
        e.preventDefault();
        return false;
    });
}(window.jQuery));