$(function() {
    // Margen derecho por bloque de noticia de 5px.
    // Ancho de icono de imagen por bloque de noticia de 60px.
    // Calculo de ancho por bloque = (anchoContenedor - (noBloques - 1) * margenDerecho) / noBloques
    var _ancho = ($('.noticiasR').outerWidth() - ((3 - 1) * 5)) / 3;
    $('.bloqueNR').each(function() {
        $(this).css({
            'width': _ancho + 'px'
        });
    });
});