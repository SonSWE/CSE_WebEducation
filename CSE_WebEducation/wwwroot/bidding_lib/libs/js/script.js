

$(document).ready(function () {

    //nut scroll top
    $("#back-to-top").click(function () {
        $("html, body").animate({ scrollTop: 0 }, "slow");
        return false;
    });
    $(window).scroll(function () {
        if ($(window).scrollTop() >= 300) {
            $('#back-to-top').show();
        }
        else {
            $('#back-to-top').hide();
        }
    });

    //js mutiple
    $('.sl-multiple').multipleSelect({
        width: '100%',
        single: false,
        filter: true,
        placeholder: "Chọn giá trị",
    }).multipleSelect('refresh');

    //js accordion danh sach
    $('.category-investor').click(function () {
        $(this).next().slideToggle();
        $(this).toggleClass('active');
    })

    //$('.title-process').click(function(){
    //    $(this).nextAll('.body-process').slideToggle();
    //    $(this).toggleClass('active');
    //})

    //js tim kiem nang cao
    //$('.animated-checkbox a').on('click',function(){
    //    $(this).toggleClass('checked');
    //    $(this).parent().parent().find('.item-cate').toggleClass('selected');
    //    $(".box-search.box-search-active").css('display','flex');
    //    let hClass = $(this).hasClass('checked');
    //    if(hClass){
    //        $(".box-search.box-search-active").slideDown();
    //    }
    //    else{
    //        $(".box-search.box-search-active").slideUp();
    //    }
    //});

    //js accordion bao cao
    //$('.head-report').click(function(){
    //    $('.child-list').slideUp();
    //    $('.head-report').removeClass('active');

    //    if (!$(this).next('.child-list').is(':visible')){
    //        $(this).next('.child-list').slideDown();
    //        $(this).addClass('active');
    //    }
    //})

    //focus calender input show outline
    //const el = document.querySelector('.calendar');

    //el.addEventListener('focusin', () => {
    //    el.classList.add('border-ac');
    //});

    //el.addEventListener('focusout', () => {
    //    el.classList.remove('border-ac');
    //});
    //end

    //Menu toggle bar
    $('.toggle').click(function () {
        $('.navigation .navigation').toggleClass('active');
        $('.overlay').toggleClass('active');
        $('.block-dropdown').slideUp();
        $('.has-dropdown > a').removeClass('rotate');
    });

    $('.overlay').click(function () {
        $('.navigation .navigation').toggleClass('active');
        $('.overlay').toggleClass('active');
        $('.block-dropdown').slideUp();
        $('.has-dropdown > a').removeClass('rotate');
        $('.toggle').toggleClass('change');
    });

    //Drodown block-dropdown
    //$('.block-dropdown').hide();
    if (innerWidth < 992) {
        //Dropdown navigation
        $('.has-dropdown > a').click(function () {

            $('.has-dropdown > a').removeClass('rotate');

            $('.block-dropdown').slideUp();

            if (!$(this).next('.block-dropdown').is(':visible')) {
                $(this).next('.block-dropdown').slideDown();
                $(this).addClass('rotate');
            }
        });
    }


    //js dropdown news

    $('.txt-news').click(function () {
        $('.dropdown-news').slideToggle();
    })

    //js datepicker month year
    //var startDate = new Date();
    //var fechaFin = new Date();
    //var FromEndDate = new Date();
    //var ToEndDate = new Date();
    //$('.sl-month-year').datepicker({
    //    autoclose: true,
    //    minViewMode: 1,
    //    format: 'mm/yyyy'
    //}).on('changeDate', function (selected) {
    //    startDate = new Date(selected.date.valueOf());
    //    startDate.setDate(startDate.getDate(new Date(selected.date.valueOf())));
    //    $('.to').datepicker('setStartDate', startDate);
    //});
});

//js tinh vi tri của tooltip

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

$(document).ready(function (event) {

    // $('.showhide-pass div').click(function () {
    //     var x = document.getElementById("password-login");
    //     $('.showhide-pass div').toggleClass('active');
    //     if (x.type === "password") {
    //         x.type = "text";
    //     } else {
    //         x.type = "password";
    //     }
    // });
    // // begin click outside
    //
    // $(".btn-category").click(function (event) {
    //    $('.navigation').slideToggle(300,'swing');
    //    $(this).toggleClass('active');
    //     event.stopPropagation();
    // });
    // const $menu = $('.header-home');
    // $(document).mouseup(e => {
    //     if (!$menu.is(e.target)
    //         && $menu.has(e.target).length === 0)
    //     {
    //         $('.btn-category').removeClass('active');
    //         $('.navigation').slideUp(300,'swing');
    //     }
    // });
    // // end click outside
    //
    // function readURL(input) {
    //     if (input.files && input.files[0]) {
    //         var reader = new FileReader();
    //         reader.onload = function(e) {
    //             $('#imagePreview').css('background-image', 'url('+e.target.result +')').hide().fadeIn(650);
    //         };
    //         reader.readAsDataURL(input.files[0]);
    //     }
    // }
    // $("#imageUpload").change(function() {
    //     readURL(this);
    // });
    // $('#text-cmt').keyup(function (e) {
    //
    //    if(e.target.value && e.target.value.length > 0){
    //         $('.fly-cmt svg path').attr('fill','#4782FD');
    //    }
    //    else {
    //        $('.fly-cmt svg path').attr('fill','#616770');
    //        // alert('a')
    //
    //    }
    //
    // });


   


});


