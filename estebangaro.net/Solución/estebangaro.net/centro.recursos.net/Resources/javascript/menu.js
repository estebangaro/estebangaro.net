$(function() {
    $(window).resize(function() {
        $('.iconoMenu').attr('src', '/Resources/imagenes/menu/icono_menu_2.png');
    });
    $('#contenedorMenu nav').click(function() {
        muestraOpcion($(this));
    });
    $('#contenedorMenu li').click(function(e) {
        e.stopPropagation();
        muestraOpcion($(this));
    });
    $('.iconoMenu').click(function() {
        if (muestraOpciones()) {
            $('#agrupadorOpciones nav').css({
                'visibility': 'visible',
                'opacity': '1'
            });
            $('#agrupadorOpciones>div').css({ 'background-color': 'rgba(2, 2, 2, 0.8)', display: 'block' });
            $(this).attr('src', '/Resources/imagenes/menu/cerrar.png');
        } else {
            $('#agrupadorOpciones nav').css({
                'visibility': 'hidden',
                'opacity': '0'
            });
            $('#agrupadorOpciones').find('ul').css({
                'visibility': 'hidden',
                'max-height': '0'
            });
            $('#agrupadorOpciones>div').css({ 'background-color': 'transparent', display: 'none' });
            $(this).attr('src', '/Resources/imagenes/menu/icono_menu_2.png');
        }
    });
});

function muestraOpciones() {
    return $('#agrupadorOpciones nav').css('visibility') == 'hidden';
}

function muestraOpcion(dePadre) {
    if (validaIconoMovil()) {
        if (validaOpcion(dePadre)) {
            dePadre.children('ul').css({
                'visibility': 'visible',
                'max-height': 'inherit'
            });
        } else {
            dePadre.find('ul').css({
                'visibility': 'hidden',
                'max-height': '0'
            });
        }
    }
}

function validaOpcion(dePadre) {
    var _dato = dePadre.children('ul').css('height');
    return _dato == '0' || _dato == '0px';
}

function validaIconoMovil() {
    var _valor = $('.iconoMenu').css('display');
    return _valor == 'inline' || _valor == 'block';
}