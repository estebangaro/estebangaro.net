        $(function() {
            ajustaResize();
            enlazaEvts();
            $(window).resize(ajustaResize);
        });

        function resetFranjaOp(seleccionado) {
            if (seleccionado == undefined) {
                if ($('#franjaSelector').css('display') != 'block')
                    $('.franjaOpcion').css({
                        'background-color': 'initial',
                        'color': 'white'
                    });
                $('.flechaFranjaOp').css({
                    'display': 'none'
                });
            } else {
                if ($('#franjaSelector').css('display') != 'block') {
                    seleccionado.css({
                        'background-color': 'whitesmoke',
                        'color': 'cornflowerblue'
                    });
                    seleccionado.children('.flechaFranjaOp').css({
                        'display': 'block'
                    });
                } else
                    $('.franjaOpcion').css({
                        'visibility': 'hidden',
                        'opacity': '0'
                    });

                $('#franjaSelector > span').text(seleccionado.text());
            }
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
            $(window).scroll();
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