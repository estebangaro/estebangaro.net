        $(function() {
            ajustaResize();
            enlazaEvts();
            $(window).resize(ajustaResize);
            ajustarDimensionCtdrs();
        });

        function resetFranjaOp(seleccionado) {
            if (seleccionado == undefined) {
                if ($('#franjaSelector').css('display') != 'block') {
                    $('.franjaOpcion').css({
                        'background-color': 'initial',
                        'color': 'white'
                    });
                    $('.franjaOpcion').css({
                        'visibility': 'visible',
                        'opacity': '1'
                    });
                } else {
                    $('.franjaOpcion').css({
                        'color': 'white',
                        'background-color': 'rgba(0, 255, 255, 0.5)'
                    });
                    $('.franjaOpcion').css({
                        'visibility': 'hidden',
                        'opacity': '0'
                    });
                }
                $('.flechaFranjaOp').css({
                    'display': 'none'
                });
            } else {
                if ($('#franjaSelector').css('display') != 'block')
                    seleccionaFranjaOpcion(seleccionado);
                else
                    seleccionaFranjaOpcion(seleccionado, false);

                $('#franjaSelector > span').text(seleccionado.text());
            }
        }

        function seleccionaFranjaOpcion(seleccionado, modoNormal = true) {
            if (modoNormal) {
                seleccionado.css({
                    'background-color': 'whitesmoke',
                    'color': 'cornflowerblue'
                });
                seleccionado.children('.flechaFranjaOp').css({
                    'display': 'block'
                });
            } else {
                seleccionado.css({
                    'color': 'yellow'
                });
                $('.franjaOpcion').css({
                    'visibility': 'hidden',
                    'opacity': '0'
                });
            }
        }

        function ajustarDimensionCtdrs() {
            var opcionesMultimedia = parseInt($('#numeroOpcionesMult').val());
            $('#ctdrMultimedia').css('width', (opcionesMultimedia * 100) + '%');
            $('#ctdrMultimedia>.ctdMultimedia').css('width', (100/opcionesMultimedia).toFixed(2) + '%');
        }

        function enlazaEvts() {
            $('.franjaOpcion').click(function() {
                resetFranjaOp();
                resetFranjaOp($(this));
                $('#ctdrMultimedia').css({
                    'left': (parseInt($(this).attr('id').replace("op_", "")) * -100) + '%'
                });
            });

            $('#franjaSelector').click(function() {
                if ($(this).css('display') == 'block') {
                    if ($('.franjaOpcion').css('opacity') == '0')
                        $('.franjaOpcion').css({
                            'visibility': 'visible',
                            'opacity': '1'
                        });
                    else
                        $('.franjaOpcion').css({
                            'visibility': 'hidden',
                            'opacity': '0'
                        });
                }
            });

            $(window).scroll(function() {
                if ($('#ctdrCampoBusqueda').css('display') != 'none') {
                    var posYseccion3 = parseInt($('#seccion3Principal').css('top').replace('px', ''));
                    var altoSeccion3 = $('#seccion3Principal').height();
                    var altoMenu = $('#fondoMenu').innerHeight();
                    var posicionScroll = $(document).scrollTop();

                    if (posicionScroll >= (posYseccion3 - altoMenu) && posicionScroll <= posYseccion3) {
                        var topMenu = posicionScroll - (posYseccion3 - altoMenu);
                        $('#fondoMenu').css({ 'top': (-1 * topMenu) + 'px' });
                    } else if (posicionScroll > posYseccion3 && posicionScroll <= (posYseccion3 + altoSeccion3)) {
                        if (posicionScroll >= (posYseccion3 + altoSeccion3 - altoMenu)) {
                            var topMenu = posicionScroll - posYseccion3 - altoSeccion3;
                            $('#fondoMenu').css({ 'top': topMenu + 'px' });
                        } else
                            $('#fondoMenu').css({ 'top': (-1 * altoMenu) + 'px' });
                    } else
                        $('#fondoMenu').css({ 'top': '0' });
                }
            });
        }

        function ajustaResize() {
            ajustaAltoMsjMultimedia();
            ajustaDimensionFlecha();
            ajustaSeleccion();
            $(window).scroll();
        }

        function ajustaSeleccion() {
            resetFranjaOp();
            $('.franjaOpcion > span').each(function() {
                if ($(this).text().trim() == $('#franjaSelector > span').text().trim())
                    resetFranjaOp($(this).parent());
            });
        }

        function ajustaDimensionFlecha() {
            var tamFranjaOp = ($('.ctdrFranjaOpcion').height() -
                $('.franjaOpcion > span').height()) / 2;
            $('.flechaFranjaOp')
                .height(tamFranjaOp)
                .width(tamFranjaOp)
                .css({
                    'margin-left': (-tamFranjaOp / 2) + 'px',
                    'bottom': (-tamFranjaOp / 2) + 'px'
                });
        }

        function ajustaAltoMsjMultimedia() {
            var altoPadre = $('.ctdMultimedia').height() / 2;
            $('.msjMultimedia').each(function() {
                $(this).css({
                    'margin-top': (altoPadre - $(this).height() / 2) + 'px'
                });
            });
        }