            $(function(){
                cargaComentarios();
                $(window).resize(function(){
                    ajustaComponenteComentarios();
                });
                enlazaEventos();
            });

            function enlazaEventos(){
                $('.btnPublicar').click(function(){
                    // Solicitar información de autor del comentario:
                    // Nombre, email, verificación de no robot (re captcha).
                    btnPublicarManejadorClic($(this));
                });
                $('.lblResponder').click(function(){
                    lblResponderManejadorClic($(this));
                });
                $('.mostrarComentarios').click(function(){
                    muestraOcultaComentariosAnidados($(this));
                });
                $('.cerrarComentario').click(function(){
                    $(this).siblings('.mostrarComentarios').first().click();
                });
                $('.mostrarMasComentarios').click(function(){
                    cargaComentarios('antiguos');
                });
            }

            function ajustaLblResponder(lblResponder, accion){
                lblResponder.children('img')
                    .attr('src', accion == "Responder" ? 
                        '/Resources/imagenes/comentarios/cancelarrojo.svg' : '/Resources/imagenes/comentarios/responder.svg');
                lblResponder.children('span')
                    .html(accion == "Responder"? 
                        'Cancelar': 'Responder');
            }

            function lblResponderManejadorClic(lblResponder){
                var accion = lblResponder.children('span').text();
                ajustaLblResponder(lblResponder, accion);
                if(accion == "Responder"){
                    var comentarioRespuesta = $('.respuestaModelo').first().clone(true);
                    comentarioRespuesta.insertAfter(lblResponder);
                    comentarioRespuesta.show();
                    comentarioRespuesta.find('textarea').focus();
                }else{
                    lblResponder.parent().children('.respuestaModelo').remove();
                }
                ajustaAltoComentsComents();
            }

            function btnPublicarManejadorClic(btnPublicar){
                var accion = btnPublicar.children('span').text();
                mostrarPopupG({
                    Icono: 'MSJ',
                    Titulo: '¡Agradezco tu comentario!',
                    Contenido: obtenInputs(),
                    Botones: [{Etiqueta: accion, Manejador: function(datosPopUpG){
                        btnPublicarManejadorClicOK(btnPublicar, obtenObjDatosPopUp(datosPopUpG));
                    }}]
                });
            }

            function obtenObjDatosPopUp(datosPopUpG){
                var uriAvatar = obtenAvatar($('div.avatar', datosPopUpG));
                var datosPopUp = {
                    Avatar: uriAvatar,
                    Autor: $('input', datosPopUpG).eq(1).val(),
                    Email: $('input', datosPopUpG).eq(0).val(),
                    AvatarNombre: obtenNombreURI(uriAvatar),
                    EstatusCliente: 1
                };

                return datosPopUp;
            }

            function obtenAvatar(avatar){
                var avatarURI = $('div > div > img', avatar).eq(popupG_index).attr('src');
                return avatarURI;
            }

            function obtenNombreURI(uri){
                return uri.substring(uri.lastIndexOf('/') + 1);
            }

            function btnPublicarManejadorClicOK(btnPublicar, datosPopUp){
                var accion = btnPublicar.children('span').text();
                var idComentario;
                    publicaComentario(
                        {
                            Accion : accion,
                            Avatar: datosPopUp.Avatar, // '/Resources/imagenes/comentarios/akane.jpg',
                            AltAvatar : 'avatar',
                            Autor : datosPopUp.Autor, // 'Esteban GaRo',
                            Fecha : '',
                            Contenido : accion == "Publicar"?
                                 $('#comentarioPublicar .contenido > textarea').val():
                                 btnPublicar.parent().find('textarea').val(),
                            Boton: btnPublicar,
                            Padre: accion == 'Publicar' ? null : btnPublicar.parent().parent().parent().attr('id'),
                            Email: datosPopUp.Email + ';' + datosPopUp.EstatusCliente + ';' + datosPopUp.AvatarNombre + ';' +
                                // 'estebangaro4@outlook.com;1;ranma.jpg;' +
                                ((idComentario = obtenIdComentario(accion, btnPublicar)) == undefined? '0': idComentario)
                        }
                    );
            }

            function obtenIdComentario(accion, boton) {
                return accion == 'Publicar' ? boton.parent().next().attr('id') :
                    accion == 'Responder' ?
                        boton.parent().parent().children('.cdrComentario').eq(1).attr('id') :
                    accion == 'MostrarMas'?
                        $('.CtdrComentarios > .cdrComentario:last-child').attr('id'):
                    undefined;
            }

            function muestraOcultaComentariosAnidados(btnOcultaMuestra){
                var comentarios = btnOcultaMuestra
                    .parents('.comentarios')
                    .first()
                    .children('.cdrComentario');
                if(comentarios.length > 0){
                    var accion = comentarios.first().css('display') == 'none'? 'mostrar': 'ocultar';
                    MuestraComentarios(comentarios, btnOcultaMuestra, accion);
                    ajustaAltoComentsComents();
                }
            }

            function MuestraComentarios(comentarios, btnOcultaMuestra, accion){
                if(accion == 'ocultar'){
                    comentarios.hide();
                    btnOcultaMuestra.parents('.cdrComentario').first().children('.bordeComentarioP').first()
                        .hide();
                    btnOcultaMuestra.parent().siblings('.contenido').hide();
                    btnOcultaMuestra.attr('src', btnOcultaMuestra.attr('src').replace("flechaU", "flechaDn")).parent()
                        .addClass('comentarioOculto');
                    btnOcultaMuestra.parents('.cdrComentario').first().children('.avatarComentario')
                        .first().css('opacity', '0.5');
                    btnOcultaMuestra.siblings('.cerrarComentario').show();
                    btnOcultaMuestra.parents('.comentarios').first().children('.lblResponder').hide();
                }else{
                    comentarios.css('display', '');
                    btnOcultaMuestra.parents('.cdrComentario').first().children('.bordeComentarioP').first()
                        .css('display', '');
                    btnOcultaMuestra.parent().siblings('.contenido').show();
                    btnOcultaMuestra.attr('src', btnOcultaMuestra.attr('src').replace("flechaDn", "flechaU"))
                        .parent().removeClass('comentarioOculto');
                    btnOcultaMuestra.parents('.cdrComentario')
                        .first().children('.avatarComentario').first()
                        .css('opacity', '1');
                    btnOcultaMuestra.siblings('.cerrarComentario').hide();
                    btnOcultaMuestra.parents('.comentarios').first().children('.lblResponder').show();
                }
            }

            function registraComentario(comentarioValores, indice, tope){
                var comentario = $('.comentarioModelo').clone(true);
                estableceValoresComentario(comentario, comentarioValores);
                if(comentarioValores.Accion == "Publicar"){
                    comentario.insertAfter('#comentarioPublicar');
                    $('.contenido > textarea').val('');
                } else {
                    if (indice == (tope - 1))
                        comentarioValores.Boton.parent().replaceWith(comentario);
                    else
                        comentario.insertAfter(comentarioValores.Boton.parent());
                    var bordeS = $('<div class="bordeComentarioS"></div>');
                    bordeS.insertBefore(comentario);
                    if(comentario.parent().parent()
                        .children('.bordeComentarioP').length == 0)
                        comentario.parent().parent().prepend($('<div class="bordeComentarioP"></div>'));
                    ajustaLblResponder(comentario.parent().children('.lblResponder'), 'Cancelar');
                    if(comentario.parents('.cdrComentario').length == 2){
                        comentario.children('.comentarios').children('.lblResponder')
                            .css('visibility', 'hidden');
                    }
                }
                comentario.removeClass('comentarioModelo')
                    .attr('id', comentarioValores.Padre);
                comentario.show();
                ajustaComponenteComentarios(comentario); // Aplicar ajustes a elemento recién creado.
            }

            function estableceValoresComentario(comentario, valores){
                $('.avatarComentario > img', comentario)
                    .attr('src', valores.Avatar) // '..\..\imagenes\akane.jpg'
                    .attr('alt', valores.AltAvatar); // 'avatar'
                $('.autorComentario', comentario) // 'esteban garo'
                    .html(valores.Autor);
                $('.fechaComentario', comentario) // '07/10/2017'
                    .html(valores.Fecha);
                $('.contenido', comentario) // 'Prueba de comentarios' 
                    .html(valores.Contenido);
            }

            function validaComentarios(comentario, selector, selector2){
                return comentario == null || comentario == undefined? 
                    $(selector): $(selector2 == null || selector2 == undefined?
                    selector: selector2, comentario); 
            }

            function ajustaComponenteComentarios(comentario){
                ajustaFlechaComentsCaja(comentario);
                ajustaAnchoComentsCaja(comentario);
                ajustaAltoComentsComents(comentario);
                ajustaDespComentsS(undefined);
            }

            function ajustaDespComentsS(bordeComentarioS){
                var anchoAvatar = $('.avatarComentario').outerWidth() * 0.75;
                var altoAvatar = ($('.avatarComentario').outerHeight() / 2);
                var marginTop = parseInt($('.cdrComentario').css('margin-top').replace("px", ""));
                var estlo = $('.comentario').css('border-width');
                var estilo2 = $('.bordeComentarioS').css('border-width');
                var bordertop = 1;//parseFloat($('.comentario').css('border-width').replace("px",""));
                var borderS = 1;//parseFloat($('.bordeComentarioS').css('border-width').replace("px",""));
                if(bordeComentarioS == undefined)
                    bordeComentarioS = $('.bordeComentarioS');
                bordeComentarioS
                    .css('width', + anchoAvatar + 'px')
                    .css('top', (altoAvatar + marginTop + bordertop + borderS) + 'px')
                    .css('left', -1*anchoAvatar);
            }

            function ajustaAltoComentsComents(comentario){
                var ultimoComentario = validaComentarios(undefined,
                    '.cdrComentario > .comentarios > .cdrComentario:last-child');
                ultimoComentario
                .each(function(){
                    var altoCtdr = $(this).outerHeight();
                    var altoAvatar = $('.avatarComentario').outerHeight() / 2;
                    var anchoAvatar = $('.avatarComentario').outerWidth() / 2;
                    var despVertical = (altoCtdr-altoAvatar)*-1;
                    var bordeP = $(this).parent().parent().children('.bordeComentarioP');
                    bordeP.css('top', despVertical + 'px');
                    bordeP.css('left', anchoAvatar + 'px');
                });
            }

            function ajustaFlechaComentsCaja(comentario){
                var altoAvatar = $('.avatarComentario').outerHeight() / 4;
                var anchoAvatar = $('.avatarComentario').outerWidth() / 4;
                validaComentarios(comentario, '.flechaComentario')
                .css('width', anchoAvatar + 'px')
                .css('height', altoAvatar + 'px')
                .each(function(){
                    var altoAvatar = $(this).parent().parent().prev().outerHeight();
                    var desplazamientoV = (altoAvatar / 2) - ($(this).outerHeight() / 2);
                    var desplazamientoH = $(this).outerWidth() / 2 * -1;
                    $(this).css('top', desplazamientoV + 'px').css('left', desplazamientoH + 'px');
                });
            }

            function ajustaAnchoComentsCaja(comentario){
                validaComentarios(comentario, '.comentario')
                .each(function(){
                    var anchoAvatar = $(this).parent().prev().outerWidth();
                    var anchoContenedor = $(this).parent().parent().innerWidth();
                    var anchoFlecha = $('.flechaComentario', $(this)).outerWidth();
                    var porcentajeAncho = anchoAvatar != null && anchoFlecha != null?
                        100.00 - parseFloat(((anchoAvatar + anchoFlecha) * 100) / anchoContenedor):
                        100;
                        
                    $(this).parent().css('width', porcentajeAncho + '%');
                });
            }

