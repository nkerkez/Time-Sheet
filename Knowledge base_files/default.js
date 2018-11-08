// Init Detectizr script
Detectizr.detect({detectScreen:false});

$(document).ready(function () {

	//Back to top button
	$('.back-to-top').click(function() {
		$('html, body').animate({ scrollTop: 0 }, 800);
	});

    //toggle mobile navigation
    $('.ico.hamburger').click(function () {
        var $mobileNav = $('.sidebar-nav'),
			$btn = $('.ico.hamburger');

        if ($mobileNav.hasClass('open')) {
            $mobileNav.removeClass('open')
		 		.slideUp();
            $btn.removeClass('open');
        } else {
            $mobileNav.addClass('open')
		  		.slideDown();
            $btn.addClass('open');
        }
    });

    // sidebar nav
    $('ul.sidebar-nav li a i').click(function () {
        var $this = $(this),
            hasNextLevel = $this.parent().next(),
            $LiParent = $this.parent().parent(),
            $LiSiblings = $LiParent.siblings();

        if (!($LiParent.hasClass('expanded')) && (hasNextLevel.length > 0)) {
            $LiParent
                .addClass('expanded')
                .children('ul.sub-nav')
                .slideDown();
        } else {
            $LiParent.removeClass('expanded')
                .find('ul.sub-nav').slideUp();
        }
        return false;
    });

	$('.anchors-list a').on('click', function (event) {
		var $levels = $(this).parentsUntil('.anchors-list');
		var numbers = [];
		var $scrollTo;
		$levels.each(function (index) {
			var $listElement = $(this);
			if($listElement.is('li')) {
				numbers.push($listElement.index() + 1);
			}
		});
		numbers = numbers.reverse().join('.');

		if($('.description').find(':header:contains(' + numbers + ')')) {
			$scrollTo = $('.description').find(':header:contains(' + numbers + ')');
		}

		$('.description').find('h4:contains(' + numbers + ')');
		$('html, body').animate({
			scrollTop: $scrollTo.offset().top
		}, 1200);
		event.preventDefault();
	});

    //search
    //search.initSearch('input[id$="tboxSearchQuery"]', 'a[id$="lnkSearch"]');
    search.initSearch('input[id$="tboxHeaderSearch"]', '#lnkHeaderSearch');
    search.initSearch('input[id$="tboxHeaderSearch"]', '#lnkHeaderSearchButton');
    //search.initSearch('input[id$="tboxHeaderMobileSearch"]', '#lnkHeaderMobileSearch');
    //search.initSearch('input[id$="tboxHeaderSearchResult"]', '#lnkHeaderSearchResult');

});

$(window).scroll(function(){

	if($(window).scrollTop() >= 500){
		$('.back-to-top').addClass('visible');
	} else {
		$('.back-to-top').removeClass('visible');
	}

});
