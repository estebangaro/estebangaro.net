            $(function(){
                ajustaComponenteComentarios();
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
            }

            function ajustaLblResponder(lblResponder, accion){
                lblResponder.children('img')
                    .attr('src', accion == "Responder"? 
                        '..\\..\\imagenes\\cancelarrojo.svg': '..\\..\\imagenes\\responder.svg');
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
                    registraComentario(
                        {
                            Accion : accion,
                            Avatar : '..\\..\\imagenes\\akane.jpg',
                            AltAvatar : 'avatar',
                            Autor : 'Esteban GaRo',
                            Fecha : '07/10/2017',
                            Contenido : accion == "Publicar"?
                                 $('.contenido > textarea').val():
                                 btnPublicar.parent().find('textarea').val(),
                            Boton: btnPublicar
                        }
                    );
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

            function registraComentario(comentarioValores){
                var comentario = $('.comentarioModelo').clone(true);
                estableceValoresComentario(comentario, comentarioValores);
                if(comentarioValores.Accion == "Publicar"){
                    // Añadir a contenedor de comentarios.
                    comentario.insertAfter('#comentarioPublicar');
                    $('.contenido > textarea').val('');
                }else{
                    comentarioValores.Boton.parent().replaceWith(comentario);                    
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
                comentario.removeClass('comentarioModelo');
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