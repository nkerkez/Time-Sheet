var search = (function () {
    return {
        initSearch: function (tboxSelector, linkSelector) {
            var $tboxSearch = $(tboxSelector),
                $link = $(linkSelector);

            if ($tboxSearch.length > 0 && $link.length > 0) {
                $link.click(function () {
                    var $val = $tboxSearch.val();

                    if ($val != null) {
                        if (Modernizr.input.placeholder) { //if it is not lower version of IE (where trim() is not supported)
                            $val = $val.trim();
                        }
                        if ($val != '' && $val != $tboxSearch.attr('placeholder') && searchUrl != null) {
                            window.location.href = searchUrl + "?q=" + escape($val);
                        }
                    }
                    return false;
                });

                $tboxSearch.keypress(function (event) {
                    var $self = $(this),
                        $val = $self.val();

                    if (event.keyCode == 13 && $val != null) {
                        event.stopPropagation();
                        if (Modernizr.input.placeholder) { //if it is not lower version of IE (where trim() is not supported)
                            $val = $val.trim();
                        }

                        if ($val != '' && $val != $self.attr('placeholder')) {
                            $link.click();
                        }

                        return false;
                    } else if (event.keyCode == 13) {
                        return false;
                    }

                    return true;
                });
            }
        }
    }
}());