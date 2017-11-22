        var popupG_index = 0;
        var popupG_avatarCuenta = 3;
        $(function(){
            // Para fines de diseño de formulario
            centraPopupG();
            $('#popupG .cerrar').click(function(){
                cerrarPopupG($(this));
            });
            $('button.popupG_confirmacion').click(pruebaPopupG);
            $('button.popupG_formulario').click(pruebaPopupGFormulario);
            $('#popupG_Campos > .avatar > img').click(function(){
                desplazarAvatar($(this).attr('class'));
            });
        });

        function desplazarAvatar(sentido){
            // Determinar el valor de desplazamiento, en función del índice del avatar seleccionado:
            // Desplazamiento = (indiceAvatarSeleccionado * -100) + 'px';
            var avatarVentana = $('#popupG_preguntas .campos .avatar > div');
            popupG_index += (sentido == 'flechaD'? 1: -1);

            if(popupG_index == popupG_avatarCuenta)
                popupG_index = 0;
            else if(popupG_index < 0)
                popupG_index = popupG_avatarCuenta - 1;

            avatarVentana.css('left', (popupG_index * -100) + '%');
        }

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
        function mostrarPopupG(configuracion, 
            esConfirmacion = true){
            var popupG = $('#popupG_preguntas');
                EstablecePopupG(
                    popupG,
                    {
                        Icono: obtenIcono(configuracion.Icono),
                        Titulo: $('<span/>').text(configuracion.Titulo),
                        Contenido: configuracion.Contenido.length == undefined? 
                            $('<span/>').text(configuracion.Contenido):
                            configuracion.Contenido,
                        Botones: obtenBotones(configuracion.Botones)
                    }
                );
                centraPopupG();
                popupG.show();
                popupG.parent().animate({top: '0'}, 1000);
        }

        function obtenBotones(arregloBtns){
            var btn, btnObj;
            var btnElemento = $('<div class="boton"/>');
            for(var llave in arregloBtns){
                btnObj = arregloBtns[llave];
                btn = $('<span/>').text(btnObj.Etiqueta)
                .attr('class', llave)
                .click(function(){
                    var camposPopupG = $('#popupG_preguntas .campos').clone(true);
                    $('#popupG_preguntas > .cerrar').click();
                    var index = parseInt($(this).attr('class'));
                    if(arregloBtns[index].Manejador != undefined)
                        arregloBtns[index].Manejador(camposPopupG);
                });
                btnElemento.append(btn);
            }

            return btnElemento;
        }

        function obtenIcono(etiqueta){
            if(etiqueta.toUpperCase() == 'OK')
                return $('#popupG_OK').clone().removeClass('popupG_icono');
            else if(etiqueta.toUpperCase() == 'MSJ')
                return $('#popupG_MSJ').clone().removeClass('popupG_icono');
            else
                return '';
        }

        function obtenInputs(){
            return $('#popupG_Campos').clone(true).removeClass('popupG_icono').attr('id', '');
        }

        function cerrarPopupG(btnCerrar){
            var popupG = btnCerrar.parent();
            popupG.parent().animate({top: '-100%'}, 1000, function(){
                EstablecePopupG(popupG, {titulo: '', contenido: '', botones: '', icono: ''});
                popupG.hide();
            });
        }

        function EstablecePopupG(popup, elementos){
            var llave;
            for(var i in elementos){
                llave = i.toLowerCase();
                $('.' + llave, popup).html(elementos[i]);
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