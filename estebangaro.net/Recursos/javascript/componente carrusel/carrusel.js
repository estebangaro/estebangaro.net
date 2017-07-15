var _carruselconfiguracion;
var _carruselTemporizador;

$(function() {
    cargaAvisos();
    enlazarEvts();
    ajustaAlturaCarrusel();
    estableceTemporizadorCarrusel(true);
    $(window).resize(
        function() {
            ajustaAlturaCarrusel();
        });
});

function ajustaAlturaCarrusel() {
    var _alto = $('#contenedorMenu').innerHeight();
    var _altoVentana = $(window).height();
    _carruselconfiguracion.alturaCarrusel = _altoVentana - _alto;
    $('#contenedorCarrusel').css({
        'top': _alto + 'px',
        'height': (_altoVentana - _alto) + 'px'
    });

    $('#cont_1 > div').each(function() {
        ajustaTextoCarrusel($(this));
    });
    $('#cont_2 > div').each(function() {
        ajustaTextoCarrusel($(this));
    });
}

function ajustaTextoCarrusel(aviso) {
    var _ancho = (100 / _carruselconfiguracion.cantidadElementos);
    aviso.css('width', _ancho + '%');
    var _altoAviso = aviso.height();
    aviso.css({
        'margin-top':
            ((_carruselconfiguracion.alturaCarrusel - _altoAviso) / 2 -
                $('#contenidoBtns').height()) + 'px',
        'width': _ancho + '%'
    });
}

function estableceTemporizadorCarrusel(estado) {
    if (estado)
        _carruselTemporizador = setInterval(function() {
            muestraSiguienteAviso($('#contenidoFlechas > img:first-child'),
                validaDesplazamientoInactivo);
        }, 7000);
    else
        clearInterval(_carruselTemporizador);
}

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
            return true;
        } else if (_carruselconfiguracion.indice == _carruselconfiguracion.cantidadElementos + 1) {
            configuraContenido(_inactivo, true, (_carruselconfiguracion.cantidadElementos * -100) + '%',
                obtenDesplazamientoHorizontal(1) + '%');
            return true;
        }
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
            'visibility': 'visible',
            'opacity': '1'
        });
    } else {
        $('#' + contenidoC).css({
            'visibility': 'hidden',
            'opacity': '0'
        });
        $('#' + contenidoC).removeClass('animaCarrusel');
    }
}

function desplazaCarrusel(manejadorFlecha, ejecutaAlTerminoAnimacion) {
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
            $('#' + _inactivo).attr('class', 'contenidoCarrusel animaCarrusel');
            $('#' + _inactivo).css({
                'left': _carruselconfiguracion.indice == 0 ? '0' : obtenDesplazamientoHorizontal(1) + '%'
            });
        }, 10);
    }
    if (manejadorFlecha != undefined)
        setTimeout(function() {
            manejadorFlecha(false); // Actualización de contenedor espejo activo.
        }, 1500);

    if (ejecutaAlTerminoAnimacion != undefined)
        setTimeout(function() {
            ejecutaAlTerminoAnimacion(true); // Método de utilería para habilitar algún evento y temporizador.
        }, 1500);
}

function cargaAvisos() {
    _carruselconfiguracion = {
        indice: 1, // índice de aviso inicial.
        cantidadElementos: parseInt($('#avisosCuenta').val()), // cantidad de elementos "aviso" a visualizar.
        activo: 'cont_1'
    };
    refrescaBotonDesplazamiento();
    $('#cont_1').addClass('animaCarrusel')
        .css({ 'width': (_carruselconfiguracion.cantidadElementos * 100) + '%' });
    $('#cont_2').css({ 'width': (_carruselconfiguracion.cantidadElementos * 100) + '%' });
}

function obtenPosicionBtn(botonOrigen) {
    return parseInt(botonOrigen.attr('alt'));
}

function enlazarEvts() {
    $('#contenidoBtns > img').on('click', function() {
        click_Boton($(this));
    });
    $('#contenidoFlechas>img').on('click', function() {
        click_Boton($(this), validaDesplazamientoInactivo);
    });
}

function click_Boton(flecha, eventoCirculo) {
    estableceTemporizadorCarrusel(false);
    muestraSiguienteAviso(flecha, eventoCirculo, estableceTemporizadorCarrusel);
}

function estableceEventos(estado) {
    if (estado)
        setTimeout(function() {
            enlazarEvts();
        }, 1500);
    else {
        $('#contenidoFlechas > img').off('click');
        $('#contenidoBtns > img').off('click');
    }
}

function muestraSiguienteAviso(boton, eventoCirculo, ejecutaAlfinal) {
    var _btnParent = boton.parent().attr('id');
    estableceEventos(false);
    if (_btnParent == 'contenidoFlechas')
        _carruselconfiguracion.indice += obtenPosicionBtn(boton);
    else
        _carruselconfiguracion.indice = obtenPosicionBtn(boton);
    desplazaCarrusel(eventoCirculo, function(estado) {
        estableceEventos(estado);
        refrescaBotonDesplazamiento();
        if (ejecutaAlfinal != undefined)
            ejecutaAlfinal(estado);
    });
}

function refrescaBotonDesplazamiento() {
    $('#contenidoBtns > img').attr('src', '../../imagenes/boton_c3.png')
        .eq(_carruselconfiguracion.indice - 1)
        .attr('src', '../../imagenes/boton_c1.png');
}