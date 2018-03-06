        $(function(){
            $('#agrupadorOpciones > div').append($('<nav></nav>').html($('<span></span>').text('Buscar')));
            $('#popupGr .cerrar').click(function(){
                cerrarPopupR($(this));
            });
            $('#agrupadorOpciones>div>nav:last-child').click(function(){
                mostrarPopupRBusqueda();
            });
            $(window).resize(function(){
                centraPopupR();
            });
        });

        function enlazaEvtsArt(){
            $('#popupGr > .Contenido > .ResultadosBusqueda > tbody > tr').click(function(){
                $('a', $(this)).get(0).click();
            });

            $('#popupGr > .Contenido > .ResultadosBusqueda > tbody > tr a').click(function(e){
                e.stopPropagation();
            });
        }

        function cerrarPopupR(btnCerrar){
            $('body').css('overflow', 'auto');
            var popupG = btnCerrar.parent();
            popupG.parent().css({left: '-100%'});
        }

        function centraPopupR(){
            var ancho = $('#popupGr').outerWidth() / 2;
            var alto = $('#popupGr').outerHeight() / 2;
            $('#popupGr').css({
                'margin-left': (-1 * ancho) + 'px',
                'margin-top': (-1 * alto) + 'px'
            });
        }

        function mostrarPopupR(resultados, busqueda){
            $('body').css('overflow', 'hidden');
            var articulos = resultados.length;
            var link1 = resultados.length > 1? ["han"," artículos"]: ["ha"," artículo"];
            $('#popupGr table > tbody').html('');
            $('#popupGr .Contenido > p')
                .html(articulos > 0?
                    'Se ' + link1[0] + ' encontrado ' + resultados.length + link1[1] + ' que pueden interesarte.':
                    '"' + busqueda + '" no produjo ningún resultado.');
            resultados.forEach(articulo => {
                $('#popupGr table > tbody').append(
                $('<tr></tr>')
                    .append($('<td></td>').append(
                        $('<a></a>').attr('href', articulo.URI)
                            .html(articulo.Titulo)
                    )).append($('<td></td>').html(
                        articulo.Descripcion
                    )));
            });
            enlazaEvtsArt();
            centraPopupR();
            $('#popupGr').parent().css('left', '0');
        }

        function mostrarPopupRBusqueda(){
            $('body').css('overflow', 'hidden');
            $('#buscarPopupGr > input').val('');
            $('#ctdrCampoBusqueda input').val('');
            $('#popupGr table > tbody').html('');
            $('#popupGr .Contenido > p').html('');
            centraPopupR();
            $('#popupGr').parent().css('left', '0');
        }