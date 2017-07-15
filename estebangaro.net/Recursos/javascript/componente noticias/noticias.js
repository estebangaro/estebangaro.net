$(function() {
    ajustaAltoCdrNoticias();
    ajustaAnchoNoticias();
});

function ajustaAnchoNoticias() {
    // Margen derecho por bloque de noticia de 5px.
    // Ancho de icono de imagen por bloque de noticia de 60px.
    // Calculo de ancho por bloque = (anchoContenedor - (noBloques - 1) * margenDerecho) / noBloques
    var _ancho = ($('.noticiasR').outerWidth() - ((3 - 1) * 5)) / 3;
    $('.bloqueNR').each(function() {
        $(this).css({
            'width': _ancho + 'px'
        });
    });
}

function ajustaAltoCdrNoticias() {
    var _alto = $('#contenedorMenu').innerHeight();
    var _altoVentana = $(window).height();
    var _altoSeccionNoticias = _altoVentana - _alto;
    $('#seccion2Principal > .Contenido').css({ 'top': _alto + 'px', 'height': _altoSeccionNoticias + 'px' });
}