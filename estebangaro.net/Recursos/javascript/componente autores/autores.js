var _numAutoresVisible_autores, _numDesplazamientos_autores, _posicion_autores,
    _temporizador_autores;
$(function() {
    ajustaVentanaAutores();
    $('.btns').click(desplazaAutores);
    iniciaTemporizador();
    $(window).resize(
        function() {
            ajustaVentanaAutores();
        }
    )
});

function ajustaVentanaAutores() {
    var anchoPorcentaje = parseFloat(($('.ctdrAutor').width() * 100) /
        $('#ventanaAutores').width()).toFixed(2);
    var numeroAutores = parseInt($('#numAutores').val());
    _numAutoresVisible_autores = parseInt(100 / anchoPorcentaje);
    _numAutoresVisible_autores = _numAutoresVisible_autores > numeroAutores ?
        numeroAutores : _numAutoresVisible_autores;
    if (numeroAutores == _numAutoresVisible_autores) {
        $('.btnsAutores').css('display', 'none');
        anchoPorcentaje = 100 / numeroAutores;
    }
    _numDesplazamientos_autores = numeroAutores - _numAutoresVisible_autores;
    _posicion_autores = 0;

    $('.ctdrAutor').css('width', (100 / numeroAutores) + '%');
    $('#ventanaAutores').
    css({
        'top': '50%',
        'margin-top': ($('#ventanaAutores').height() / -2) + 'px',
        'width': (numeroAutores * anchoPorcentaje) + '%'
    });

    if ($('.iconoMenu').css('display') != 'none') {
        var topSeccion4 = (parseFloat($('#seccion1Principal').height()) +
            parseFloat($('#seccion2Principal').height()) +
            parseFloat($('#seccion3Principal').height()));

        $('#seccion4Principal').css({
            'top': topSeccion4 + 'px'
        });
    }
}

function iniciaTemporizador() {
    _temporizador_autores = setInterval(function() {
        desplazaAutores({ data: true });
    }, 5000);
}

function desplazaAutores(estado) {
    if ($('.btnsAutores').css('display') != 'none') {
        if (estado.data == null) {
            clearInterval(_temporizador_autores);
            iniciaTemporizador();
        }

        if ($(this).attr('id') == 'izqAutor')
            _posicion_autores -= 1;
        else
            _posicion_autores += 1;

        _posicion_autores = _posicion_autores < 0 ? _numDesplazamientos_autores :
            _posicion_autores > _numDesplazamientos_autores ? 0 :
            _posicion_autores;

        $('#ventanaAutores').css(
            'left', ((-100 * _posicion_autores) / _numAutoresVisible_autores) + '%'
        );
    }
}