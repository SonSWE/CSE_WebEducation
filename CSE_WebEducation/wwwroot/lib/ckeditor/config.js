/**
 * @license Copyright (c) 2003-2019, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see https://ckeditor.com/legal/ckeditor-oss-license
 */

CKEDITOR.editorConfig = function (config) {
    // Define changes to default configuration here. For example:
    config.language = 'vi';

    config.enterMode = CKEDITOR.ENTER_BR;
    config.fillEmptyBlocks = false;
    config.basicEntities = false;
    config.entities = false;
    // Include 'wordcount' plugin
    config.extraPlugins = 'wordcount';

    config.wordcount = {
        showCharCount: true,
        countHTML: true,
        countSpacesAsChars: true,
    };


    config.toolbar = [
        { name: 'clipboard', items: ['Cut', 'Copy', 'Paste', 'PasteText', 'PasteFromWord', '-', 'Undo', 'Redo'] },
        { name: 'editing', items: ['Find', 'Replace', 'SelectAll'] },
        { name: 'links', items: ['Link', 'Unlink', 'Anchor'] },
        { name: 'insert', items: ['Image', 'Table', 'HorizontalRule', 'SpecialChar'] },
        { name: 'tools', items: ['Maximize'] },
        { name: 'document', items: ['Preview', 'Templates', 'document', 'Source'] },
        { name: 'basicstyles', items: ['Bold', 'Italic', 'Underline', 'Strike', '-', 'RemoveFormat'] },
        { name: 'paragraph', items: ['NumberedList', 'BulletedList', '-', 'Outdent', 'Indent', '-', 'Blockquote', '-', 'JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock'] },
        { name: 'styles', items: ['Styles', 'Format', 'Font', 'FontSize'] },
        { name: 'colors', items: ['TextColor', 'BGColor'] },
        { name: 'about', items: ['About'] }
    ]
    config.removeDialogTabs = 'image:advanced;image:Link;link:advanced;link:target';

    config.filebrowserBrowseUrl = '/ckeditor/browse'; //Action for Browse file
    config.filebrowserUploadUrl = '/ckeditor/upload'; //Action for Uploading file

    config.allowedContent = true;
    config.pasteFromWordRemoveFontStyles = true;
};

CKEDITOR.on("instanceReady", function (event) {
    event.editor.on("beforeCommandExec", function (event) {
        // Show the paste dialog for the paste buttons and right-click paste
        if (event.data.name == "paste") {
            event.editor._.forcePasteDialog = true;
        }
        // Don't show the paste dialog for Ctrl+Shift+V
        if (event.data.name == "pastetext" && event.data.commandData.from == "keystrokeHandler") {
            event.cancel();
        }
    })
});
