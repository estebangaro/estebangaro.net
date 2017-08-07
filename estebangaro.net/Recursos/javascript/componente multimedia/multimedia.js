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
        }

        function ajustaResize() {
            ajustaAltoMsjMultimedia();
            ajustaDimensionFlecha();
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