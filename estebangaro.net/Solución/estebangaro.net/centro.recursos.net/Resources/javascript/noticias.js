$(function() {
    ajustaAltoCdrNoticias();
    ajustaAnchoNoticias();
    $(window).resize(function() {
        ajustaAltoCdrNoticias();
        ajustaAnchoNoticias();
    });
});

function ajustaAnchoNoticias() {
    // Margen derecho por bloque de noticia de 5px.
    // Ancho de icono de imagen por bloque de noticia de 60px.
    // Calculo de ancho por bloque = (anchoContenedor - (noBloques - 1) * margenDerecho) / noBloques
    if (!validaIconoMovil()) {
        var _ancho = ($('.noticiasR').outerWidth() - ((3 - 1) * 5)) / 3;
        $('.bloqueNR').each(function() {
            $(this).css({
                'width': _ancho + 'px'
            });
        });
    }
}

function ajustaAltoCdrNoticias() {
    if (!validaIconoMovil()) {
        var _alto = $('#contenedorMenu').innerHeight();
        var _altoVentana = $(window).height();
        var _altoSeccionNoticias = (_altoVentana) < 659 ? 659 : (_altoVentana - _alto);
        $('#seccion2Principal > .Contenido').css({
            'top': (_altoVentana < 659 ? 0 : _alto) + 'px',
            'height': _altoSeccionNoticias + 'px'
        });
        if (_altoVentana < 659)
            $('#seccion2Principal').css({
                'height': 659 + 'px'
            });
    }
}

function validaIconoMovil() {
    var _valor = $('.iconoMenu').css('display');
    return _valor == 'inline' || _valor == 'block';
}