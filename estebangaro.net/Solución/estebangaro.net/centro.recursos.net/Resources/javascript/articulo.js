$(function(){
    ajustaPaddingSeccionArt();
    $(window).resize(function(){
        ajustaPaddingSeccionArt();
    });
    enlazaEventosArt();
});

function ajustaPaddingSeccionArt(){
    var _alto = $('#contenedorMenu').innerHeight();
    $('#SeccionArticulo').css('padding-top', _alto + 'px');
}

function enlazaEventosArt(){
    $('#InfoArticulo > .Autor > .Nombre > span').click(function(){
        consultaAutor($(this).attr('id'));
    });
}

function muestraInfoAutor(autorEntidad, edad){
    var plantillaAutor = $('#popupG_Autor').clone();
    plantillaAutor.attr('id', '');
    var rutaImgAutor = $('#InfoArticulo > .Autor > img').attr('src');
    rutaImgAutor = rutaImgAutor.substring(0, rutaImgAutor.lastIndexOf("/") + 1);

    plantillaAutor.children().first().attr('src', rutaImgAutor + autorEntidad.Imagen);
    $('div > span', plantillaAutor).eq(0)
        .text(edad);
    $('div > span', plantillaAutor).eq(1)
        .text(autorEntidad.Puesto.Descripcion);
    $('div > span', plantillaAutor).eq(2)
        .text(autorEntidad.Email);

    mostrarPopupG({
            Icono: 'autor',
            Titulo: $('<span/>').text(autorEntidad.Nombre + " " +
                autorEntidad.Apellido + 
                (autorEntidad.ApellidoM != undefined? " " + autorEntidad.ApellidoM: "")),
            Contenido: plantillaAutor,
            Botones: obtenBotonesAutor(autorEntidad.RedesSociales)
    });
}

function obtenBotonesAutor(redes){
    var btnElemento = $('<div class="boton"/>');
    redes.forEach(red => {
        btnElemento.append(
            $('<a/>').attr('href', red.URI)
                .attr('target', '_blank')
                .html($('<img/>').attr('src', '/Resources/imagenes/infoarticulo/' + red.Icono))
        );
    });

    return btnElemento;
}

function consultaAutor(id) {
    // mostrarEsperaPopUpG(true, '#popupG_preguntas');
    $.ajax({
        type: 'GaroAutores',
        url: '/api/Autor/' + id,
        contentType: 'application/json; charset=utf-8',
        success: function(data) {
            // mostrarEsperaPopUpG(false, '#popupG_preguntas');
            if(data.Estado)
                muestraInfoAutor(data.Autor, data.Edad);
            else
                alert('Sin coincidencia de búsqueda');
        },
        error: function(err) {
            // mostrarEsperaPopUpG(false, '#popupG_preguntas');
            alert('Ha fallado la consulta de Autor');
        }
    });
}

