function ShowPopupDialog(divWrapperPopup, Title, options) {
    let defaultOptions = {
        height: 0,
        idFocus: "",
        confirmClose: true,
        allowClose: true,
    };
    defaultoptions = Object.assign(defaultOptions, options);
    //
    let pHeight = defaultOptions.height;
    let txtIdFocus = defaultOptions.idFocus;

    var _height = pHeight + "px";
    if (pHeight == 0) {
        _height = "auto";
    }

    if ($("#" + divWrapperPopup + " .d-popup-header").length > 0) {
        $("#" + divWrapperPopup + " .d-popup-header").remove();
    }
    // bỏ alert hỏi có cần thoát hay ko nếu yêu cầu có hỏi kho thoát thì mở ra    
    $("#" + divWrapperPopup + " .d-popup").prepend('<div class="d-popup-header"><div class="popup-title">' + Title + '</div><button type="button" onclick="ClosePopupDialog(\'' + divWrapperPopup + '\', ' + (defaultOptions.confirmClose == false ? 'false' : 'true') + ')" class="btn-exit-popup fa fa-times"></button></div>');
    $("#" + divWrapperPopup + " .d-popup").css({ "height": _height });

    $("#" + divWrapperPopup).fadeIn(150).addClass('popup-flex');

    if (txtIdFocus != "") {
        var idFocus = "#" + txtIdFocus;
        $(idFocus).focus().val($(idFocus).val());
    }
    else {
        $('#' + divWrapperPopup + ' .btn-exit-popup').focus();
    }
    if (!defaultOptions.allowClose) {
        $('.btn-close').css('display', 'none');
        $('#btnExit').css('display', 'none');
        $('#btnLogOut').show();
    } else {
        $('#btnLogOut').hide();
    }
    disableScrolling();
    //$("body").addClass("hide-scroll");
    //fixScrollbar();
    return true;
}

function ClosePopupDialog(divWrapperPopup, confirmClose, clearContent) {
    var clearContent = clearContent || false;
    confirmClose = confirmClose || false;
    var timer = 150, timerCallback = 200;
    if (confirmClose) {
        nvsConfirm("Bạn có chắc chắn muốn thoát?", function () {
            $("#" + divWrapperPopup).fadeOut(timer);
            setTimeout(function () { $("#" + divWrapperPopup).removeClass('popup-flex'); }, timer);
            enableScrolling();
            //$("body").removeClass("hide-scroll");
            //undoScrollbar();

            if (!!$.datetimepicker) {
                // Clear date input
                $("#" + divWrapperPopup + " .ip-popup-clrdate").datetimepicker("destroy");
            }
            setTimeout(function () {
                $("#" + divWrapperPopup).trigger("customTriggerClosePopup");
            }, timerCallback);
        });
    }
    else {
        $("#" + divWrapperPopup).fadeOut(timer);
        setTimeout(function () { $("#" + divWrapperPopup).removeClass('popup-flex'); }, timer);
        enableScrolling();
        //$("body").removeClass("hide-scroll");
        //undoScrollbar();

        if (!!$.datetimepicker) {
            // Clear date input
            $("#" + divWrapperPopup + " .ip-popup-clrdate").datetimepicker("destroy");
        }
        setTimeout(function () {
            $("#" + divWrapperPopup).trigger("customTriggerClosePopup");
        }, timerCallback);
    }
    if (clearContent && clearContent != null) {
        $("#" + divWrapperPopup).html('');
    }
}

function disableScrolling() {
    var x = window.scrollX;
    var y = window.scrollY;
    window.onscroll = function () { window.scrollTo(x, y); };
}

function enableScrolling() {
    window.onscroll = function () { };
}


var _countSpinLoading = 0; // Dem so lan func dc goi
function SpinLoading($create) {
    $create = $create || false;
    if ($create) {
        _countSpinLoading++;
        $('#divLoader').css('display', 'block');
    } else {
        _countSpinLoading--;
        if (_countSpinLoading <= 0) {
            _countSpinLoading = 0;
            $('#divLoader').css('display', 'none');
        }
    }
}

