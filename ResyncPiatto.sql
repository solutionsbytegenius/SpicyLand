USE [SpicyLand]
GO
/****** Object:  StoredProcedure [dbo].[sp_ResyncPiatto]    Script Date: 03/06/2024 09:38:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[sp_ResyncPiatto]
    @PaninoID UNIQUEIDENTIFIER,
    @Nome NVARCHAR(100),
    @Prezzo DECIMAL(10, 2),
    @PaninoMese BIT,
    @PathImage NVARCHAR(100),
    @InMenu BIT,
    @Descrizione NVARCHAR(MAX),
    @Categoria NVARCHAR(50),
    @Add BIT
AS
BEGIN
    IF @Add = 1 -- Inserimento di un nuovo record
    BEGIN
        INSERT INTO Panino (PaninoID, Nome, Prezzo, PaninoMese, PathImage, InMenu, Descrizione, Categoria,DataCreazione)
        VALUES (Newid(), @Nome, @Prezzo, @PaninoMese, @PathImage, @InMenu, @Descrizione, @Categoria,GETDATE());
    END
    ELSE -- Modifica di un record esistente
    BEGIN
        UPDATE Panino
        SET Nome = @Nome,
            Prezzo = @Prezzo,
            PaninoMese = @PaninoMese,
            PathImage = case when ISNULL(@PathImage,'') = '' then PathImage else @PathImage end,
            InMenu = @InMenu,
            Descrizione = @Descrizione,
            Categoria = @Categoria
        WHERE PaninoID = @PaninoID;
    END
END
