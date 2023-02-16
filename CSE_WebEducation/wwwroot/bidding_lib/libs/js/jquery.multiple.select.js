//Cách dùng cấu hình chon 1 giá trị 
//$('#cboKhachHang').multipleSelect({
//    width: '80%', id: 'txtKhachHang', filter: true, single: true
//});

//Cach cau hinh  mutilselect   sangdd comment 

//$('#cboSymbol').multipleSelect({
//    width: '150px', filter: true,
//    noMatchesFound: "Không tìm thấy dữ liệu",
//    isdatastring: true  //kiểu dl tra ra là string 
//}).multipleSelect("checkAll");

//focus    $("#cboSymbol").multipleSelect("focus");

//neu check nhieu gia tri thi lay theo hamsau  sangdd comment 
//var p_chiso = $("#idCBo").val() == null ? "0" : $('#idCBo').multipleSelect("getSelectVal");
//neu check 1 thi p_chiso = $("#idCBo").val()
//// lấy selected text của list những thằng đc chọn
//$("#cboLoaiTraiPhieu").multipleSelect("getSelects", "text").join(','));

//List mặc đinhj ko chọn thằng nào
//$('#_cboFiles').multipleSelect({
//    width: '700px', id: 'txtfiles', filter: true, isdatastring: true,
//    placeholder: 'None',
//}).multipleSelect("uncheckAll");


//chọn 1 giá trị 
//$('#cboIndustry').multipleSelect({
//    width: '200px', id: 'txtIndustry', filter: true, single: true
//});

//disable thuoc tinh cua combobox 
//$("select").multipleSelect("disable")