function registraPublicacion(ctdrComentario){
    ctdrComentario.insertAfter($('#comentarioPublicar'));
}

function registraRespuesta(ctdrComentario, indice, comentarioPadreLbl, 
    comentarioBackEnd, Boton){
    if(indice > 0){
        comentarioPadreLbl = comentarioPadreLbl == undefined?
            $('#' + comentarioBackEnd.IdComentarioP).find('.lblResponder').first(): comentarioPadreLbl;
        ctdrComentario.insertAfter(comentarioPadreLbl);
    }else{
        Boton.parent().replaceWith(ctdrComentario);
    }
    $('<div class="bordeComentarioS"></div>').insertBefore(ctdrComentario);
}

function sonComentariosAnidados(nivel, ctdrComentario){
    if(nivel > 0){ ajustaLblResponder(ctdrComentario.parent()
        .children('.lblResponder'), 'Cancelar');
        if(ctdrComentario.parent().parent().children('.bordeComentarioP').length == 0)
            ctdrComentario.parent().parent().prepend($('<div class="bordeComentarioP"></div>'));
    }
}

function procesaComentarios(comentarios, comentario){
    var nivel, comentarioPadreLbl, ctdrComentario;
    // alert("El comentario, se ha almacenado correctamente");
    $.each(comentarios, function (index, value) {
        nivel = nivel == undefined? value.IdComentarioP == undefined? 0: 
            $('#' + value.IdComentarioP).parents('.cdrComentario').length + 1: 
            nivel;
        ctdrComentario = obtenMarcado(value, nivel);
        if(comentario.Accion == "Publicar"){
            registraPublicacion(ctdrComentario);
        }else if(comentario.Accion == "Responder"){
            registraRespuesta(ctdrComentario, index, comentarioPadreLbl, 
                value, comentario.Boton);
        }else{
            alert("No es posible procesar el comentario, bajo la acción especificada.");
        }
    });
    sonComentariosAnidados(nivel, ctdrComentario);
    ajustaComponenteComentarios();
}

