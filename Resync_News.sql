USE [SpicyLand]
GO
/****** Object:  StoredProcedure [dbo].[sp_ResyncNews]    Script Date: 03/06/2024 09:38:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER   PROCEDURE [dbo].[sp_ResyncNews]
    @NewsID UNIQUEIDENTIFIER,
    @Titolo NVARCHAR(100),
    @Corpo NVARCHAR(MAX),
    @Occhiello NVARCHAR(100),
    @Visibile BIT,
    @PathImage NVARCHAR(100),
    @Scaduta BIT,
    @PrimoPiano BIT,
    @Add BIT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        IF @Add = 1 -- Inserimento di un nuovo record
        BEGIN
            INSERT INTO News (NewsID, TitoloNotizia, CorpoNotizia, Occhiello, Visibile, ImmaginePath, Scaduta, InPrimoPiano, DataInserimento)
            VALUES (NEWID(), @Titolo, @Corpo, @Occhiello, @Visibile, @PathImage, @Scaduta, @PrimoPiano, GETDATE());
        END
        ELSE -- Modifica di un record esistente
        BEGIN
            UPDATE News
            SET TitoloNotizia = @Titolo,
                CorpoNotizia = @Corpo,
                Occhiello = @Occhiello,
                Visibile = @Visibile,
                ImmaginePath = case when ISNULL(@PathImage,'') = '' then ImmaginePath else @PathImage end,
                Scaduta = @Scaduta,
                ScadenzaNotizia = CASE WHEN @Scaduta = 1 THEN GETDATE() ELSE NULL END,
                InPrimoPiano = @PrimoPiano
            WHERE NewsID = @NewsID;
        END
    END TRY
    BEGIN CATCH
        -- Registra l'errore nel log
        DECLARE @ErrorMessage NVARCHAR(MAX) = ERROR_MESSAGE();
        DECLARE @ErrorSeverity INT = ERROR_SEVERITY();
        DECLARE @ErrorState INT = ERROR_STATE();

        RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH
END
