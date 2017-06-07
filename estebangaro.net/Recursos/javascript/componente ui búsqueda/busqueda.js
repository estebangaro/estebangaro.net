$(function() {
    configBtnBusquedaCUI();
});

function configBtnBusquedaCUI() {
    // Configuración de Botón de búsqueda (añadiendo manejo de evento).
    $('#ctdrCampoBusqueda img').click(function() {
        if (validaEjecucionBtnBusqueda()) {
            _componenteUI_btnBusqueda = false;
            var _ancho = $('#ctdrCampoBusqueda input').css('width');
            if (_ancho != '300px') {
                animaCampoBusqueda(true);
                $('#ctdrCampoBusqueda input').focus();
            } else {
                // Lógica de búsqueda
            }
        }
    });
    // Configuración de Campo de búsqueda (añadiendo manejo de evento).
    $('#ctdrCampoBusqueda input').click(function() {
        if (validaEjecucionBtnBusqueda()) {
            _componenteUI_btnBusqueda = false;
        }
    });
}

function validaEjecucionBtnBusqueda() {
    var _esCursor = $('#ctdrCampoBusqueda img').css('cursor');
    return $('#ctdrCampoBusqueda img').css('cursor') == 'pointer';
}

function animaCampoBusqueda(estado) {
    $('#ctdrCampoBusqueda input').css({
        'width': !estado ? '0' : '280px',
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
    }
}

function ajustaBtnBusqueda() {
    if (validaEjecucionBtnBusqueda()) {
        if (_componenteUI_btnBusqueda)
            animaCampoBusqueda(false);
        else
            _componenteUI_btnBusqueda = true;
    }
}