var _carruselconfiguracion;

$(function() {
    cargaAvisos();
    estableceBotonesDesp();
    enlazarEvts();
});

function obtenDesplazamientoHorizontal() {
    return (_carruselconfiguracion.cantidadElementos - _carruselconfiguracion.indice) * -100;
}

function desplazaCarrusel(manejadorFlecha) {
    if (manejadorFlecha != undefined)
        manejadorFlecha(); // Configuración y desplazamiento de contenedor espejo inactivo.
    var _posicion = obtenDesplazamientoHorizontal() + '%';
    $('#' + _carruselconfiguracion.activo).css({
        'left': _posicion
    });
    if (manejadorFlecha != undefined)
        manejadorFlecha(true); // Actualización de contenedor espejo activo.
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
}