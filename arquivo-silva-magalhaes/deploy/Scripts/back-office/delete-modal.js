/**
 * The following code shows a nice
 * modal dialog when the user clicks on
 * a delete link in list-like pages.
 */
(function ($) {
    var delButton = $('#afsm-doDelete'),
        alertModal = $('#afsm-deleteModalAlert'),
        url = "", correspondingForm,
        message;

    delButton.click(function () {
        correspondingForm.submit();
    });

    $('body').on('click', '[data-afsm-show-modal="true"]', function (e) {
        var $element = $(this);
        url = $element.attr('href');
        message = $element.data('afsm-delete-warning');

        correspondingForm = $element.parent('form');

        $('#afsm-delete-modal-warning-message').text(message);

        alertModal.modal('show');

        e.preventDefault();

        return false;
    });
}(jQuery));
