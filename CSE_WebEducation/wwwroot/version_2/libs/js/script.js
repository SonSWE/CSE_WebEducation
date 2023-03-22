$( document ).ready(function() {

    $('.show-utitlity').click(function () {
        $('.block-utilities').toggle();
        $(this).toggleClass('active');
    });
    $('.name-user').click(function () {
        $('.block-utilities').toggle();
        $('.show-utitlity').toggleClass('active');
    });
    $('.box-notify').click(function () {
        $('.list-notify').slideToggle();
    });

    


    //js menubar
    $(".menuchild").hide();
    $(".has-dropdown.active .menuchild").show();
    $("header.active .has-dropdown.active .menuchild").hide();
    $(".has-dropdown > a").click(function(){
        $(".has-dropdown").removeClass('rotate-down');

        $(".menuchild").slideUp();
        if (!$(this).next(".menuchild").is(":visible"))
        {
            $(this).next(".menuchild").slideDown();
            $(this).parent().addClass('rotate-down');
        }
    });

    $(".panel-left").resizable({
        handleSelector: ".splitter",
        resizeHeight: false,
        
    });

    //js resize table 
    $(function () {
        var thHeight = $("table#demo-table th:first").height();
        $("table#demo-table th").resizable({
            handles: "e",
            minHeight: thHeight,
            maxHeight: thHeight,
            minWidth: 40,
            resize: function (event, ui) {
                var sizerID = "#" + $(event.target).attr("id") + "-sizer";
                $(sizerID).width(ui.size.width);
            }
        });
    });


    //$(".has-dropdown ul li a").click(function () {
    //    $(this).closest('.menuchild').preventDefault();
    //});




    //js tinh vi tri menu con
    //$('header.active .menu-parent > li').off('mouseover').on('mouseover', function () {
    //    var _windowScroll = $(window).scrollTop();

    //    var _offset = $(this).offset();
    //    $('.menuchild', $(this)).css({"display":"block", "top": (_offset.top - _windowScroll) + "px", "left": _offset.left + "px" })
    //}).off('mouseout').on('mouseout', function () {
    //    $('.menuchild', $(this)).css({ "top": "-1000px", "left": "-1000px","display":"none" })
    //});



    $('.icon-close-ip').click(function () {
        $(this).prev('.text-null').val("");
    });

    
    //js input file

    //$('input[type="file"]').each(function() {
    //    // get label text
    //    var label = $(this).parents('.form-group').find('label').text();
    //    label = (label) ? label : 'Upload File';

    //    // wrap the file input
    //    $(this).wrap('<div class="input-file"></div>');
    //    // display label
    //    $(this).before('<span class="btn">'+label+'</span>');
    //    // we will display selected file here
    //    $(this).before('<span class="file-selected"></span>');

    //    // file input change listener
    //    $(this).change(function(e){
    //        // Get this file input value
    //        var val = $(this).val();

    //        // Let's only show filename.
    //        // By default file input value is a fullpath, something like
    //        // C:\fakepath\Nuriootpa1.jpg depending on your browser.
    //        var filename = val.replace(/^.*[\\\/]/, '');

    //        // Display the filename
    //        $(this).siblings('.file-selected').text(filename);
    //    });
    //});

    //// Open the file browser when our custom button is clicked.
    //$('.input-file .btn').click(function() {
    //    $(this).siblings('input[type="file"]').trigger('click');
    //});


    



    //nut scroll top

    $("#back-to-top").click(function () {
        $("html, body").animate({scrollTop : 0},"slow");
        return false;
    });
    $(window).scroll(function () {
        if ($(window).scrollTop() >=300) {
            $('#back-to-top').show();
        }
        else {
            $('#back-to-top').hide();
        }
    });

    // //script sidebar
    //
    // $(".menucate-lv2").hide();
    // $(".rotate-down .menucate-lv2").show();
    // $(".menucate-lv1 > li > a").click(function(){
    //     $(".menucate-lv1 > li.hasdrop-cate").removeClass('rotate-down');
    //
    //     $(".menucate-lv2").slideUp();
    //     if(!$(this).next(".menucate-lv2").is(":visible"))
    //     {
    //         $(this).next(".menucate-lv2").slideDown();
    //         $(this).parent().addClass('rotate-down');
    //     }
    // });
    //
    // //js nut bars
    // $('.bars-close').click(function () {
    //     $('.width-resize').toggleClass('change-width');
    //     $('.col-vsd-60').toggleClass('change-width');
    // });
    // $('.bars-close').hover(function () {
    //     $('.heading-diary').toggleClass('change-color');
    // });




    // custom datepicker

    jQuery('.datepicker-vsd').datetimepicker({
        timepicker:false,
        format: 'd/m/Y'
    });
    jQuery('.datetimepicker-vsd').datetimepicker({
        format: 'd/m/Y H:m'
    });
    $.datetimepicker.setLocale('vi');

 //   $('input[name="daterange"]').daterangepicker({
 //       opens: 'left',
 //       "autoApply": true
 //   },
 //function(start, end, label) {
 //       console.log("A new date selection was made: " + start.format('YYYY-MM-DD') + ' to ' + end.format('YYYY-MM-DD'));
 //   });


});
//function vtuti() {
//    $('.btn-function').off('mouseover').on('click', function () {
//        var _windowScroll = $(window).scrollTop();
//        var _windowLeft = $(window).scrollLeft();
//        // tinh vị trí của của item-list
//        var _offset = $(this).offset();
//        $('.name-function-fix', $(this)).css({ "top": (_offset.top - _windowScroll) + "px", "left": (_offset.left - _windowLeft) + "px" })
//    }).off('mouseout').on('mouseout', function () {
//        $('.name-function-fix', $(this)).css({ "top": "-1000px", "left": "-1000px" })
//    });
//}


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

//begin: tinh vị trí reject noti

function inittitlefuctionNoti() {
    $('.noti-reject').off('mouseover').on('mouseover', function () {
        var _windowScroll = $(window).scrollTop();
        var _windowLeft = $(window).scrollLeft();
        var _offset = $(this).offset();
        $('.content-reject', $(this)).css({ "top": (_offset.top - _windowScroll) + "px", "left": (_offset.left - _windowLeft) + "px" })
    }).off('mouseout').on('mouseout', function () {
        $('.content-reject', $(this)).css({ "top": "-1000px", "left": "-1000px" })
    })
    if (innerWidth < 991) {
        $('.noti-reject').bind('touchstart', function (e) {
            e.preventDefault();
            var _windowScroll = $(window).scrollTop();
            var _windowLeft = $(window).scrollLeft();
            var _offset = $(this).offset();
            $('.content-reject', $(this)).css({ "top": (_offset.top - _windowScroll) + "px", "left": (_offset.left - _windowLeft) + "px" })
        }).bind('touchend', function (e) {
            event.stopPropagation();
            event.preventDefault();
            $('.content-reject', $(this)).css({ "top": "-1000px", "left": "-1000px" })
        });

    }
}

//end: tinh vị trí reject noti