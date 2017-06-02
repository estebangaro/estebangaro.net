$(function(){
    $('#contenedorMenu nav').click(function(){
        muestraOpcion($(this));
    });
    $('#contenedorMenu li').click(function(e){
        e.stopPropagation();
        muestraOpcion($(this));
    });
    $('.iconoMenu').click(function(){
        if(muestraOpciones()){
            $('#agrupadorOpciones nav').css('display', 'block');
            $(this).attr('src', '../../imagenes/cerrar.png');
        }else{
            $('#agrupadorOpciones nav').css('display', 'none');
            $('#agrupadorOpciones').find('ul').css({
            'visibility':'hidden',
            'max-height':'0'
        });
        $(this).attr('src', '../../imagenes/icono_menu_2.png');
        }
    });
});

function muestraOpciones(){
    return $('#agrupadorOpciones nav').css('display') == 'none';
}

function muestraOpcion(dePadre){
    if(validaOpcion(dePadre)){
    dePadre.children('ul').css({
            'visibility':'visible',
            'max-height':'inherit'
        });
    }else{
        dePadre.find('ul').css({
            'visibility':'hidden',
            'max-height':'0'
        });
    }
}

function validaOpcion(dePadre){
    var _dato = dePadre.children('ul').css('height');
    return _dato == '0' || _dato == '0px';
}