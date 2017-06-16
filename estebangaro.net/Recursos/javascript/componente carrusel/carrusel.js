var _carruselconfiguracion;

$(function() {
    cargaAvisos();
    estableceBotonesDesp();
    enlazarEvts();
});

function obtenDesplazamientoHorizontal(indice) {
    return (_carruselconfiguracion.cantidadElementos -
        (indice != undefined ? indice :
            _carruselconfiguracion.indice)) * -100;
}

function validaDesplazamientoInactivo(estado) {
    var _inactivo = _carruselconfiguracion.activo == 'cont_1' ? 'cont_2' : 'cont_1';
    if (estado) {
        if (_carruselconfiguracion.indice == 0) {
            configuraContenido(_inactivo, true, '100%', '0');
        } else if (_carruselconfiguracion.indice == _carruselconfiguracion.cantidadElementos + 1) {
            configuraContenido(_inactivo, true, (_carruselconfiguracion.cantidadElementos * -100) + '%',
                obtenDesplazamientoHorizontal(1) + '%');
        }
        return true;
    } else if (_carruselconfiguracion.indice == 0 ||
        _carruselconfiguracion.indice == _carruselconfiguracion.cantidadElementos + 1) {
        configuraContenido(_carruselconfiguracion.activo, false);
        _carruselconfiguracion.activo = _inactivo;
        _carruselconfiguracion.indice = _carruselconfiguracion.indice == 0 ?
            _carruselconfiguracion.cantidadElementos : 1;
    }
}

function configuraContenido(contenidoC, estado, desplazamiento, anima) {
    if (estado) {
        $('#' + contenidoC).css({
            'left': desplazamiento, // obtenDesplazamientoHorizontal(_carruselconfiguracion.cantidadElementos + 1)
            'display': 'block'
        }).attr('class', 'contenidoCarrusel animaCarrusel');
    } else
        $('#' + contenidoC).css({
            'display': 'none'
        }).removeClass('animaCarrusel');
}

function desplazaCarrusel(manejadorFlecha) {
    var _desplazaI;
    var _inactivo = _carruselconfiguracion.activo == 'cont_1' ? 'cont_2' : 'cont_1';
    if (manejadorFlecha != undefined)
        _desplazaI = manejadorFlecha(true); // Configuración y desplazamiento de contenedor espejo inactivo.
    var _posicion = obtenDesplazamientoHorizontal() + '%';
    $('#' + _carruselconfiguracion.activo).css({
        'left': _posicion
    });
    if (_desplazaI) {
        setTimeout(function() {
            $('#' + _inactivo).css({
                'left': _carruselconfiguracion.indice == 0 ? '0' : obtenDesplazamientoHorizontal(1) + '%'
            });
        }, 10);
    }
    if (manejadorFlecha != undefined)
        setTimeout(function() {
            manejadorFlecha(false); // Actualización de contenedor espejo activo.
        }, 1000);
}

function cargaAvisos() {
    _carruselconfiguracion = {
        indice: 1, // índice de aviso inicial.
        cantidadElementos: 4, // cantidad de elementos "aviso" a visualizar.
        activo: 'cont_1'
    };
}

function estableceBotonesDesp() {

}

function obtenPosicionBtn(botonOrigen) {
    return parseInt(botonOrigen.attr('alt'));
}

function enlazarEvts() {
    $('#contenidoBtns > img').click(function() {
        _carruselconfiguracion.indice = obtenPosicionBtn($(this));
        desplazaCarrusel();
    });
    $('#contenidoFlechas>img').click(function() {
        _carruselconfiguracion.indice += obtenPosicionBtn($(this));
        desplazaCarrusel(validaDesplazamientoInactivo);
    });
}