(function ($) {
    var $input,
        $modal;

    $('body').on('click', '[data-afsm-use="modal"]', function () {
        var $btn = $(this);

        // Get the input on which the value will be added to
        // and the modal to choose the items.
        $input = $($btn.data('afsm-for'));
        $modal = $($btn.data('afsm-modal'));

        $modal.modal('show');
    });

    // When the user clicks on an item with a value,
    // get the value, update the list and close the modal dialog.
    $('body').on('click', '[data-afsm-value]', function (e) {
        var $img = $(this);

        $input.val($img.data('afsm-value'));

        $modal.modal('hide');

        e.preventDefault();
        return false;
    });
}($));