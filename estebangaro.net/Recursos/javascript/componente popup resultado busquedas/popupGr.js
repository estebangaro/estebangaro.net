        $(function(){
            centraPopupR();
            $('#popupGr .cerrar').click(function(){
                cerrarPopupR($(this));
            });
        });

        function cerrarPopupR(btnCerrar){
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

        function mostrarPopupR(resultados){
            $('#popupGr table > tbody').html('');
            $('#popupGr .Contenido > p')
                .html('Se han encontrado ' + resultados.lenght + ' articulo(s) que pueden interesarte.');
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

            $('#popupGr').parent().css('left', '0');
        }