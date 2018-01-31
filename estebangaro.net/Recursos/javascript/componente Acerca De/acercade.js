        $(function() {
            /*Declaración e inicialización de variables "globales"*/
            var tabPropiedadesSeleccionado = {
                'background-color': 'rgb(255, 0, 0)',
                'border-bottom-width': '2px',
                'border-bottom-color': 'rgb(255, 0, 0)'
            };
            var tabPropiedadesDeseleccionado = {
                'background-color': 'inherit',
                'border-bottom-width': '0',
                'border-bottom-color': 'inherit'
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
            }

            function AjustaAcercaDe() {
                centrarTabs();
                centrarAcercaDe();
                ajustaEtiquetaAcercaDe($('div[class="botonH Inf"]'),
                    $('#AcercaDeG > .seccion').eq(1));
                desplazaSeccion2AcercaDe();
            }

            function desplazaSeccion2AcercaDe() {
                var seccion2 = $('#AcercaDeG > .seccion').eq(1);
                var altoSeccion = seccion2.outerHeight();
                seccion2.css('top', (altoSeccion * -1) + 'px');
            }

            function ajustaEtiquetaAcercaDe(boton, siguienteSeccion) {
                boton.text(siguienteSeccion.attr('id').replace(/_/g, " "));
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

                    estilaElemento(tabPropiedadesDeseleccionado, tabSeleccionada);
                    estilaElemento(seccionPropiedadesDeseleccionada, seccionSeleccionada);
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
                var idSeccionElemento = botonSeccion.text().replace(/ /g, "_");
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