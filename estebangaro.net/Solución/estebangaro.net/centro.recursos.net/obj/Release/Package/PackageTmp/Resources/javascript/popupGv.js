        var _popupGv_validaciones;
        $(function(){
            $('.popupGv_validacion > .Cerrar').click(function(){
                cerrarPopupGv(obtenIndicePopupGv($(this).parent().attr('id')));
            });
            $(window).resize(posicionaResizePopupGv);
        });

        // ValidacionP: {Funcion, {}, string}
        // EsRequerido: {bool, string}
        // LongitudMinina: {int, string}
        // LongitudMáxima: {int, string}
        // Patron: {RegExp, string}
        // Elemento: obj JS
        // Valor: cadena.
        function validaElementoPopupGv(val){
            var estado = true;
            if(validaDesconocido(val.Elemento.attr('popupGv'))){
            if(validaDesconocido(val.ValidacionP)){
                if(!validaDesconocido(val.EsRequerido) && val.EsRequerido.Valor){
                    if(!validaCadenaDesconocida(val.Valor)){
                        estado = muestraPopupGv({elemento: val.Elemento,
                            titulo: val.EsRequerido.Titulo,
                            descripcion: val.EsRequerido.Descripcion
                        }); 
                    }
                    else if(!validaDesconocido(val.LongitudMinima) && 
                        !validaCadenaLongitud(val.Valor, val.LongitudMinima.Valor, false))
                        estado = muestraPopupGv({elemento: val.Elemento,
                            titulo: val.LongitudMinima.Titulo,
                            descripcion: val.LongitudMinima.Descripcion
                        }); 
                    else if (!validaDesconocido(val.LongitudMaxima) &&
                    !validaCadenaLongitud(val.Valor, val.LongitudMaxima.Valor)){
                        estado = muestraPopupGv({elemento: val.Elemento,
                            titulo: val.LongitudMaxima.Titulo,
                            descripcion: val.LongitudMaxima.Descripcion
                        }); 
                    }else if(!validaDesconocido(val.Patron) && !val.Patron.Valor.test(val.Valor)){
                        estado = muestraPopupGv({elemento: val.Elemento,
                            titulo: val.Patron.Titulo,
                            descripcion: val.Patron.Descripcion
                        }); 
                    }
                }
            }else{
                if(!val.ValidacionP.Funcion(val.ValidacionP.Parametros))
                    estado = muestraPopupGv({elemento: val.Elemento,
                        titulo: 'ATENCIÓN',
                        descripcion: val.ValidacionP.Descripcion
                    });
            }
        }else
            estado = false;

            return estado;
        }

        function validaCadenaLongitud(cadena, longitud, esMax = true){
            return esMax? cadena.length <= longitud: cadena.length >= longitud;
        }

        function validaCadenaDesconocida(cadena){
            cadena = cadena.trim();
            return cadena != null && cadena != undefined && cadena != '';
        }

        function validaDesconocido(valor){
            return valor == null || valor == undefined;
        }

        function validaFormatoCadena(cadena, formato){
            return new RegExp(formato).test(cadena);
        }

        function muestraPopupGv(estadoValidacion){
            var esNuevo = validaDesconocido(estadoValidacion.popupGv);
            var posicionInicial = obtenPosicionPopupGv(estadoValidacion.elemento);
            var popupGv = esNuevo? creaPopupGv(estadoValidacion): estadoValidacion.popupGv;
            posicionaPopupGv(popupGv, posicionInicial, esNuevo);

            return false;
        }

        function cerrarPopupGv(indice){
            $('#popupGv_validacion_' + indice).remove();
            var elemento = $('[popupGv="' + indice + '"]');
            limpiaElemento(indice, elemento);
            elemento.focus();
        }

        function focoPopupGv(elemento){
            elemento = $(elemento.target);
            var indicePopupGv = elemento.attr('popupGv');
            var popupgv = $('#popupGv_validacion_' + indicePopupGv);
            popupgv.remove();
            limpiaElemento(indicePopupGv, elemento);
        }

        function limpiaElemento(indice, elemento){
            /* if($('input[popupGv="' + indice + '"]').length > 0)
                elemento.val('');
            else
                elemento.empty(); */
            elemento.removeAttr('popupGv');
            elemento.off('focusin', focoPopupGv);
        }

        function obtenIndicePopupGv(id){
            return parseInt(id.substr(id.lastIndexOf("_") + 1 ));
        }

        function obtenPosicionPopupGv(elemento){
            var posicionElemento = elemento.offset();
            var anchoElemento = elemento.outerWidth();
            var altoElemento = elemento.outerHeight();

            return { top: posicionElemento.top + (altoElemento / 2), 
                left: posicionElemento.left + anchoElemento };
        }

        function creaPopupGv(configuracion){
            var popupGv = $('#popupGv_validacion_model').clone(true);
            var idPopupGv = obtenUltimoIndicePopupGv();
            popupGv.attr('id', 'popupGv_validacion_' + idPopupGv);
            $('.Titulo > span', popupGv).text(configuracion.titulo);
            $('.Contenido > span', popupGv).text(configuracion.descripcion);
            configuracion.elemento.attr('popupGv', idPopupGv);
            configuracion.elemento.on('focusin', focoPopupGv);

            return popupGv;
        }

        function obtenUltimoIndicePopupGv(){
            var ultimopgv = $('.popupGv_validacion[id!="popupGv_validacion_model"]').last();
            return ultimopgv.length > 0? obtenIndicePopupGv(ultimopgv.attr('id')) + 1: 0;
        }

        function posicionaPopupGv(popup, posicion, esNuevo){
            var altoPopupGv;
            var anchoVentana = $(window).width();
            var anchoPopupGv = $('.popupGv_validacion').outerWidth();
            // +11: Ancho/2 Flecha + 5 px de margen.
            var despHorizontal = (anchoPopupGv + posicion.left >= anchoVentana)? 
                (-11 - anchoPopupGv): 11;
            popup.css({
                top: posicion.top,
                left: posicion.left + despHorizontal,
                display: 'block'
            });
            if(esNuevo)
                popup.appendTo($('body'));
            altoPopupGv = popup.outerHeight();
            popup.css('margin-top', (altoPopupGv / -2) + 'px',);
        }

        function posicionaResizePopupGv(){
            $('[popupGv]').each(function(){
                var popupGv = $('#popupGv_validacion_' + $(this).attr('popupGv'));
                muestraPopupGv({elemento: $(this), popupGv: popupGv});
            });
        }