var mangso = ['không', 'một', 'hai', 'ba', 'bốn', 'năm', 'sáu', 'bảy', 'tám', 'chín'];
function dochangchuc(so, daydu) {
    var chuoi = "";
    chuc = Math.floor(so / 10);
    donvi = so % 10;
    if (chuc > 1) {
        chuoi = " " + mangso[chuc] + " mươi";
        if (donvi == 1) {
            chuoi += " mốt";
        }
    } else if (chuc == 1) {
        chuoi = " mười";
        if (donvi == 1) {
            chuoi += " một";
        }
    } else if (daydu && donvi > 0) {
        chuoi = " lẻ";
    }
    if (donvi == 5 && chuc >= 1) {
        chuoi += " lăm";
    } else if (donvi > 1 || (donvi == 1 && chuc == 0)) {
        chuoi += " " + mangso[donvi];
    }
    return chuoi;
}
function docblock(so, daydu) {
    var chuoi = "";
    tram = Math.floor(so / 100);
    so = so % 100;
    if (daydu || tram > 0) {
        chuoi = " " + mangso[tram] + " trăm";
        chuoi += dochangchuc(so, true);
    } else {
        chuoi = dochangchuc(so, false);
    }
    return chuoi;
}
function dochangtrieu(so, daydu) {
    var chuoi = "";
    trieu = Math.floor(so / 1000000);
    so = so % 1000000;
    if (trieu > 0) {
        chuoi = docblock(trieu, daydu) + " triệu";
        daydu = true;
    }
    nghin = Math.floor(so / 1000);
    so = so % 1000;
    if (nghin > 0) {
        chuoi += docblock(nghin, daydu) + " nghìn";
        daydu = true;
    }
    if (so > 0) {
        chuoi += docblock(so, daydu);
    }
    return chuoi;
}
function docso(so) {
    if (so == 0) return mangso[0];
    var chuoi = "", hauto = "";
    do {
        ty = so % 1000000000;
        so = Math.floor(so / 1000000000);
        if (so > 0) {
            chuoi = dochangtrieu(ty, true) + hauto + chuoi;
        } else {
            chuoi = dochangtrieu(ty, false) + hauto + chuoi;
        }
        hauto = " tỷ";
    } while (so > 0);
    var strReturn = chuoi.trim().charAt(0).toUpperCase() + chuoi.trim().slice(1);
    return strReturn;
}
function docsoam(so) {
    var soAm = "";
    var checkSoAm = false;
    if (so.charAt(0) == "-") {
        so = so.replace('-', '');
        checkSoAm = true;
    }

    if (so == 0) return mangso[0];
    var chuoi = "", hauto = "";
    do {
        ty = so % 1000000000;
        so = Math.floor(so / 1000000000);
        if (so > 0) {
            chuoi = dochangtrieu(ty, true) + hauto + chuoi;
        } else {
            chuoi = dochangtrieu(ty, false) + hauto + chuoi;
        }
        hauto = " tỷ";
    } while (so > 0);
    var strReturn = chuoi.trim().charAt(0).toUpperCase() + chuoi.trim().slice(1);
    if (checkSoAm == true) {
        strReturn = "Âm " + chuoi.trim();
    }
    return strReturn;
}
function escapeSpecialChars(str) {
    return str.replace(/[\u0000-\u0019]+/g, "");
}

//  check giờ định dạng HH:mm
// type = am/pm
function Check_fomat_Hours(p_name, p_hour, p_type) {
    try {
        var _arr_time = p_hour.split(":");
        if (_arr_time.length != 2) {
            showError(p_name + " không đúng định dạng!");
            return false;
        }
        for (var i in _arr_time) {
            var _time = _arr_time[i];

            if (_time.length != 2) {
                showError(p_name + " không đúng định dạng!");
                return false;
            }

            if (i == 0) {
                if (parseFloat(_time) > 23 || parseFloat(_time) < 0) {
                    showError("Số giờ tại " + p_name + " phải trong khoảng từ 0h-23h!");
                    return false;
                }

                if (p_type == "AM") {
                    if (parseFloat(_time) > 12) {
                        showError("Số giờ tại " + p_name + " phải trong khoảng từ 0h-11h59 !");
                        return false;
                    }
                }
                else if (p_type == "PM") {
                    if (parseFloat(_time) < 12) {
                        showError("Số giờ tại " + p_name + " phải trong khoảng từ 12h-23h59 !");
                        return false;
                    }
                }
            }
            else if (i == 1) {
                if (parseFloat(_time) > 59 || parseFloat(_time) < 0) {
                    showError("Phút trong khoảng " + p_name + " phải trong khoảng từ 00-59!");
                    return false;
                }
            }
        }

        return true;

    } catch (e) {
        alert(e);
        return false;
    }
}