function CreateFixedTable($tabletarget, $fixColumnOptions, $options) {
    try {
        let defaultOptions = {
            retrieve: true,
            scrollX: true,
            scrollY: "calc(100vh - 264px)",
            scrollCollapse: true,
            "info": false,
            "paging": false,
            "ordering": false,
            "filter": false,
            "autoWidth": false,
            "language": {
                oPaginate: {
                    sFirst: "&#x25C4;&#x25C4;",
                    sLast: "&#x25BA;&#x25BA;",
                    sNext: "&#x25BA;",
                    sPrevious: "&#x25C4;"
                },
                sEmptyTable: "Không có dữ liệu",
                sLengthMenu: "Hiển thị _MENU_ bản ghi",
                sInfo: "Hiển thị từ _START_ đến _END_ trên tổng _TOTAL_ bản ghi",
                sInfoEmpty: "Hiển thị từ 0 đến 0 trên tổng 0 bản ghi",
            },
            "aLengthMenu": [10, 20, 50, 100],
        };
        let defaultFixColumnOptions = {
            leftColumns: 2,
            rightColumns: 0
        };

        defaultOptions = Object.assign(defaultOptions, $options)
        defaultFixColumnOptions = Object.assign(defaultFixColumnOptions, $fixColumnOptions)
        defaultOptions.fixedColumns = defaultFixColumnOptions;

        //
        let hiddenColumnCount = 0;
        if (defaultOptions.hiddenColumns && defaultOptions.hiddenColumns.length > 0) {
            hiddenColumnCount = defaultOptions.hiddenColumns.length;

            let columnDefs = defaultOptions.columnDefs || [];
            columnDefs.push({ targets: defaultOptions.hiddenColumns, visible: false });
            defaultOptions.columnDefs = columnDefs;
        }

        // Tinh toan add class style shadow
        if (defaultFixColumnOptions.leftColumns > 0 || defaultFixColumnOptions.rightColumns > 0) {
            var colsTargetIdx = [];

            // neu co fix cot trai
            if (defaultFixColumnOptions.leftColumns > 0) {
                colsTargetIdx.push(defaultFixColumnOptions.leftColumns + hiddenColumnCount - 1); // danh cho TD phai la so
                colsTargetIdx.push((defaultFixColumnOptions.leftColumns + hiddenColumnCount - 1).toFixed(0)); // danh cho TH phai la chu
            }

            // neu co fix cot phai
            if (defaultFixColumnOptions.rightColumns > 0) {
                colsTargetIdx.push(defaultFixColumnOptions.rightColumns * -1); // danh cho TD phai la so
                colsTargetIdx.push((defaultFixColumnOptions.rightColumns * -1).toFixed(0)); // danh cho TH phai la chu
            }

            //
            let columnDefs = defaultOptions.columnDefs || [];
            columnDefs.push({ targets: colsTargetIdx, className: 'dtfc-fixed-shadow' });
            defaultOptions.columnDefs = columnDefs;
        }

        //Measure table
        let tablecontainer = $($tabletarget).closest('#fx-table-container');
        if ($($tabletarget).closest('#fx-table-container').length == 0) {
            tablecontainer = $($tabletarget).closest('.over-flow-x');
        }
        //console.log(tablecontainer);
        tablecontainer.css({
            "height": tablecontainer.height(),
            "width": "100%"
        });
        tablecontainer.find("> *").addClass('hidden');
        var width = tablecontainer.width();
        tablecontainer.css({
            "height": "auto",
            "width": width
        });
        tablecontainer.find("> *").removeClass('hidden');
        //
        var table = $($tabletarget).DataTable(defaultOptions);
        /*
         * 12/10/2022 longdh bỏ false để table tự động tính toán width các cột
        table.columns.adjust(false);
        table.rows().recalcHeight(false);
        */
        table.columns.adjust();
        table.rows().recalcHeight();
        table.draw();

        return table;
    } catch (e) {
        //console.log(e);
    }
}

