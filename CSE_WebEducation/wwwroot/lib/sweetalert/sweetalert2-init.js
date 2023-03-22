var nvsAlert = function ($title, $content, $fncallback) {
    try {
        $title = $title == undefined ? "Thông báo" : $title;
        swal({
            title: $title,
            text: $content,
            allowEscapeKey: false
        }).then(function () {
            if (typeof $fncallback === "function") {
                $fncallback();
            }
        }, function (dismiss) {
            if (typeof $fncallback === "function") {
                $fncallback();
            }
        });
    } catch (e) {
    }
},
    nvsInfo = function ($content, $fncallback) {
        try {
            swal({
                title: "Thông báo",
                text: $content,
                type: "info",
                showCancelButton: true,
                allowEscapeKey: false
            }).then(function () {
                if (typeof $fncallback === "function") {
                    $fncallback();
                }
            }, function (dismiss) {
                if (typeof $fncallback === "function") {
                    $fncallback();
                }
            });
        } catch (e) {
        }
    },
    nvsSuccess = function ($content, $fncallback) {
        try {
            swal({
                title: "Thành công",
                text: $content,
                type: "success"
            }).then(function () {
                if (typeof $fncallback === "function") {
                    $fncallback();
                }
            }, function (dismiss) {
                if (typeof $fncallback === "function") {
                    $fncallback();
                }
            });

        } catch (e) {
        }
    },
    nvsConfirm = function ($content, $fnokcallback, $fncancelcallback) {
        try {
            swal({
                title: "Cảnh báo",
                text: $content,
                type: "question",
                showCancelButton: true,
                allowEscapeKey: false,
                confirmButtonText: 'Đồng ý',
                cancelButtonText: "Hủy",
            }).then(function () {
                if (typeof $fnokcallback === "function") {
                    $fnokcallback();
                }
            }, function (dismiss) {
                if (typeof $fncancelcallback === "function") {
                    $fncancelcallback();

                }
            });
        } catch (e) {
        }
    },
    nvsConfirmInfo = function ($content, $fnokcallback, $fncancelcallback) {
        try {
            swal({
                title: "Thông báo",
                text: $content,
                type: "question",
                showCancelButton: true,
                allowEscapeKey: false,
                confirmButtonText: 'Đồng ý',
                cancelButtonText: "Hủy",
            }).then(function () {
                if (typeof $fnokcallback === "function") {
                    $fnokcallback();
                }
            }, function (dismiss) {
                if (typeof $fncancelcallback === "function") {
                    $fncancelcallback();
                }
            });
        } catch (e) {
        }
    },
    nvsWarning = function ($content, $fnokcallback, $fncancelcallback) {
        try {
            swal({
                title: "Cảnh báo",
                text: $content,
                type: "warning",
                showCancelButton: true,
                confirmButtonText: 'Đồng ý',
                cancelButtonText: "Hủy",
            }).then(function () {
                if (typeof $fnokcallback === "function") {
                    $fnokcallback();
                }
            }, function (dismiss) {
                if (typeof $fncancelcallback === "function") {
                    $fncancelcallback();
                }
            });
        } catch (e) {
        }
    },
    nvsError = function ($content, $fncallback) {
        try {
            swal({
                title: "Lỗi",
                text: $content,
                type: "error"
            }).then(function () {
                if (typeof $fncallback === "function") {
                    $fncallback();
                }
            }, function (dismiss) {
                if (typeof $fncallback === "function") {
                    $fncallback();
                }
            });
        } catch (e) {
        }
    },
    nvsAlertWithHtml = function ($title, $html, $type, $fncallback) {
        try {
            swal({
                title: $title,
                html: $html,
                type: $type
            }).then(function () {
                if (typeof $fncallback === "function") {
                    $fncallback();
                }
            }, function (dismiss) {
                if (typeof $fncallback === "function") {
                    $fncallback();
                }
            });
        } catch (e) {
        }
    },
    nvsPrompt = function ($title, $content, $type, $fnokcallback, $fnreject, $fncancelcallback, $maxlength) {
        try {
            $title = $title == undefined ? "" : $title;
            swal({
                title: $title,
                text: $content,
                input: $type,
                showCancelButton: true,
                //closeOnConfirm: false,
                animation: "slide-from-top",
                inputPlaceholder: "",
                confirmButtonText: 'Chấp nhận',
                cancelButtonText: 'Hủy',
                inputAttributes: { maxlength: $maxlength, id: 'txtPromt_p' },
                preConfirm: function (inputValue) {
                    return new Promise(function (resolve, reject) {
                        if (typeof $fnreject === "function") {
                            $fnreject(inputValue, resolve, reject);
                        }
                    })
                },
                allowOutsideClick: false

            }).then(function (inputValue) {
                if (typeof $fnokcallback === "function") {
                    $fnokcallback(inputValue);
                }
            }, function (dismiss) {
                if (typeof $fncancelcallback === "function") {
                    $fncancelcallback();
                }
            });
        } catch (e) {
        }
    };