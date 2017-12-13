        var popupG_cerrarBandera;
        $(function(){
            // Para fines de diseño de formulario
            // centraPopupG();
            $('#popupG .cerrar').click(function(){
                cerrarPopupG();
            });
            $('button.popupG_confirmacion').click(pruebaPopupG);
            $('button.popupG_formulario').click(pruebaPopupGFormulario);
            $(window).resize(centraPopupG);
        });

        function pruebaPopupG(){
            mostrarPopupG({
                Icono: 'OK',
                Titulo: 'Publicar Comentario',
                Contenido: '¡El comentario se ha añadido correctamente!',
                Botones: [{Etiqueta: 'Aceptar', Manejador: function(){
                    alert('Evento click botón aceptar');
                }}, {Etiqueta: 'Salir', Manejador: Salir},
                {Etiqueta: 'Nada'}]
            });
        }

        function pruebaPopupGFormulario(){
            mostrarPopupG({
                Icono: 'MSJ',
                Titulo: '¡Agradezco tu comentario!',
                Contenido: obtenInputs(),
                Botones: [{Etiqueta: 'Publicar', Manejador: undefined}]
            });
        }

        function Salir(){
            alert('Evento click botón Salir');
        }

        // Configuración:
        // Icono : Cadena texto [OK, PREGUNTA, ADVERTENCIA, ERROR]
        // Titulo: Cadena texto ['ATENCIÓN'].
        // Contenido: Cadena texto ['El mensaje se ha guardado correctamente'].
        // Botones: array JS [{Etiqueta: 'Aceptar', Manejador: function(){}},...]
        function mostrarPopupG(configuracion){
            $('body').css('overflow', 'hidden');
            popupG_cerrarBandera = false;
            var popupG = $('#popupG_preguntas');
                EstablecePopupG(
                    popupG,
                    {
                        Icono: obtenIcono(configuracion.Icono),
                        Titulo: configuracion.Titulo,
                        Contenido: configuracion.Contenido,
                        Botones: configuracion.Botones
                    }
                );
                centraPopupG();
                popupG.show();
                popupG.parent().animate({top: '0'}, 1000);
        }

        function mostrarEsperaPopUpG(estado, contenedor){
            if(contenedor == undefined){
                if(estado){
                    mostrarPopupG({
                        Icono: 'LOAD',
                        Titulo: 'CARGANDO',
                        Contenido: $('<span>Espere un momento por favor</span>'),
                        Botones: ''
                    });
                }else{
                    cerrarPopupG();
                }   
            }else{
                if(estado){
                    var esperaPopUpG = $('<div></div>')
                        .append(obtenIcono('LOAD'))
                        .addClass('popupG_espera');
                    $(contenedor).append(esperaPopUpG);
                }else{
                    $(contenedor + ' > .popupG_espera').remove();
                }
            }
        }

        function obtenIcono(etiqueta){
            if(etiqueta.toUpperCase() == 'OK')
                return $('#popupG_OK').clone().removeClass('popupG_icono');
            else if(etiqueta.toUpperCase() == 'MSJ')
                return $('#popupG_MSJ').clone().removeClass('popupG_icono');
            else if(etiqueta.toUpperCase() == 'LOAD')
                return $('#popupG_LOAD').clone().removeClass('popupG_icono');
            else
                return '';
        }

        function cerrarPopupG(){
            $('body').css('overflow', 'auto');
            popupG_cerrarBandera = true;
            var popupG = $('#popupG_preguntas');
            $('input', popupG).each(function(){
                if(!validaDesconocido($(this).attr('popupGv')))
                    $(this).focus();
            });
            popupG.parent().animate({top: '-100%'}, 1000, function(){
                if(popupG_cerrarBandera){
                    EstablecePopupG(popupG, {titulo: '', contenido: '', botones: '', icono: ''});
                    popupG.hide();
                }
            });
        }

        function EstablecePopupG(popup, elementos){
            var llave;
            for(var i in elementos){
                llave = i.toLowerCase();
                $('.' + llave, popup).html(elementos[i]);
                if(llave == 'botones')
                    $('.' + llave, popup).append($('<div class="flecha"></div>'));
                if(elementos[i] == '')
                    $('.' + llave, popup).hide();
                else
                    $('.' + llave, popup).show();
            }
        }

        function centraPopupG(){
            var ancho = $('#popupG_preguntas').outerWidth() / 2;
            var alto = $('#popupG_preguntas').outerHeight() / 2;
            $('#popupG_preguntas').css({
                'margin-left': (-1 * ancho) + 'px',
                'margin-top': (-1 * alto) + 'px'
            });
        }