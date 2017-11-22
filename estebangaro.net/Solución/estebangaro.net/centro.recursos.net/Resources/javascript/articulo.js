$(function(){
    ajustaPaddingSeccionArt();
    $(window).resize(function(){
        ajustaPaddingSeccionArt();
    });
});

function ajustaPaddingSeccionArt(){
    var _alto = $('#contenedorMenu').innerHeight();
    $('#SeccionArticulo').css('padding-top', _alto + 'px');
}