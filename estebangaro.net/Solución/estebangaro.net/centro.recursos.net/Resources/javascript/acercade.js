        $(function() {
            /*Declaración e inicialización de variables "globales"*/
            var tabPropiedadesSeleccionado = {
                'background-color': 'rgb(0, 0, 0)',
                'border-bottom-width': '2px',
                'border-bottom-color': 'rgb(0, 0, 0)'
            };
            var tabPropiedadesDeseleccionado = {
                'background-color': 'inherit',
                'border-bottom-width': '0',
                'border-bottom-color': 'inherit',
                'color': 'inherit'
            };
            var seccionPropiedadesSeleccionada = {
                'visibility': 'visible',
                'opacity': '1'
            };
            var seccionPropiedadesDeseleccionada = {
                'visibility': 'hidden',
                'opacity': '0'
            };
            var seccionAcercaDeSeleccionada = {
                'top': '0px',
                'left': '0px'
            }

            /*Invocación predeterminadas de funciones.*/
            AjustaAcercaDe();
            enlazaEventosAcercaDe();

            /*Declaración y definición de funciones*/
            function enlazaEventosAcercaDe() {
                $('#AcercaDeG > .seccion > .tabs > .tab').click(function() {
                    refrescaTabs($(this));
                });
                $('div[class="botonH Inf"]').on('click', function() {
                    cambiaSeccion($(this));
                });
                $('.botonV').on('click', function() {
                    BtnVertical_Clic($(this));
                });
                $('#agrupadorOpciones>div>nav:first-child').click(function () {
                    $('#FondoAcercaDeG')
                        .css('top', '0');
                });
                $('#AcercaDeG .cerrar').click(function () {
                    $('#FondoAcercaDeG')
                        .css('top', '-100%');
                });
                $('#frmContactoGr > span.botonenviar').click(function () {
                    if (ValidaCamposContacto()) {
                        GuardarComentario();
                    }
                });
            }

            function ValidaCamposContacto() {
                var estadoGeneral = true;
                var validacion;
                $('#frmContactoGr .campoTxt, #frmContactoGr > textarea').each(function () {
                    if (estadoGeneral) {
                        validacion = true;
                        var campoTxt = $(this);
                        var valorCampoTxt = campoTxt.val();
                        var idCampoTxt = campoTxt.attr('id');
                        var configValidacion = ObtenConfigValidacionAcercaD(idCampoTxt);
                        if (configValidacion != undefined) {
                            configValidacion.Elemento = campoTxt;
                            configValidacion.Valor = valorCampoTxt;
                            validacion = validaElementoPopupGv(configValidacion);
                        }
                        estadoGeneral = estadoGeneral && validacion;
                    }
                });

                return estadoGeneral;
            }

            function GuardarComentario() {
                var comentarioAcercaDe = {};
                $('#frmContactoGr .campoTxt, #frmContactoGr > textarea').each(function () {
                    var campoTxt = $(this);
                    var valorCampoTxt = campoTxt.val();
                    var idCampoTxt = campoTxt.attr('id');
                    comentarioAcercaDe[idCampoTxt] = valorCampoTxt;
                });

                GuardarComentarioBD(comentarioAcercaDe);
            }

            function LimpiaComentariosAcercaD() {
                $('#frmContactoGr .campoTxt, #frmContactoGr > textarea').each(function () {
                    $(this).val('');
                });
            }

            function GuardarComentarioBD(comentario) {
                mostrarEsperaPopUpG(true, '#frmContactoGr');
                $.ajax({
                    url: '/api/Comentario',
                    type: 'GaroComentariosAcercaD',
                    dataType: 'json',
                    data: JSON.stringify(comentario),
                    contentType: 'application/json; charset=utf-8',
                    success: function (data) {
                        mostrarEsperaPopUpG(false, '#frmContactoGr');
                        muestraPopUpGAcercaDe('OK', 'Enviar Mensaje',
                            'Hemos recibido tu mensaje, en breve nos comunicaremos contigo.',
                            obtenBotonPopUpGAcercaDe());
                    },
                    error: function () {
                        mostrarEsperaPopUpG(false, '#frmContactoGr');
                        muestraPopUpGAcercaDe('MSJ', 'Enviar Mensaje',
                            'Ha fallado el envío del mensaje, favor de intentarlo mas tarde.',
                            obtenBotonPopUpGAcercaDe());
                    }
                });
            }

            function obtenBotonPopUpGAcercaDe() {
                var boton = $('<span/>').text('Aceptar')
                    .addClass('habilitado')
                    .click(function () {
                        $('#popupG_preguntas > .cerrar').click();
                        LimpiaComentariosAcercaD();
                    });

                return $('<div class="boton"></div>').append(boton);
            }

            function muestraPopUpGAcercaDe(Icono, Titulo, Contenido, Botones) {
                mostrarPopupG({
                    Icono: Icono,
                    Titulo: $('<span/>').text(Titulo),
                    Contenido: $('<span/>').text(configuracion.Contenido),
                    Botones: Botones
                });
            }

            function ObtenConfigValidacionAcercaD(idCampoTxt) {
                var configObj;

                switch (idCampoTxt) {
                    case 'Nombre':
                        configObj = {
                            EsRequerido: {
                                Valor: true,
                                Titulo: 'Campo Obligatorio',
                                Descripcion: 'Favor de introducir tu nombre'
                            },
                            LongitudMinima: {
                                Valor: 2,
                                Titulo: '¡Atención!',
                                Descripcion: 'La longitud mínina es de 2 caracteres'
                            },
                            LongitudMaxima: {
                                Valor: 60,
                                Titulo: '¡Atención!',
                                Descripcion: 'La longitud máxima es de 60 caracteres'
                            },
                            Patron: {
                                Valor: /^[A-Z a-z0-9_-ñ\.\wáéíóú@]+$/,
                                Titulo: 'Verificar Formato',
                                Descripcion: 'Caracteres permitidos: A-Z a-z 0-9_-ñ.áéíóú@'
                            }
                        };
                        break;
                    case 'Email':
                        configObj = {
                            EsRequerido: {
                                Valor: true,
                                Titulo: 'Campo Obligatorio',
                                Descripcion: 'Favor de introducir una cuenta de correo electrónico'
                            },
                            LongitudMinima: {
                                Valor: 5,
                                Titulo: '¡Atención!',
                                Descripcion: 'La longitud mínina es de 5 caracteres'
                            },
                            LongitudMaxima: {
                                Valor: 255,
                                Titulo: '¡Atención!',
                                Descripcion: 'La longitud máxima es de 255 caracteres'
                            },
                            Patron: {
                                Valor: /^[\.A-Za-z0-9_-]+@[A-Za-z0-9_-]+(\.[A-Za-z0-9_-]+)*$/,
                                Titulo: 'Verificar Formato',
                                Descripcion: 'Favor de introducir una cuenta de correo electrónico válida'
                            }
                        };
                        break;
                    case 'Ciudad':
                        configObj = {
                            EsRequerido: {
                                Valor: true,
                                Titulo: 'Campo Obligatorio',
                                Descripcion: 'Favor de introducir tu ciudad'
                            },
                            LongitudMinima: {
                                Valor: 2,
                                Titulo: '¡Atención!',
                                Descripcion: 'La longitud mínina es de 2 caracteres'
                            },
                            LongitudMaxima: {
                                Valor: 50,
                                Titulo: '¡Atención!',
                                Descripcion: 'La longitud máxima es de 50 caracteres'
                            },
                            Patron: {
                                Valor: /^[A-Z a-z0-9ñ\.\wáéíóú]+$/,
                                Titulo: 'Verificar Formato',
                                Descripcion: 'Caracteres permitidos: A-Z a-z 0-9ñ.áéíóú'
                            }
                        };
                        break;
                    case 'Asuntomsj':
                        configObj = {
                            EsRequerido: {
                                Valor: true,
                                Titulo: 'Campo Obligatorio',
                                Descripcion: 'Favor de introducir tu ciudad'
                            },
                            LongitudMinima: {
                                Valor: 2,
                                Titulo: '¡Atención!',
                                Descripcion: 'La longitud mínina es de 2 caracteres'
                            },
                            LongitudMaxima: {
                                Valor: 30,
                                Titulo: '¡Atención!',
                                Descripcion: 'La longitud máxima es de 30 caracteres'
                            },
                            Patron: {
                                Valor: /^[A-Z a-z0-9ñ\.\wáéíóú]+$/,
                                Titulo: 'Verificar Formato',
                                Descripcion: 'Caracteres permitidos: A-Z a-z 0-9ñ.áéíóú'
                            }
                        };
                        break;
                    case 'Contenidomsj':
                        configObj = {
                            EsRequerido: {
                                Valor: true,
                                Titulo: 'Campo Obligatorio',
                                Descripcion: 'Favor de introducir un comentario'
                            },
                            LongitudMinima: {
                                Valor: 2,
                                Titulo: '¡Atención!',
                                Descripcion: 'La longitud mínina es de 2 caracteres'
                            },
                            LongitudMaxima: {
                                Valor: 500,
                                Titulo: '¡Atención!',
                                Descripcion: 'La longitud máxima es de 500 caracteres'
                            },
                            Patron: {
                                Valor: /^[A-Z a-z0-9_-ñ\.\wáéíóú,*\(\)\[\]\{\}\"\!\¡\'\;\+\-\#\$\%\&\/\¿\?\=\:@]+$/,
                                Titulo: 'Verificar Formato',
                                Descripcion: 'Caracteres permitidos: A-Z a-z0-9_-ñ.áéíóú,*()[]{}"!¡\';+-#$%&/¿?=:@'
                            }
                        };
                        break;
                    default:
                        configObj = undefined;
                }
                return configObj;
            }

            function AjustaAcercaDe() {
                ajustaEtiquetaBtnVertical();
                ajustaAltoAcercaDegaRoNET();
                centrarTabs();
                centrarAcercaDe();
                actualizaEtiquetaBtnVertical();
                ajustaEtiquetaAcercaDe($('div[class="botonH Inf"]'),
                    $('#AcercaDeG > .seccion').eq(1));
                desplazaSeccion2AcercaDe();
            }

            function ajustaAltoAcercaDegaRoNET(){
                var _alto = $('#acercaDegaRoNET').outerHeight() / 2;
                var _altoFondo = $('#acercaDegaRoNET > div.fondoAD').outerHeight() / 2;
                $('#acercaDegaRoNET > div.fondoAD').css('margin-top', (1 * (_alto - _altoFondo)) + 'px')
            }

            function ajustaEtiquetaBtnVertical() {
                $('.etiquetaV').each(function() {
                    var altoEtiqueta = $(this).outerHeight() * -1;
                    $(this).css('margin-top', (altoEtiqueta / 2) + 'px');
                });
            }

            function BtnVertical_Clic(btn) {
                var noTabSel = parseInt(btn.attr('id').substring(btn.attr('id').indexOf('_') + 1));
                var seccionSeleccionada = obtenElementoSeleccionado('top',
                    seccionAcercaDeSeleccionada['top'], $('#AcercaDeG > .seccion'));
                var tabsSeccionSeleccionada = $('.tabs > .tab', seccionSeleccionada);
                tabsSeccionSeleccionada.eq(noTabSel).click();
            }

            function actualizaEtiquetaBtnVertical() {
                var seccionSeleccionada = obtenElementoSeleccionado('top',
                    seccionAcercaDeSeleccionada['top'], $('#AcercaDeG > .seccion'));
                var tabsSeccionSeleccionada = $('.tabs > .tab', seccionSeleccionada);
                var tabSeleccionada = obtenElementoSeleccionado('background-color',
                    tabPropiedadesSeleccionado['background-color'],
                    tabsSeccionSeleccionada);
                var noTab = tabSeleccionada.index();
                var tabPrevia = tabsSeccionSeleccionada.eq(
                    (noTab - 1) < 0 ? tabsSeccionSeleccionada.length - 1 : noTab - 1
                );
                var tabSiguiente = tabsSeccionSeleccionada.eq(
                    (noTab + 1) == tabsSeccionSeleccionada.length ? 0 : noTab + 1
                );
                $('#AcercaDeG div[class="botonV Izq"] > div.etiquetaV').
                html(convierteTextoASpan(tabPrevia.text())).parent().
                attr('id', 'etiquetaV_' + tabPrevia.index());
                $('#AcercaDeG div[class="botonV Der"] > div.etiquetaV').
                html(convierteTextoASpan(tabSiguiente.text())).parent().
                attr('id', 'etiquetaV_' + tabSiguiente.index());
                ajustaEtiquetaBtnVertical();
            }

            function convierteTextoASpan(texto) {
                var htmlSpans = '';
                for (var i = 0; i < texto.length; i++)
                    if (texto[i] != ' ')
                        htmlSpans += '<span>' + texto[i] + '</span>';
                    else
                        htmlSpans += '<span style="color:transparent">' + 'a' + '</span>';

                return htmlSpans;
            }

            function desplazaSeccion2AcercaDe() {
                var seccion2 = $('#AcercaDeG > .seccion').eq(1);
                var altoSeccion = seccion2.outerHeight();
                seccion2.css('top', (altoSeccion * -1) + 'px');
            }

            function ajustaEtiquetaAcercaDe(boton, siguienteSeccion) {
                $('span', boton).text(siguienteSeccion.attr('id').replace(/_/g, " "));
                /*
                boton.html(
                    '<p><img src="../../imagenes/seccion.png"/></p>' +
                    '<span>' +
                    siguienteSeccion.attr('id').replace(/_/g, " ") +
                    '</span>'
                );*/
            }

            function centrarTabs() {
                $('#AcercaDeG > .seccion > .tabs').each(function(index, elem) {
                    var anchoTabs = 0;
                    var anchoAcercaDe = $('#AcercaDeG').outerWidth();
                    $(this).children().each(function() {
                        anchoTabs += $(this).outerWidth();
                    });
                    $(this).css('margin-left', ((anchoAcercaDe / 2) - (anchoTabs / 2)) + 'px');
                });
            }

            function centrarAcercaDe() {
                var alto = $('#AcercaDeG').outerHeight() * -1;
                var ancho = $('#AcercaDeG').outerWidth() * -1;
                $('AcercaDeG').css({
                    'margin-top': (alto / 2) + 'px',
                    'margin-left': (ancho / 2) + 'px'
                });
            }

            function refrescaTabs(tabElemento) {
                var seccionElemento = obtenSeccionXTab(tabElemento);
                var tabSeleccionada = obtenElementoSeleccionado('background-color',
                    tabPropiedadesSeleccionado['background-color'],
                    tabElemento.parent().children());

                if (tabElemento.text() != tabSeleccionada.text()) {
                    var seccionSeleccionada = obtenSeccionXTab(tabSeleccionada);
                    tabPropiedadesSeleccionado['background-color'] =
                        tabPropiedadesSeleccionado['border-bottom-color'] =
                        seccionElemento.css('background-color');

                    seccionElemento.on("transitionend",
                        function(e) {
                            actualizaEtiquetaBtnVertical();
                            $(this).off("transitionend");
                        });
                    tabSeleccionada.attr('style', '');
                    estilaElemento(tabPropiedadesDeseleccionado, tabSeleccionada);
                    estilaElemento(seccionPropiedadesDeseleccionada, seccionSeleccionada);
                    tabElemento.attr('style', '');
                    estilaElemento(tabPropiedadesSeleccionado, tabElemento);
                    estilaElemento(seccionPropiedadesSeleccionada, seccionElemento);
                }
            }

            function estilaElemento(Propiedades, Elemento,
                funcionPreAnimacionCallback = undefined,
                funcionPostAnimacionCallback = undefined, animaEstilo = false) {
                if (!animaEstilo)
                    Elemento.css(Propiedades);
                else if (funcionPreAnimacionCallback != undefined && funcionPostAnimacionCallback != undefined) {
                    funcionPreAnimacionCallback();
                    Elemento.animate(Propiedades, 1000, "swing", function() {
                        funcionPostAnimacionCallback();
                    });
                } else
                    Elemento.animate(Propiedades, 1000, "swing");
            }

            function obtenElementoSeleccionado(propiedad, valor, tabs, esIgualdadOperador = true) {
                var seleccionado = undefined;
                tabs.each(function() {
                    if (seleccionado == undefined) {
                        var valorProp = $(this).css(propiedad);
                        if (esIgualdadOperador ? valorProp == valor : valorProp != valor)
                            seleccionado = $(this);
                    }
                });
                return seleccionado;
            }

            function obtenSeccionXTab(tab) {
                var noTab = tab.index();
                return tab.parent().next().children().eq(noTab);
            }

            function cambiaSeccion(botonSeccion) {
                var idSeccionElemento = $('span', botonSeccion).text().replace(/ /g, "_");
                var acercaDeSeccionElemento = $('#' + idSeccionElemento);
                var acercaDeSeccionSeleccionada = obtenElementoSeleccionado('top',
                    seccionAcercaDeSeleccionada['top'], $('#AcercaDeG > .seccion'));
                var altoSeccionSeleccionada = acercaDeSeccionSeleccionada.outerHeight();

                estilaElemento({
                    'top': (acercaDeSeccionSeleccionada.index() == 0 ? altoSeccionSeleccionada : altoSeccionSeleccionada * -1) + 'px',
                    'left': '0'
                }, acercaDeSeccionSeleccionada, undefined, undefined, true);
                estilaElemento(seccionAcercaDeSeleccionada, acercaDeSeccionElemento,
                    function() { // Pre animación
                        $('div[class="botonH Inf"]').off('click');
                    },
                    function() { // Post animación
                        actualizaEtiquetaBtnVertical();
                        ajustaEtiquetaAcercaDe(botonSeccion, acercaDeSeccionSeleccionada);
                        $('div[class="botonH Inf"]').on('click', function() {
                            cambiaSeccion($(this));
                        });
                    }, true);

                var tabPrevSeleccionada = obtenElementoSeleccionado(
                    'background-color', 'rgba(0, 0, 0, 0)',
                    $('.tabs > .tab', acercaDeSeccionElemento), false);
                tabPropiedadesSeleccionado['background-color'] =
                    tabPropiedadesSeleccionado['border-bottom-color'] =
                    tabPrevSeleccionada.css('background-color');

                refrescaTabs($('.tabs > .tab:first-child', acercaDeSeccionElemento));
            }
        });