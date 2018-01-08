$(function(){
    $('#dialogMsjArtRelG img').click(function(){
        if(obtenNombreURI($(this).attr('src')).includes('flecha')){
            $(this).parents('#dialogMsjArtRelG').css('left', '0');
            $('#dialogMsjArtRelG > .cerrar').show();
            $(this).parent().hide();
        }
        else{
            var anchoCtdr = $(this).parents('#dialogMsjArtRelG').outerWidth() * -1;
            $(this).parents('#dialogMsjArtRelG').css('left', anchoCtdr + 'px');
            $('#dialogMsjArtRelG > .Flecha').show();
            $(this).hide();
        }
    });

    centraPopupR();
    $(window).resize(function(){
        centraPopupR();
    });

    function obtenNombreURI(uri) {
        return uri.substring(uri.lastIndexOf('/') + 1);
    }

    function centraPopupR(){
        var alto = $('#dialogMsjArtRelG').outerHeight() / 2;
        $('#dialogMsjArtRelG').css({
            'margin-top': (-1 * alto) + 'px'
        });
    }
});