(function ($) {

    'use strict';

    function MultipleSelect($el, options) {
        var that = this,
            name = $el.attr('name') || options.name || ''

        $el.parent().hide();
        var elWidth = $el.css("width");
        $el.parent().show();
        if (elWidth == "0px") { elWidth = $el.outerWidth() + 20 }

        this.$el = $el.hide();
        this.options = options;
        this.$parent = $('<div' + $.map(['class', 'title'], function (att) {
            var attValue = that.$el.attr(att) || '';
            attValue = (att === 'class' ? ('ms-parent' + (attValue ? ' ' : '')) : '') + attValue;
            return attValue ? (' ' + att + '="' + attValue + '"') : '';
        }).join('') + ' />');
        this.$choice = $('<button type="button" class="ms-choice"><span id="' + options.id + '" class="placeholder">' +
            options.placeholder + '</span><div></div></button>');
        this.$drop = $('<div class="ms-drop ' + options.position + '"></div>');
        this.$el.after(this.$parent);
        this.$parent.append(this.$choice);
        this.$parent.append(this.$drop);

        if (this.$el.prop('disabled')) {
            this.$choice.addClass('disabled');
        }
        this.$parent.css('width', options.width || elWidth);

        if (!this.options.keepOpen) {
            $('body').click(function (e) {
                if ($(e.target)[0] === that.$choice[0] ||
                    $(e.target).parents('.ms-choice')[0] === that.$choice[0]) {
                    return;
                }
                if (($(e.target)[0] === that.$drop[0] ||
                    $(e.target).parents('.ms-drop')[0] !== that.$drop[0]) &&
                    that.options.isOpen) {
                    that.close();
                }
            });
        }

        this.selectAllName = 'name="selectAll' + name + '"';
        this.selectGroupName = 'name="selectGroup' + name + '"';
        this.selectItemName = 'name="selectItem' + name + '"';
    }

    MultipleSelect.prototype = {
        constructor: MultipleSelect,

        init: function () {
            var that = this,
                html = [];
            if (this.options.filter) {
                html.push(
                    '<div class="ms-search">',
                    '<input type="text" autocomplete="off" autocorrect="off" autocapitilize="off" spellcheck="false">',
                    '</div>'
                );
            }

            // -- Thêm cái div bao ngoài ul
            html.push('<div class="ms-ul"><ul>');
            if (this.options.selectAll && !this.options.single) {
                html.push(
                    '<li class="ms-select-all">',
                    '<label>',

                    '<input type="checkbox" value=""' + this.selectAllName + ' /> ', //SANGDD THEM VALUE='' 
                    this.options.selectAllDelimiter[0] + this.options.selectAllText + this.options.selectAllDelimiter[1],
                    '</label>',
                    '</li>'
                );
            }
            $.each(this.$el.children(), function (i, elm) {
                html.push(that.optionToHtml(i, elm));
            });
            html.push('<li class="ms-no-results"><label>' + this.options.noMatchesFound + '</label></li>');
            // Thêm đóng cái div bao ngoài ul
            html.push('</ul></div>');
            this.$drop.html(html.join(''));

            this.$drop.css('width', '100%' || (this.$parent.width() + 'px'));

            this.$drop.find('ul').css('max-height', this.options.maxHeight + 'px');
            this.$drop.find('.multiple').css('width', this.options.multipleWidth + 'px');

            this.$searchInput = this.$drop.find('.ms-search input');
            this.$selectAll = this.$drop.find('input[' + this.selectAllName + ']');
            this.$selectGroups = this.$drop.find('input[' + this.selectGroupName + ']');
            this.$selectItems = this.$drop.find('input[' + this.selectItemName + ']:enabled');
            this.$disableItems = this.$drop.find('input[' + this.selectItemName + ']:disabled');
            this.$noResults = this.$drop.find('.ms-no-results');
            this.events();
            this.updateSelectAll(true);
            this.update(true);

            if (this.options.isOpen) {
                this.open();
            }
        },

        optionToHtml: function (i, elm, group, groupDisabled) {
            var that = this,
                $elm = $(elm),
                html = [],
                multiple = this.options.multiple,
                optAttributesToCopy = ['class', 'title'];
            if (this.options.addtlOptAttributesToCopy.length > 0) {
                optAttributesToCopy.push(...this.options.addtlOptAttributesToCopy)
            }
            var clss = $.map(optAttributesToCopy, function (att, i) {
                    var isMultiple = att === 'class' && multiple;
                    var attValue = $elm.attr(att) || '';
                    return (isMultiple || attValue) ?
                        (' ' + att + '="' + (isMultiple ? ('multiple' + (attValue ? ' ' : '')) : '') + attValue + '"') :
                        '';
                }).join(''),
                disabled,
                type = this.options.single ? 'radio' : 'checkbox';
            
            if ($elm.is('option')) {
                var value = $elm.val(),
                    text = that.options.textTemplate($elm).replace(/</g, '&lt;').replace(/>/g, '&gt;'),
                    selected = (that.$el.attr('multiple') != undefined) ? $elm.prop('selected') : ($elm.attr('selected') == 'selected'),
                    style = this.options.styler(value) ? ' style="' + this.options.styler(value) + '"' : '';

                disabled = groupDisabled || $elm.prop('disabled');
                if ((this.options.blockSeparator > "") && (this.options.blockSeparator == $elm.val())) {
                    html.push(
                        '<li' + clss + style + '>',
                        '<label class="' + this.options.blockSeparator + (disabled ? 'disabled' : '') + '">',
                        text,
                        '</label>',
                        '</li>'
                    );
                } else {
                    html.push(
                        '<li' + clss + style + '>',
                        '<label' + (disabled ? ' class="disabled"' : '') + '>',
                        '<input type="' + type + '" ' + this.selectItemName + ' value="' + value + '"' +
                        (selected ? ' checked="checked"' : '') +
                        (disabled ? ' disabled="disabled"' : '') +
                        (group ? ' data-group="' + group + '"' : '') +
                        '/> ',
                        text,
                        '</label>',
                        '</li>'
                    );
                }
            } else if (!group && $elm.is('optgroup')) {
                var _group = 'group_' + i,
                    label = $elm.attr('label');

                disabled = $elm.prop('disabled');
                html.push(
                    '<li class="group">',
                    '<label class="optgroup' + (disabled ? ' disabled' : '') + '" data-group="' + _group + '">',
                    (this.options.hideOptgroupCheckboxes ? '' : '<input type="checkbox" ' + this.selectGroupName +
                        (disabled ? ' disabled="disabled"' : '') + ' /> '),
                    label,
                    '</label>',
                    '</li>');
                $.each($elm.children(), function (i, elm) {
                    html.push(that.optionToHtml(i, elm, _group, disabled));
                });
            }
            return html.join('');
        },

        events: function () {
            var that = this;

            function toggleOpen(e) {
                e.preventDefault();
                that[that.options.isOpen ? 'close' : 'open']();
            }

            var label = this.$el.parent().closest('label')[0] || $('label[for=' + this.$el.attr('id') + ']')[0];
            if (label) {
                $(label).off('click').on('click', function (e) {
                    if (e.target.nodeName.toLowerCase() !== 'label' || e.target !== this) {
                        return;
                    }
                    toggleOpen(e);
                    if (!that.options.filter || !that.options.isOpen) {
                        that.focus();
                    }
                    e.stopPropagation(); // Causes lost focus otherwise
                });
            }
            this.$choice.off('click').on('click', toggleOpen)
                .off('focus').on('focus', this.options.onFocus)
                .off('blur').on('blur', this.options.onBlur);

            this.$choice.off('keydown').on('keydown', function (e) {
                if (that.options.single) {
                    var key_code = e.keyCode || e.which;
                    if (key_code === 9) { // Ensure shift-tab causes lost focus from filter as with clicking away
                        that.close();
                    }
                    else if (key_code == 38) { //  Key Up
                        if (that.options.single) {
                            var crr_select = that.$selectItems.filter(":visible").parent().parent(".selected");

                            if (crr_select.length > 0) {
                                crr_select.find("input:radio").focus();
                            }
                            else {
                                var all_el = that.$selectItems.filter(":visible");
                                if (all_el.length > 0) {
                                    all_el.eq(all_el.length - 1).focus();
                                }
                            }
                        }
                        else {
                            var all_el = that.$selectItems.filter(":visible");
                            if (all_el.length > 0) {
                                all_el.eq(all_el.length - 1).focus();
                            }
                        }

                        e.preventDefault();
                    }
                    else if (key_code == 40) { //  Key Down
                        if (that.options.single) {
                            var crr_select = that.$selectItems.filter(":visible").parent().parent(".selected");

                            if (crr_select.length > 0) {
                                crr_select.find("input:radio").focus();
                            }
                            else {
                                var all_el = that.$selectItems.filter(":visible");
                                if (all_el.length > 0) {
                                    all_el.eq(0).focus();
                                }
                            }
                        }
                        else {
                            var all_el = that.$selectItems.filter(":visible");
                            if (all_el.length > 0) {
                                all_el.eq(0).focus();
                            }
                        }

                        e.preventDefault();
                    }
                }
            });

            this.$parent.off('keydown').on('keydown', function (e) {
                switch (e.which) {
                    case 27: // esc key
                        that.close();
                        that.$choice.focus();
                        break;
                }
            });
            this.$searchInput.off('keydown').on('keydown', function (e) {
                var key_code = e.keyCode || e.which;

                if (key_code === 9 && e.shiftKey) { // Ensure shift-tab causes lost focus from filter as with clicking away
                    that.close();
                }
                else if (key_code === 9) { // Ensure shift-tab causes lost focus from filter as with clicking away
                    if (that.options.single) {
                        that.close();
                    }
                }
                else if (key_code == 38) { //  Key Up
                    var crr_select = that.$selectItems.filter(":visible").parent().parent(".selected");

                    if (that.options.single && crr_select.length > 0) {
                        crr_select.find("input:radio").focus();
                    }
                    else {
                        var all_el = that.$selectItems.filter(":visible");
                        if (all_el.length > 0) {
                            all_el.eq(all_el.length - 1).focus();
                        }
                    }

                    e.preventDefault();
                }
                else if (key_code == 40) { //  Key Down
                    var crr_select = that.$selectItems.filter(":visible").parent().parent(".selected");

                    if (that.options.single && crr_select.length > 0) {
                        crr_select.find("input:radio").focus();
                    }
                    else {
                        if (that.options.selectAll && !that.options.single) {
                            $(that.$selectAll).focus();
                        }
                        else {
                            var all_el = that.$selectItems.filter(":visible");
                            if (all_el.length > 0) {
                                all_el.eq(0).focus();
                            }
                        }
                    }

                    e.preventDefault();
                }
                else if (key_code == 13) { //  Key Enter
                    that.close();
                    that.focus();
                    e.preventDefault();
                }
            }).off('keyup').on('keyup', function (e) {
                if (that.options.filterAcceptOnEnter &&
                    (e.which === 13 || e.which == 32) && // enter or space
                    that.$searchInput.val() // Avoid selecting/deselecting if no choices made
                ) {
                    that.$selectAll.click();
                    that.close();
                    that.focus();
                    return;
                }
                that.filter();
            });
            this.$selectAll.off('click').on('click', function (e) {

                var px = e.clientX, py = e.clientY;
                if (px != 0 && py != 0) {
                    $(this).parent().parent().removeClass("testhover");
                }

                var checked = $(this).prop('checked'),
                    $items = that.$selectItems.filter(':visible');

                if ($items.length === that.$selectItems.length) {
                    that[checked ? 'checkAll' : 'uncheckAll']();
                } else { // when the filter option is true
                    that.$selectGroups.prop('checked', checked);
                    $items.prop('checked', checked);
                    that.options[checked ? 'onCheckAll' : 'onUncheckAll']();
                    that.update();
                }
            });
            this.$selectGroups.off('click').on('click', function () {
                var group = $(this).parent().attr('data-group'),
                    $items = that.$selectItems.filter(':visible'),
                    $children = $items.filter('[data-group="' + group + '"]'),
                    checked = $children.length !== $children.filter(':checked').length;
                $children.prop('checked', checked);
                that.updateSelectAll();
                that.update();
                that.options.onOptgroupClick({
                    label: $(this).parent().text(),
                    checked: checked,
                    children: $children.get()
                });
            });
            this.$selectItems.off('click').on('click', function (e) {

                var px = e.clientX, py = e.clientY;
                if (px != 0 && py != 0) {
                    $(this).parent().parent().removeClass("testhover");
                }

                that.updateSelectAll();
                that.update();
                that.updateOptGroupSelect();
                that.options.onClick({
                    label: $(this).parent().text(),
                    value: $(this).val(),
                    checked: $(this).prop('checked')
                });

                if (that.options.single && that.options.isOpen && !that.options.keepOpen) {
                    that.close();
                    //that.focus();

                    that.$choice.focus();
                }
                //if (that.options.single) {
                //    $(that.$choice).trigger("keydown", [9]);
                //}
            });

            this.$selectAll.off('focus').on('focus', function () {
                $(this).parent().parent().addClass("testhover");
            });
            this.$selectAll.off('blur').on('blur', function () {
                $(this).parent().parent().removeClass("testhover");
            });
            this.$selectItems.off('focus').on('focus', function () {
                $(this).parent().parent().addClass("testhover");
            });
            this.$selectItems.off('blur').on('blur', function () {
                $(this).parent().parent().removeClass("testhover");
            });

            this.$selectAll.off('keydown').on('keydown', function (e) {
                var key_code = e.keyCode || e.which;
                if (key_code === 9 && e.shiftKey) { // Ensure shift-tab causes lost focus from filter as with clicking away
                    if (that.options.filter && that.options.selectAll) {
                        that.$selectAll.focus();
                    }
                    else {
                        that.close();
                    }
                }
                else if (key_code === 9) { // Ensure shift-tab causes lost focus from filter as with clicking away
                    that.close();
                    that.$choice.trigger("keydown", [9]);
                }
                else if (key_code == 38) { //  Key Up
                    var all_el = that.$selectItems.filter(":visible");

                    if (that.options.filter) { // focus vào text box filter
                        that.$searchInput.focus();
                    }

                    e.preventDefault();
                }
                else if (key_code == 40) { //  Key Down
                    var all_el = that.$selectItems.filter(":visible");
                    if (all_el.length > 0) {
                        all_el.eq(0).focus();
                    }

                    e.preventDefault();
                }
                else if (key_code == 13) { //  Key Enter
                    this.click();

                    return false;
                }
            });

            this.$selectItems.off('keydown').on('keydown', function (e) {
                var key_code = e.keyCode || e.which;
                if (key_code === 9 && e.shiftKey) { // Ensure shift-tab causes lost focus from filter as with clicking away
                    if (that.options.filter && that.options.selectAll) {
                        that.$selectAll.focus();
                    }
                    else {
                        that.close();
                    }
                }
                else if (key_code === 9) { // Ensure shift-tab causes lost focus from filter as with clicking away
                    that.close();
                    that.$choice.trigger("keydown", [9]);
                }
                else if (key_code == 38) { //  Key Up
                    var all_el = that.$selectItems.filter(":visible");
                    var crr_index = all_el.index(this);

                    if (crr_index != 0) {
                        all_el.eq(crr_index - 1).focus();
                    }
                    else if (crr_index == 0 && that.options.selectAll && !that.options.single) { // Nếu multilselect + selectall thì focuss
                        that.$selectAll.focus();
                    }
                    else if (crr_index == 0 && that.options.single) { // Nếu single và filter thì focus
                        that.$searchInput.focus();
                    }

                    e.preventDefault();
                }
                else if (key_code == 40) { //  Key Down

                    var all_el = that.$selectItems.filter(":visible");
                    var crr_index = all_el.index(this);

                    if (crr_index < that.$selectItems.filter(":visible").length - 1) {
                        all_el.eq(crr_index + 1).focus();
                    }

                    e.preventDefault();
                }
                else if (key_code == 13) { //  Key Enter
                    this.click();
                    return false;
                }
            });
        },

        open: function () {
            if (this.$choice.hasClass('disabled')) {
                return;
            }
            this.options.isOpen = true;
            this.$choice.find('>div').addClass('open');
            this.$drop.show();

            // fix filter bug: no results show
            this.$selectAll.parent().show();
            this.$noResults.hide();

            // Fix #77: 'All selected' when no options
            if (this.$el.children().length === 0) {
                this.$selectAll.parent().hide();
                this.$noResults.show();
            }

            if (this.options.container) {
                var offset = this.$drop.offset();
                this.$drop.appendTo($(this.options.container));
                this.$drop.offset({ top: offset.top, left: offset.left });
            }

            //-20171208-Fix position
            if (this.options.fixPosition) {
                this.setPos();
            }

            if (this.options.filter) {
                this.$searchInput.val('');
                this.$searchInput.focus();
                this.filter();
            }
            else if (this.options.selectAll) {
                this.$selectAll.focus();
            }
            else {
                this.$selectItems.eq(0).focus();
            }

            this.options.onOpen();
        },

        close: function () {
            this.options.isOpen = false;
            this.$choice.find('>div').removeClass('open');
            this.$drop.hide();
            if (this.options.container) {
                this.$parent.append(this.$drop);
                this.$drop.css({
                    'top': 'auto',
                    'left': 'auto'
                });
            }
            this.options.onClose();
        },

        update: function (isInit) {
            var selects = this.getSelects(),
                $span = this.$choice.find('>span');
            var attrDisplay = this.options.attrDisplay || 'text'
            if (selects.length === 0) {
                $span.html(this.options.placeholder);  $span.addClass('placeholder').html(this.options.placeholder);  // addClass('placeholder') làm mờ màu chữ khi không chọn j
            } else if (this.options.allSelected && !this.options.single &&
                selects.length === this.$selectItems.length + this.$disableItems.length) {
                $span.removeClass('placeholder').html(this.options.allSelected);
            } else if (this.options.countSelected && selects.length < this.options.minimumCountSelected) {
                $span.removeClass('placeholder').html(
                    (this.options.displayValues ? selects : this.getSelects(attrDisplay))
                        .join(this.options.delimiter).replace(/</g, '&lt;').replace(/>/g, '&gt;'));
            } else if ((this.options.countSelected || this.options.etcaetera) && selects.length > this.options.minimumCountSelected) {
                if (this.options.etcaetera) {
                    $span.removeClass('placeholder').html((this.options.displayValues ? selects : this.getSelects(attrDisplay).slice(0, this.options.minimumCountSelected)).join(this.options.delimiter).replace(/</g, '&lt;').replace(/>/g, '&gt;') + '...');
                }
                else {
                    $span.removeClass('placeholder').html(this.options.countSelected
                        .replace('#', selects.length)
                        .replace('%', this.$selectItems.length + this.$disableItems.length));
                }
            } else {
                $span.removeClass('placeholder').html(
                    (this.options.displayValues ? selects : this.getSelects(attrDisplay))
                        .join(this.options.delimiter).replace(/</g, '&lt;').replace(/>/g, '&gt;'));
            }
            // set selects to select
            this.$el.val(this.getSelects());

            // add selected class to selected li
            this.$drop.find('li').removeClass('selected');
            this.$drop.find('input[' + this.selectAllName + ']:checked').each(function () {
                $(this).parents('li').first().addClass('selected');
            });
            this.$drop.find('input[' + this.selectItemName + ']:checked').each(function () {
                $(this).parents('li').first().addClass('selected');
            });

            // trigger <select> change event
            if (!isInit) {
                this.$el.trigger('change');
            }
        },

        updateSelectAll: function (Init) {
            var $items = this.$selectItems;
            if (!Init) { $items = $items.filter(':visible'); }
            this.$selectAll.prop('checked', $items.length &&
                $items.length === $items.filter(':checked').length);
            if (this.$selectAll.prop('checked')) {
                this.options.onCheckAll();
            }
        },

        updateOptGroupSelect: function () {
            var $items = this.$selectItems.filter(':visible');
            $.each(this.$selectGroups, function (i, val) {
                var group = $(val).parent().attr('data-group'),
                    $children = $items.filter('[data-group="' + group + '"]');
                $(val).prop('checked', $children.length &&
                    $children.length === $children.filter(':checked').length);
            });
        },

        //SANGDD THÊM TRẢ VỀ DẠNG CHUỖI CÁC ID KIỂU 1,2,3
        getSelectVal: function (type) {
            var that = this,
                texts = [],
                values = '';
            if (this.$drop.find('input[' + this.selectItemName + ']').length <= 0) {
                values += '-1';
            }
            else {
                var _isCheckAll = this.$drop.find('input[' + this.selectAllName + ']');
                if (_isCheckAll.prop('checked') == true && that.options.returnValues == false) {
                    // Kiểm NH sửa ngày 26/05/2015 . bản gốc :  values += '0';
                    var _item = this.$drop.find('input[' + this.selectItemName + ']').length;
                    var _item_selected = this.$drop.find('input[' + this.selectItemName + ']:checked').length;
                    if (_item_selected >= _item) {
                        values += '-2';
                    }
                    else {
                        this.$drop.find('input[' + this.selectItemName + ']:checked').each(function () {
                            texts.push($(this).parents('li').first().text());
                            values += that.options.isdatastring == false ? $(this).val() + ',' : '\'' + $(this).val() + '\',';
                        });
                    }

                    // End Kiểm NH sửa ngày 26/05/2015 
                } else {
                    var _item_selected = this.$drop.find('input[' + this.selectItemName + ']:checked').length;
                    if (_item_selected > 0) {
                        this.$drop.find('input[' + this.selectItemName + ']:checked').each(function () {
                            texts.push($(this).parents('li').first().text());
                            values += that.options.isdatastring == false ? $(this).val() + ',' : '\'' + $(this).val() + '\',';
                        });
                    }
                    else {
                        values += '-1';
                    }
                }
            }

            values = values.trim();
            if (values.indexOf(',') >= 0) {
                values = values.substring(0, values.length - 1);
            }
            return values;
        },

        getSelectValue: function (type) {
            var that = this,
                texts = [],
                values = '';
            if (this.$drop.find('input[' + this.selectItemName + ']').length <= 0) {
                values += '-1';
            }
            else {
                var _isCheckAll = this.$drop.find('input[' + this.selectAllName + ']');
                if (_isCheckAll.prop('checked') == true && that.options.returnValues == false) {
                    // Kiểm NH sửa ngày 26/05/2015 . bản gốc :  values += '0';
                    var _item = this.$drop.find('input[' + this.selectItemName + ']').length;
                    var _item_selected = this.$drop.find('input[' + this.selectItemName + ']:checked').length;
                    if (_item_selected > _item) {
                        values += '-2';
                    } else {
                        this.$drop.find('input[' + this.selectItemName + ']:checked').each(function () {
                            texts.push($(this).parents('li').first().text());
                            values += that.options.isdatastring == false ? $(this).val() + ',' : '' + $(this).val() + ',';
                        });
                    }

                    // End Kiểm NH sửa ngày 26/05/2015 
                } else {
                    var _item_selected = this.$drop.find('input[' + this.selectItemName + ']:checked').length;
                    if (_item_selected > 0) {
                        this.$drop.find('input[' + this.selectItemName + ']:checked').each(function () {
                            texts.push($(this).parents('li').first().text());
                            values += that.options.isdatastring == false ? $(this).val() + ',' : '' + $(this).val() + ',';
                        });
                    }
                    else {
                        values += '-1';
                    }
                }
            }

            values = values.trim();
            if (values.indexOf(',') >= 0) {
                values = values.substring(0, values.length - 1);
            }
            return values;
        },
        //value or text, default: 'value'
        getSelects: function (type) {
            var that = this,
                texts = [],
                values = [],
                texts_values = [],
                texts_attr = [];
            if (type && type != 'text' && type != 'all' && $('input[' + this.selectItemName + ']:checked').length <= this.options.maximumItemsToDislayText) {
                type = 'text';
            }
            this.$drop.find('input[' + this.selectItemName + ']:checked').each(function () {
                texts.push($(this).parents('li').first().text());
                if (type && type != 'text') {
                    texts_attr.push($(this).parents('li').first().attr(type) || "");
                }
                //values.push($(this).val());
                that.options.isdatastring == true && that.options.single == false ? values.push("'" + $(this).val() + "'") : values.push($(this).val());
                texts_values.push($(this).val() + " - " + $(this).parents('li').first().text().trim());
            });
           
            if (type === 'text' && this.$selectGroups.length) {
                texts = [];
                this.$selectGroups.each(function () {
                    var html = [],
                        //sangdd rem 11042015
                        // text = $.trim($(this).parent().text()),

                        text = $(this).parent().text(),
                        group = $(this).parent().data('group'),
                        $children = that.$drop.find('[' + that.selectItemName + '][data-group="' + group + '"]'),
                        $selected = $children.filter(':checked');

                    if ($selected.length === 0) {
                        return;
                    }

                    html.push('[');
                    html.push(text);
                  
                    if ($children.length > $selected.length) {
                        var list = [];
                        $selected.each(function () {
                            list.push($(this).parent().text());
                        });
                        html.push(': ' + list.join(', '));
                    }
                    html.push(']');
                    texts.push(html.join(''));
                });
            } else if (type && type != 'text' && type !== 'all' && this.$selectGroups.length) {
                texts_attr = [];
                this.$selectGroups.each(function () {
                    var html = [],
                        //sangdd rem 11042015
                        // text = $.trim($(this).parent().text()),

                        text = $(this).parent().attr(type),
                        group = $(this).parent().data('group'),
                        $children = that.$drop.find('[' + that.selectItemName + '][data-group="' + group + '"]'),
                        $selected = $children.filter(':checked');

                    if ($selected.length === 0) {
                        return;
                    }

                    html.push('[');
                    html.push(text);
                    if ($children.length > $selected.length) {
                        var list = [];
                        $selected.each(function () {
                            list.push($(this).parent().attr(type));
                        });
                        html.push(': ' + list.join(', '));
                    }
                    html.push(']');
                    texts_attr.push(html.join(''));
                });
            }

            return type === 'text' ? texts : (type === 'all' ? texts_values : (type ? texts_attr : values));
        },

        setSelects: function (values) {
            var that = this;
            this.$selectItems.prop('checked', false);
            $.each(values, function (i, value) {
                that.$selectItems.filter('[value="' + value + '"]').prop('checked', true);
            });
            this.$selectAll.prop('checked', this.$selectItems.length ===
                this.$selectItems.filter(':checked').length);
            this.update();
        },

        enable: function () {
            this.$choice.removeClass('disabled');
        },

        disable: function () {
            this.$choice.addClass('disabled');
        },

        checkAll: function () {
            this.$selectItems.prop('checked', true);
            this.$selectGroups.prop('checked', true);
            this.$selectAll.prop('checked', true);
            this.update();
            this.options.onCheckAll();
        },

        uncheckAll: function () {
            this.$selectItems.prop('checked', false);
            this.$selectGroups.prop('checked', false);
            this.$selectAll.prop('checked', false);
            this.update();
            this.options.onUncheckAll();
        },

        focus: function () {
            this.$choice.focus();
            this.options.onFocus();
        },

        blur: function () {
            this.$choice.blur();
            this.options.onBlur();
        },

        refresh: function () {
            this.init();
        },

        filter: function () {
            var that = this,
                //sangdd rem 11042915
                //  text = $.trim(this.$searchInput.val()).toLowerCase();
                text = this.$searchInput.val().normalize("NFD").toLowerCase();
            if (text.length === 0) {
                this.$selectItems.parent().parent("li").show();
                this.$disableItems.parent().parent("li").show();
                this.$selectAll.parent().parent("li").show();
                this.$noResults.hide();
                this.$selectGroups.parent().show();
            } else {
                this.$selectItems.each(function () {
                    //namlt lấy thằng li ẩn thằng li đi chứ không cần ẩn thằng label
                    var $parent = $(this).parent().parent("li");
                    $parent[$parent.text().toLowerCase().normalize("NFD").indexOf(text) < 0 ? 'hide' : 'show']();
                });
                this.$disableItems.parent().parent("li").hide();
                this.$selectGroups.each(function () {
                    var $parent = $(this).parent();
                    var group = $parent.attr('data-group'),
                        $items = that.$selectItems.filter(':visible');
                    $parent[$items.filter('[data-group="' + group + '"]').length === 0 ? 'hide' : 'show']();
                });

                //Check if no matches found
                if (this.$selectItems.filter(':visible').length) {
                    this.$selectAll.parent().parent("li").show();
                    this.$noResults.hide();
                } else {
                    this.$selectAll.parent().parent("li").hide();
                    this.$noResults.show();
                }
            }
            this.updateOptGroupSelect();
            this.updateSelectAll();
        },

        //-20171208-Set position
        setPos: function () {
            this.$drop.css({ 'position': 'fixed' });

            var top = this.$parent.offset().top, left = this.$parent.offset().left, windownScrollLeft = $(window).scrollLeft(),
                parentHeight = this.$parent.height(), parentWidth = this.$parent.width();

            this.$drop.css({
                'width': parentWidth + 'px',
                'top': (top + parentHeight) + 'px',
                'left': (left - windownScrollLeft) + 'px'
            });
        }
    };

    $.fn.multipleSelect = function () {
        var option = arguments[0],
            args = arguments,

            value,
            allowedMethods = [
                'getSelects', 'setSelects',
                'getSelectVal', // getSelectVal: Hàm viết thêm dùng cho 4T. Không select: trả -1: select all: trả 0 còn lại trả chuỗi 1, 2,.. or 'a', 'b',...
                'getSelectValue',
                'enable', 'disable',
                'checkAll', 'uncheckAll',
                'focus', 'blur',
                'refresh'
            ];

        this.each(function () {
            var $this = $(this),
                data = $this.data('multipleSelect'),
                options = $.extend({}, $.fn.multipleSelect.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new MultipleSelect($this, options);
                $this.data('multipleSelect', data);
            }

            if (typeof option === 'string') {
                if ($.inArray(option, allowedMethods) < 0) {
                    throw "Unknown method: " + option;
                }
                value = data[option](args[1]);
            } else {
                data.init();
                if (args[1]) {
                    value = data[args[1]].apply(data, [].slice.call(args, 2));
                }
            }
        });

        return value ? value : this;
    };

    $.fn.multipleSelect.defaults = {
        name: '',
        id: '',
        isOpen: false,
        placeholder: '', // Hiển thị khi không có option nào được chọn
        selectAll: true, // Có checkbox Chọn tất cả hay không
        selectAllText: 'Tất cả', // Text hiển thị của checkbox chọn tất cả
        selectAllDelimiter: ['[', ']'],
        allSelected: '-- Tất cả --', // Chọn tất cả thì hiển thị
        minimumCountSelected: 3,  // Số select được chọn <= thì sẽ hiển thị text. Ngược lại hiển thị countSelected bên dưới
        countSelected: '# trên % được chọn', // Chọn bao nhiêu cái trên tổng số bao nhiêu
        noMatchesFound: 'Không tìm thấy dữ liệu', // Filter không thấy
        multiple: false,
        multipleWidth: 80,
        single: false, // Check nhiều hay select 1 cái 
        filter: false, // Có được tìm kiếm hay không
        width: undefined, // độ rộng của combobox
        maxHeight: 250,
        container: null,
        position: 'bottom', // Vị trí của list checkbox so với combo
        fixPosition: false,
        keepOpen: false,
        blockSeparator: '',
        displayValues: false,
        delimiter: ', ', // Nối các text của option selected
        
        // ===== THÊM ĐỂ LÀM 4T =====
        returnValues: false, // returnValues khi chọn thì sẽ luôn trả ra value, kể cả check all cho hàm getSelectVal (Như cái getSelects gốc)
        isdatastring: false, // isdatastring dữ liệu là kiểu string hay number, oracle "IN" clause string: 'a', 'b',.. number 1, 2,...

        attrDisplay: 'text', // text hiển thị trên box theo attribute nào, mặc định là text của option
        addtlOptAttributesToCopy: [], // attr cần copy thêm xuống multipleselect, dùng để getSelects theo attribute nếu cần
        maximumItemsToDislayText: 1, // số select được chọn <= sẽ hiển thị text bình thường, Ngược lại hiển thị theo attrDisplay, nếu bằng 0 thì lúc nào cũng hiển thị theo attrDisplay

        styler: function () {
            return false;
        },
        textTemplate: function ($elm) {
            return $elm.text();
        },

        onOpen: function () {
            return false;
        },
        onClose: function () {
            return false;
        },
        onCheckAll: function () {
            return false;
        },
        onUncheckAll: function () {
            return false;
        },
        onFocus: function () { // Muốn focus đc thì rem vào, có j lỗi sửa lại sau
            //return false;
        },
        onBlur: function () {
            return false;
        },
        onOptgroupClick: function () {
            return false;
        },
        onClick: function () {
            return false;
        }
    };
})(jQuery);