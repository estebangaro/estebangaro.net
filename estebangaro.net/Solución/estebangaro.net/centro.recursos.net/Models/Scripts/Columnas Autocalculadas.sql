/*
	Columnas autocalculadas [Vista].[Articulos]
*/

GO
ALTER TABLE Vista.Articulos  
ADD CONSTRAINT default_articulos DEFAULT GETDATE() FOR [Fecha Creacion];
GO
GO
CREATE TRIGGER actualizaFechaMod_Articulos
ON Vista.Articulos
AFTER UPDATE AS  
BEGIN
	UPDATE Modificados
	SET Modificados.[Fecha Modificacion] = GETDATE()
	FROM Vista.Articulos Modificados INNER JOIN
		inserted ON Modificados.URI = inserted.URI;
END;
GO

/*
	Columnas autocalculadas [Vista].[Comentarios]
*/

GO
ALTER TABLE [Vista].[Comentarios]
ADD CONSTRAINT default_comentarios DEFAULT GETDATE() FOR [Fecha Creacion];
GO
GO
CREATE TRIGGER actualizaFechaMod_Comentarios
ON Vista.Comentarios
AFTER UPDATE AS  
BEGIN
	UPDATE Modificados
	SET Modificados.[Fecha Modificacion] = GETDATE()
	FROM [Vista].[Comentarios] Modificados INNER JOIN
		inserted ON Modificados.Id = inserted.Id;
END;
GO

/*
	Columnas autocalculadas [Slider].[Botones]
*/

GO
ALTER TABLE [Slider].[Botones]
ADD CONSTRAINT default_botones DEFAULT GETDATE() FOR [Fecha Creacion];
GO
GO
CREATE TRIGGER actualizaFechaMod_Botones
ON [Slider].[Botones]
AFTER UPDATE AS  
BEGIN
	UPDATE Modificados
	SET Modificados.[Fecha Modificacion] = GETDATE()
	FROM [Slider].[Botones] Modificados INNER JOIN
		inserted ON Modificados.Id = inserted.Id;
END;
GO

/*
	Columnas autocalculadas [Slider].[Avisos]
*/

GO
ALTER TABLE [Slider].[Avisos]
ADD CONSTRAINT default_avisos DEFAULT GETDATE() FOR [Fecha Creacion]
GO
GO
CREATE TRIGGER actualizaFechaMod_Avisos
ON [Slider].[Avisos]
AFTER UPDATE AS  
BEGIN
	UPDATE Modificados
	SET Modificados.[Fecha Modificacion] = GETDATE()
	FROM [Slider].[Avisos] Modificados INNER JOIN
		inserted ON Modificados.Id = inserted.Id;
END;
GO

/*
	Columnas autocalculadas [SeccionPrincipal].[Noticias]
*/

GO
ALTER TABLE [SeccionPrincipal].[Noticias]
ADD CONSTRAINT default_noticias DEFAULT GETDATE() FOR [Fecha Creacion];
GO
GO
CREATE TRIGGER actualizaFechaMod_Noticias
ON [SeccionPrincipal].[Noticias]
AFTER UPDATE AS  
BEGIN
	UPDATE Modificados
	SET Modificados.[Fecha Modificacion] = GETDATE()
	FROM [SeccionPrincipal].[Noticias] Modificados INNER JOIN
		inserted ON Modificados.Id = inserted.Id;
END;
GO

/*
	Columnas autocalculadas [SeccionPrincipal].[Multimedia]
*/

GO
ALTER TABLE [SeccionPrincipal].[Multimedia]
ADD CONSTRAINT default_multimedia DEFAULT GETDATE() FOR [Fecha Creacion];
GO
GO
CREATE TRIGGER actualizaFechaMod_Multimedia
ON [SeccionPrincipal].[Multimedia]
AFTER UPDATE AS  
BEGIN
	UPDATE Modificados
	SET Modificados.[Fecha Modificacion] = GETDATE()
	FROM [SeccionPrincipal].[Multimedia] Modificados INNER JOIN
		inserted ON Modificados.MultimediaId = inserted.MultimediaId;
END;
GO

/*
	Columnas autocalculadas [Menu].[Opciones]
*/

GO
ALTER TABLE [Menu].[Opciones]
ADD CONSTRAINT default_opciones DEFAULT GETDATE() FOR [Fecha Creacion];
GO
GO
CREATE TRIGGER actualizaFechaMod_Opciones
ON [Menu].[Opciones]
AFTER UPDATE AS  
BEGIN
	UPDATE Modificados
	SET Modificados.[Fecha Modificacion] = GETDATE()
	FROM [Menu].[Opciones] Modificados INNER JOIN
		inserted ON Modificados.OpcionMenuId = inserted.OpcionMenuId;
END;
GO

/*
	Columnas autocalculadas [General].[Puestos]
*/

GO
ALTER TABLE [General].[Puestos]
ADD CONSTRAINT default_puestos DEFAULT GETDATE() FOR [Fecha Creacion];
GO
GO
CREATE TRIGGER actualizaFechaMod_Puestos
ON [General].[Puestos]
AFTER UPDATE AS  
BEGIN
	UPDATE Modificados
	SET Modificados.[Fecha Modificacion] = GETDATE()
	FROM [General].[Puestos] Modificados INNER JOIN
		inserted ON Modificados.Id = inserted.Id;
END;
GO

/*
	Columnas autocalculadas [General].[Autores]
*/

GO
ALTER TABLE [General].[Autores]
ADD CONSTRAINT default_autores DEFAULT GETDATE() FOR [Fecha Creacion];
GO
GO
CREATE TRIGGER actualizaFechaMod_Autores
ON [General].[Autores]
AFTER UPDATE AS  
BEGIN
	UPDATE Modificados
	SET Modificados.[Fecha Modificacion] = GETDATE()
	FROM [General].[Autores] Modificados INNER JOIN
		inserted ON Modificados.Id = inserted.Id;
END;
GO

/*
	Columnas autocalculadas [Configuracion].[CategoriasPalabrasCodigo]
*/

GO
ALTER TABLE [Configuracion].[CategoriasPalabrasCodigo]
ADD CONSTRAINT default_categoriaspalabrascodigo DEFAULT GETDATE() FOR [Fecha Creacion];
GO
GO
CREATE TRIGGER actualizaFechaMod_CategoriasPalabrasCodigo
ON [Configuracion].[CategoriasPalabrasCodigo]
AFTER UPDATE AS  
BEGIN
	UPDATE Modificados
	SET Modificados.[Fecha Modificacion] = GETDATE()
	FROM [Configuracion].[CategoriasPalabrasCodigo] Modificados INNER JOIN
		inserted ON Modificados.Id = inserted.Id;
END;
GO

/*
	Columnas autocalculadas [Configuracion].[CategoriasPalabrasCodigo]
*/

GO
ALTER TABLE [Configuracion].[PalabrasCodigo]
ADD CONSTRAINT default_PalabrasCodigo DEFAULT GETDATE() FOR [Fecha Creacion];
GO
GO
CREATE TRIGGER actualizaFechaMod_PalabrasCodigo
ON [Configuracion].[PalabrasCodigo]
AFTER UPDATE AS  
BEGIN
	UPDATE Modificados
	SET Modificados.[Fecha Modificacion] = GETDATE()
	FROM [Configuracion].[PalabrasCodigo] Modificados INNER JOIN
		inserted ON Modificados.Id = inserted.Id;
END;
GO