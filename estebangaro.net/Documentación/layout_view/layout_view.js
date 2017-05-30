var _componenteUI_btnBusqueda = true; // Bandera de componente ui búsqueda.

$(function() {
    $(document).click(function() {
        establecePredeterminado();
    });
    $(window).resize(function() {
        $('*').attr('style', '');
    });
});

function establecePredeterminado() {
    ajustaBtnBusqueda(); // Determina la lógica de ajuste de componente ui búsqueda.
}