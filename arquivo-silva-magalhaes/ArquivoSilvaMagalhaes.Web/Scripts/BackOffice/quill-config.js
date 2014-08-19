// This code contains the configs to automatically bind the quilljs
// editor to selected fields.
(function ($) {
    // Contains all editors and the ids of
    // the input element to update
    // with the html.
    var quillList = [];

    $('.afsm-quill').each(function (index, element) {
        var $element = $(element);
        var toolbarId = $element.data('afsm-toolbar'),
            editorId = $element.data('afsm-editor');

        var quill = new Quill(editorId, {
            modules: {
                'link-tooltip': true
            }
        });

        quill.addModule('toolbar', { container: toolbarId });

        quillList.push({
            quillItem: quill,
            target: $element.data('afsm-target')
        });

        // Add an attribute to the enclosing form so that
        // when it submits, the html is added to it.
        $element.closest('form').attr('data-afsm-needs-html', 'true');
    });

    // Use the aforementioned attribute
    // to intercept the 'submit' event
    // and set the field with the html data.
    $('[data-afsm-needs-html="true"]').submit(function (e) {
        for (var i = 0; i < quillList.length; i++) {
            var html = quillList[i].quillItem.getHTML();
            console.log(html);

            // HACK: Add a target = _blank to each link.
            $(quillList[i].target).val(html.replace(/<a href=/g, '<a target="_blank" href='));
        }
    });
}(window.jQuery));