function StringToDate_ddMMyyyy(strDate, strDelemiter) {
    strDelemiter = strDelemiter || '/';

    if (isDate_ddMMyyyy(strDate) == false) {
        return null;
    }

    var dateParts = strDate.split(strDelemiter);
    var year = dateParts[2];
    var month = dateParts[1];
    var day = dateParts[0];

    if (isNaN(day) || isNaN(month) || isNaN(year))
        return null;

    return new Date(year, month - 1, day);
}

//0: Sai định dạng 1: Ngày không tồn tại 2: Thành công
function isDate_ddMMyyyy(strDate) {
    var currVal = strDate;
    //var rxDatePattern = /^(\d{2})(\/|-)(\d{2})(\/|-)(\d{4})$/;
    var rxDatePattern = /^(\d{2})(\/)(\d{2})(\/)(\d{4})$/;
    var dtArray = currVal.match(rxDatePattern); // is format OK?
    if (dtArray == null) {
        return 0;
    }
    dtDay = dtArray[1];
    dtMonth = dtArray[3];
    dtYear = dtArray[5];
    if (dtYear < 1000)
        return 1;
    else if (dtMonth < 1 || dtMonth > 12)
        return 1;
    else if (dtDay < 1 || dtDay > 31)
        return 1;
    else if ((dtMonth == 4 || dtMonth == 6 || dtMonth == 9 || dtMonth == 11) && dtDay == 31)
        return 1;
    else if (dtMonth == 2) {
        var isleap = (dtYear % 4 == 0 && (dtYear % 100 != 0 || dtYear % 400 == 0));
        if (dtDay > 29 || (dtDay == 29 && !isleap))
            return 1;
    }
    return 2;
}

// CHECK UNICODE

function IsContainUnicode(pStrString) {
    try {
        strRegex = /à|á|ạ|ả|ã|â|ầ|ấ|ậ|ẩ|ẫ|ă|ằ|ắ|ặ|ẳ|ẵ|è|é|ẹ|ẻ|ẽ|ê|ề|ế|ệ|ể|ễ|ì|í|ị|ỉ|ĩ|ò|ó|ọ|ỏ|õ|ô|ồ|ố|ộ|ổ|ỗ|ơ|ờ|ớ|ợ|ở|ỡ|ù|ú|ụ|ủ|ũ|ư|ừ|ứ|ự|ử|ữ|ỳ|ý|ỵ|ỷ|ỹ|đ/gi;
        return strRegex.test(pStrString);
    } catch (e) {
        return false;
    }
}

//Check xem string có chứa khoảng trắng hay không 
function isContainWhitespaces(str) {
    return /\s/.test(str);
}

//Check password thỏa mãn 3/4 tiêu chí only hehe
function validatePasswordRules34(password) {
    let count = 0;
    if (!password)
        //count++;
        return false;
    if (password.length < 8)
        //count++;
        return false;
    if (password.length > 20)
        //count++;
        return false;
    if (IsContainUnicode(password))
        //count++;
        return false;
    if (password.indexOf(' ') >= 0)
        //count++;
        return false;
    if (!/\d/.test(password)) { //digit
        count++;
        //return false;
    }
    if (!/[a-z]/.test(password)) {  // lowercase
        count++;
        //return false;
    }
    if (!/[A-Z]/.test(password)) {  // uppercase
        count++;
        //return false;
    }
    if (!/[^0-9a-zA-Z]/.test(password)) { // special characters
        count++;
        //return false;
    }
    console.log(count);
    if (count > 1) {
        return false;
    } else {
        return true;
    }
}



