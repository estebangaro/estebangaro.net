$(function() {
    configBtnBusquedaCUI();
});

function configBtnBusquedaCUI() {
    $('#ctdrCampoBusqueda img').click(function(e) {
        e.stopPropagation();
        var _ancho = $('#ctdrCampoBusqueda input').css('width');
        
        if (validaEjecucionBtnBusqueda()) {
            if (_ancho == '0px' || _ancho == '0' || _ancho == '2px') {
                animaCampoBusqueda(true);
                $('#ctdrCampoBusqueda input').focus();
            }
        }

        if(_ancho != '0px' && _ancho != '0' && _ancho != '2px' &&
            $('#ctdrCampoBusqueda input').val() != undefined && 
                $('#ctdrCampoBusqueda input').val() != '') {
                ConsultaArticulos($('#ctdrCampoBusqueda input').val());
            }
    });
    
    $('#ctdrCampoBusqueda input').click(function(e) {
        if (validaEjecucionBtnBusqueda()) {
            e.stopPropagation();
        }
    }).keydown(function(e){
        if(e.which == 13){
            $('#ctdrCampoBusqueda img').click();
        }
    });
}

function ConsultaArticulos(busqueda){
    mostrarEsperaPopUpG(true, '#ctdrCampoBusqueda');
    $.ajax({
        type: 'GaroArticulos',
        url: '/Articulo/Busqueda/' + busqueda,
        contentType: 'application/json; charset=utf-8',
        success: function(articulos){
            mostrarEsperaPopUpG(false, '#ctdrCampoBusqueda');
            mostrarPopupR(articulos, busqueda);
        },
        error: function(error){
            mostrarEsperaPopUpG(false, '#ctdrCampoBusqueda');
            alert('Ha fallado la consulta de articulos');
        }
    });
}

function validaEjecucionBtnBusqueda() {
    var _esCursor = $('#ctdrCampoBusqueda img').css('cursor');
    return $('#ctdrCampoBusqueda img').css('cursor') == 'pointer';
}

function animaCampoBusqueda(estado) {
    $('#ctdrCampoBusqueda input').css({
        'width': !estado ? '0' : '250px',
        'border-bottom': !estado ? '0' : '0.5px solid whitesmoke'
    });
    if (estado) {
        $('#agrupadorOpciones').css({
            'opacity': '0',
            'visibility': 'hidden'
        });
    } else {
        $('#agrupadorOpciones').css({
            'opacity': '1',
            'visibility': 'visible'
        });
        $('#ctdrCampoBusqueda input').val('');
    }
}

function ajustaBtnBusqueda() {
    if (validaEjecucionBtnBusqueda()) {
        animaCampoBusqueda(false);
    }
}