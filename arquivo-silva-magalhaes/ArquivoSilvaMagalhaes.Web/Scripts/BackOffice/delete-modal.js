(function ($) {
    var delButton = $('#afsm-doDelete'), alertModal = $('#afsm-deleteModalAlert'), url = "", correspondingForm, message;

    delButton.click(function () {
        correspondingForm.submit();
    });

    $('[data-afsm-show-modal="true"]').click(function (e) {
        var $element = $(e.target);
        url = $element.attr('href');
        message = $element.data('afsm-delete-warning');

        correspondingForm = $element.parent('form');

        $('#afsm-delete-modal-warning-message').text(message);

        alertModal.modal('show');

        e.preventDefault();

        return false;
    });
}(jQuery));
