(function ($) {
    /*
     * Issue an AJAX request to the server when the suggest button is
     * clicked.
     */
    $('[data-afsm-suggest=true]').click(function ($event) {
        var $e = $($event.target);

        var src = $($e.data('afsm-src'));
        var entity = $($e.data('afsm-entitysrc'));

        var url = $e.data('afsm-url');
        var forElement = $($e.data('afsm-for'));

        if (!src.val()) {
            src.focus();
            alert($e.data('afsm-warning'));
        } else {
            var options = {
                type: 'post',
                url: url,
                data: {
                    parentId: src.val(),
                    entityId: entity.val()
                }
            };

            $.ajax(options)
             .done(function (data) {
                 // Replace the value and re-validate to
                 // remove any error messsages.
                 forElement.val(data);
                 forElement.valid();
             })
             .fail(function (err) {
                 console.error(err);
                 alert('Erro!!');
             });
        }
    });

    /*
     * Clear the code box when the drop-down list changes value.
     */
    $('[data-afsm-clear="true"]').change(function (evt) {
        $($(evt.target).data('afsm-target')).val('');
    });
}(jQuery));

//(function ($) {
//    /*
//     * Issue an AJAX request to the server when the suggest button is
//     * clicked.
//     */
//    $('[data-afsm-suggest=true]').click(function ($event) {
//        var $e = $($event.target);

//        var src = $('#' + $e.data('afsm-src'));
//        var url = $e.data('afsm-url');
//        var forElement = $('#' + $e.data('afsm-for'));

//        if (src.val() === '') {
//            src.focus();
//            alert($e.data('afsm-warning'));
//        } else {

//            var options = {
//                type: 'post',
//                url: url.replace('xx', src.val())
//            }

//            $.ajax(url.replace('xx', src.val()))
//             .done(function (data) {
//                 // Replace the value and re-validate to
//                 // remove any error messsages.
//                 forElement.val(data);
//                 forElement.valid();
//             })
//             .fail(function (err) {
//                 console.error(err);
//                 alert('Erro!!');
//             });
//        }
//    });

//    /*
//     * Clear the code box when the drop-down list changes value.
//     */
//    $('[data-afsm-clear="true"]').change(function (evt) {
//        $($(evt.target).data('afsm-target')).val('');
//    });
//}(jQuery));