function inittitlefuction() {
    $('.btn-function').off('mouseover').on('mouseover', function () {
        var _windowScroll = $(window).scrollTop();
        var _windowLeft = $(window).scrollLeft();
        // tinh vị trí của của item-list
        var _offset = $(this).offset();
        $('.name-function-fix', $(this)).css({ "top": (_offset.top - _windowScroll) + "px", "left": (_offset.left - _windowLeft) + "px" })
    }).off('mouseout').on('mouseout', function () {
        $('.name-function-fix', $(this)).css({ "top": "-1000px", "left": "-1000px" })
    });

    if (window.innerWidth < 1600) {
        $('.btn-function.btn-function-last3').off('mouseover').on('mouseover', function () {
            var _windowScroll = $(window).scrollTop();
            var _windowLeft = $(window).scrollLeft();
            // tinh vị trí của của item-list
            var _offset = $(this).offset();
            $('.name-function-fix', $(this)).css({ "top": (_offset.top - _windowScroll) + "px", "left": (_offset.left - _windowLeft - 25) + "px" })
        }).off('mouseout').on('mouseout', function () {
            $('.name-function-fix', $(this)).css({ "top": "-1000px", "left": "-1000px" })
        });
    }

    if (window.innerWidth < 1600) {
        $('.btn-function.btn-function-last2').off('mouseover').on('mouseover', function () {
            var _windowScroll = $(window).scrollTop();
            var _windowLeft = $(window).scrollLeft();
            // tinh vị trí của của item-list
            var _offset = $(this).offset();
            $('.name-function-fix', $(this)).css({ "top": (_offset.top - _windowScroll) + "px", "left": (_offset.left - _windowLeft - 10) + "px" })
        }).off('mouseout').on('mouseout', function () {
            $('.name-function-fix', $(this)).css({ "top": "-1000px", "left": "-1000px" })
        });
    }


    if (innerWidth < 992) {
        $('.btn-function').off('focusin').on('focusin', function () {
            var _windowScroll = $(window).scrollTop();
            var _windowLeft = $(window).scrollLeft();
            // tinh vị trí của của item-list
            var _offset = $(this).offset();
            $('.name-function-fix', $(this)).css({ "top": (_offset.top - _windowScroll) + "px", "left": (_offset.left - _windowLeft) + "px" })
        }).off('focusout').on('focusout', function () {
            $('.name-function-fix', $(this)).css({ "top": "-1000px", "left": "-1000px" })
        });
    }
}


function focusTableFixed() {
    $('.dataTables_scrollBody tbody tr').click(function () {
        var table = $(event.target).parents('.dataTables_scrollBody').find('table.dataTable.no-footer');
        trIndex = $(this).index() + 1;

        $('.dataTables_scrollBody tbody tr').removeClass("hover-ac");

        if (!$(this).hasClass('.hover-ac') !== false) {
            table.find("tr:eq(" + trIndex + ")").addClass("hover-ac");
        }
    })
}

// == FIX TABLE

function GetColConfigTable($tartable) {
    try {
        var arrIndex = [];
        //
        var $isDataTable = $.fn.DataTable.isDataTable(document.getElementById($tartable));
        var columnOrder = [];
        var columns = [];
        if ($isDataTable) {
            var $table = $(document.getElementById($tartable)).DataTable();
            columnOrder = Array.apply([], $table.colReorder.order());
            columns = Array.apply([], $table.columns().header());
        }

        // Bên Portal chỉ có xem và di chuyển cột nên viết khác
        // Bản gốc bên admin

        $.each(columns, function (idx, el) {
            var _targetcol = $(this).attr('data-colconfig');
            var _targetidx = Number($(this).attr('data-colindex'));
            var _formula = "";
            var _orderindex = -1;

            if ($isDataTable && columnOrder.length > 0) {
                _orderindex = columnOrder.indexOf(_targetidx);
            }
            else {
                _orderindex = _targetidx;
            }

            arrIndex.push({
                colindex: _targetidx,
                colname: _targetcol,
                orderindex: _orderindex,
                visible: true,
                formula: _formula
            });
        });

        return arrIndex;
    } catch (e) {
        console.log(e);
        return null;
    }
}

