        $(function() {
            $('.EnzdoCodigoFuente').click(function() {
                var padreContenedor = $(this).parents('div.codigoFuente');
                var visibles = obtenVisibles(padreContenedor);
                var codigoseleccionado =
                    $('.CdrCodigoFuente > .ctdrCodigo:nth-child(' +
                        parseInt($(this).attr('indice')) + ')', padreContenedor);
                muestraCodigoSeleccionado(visibles, false);
                muestraCodigoSeleccionado({
                    encabezado: $(this),
                    codigo: codigoseleccionado
                }, true);
            });

            $('.descargarCodigo').click(function () {
                var padre = $(this).parent();
                var visibles = obtenVisibles($(this).parents('div.codigoFuente'));
                padre.attr({
                        'href': '/Articulo/DescargarCodigo?archivo=' +
                        visibles.encabezado.text() + '&indice=' +
                        visibles.encabezado.attr('indice') + '&carpeta=' +
                        padre.next().val()
                });
            });
        });

        function muestraCodigoSeleccionado(elementos, estado) {
            elementos.encabezado.css({
                'background-color': estado ? 'white' : 'transparent',
                'color': estado ? 'black' : 'purple'
            });
            elementos.codigo.css({
                'visibility': estado ? 'visible' : 'hidden',
                'opacity': estado ? '1' : '0'
            });
        }

        function obtenVisibles(padre) {
            var encabezado, codigo;
            $('.EnzdoCodigoFuente', padre).each(function() {
                var color = $(this).css('background-color');
                if (color != 'transparent' &&
                    color != 'rgba(0, 0, 0, 0)')
                    encabezado = $(this);
            });
            codigo = $('.ctdrCodigo:visible', padre);

            return {
                encabezado: encabezado,
                codigo: codigo
            };
        }