function publicaComentario(comentario) {
    var comentarioBackEnd = obtenComentarioBackEnd(comentario);
    $.ajax({
        type: 'POST',
        url: '/api/Comentario',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        data: JSON.stringify(comentarioBackEnd),
        success: function (data) {
            procesaComentarios(data.Comentarios, comentario);
            $('#comentarioPublicar .contenido > textarea').val('');
        },
        error: function (jqxhr, error, errorthrown) {
            alert("Ha fallado el almacenamiento del comentario: " + error + ", " + errorthrown);
        }
    });
}

function configuraComentario(comentario, comentbackend) {
    comentario.Avatar = '/Resources/imagenes/comentarios/' + comentbackend.Cliente.Avatar;
    comentario.Autor = comentbackend.Cliente.Nombre;
    comentario.Fecha = comentbackend.Auditoria.Creacion;
    comentario.Contenido = comentbackend.Contenido;
    comentario.Padre = comentbackend.Id;
}

function obtenComentarioBackEnd(comentario) {
    return {
        Contenido: comentario.Contenido,
        Auditoria: { UsuarioCreacion: comentario.Autor },
        IdComentarioP: comentario.Padre,
        URI: window.location.pathname.toLowerCase(),
        Email: comentario.Email
    };
}

function obtenMarcado(comentario, nivel) {
    var ctdrComentario = $('.comentarioModelo').clone(true);
    cargaInfoComentario(comentario, ctdrComentario, nivel);

    if (comentario.Comentarios != null && comentario.Comentarios.length > 0) {
        for(var i = comentario.Comentarios.length - 1; i >= 0; i--){
            var ctdrComentarioHijo = obtenMarcado(comentario.Comentarios[i], nivel + 1);
            $('.comentarios', ctdrComentario)
                .first()
                .append(ctdrComentarioHijo);
            $('<div class="bordeComentarioS"></div>').insertBefore(ctdrComentarioHijo);
        }
        ctdrComentario.prepend($('<div class="bordeComentarioP"></div>'));
    }

    return ctdrComentario;
}

