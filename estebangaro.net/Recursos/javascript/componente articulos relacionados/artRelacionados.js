$(function(){
    $('#dialogMsjArtRelG img').click(function(){
        if(obtenNombreURI($(this).attr('src')).includes('flecha')){
            $(this).parents('#dialogMsjArtRelG').css('left', '0');
            $('#dialogMsjArtRelG > .cerrar').show();
            $(this).parent().hide();
        }
        else{
            $(this).parents('#dialogMsjArtRelG').css('left', '-100%');
            $('#dialogMsjArtRelG > .Flecha').show();
            $(this).hide();
        }
    });

    centraPopupR();

    function obtenNombreURI(uri) {
        return uri.substring(uri.lastIndexOf('/') + 1);
    }

    function centraPopupR(){
        var alto = $('#dialogMsjArtRelGContenedor').outerHeight() / 2;
        $('#dialogMsjArtRelGContenedor').css({
            'margin-top': (-1 * alto) + 'px'
        });
    }
});