var showSuccess = function (_msg, _title, _options) {
    _title = _title || "Thành công!";

    var defaults = {
        type: "success",
        mouse_over: "pause",
        placement: {
            from: "bottom",
            align: "right"
        },
        content: {
            icon: 'icon-success'
        }
    };
    _options = $.extend(true, {}, defaults, _options);

    $.notify({
        title: _title,
        message: _msg
    }, _options);
},
showInfo = function (_msg, _title, _options) {
    _title = _title || "Thông báo!";

    var defaults = {
        type: "info",
        mouse_over: "pause",
        placement: {
            from: "bottom",
            align: "right"
        },
        content: {
            icon: 'icon-info'
        }
    };
    _options = $.extend(true, {}, defaults, _options);

    $.notify({
        title: _title,
        message: _msg
    }, _options);
},
showWarning = function (_msg, _title, _options) {
    _title = _title || "Cảnh báo!";

    var defaults = {
        type: "warning",
        mouse_over: "pause",
        placement: {
            from: "bottom",
            align: "right"
        },
        content: {
            icon: 'icon-warning'
        }
    };
    _options = $.extend(true, {}, defaults, _options);

    $.notify({
        title: _title,
        message: _msg
    }, _options);
},
showError = function (_msg, _title, _options) {
    _title = _title || "Lỗi!";

    var defaults = {
        type: "error",
        mouse_over: "pause",
        placement: {
            from: "bottom",
            align: "right"
        },
        content: {
            icon: 'icon-error'
        }
    };
    _options = $.extend(true, {}, defaults, _options);

    $.notify({
        title: _title,
        message: _msg
    }, _options);
};