function cargaInfoComentario(comentario, comentarioElemento, nivel) {
    $('.avatarComentario > img', comentarioElemento)
        .attr('src', '/Resources/imagenes/comentarios/' + comentario.Cliente.Avatar);
    $('.autorComentario', comentarioElemento)
        .text(comentario.Cliente.Nombre);
    $('.fechaComentario', comentarioElemento)
        .text(muestraHoraFormato(comentario.Auditoria.Creacion));
    $('.contenido', comentarioElemento)
        .text(comentario.Contenido);
    comentarioElemento
        .removeClass('comentarioModelo')
        .attr('id', comentario.Id);
    if (nivel == 2) {
        $('.lblResponder', comentarioElemento)
            .css('visibility', 'hidden');
    }
}

function cargaComentarios(tipo = "recientes") {
    var ids = obtenIdComentario('MostrarMas');
    $.ajax({
        type: 'GET',
        url: '/api/comentario',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        data: {id: window.location.pathname.toLowerCase().substring(1).replace(/\//g, "-"),
            tipo: tipo,
            ultimoantiguo: tipo != "recientes"? 
            obtenIdComentario('MostrarMas'): 0
        },
        success: function (data) {
            if(data.Cuenta <= _topeComentarios){
                $('.mostrarMasComentarios').hide();
            }
            $.each(data.Comentarios, function (index, value) {
                var ctdrComentarioElemento =
                    obtenMarcado(value, 0);
                $('.CtdrComentarios').append(ctdrComentarioElemento);
            });
            ajustaComponenteComentarios();
        },
        error: function (error) {
            alert("Ha fallado el consumo de WEB API");
        }
    });
}

function diferenciaHrsFecha(fecha){
    var ahora = new Date();
    var diferencia = -1;
    var unidad = 'h';
    if(ahora.getDate() == fecha.getDate() && ahora.getMonth() == fecha.getMonth() 
        && ahora.getFullYear() == fecha.getFullYear()){
            var segundosAhora = obtenSegundosHora(ahora);
            var segundosFecha = obtenSegundosHora(fecha);
            diferencia = obtenParteTiempoSegundos(segundosAhora - segundosFecha, 'h');
            if(diferencia == 0){
                diferencia = obtenParteTiempoSegundos(segundosAhora - segundosFecha, 'm');
                if(diferencia == 0){
                    diferencia = segundosAhora - segundosFecha;
                    unidad = 's';
                }
                else
                    unidad = 'm';
            }
    }

    return {diferencia: diferencia, unidad: unidad};
}

function obtenSegundosHora(fecha){
    return (fecha.getHours() * 60 * 60) + (fecha.getMinutes() * 60) + fecha.getSeconds();
}

function obtenParteTiempoSegundos(segundos, parte){
    return parte == "h"? parseInt(segundos/(60*60)): 
        parte == "m"? parseInt(segundos/60):
        segundos;
}

function muestraHoraFormato(fecha){
    var formatoFecha = "";
    if(fecha != undefined){
        try{
            var FechaObj = new Date(fecha);
            var diferenciaHrs = diferenciaHrsFecha(FechaObj);
            formatoFecha = diferenciaHrs.diferencia == -1? muestraFechaFormatoMX(FechaObj):
                diferenciaHrs.diferencia == 0? "Hace un momento":
                "Hace " + diferenciaHrs.diferencia + 
                (diferenciaHrs.unidad == 'h'? diferenciaHrs.diferencia > 1? " horas": " hora":
                    diferenciaHrs.unidad == 'm'? diferenciaHrs.diferencia > 1? " minutos": " minuto":
                    diferenciaHrs.diferencia > 1? " segundos": " segundo");
        }catch(ex){
            formatoFecha = "No disponible";
        }
    }
    return formatoFecha;
}

function muestraFechaFormatoMX(FechaObj){
    return ajustaNumerosCeros(FechaObj.getDate())
        + "/" + ajustaNumerosCeros(FechaObj.getMonth() + 1) + "/" + FechaObj.getFullYear()
        + " " + ajustaNumerosCeros(FechaObj.getHours() % 12) + ":" + ajustaNumerosCeros(FechaObj.getMinutes())
        + ":" + ajustaNumerosCeros(FechaObj.getSeconds());
}

function ajustaNumerosCeros(numero, longitud = 2){
    var ceros = "";
    for(var i = 0; i < longitud; i++) ceros += "0";
    return (ceros + numero).substr(longitud*-1, longitud);
}