function CreateFixedTable($tabletarget, $fixColumnOptions, $options) {
    try {
        let defaultOptions = {
            retrieve: true,
            scrollX: true,
            scrollY: "calc(100vh - 264px)",
            scrollCollapse: true,
            "info": false,
            "paging": false,
            "ordering": false,
            "filter": false,
            "autoWidth": false,
            "language": {
                oPaginate: {
                    sFirst: "&#x25C4;&#x25C4;",
                    sLast: "&#x25BA;&#x25BA;",
                    sNext: "&#x25BA;",
                    sPrevious: "&#x25C4;"
                },
                sEmptyTable: "Không có dữ liệu",
                sLengthMenu: "Hiển thị _MENU_ bản ghi",
                sInfo: "Hiển thị từ _START_ đến _END_ trên tổng _TOTAL_ bản ghi",
                sInfoEmpty: "Hiển thị từ 0 đến 0 trên tổng 0 bản ghi",
            },
            "aLengthMenu": [10, 20, 50, 100],
        };
        let defaultFixColumnOptions = {
            leftColumns: 2,
            rightColumns: 0
        };

        defaultOptions = Object.assign(defaultOptions, $options)
        defaultFixColumnOptions = Object.assign(defaultFixColumnOptions, $fixColumnOptions)
        defaultOptions.fixedColumns = defaultFixColumnOptions;

        //
        let hiddenColumnCount = 0;
        if (defaultOptions.hiddenColumns && defaultOptions.hiddenColumns.length > 0) {
            hiddenColumnCount = defaultOptions.hiddenColumns.length;

            let columnDefs = defaultOptions.columnDefs || [];
            columnDefs.push({ targets: defaultOptions.hiddenColumns, visible: false });
            defaultOptions.columnDefs = columnDefs;
        }

        // Tinh toan add class style shadow
        if (defaultFixColumnOptions.leftColumns > 0 || defaultFixColumnOptions.rightColumns > 0) {
            var colsTargetIdx = [];

            // neu co fix cot trai
            if (defaultFixColumnOptions.leftColumns > 0) {
                colsTargetIdx.push(defaultFixColumnOptions.leftColumns + hiddenColumnCount - 1); // danh cho TD phai la so
                colsTargetIdx.push((defaultFixColumnOptions.leftColumns + hiddenColumnCount - 1).toFixed(0)); // danh cho TH phai la chu
            }

            // neu co fix cot phai
            if (defaultFixColumnOptions.rightColumns > 0) {
                colsTargetIdx.push(defaultFixColumnOptions.rightColumns * -1); // danh cho TD phai la so
                colsTargetIdx.push((defaultFixColumnOptions.rightColumns * -1).toFixed(0)); // danh cho TH phai la chu
            }

            //
            let columnDefs = defaultOptions.columnDefs || [];
            columnDefs.push({ targets: colsTargetIdx, className: 'dtfc-fixed-shadow' });
            defaultOptions.columnDefs = columnDefs;
        }

        //Measure table
        let tablecontainer = $($tabletarget).closest('#fx-table-container');
        if ($($tabletarget).closest('#fx-table-container').length == 0) {
            tablecontainer = $($tabletarget).closest('.over-flow-x');
        }
        //console.log(tablecontainer);
        tablecontainer.css({
            "height": tablecontainer.height(),
            "width": "100%"
        });
        tablecontainer.find("> *").addClass('hidden');
        var width = tablecontainer.width();
        tablecontainer.css({
            "height": "auto",
            "width": width
        });
        tablecontainer.find("> *").removeClass('hidden');
        //
        var table = $($tabletarget).DataTable(defaultOptions);
        /*
         * 12/10/2022 longdh bỏ false để table tự động tính toán width các cột
        table.columns.adjust(false);
        table.rows().recalcHeight(false);
        */
        table.columns.adjust();
        table.rows().recalcHeight();
        table.draw();

        return table;
    } catch (e) {
        //console.log(e);
    }
}

// == FIX TABLE - END

