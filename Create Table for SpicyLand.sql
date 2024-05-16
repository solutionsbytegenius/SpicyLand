--CREATE TABLE [dbo].[News](
--	[NewsID] [uniqueidentifier] NOT NULL,
--	[TitoloNotizia] [nvarchar](max) NOT NULL,
--	[CorpoNotizia] [nvarchar](max) NOT NULL,
--	[Occhiello] [nvarchar](max) NOT NULL,
--	[DataInserimento] [datetime] NOT NULL,
--	[Visibile] [bit] NOT NULL,
--	[ImmaginePath] [nvarchar](max) NOT NULL,
--	[ScadenzaNotizia] [datetime] NULL,
--	[Scaduta] [bit] NOT NULL,
--	[InPrimoPiano] [bit] NOT NULL
--) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]


--CREATE TABLE [dbo].[Orario](
--	[OrarioID] [uniqueidentifier] NOT NULL,
--	[Chiuso] [bit] NOT NULL,
--	[Giorno] [nvarchar](50) NOT NULL,
--	[NumeroGiorno] [int] NOT NULL,
--	[OrarioInizio] [nvarchar](50) NOT NULL,
--	[OrarioFine] [nvarchar](50) NOT NULL,
--	[Attivo] [bit] NOT NULL
--) ON [PRIMARY]

-- Inserimento del record per Domenica
INSERT INTO Orario (OrarioID, Chiuso, Giorno, NumeroGiorno, OrarioInizio, OrarioFine, Attivo)
VALUES ('EDB883C8-C12D-470C-91D2-AD86F392B0FB', 0, 'Domenica', 7, '19:00', '00:00', 1);

-- Inserimento del record per Lunedì
INSERT INTO Orario (OrarioID, Chiuso, Giorno, NumeroGiorno, OrarioInizio, OrarioFine, Attivo)
VALUES ('5236B3F1-63DF-4153-8A2F-4529B22C6523', 0, 'Lunedì', 1, '19:00', '23:00', 1);

-- Inserimento del record per Martedì
INSERT INTO Orario (OrarioID, Chiuso, Giorno, NumeroGiorno, OrarioInizio, OrarioFine, Attivo)
VALUES ('828EB6CE-2C1F-4A52-91E7-0F4A54F5CB61', 1, 'Martedì', 2, '', '', 1);

-- Inserimento del record per Mercoledì
INSERT INTO Orario (OrarioID, Chiuso, Giorno, NumeroGiorno, OrarioInizio, OrarioFine, Attivo)
VALUES ('41D55FCB-A686-48F7-99AE-2E32FD2C2872', 0, 'Mercoledì', 3, '19:00', '23:00', 1);

-- Inserimento del record per Giovedì
INSERT INTO Orario (OrarioID, Chiuso, Giorno, NumeroGiorno, OrarioInizio, OrarioFine, Attivo)
VALUES ('F1AD6CB7-18D0-4D80-8D91-45391B4DD52E', 0, 'Giovedì', 4, '19:00', '23:00', 1);

-- Inserimento del record per Venerdì
INSERT INTO Orario (OrarioID, Chiuso, Giorno, NumeroGiorno, OrarioInizio, OrarioFine, Attivo)
VALUES ('73D72C69-7620-4E04-9FCE-0592C4760F07', 0, 'Venerdì', 5, '19:00', '23:00', 1);

-- Inserimento del record per Sabato
INSERT INTO Orario (OrarioID, Chiuso, Giorno, NumeroGiorno, OrarioInizio, OrarioFine, Attivo)
VALUES ('808E1BD8-59D3-4640-9AAC-A699D1DA53A4', 0, 'Sabato', 6, '19:00', '00:00', 1);







--CREATE TABLE [dbo].[Ordine](
--	[OrdineID] [uniqueidentifier] NOT NULL,
--	[PaninoID] [uniqueidentifier] NOT NULL,
--	[NumeroOrdine] [int] NOT NULL,
--	[Plus] [bit] NOT NULL,
--	[PrezzoFinale] [decimal](10, 2) NOT NULL,
--	[DataPrenotazione] [datetime] NOT NULL,
--	[Consegnato] [bit] NOT NULL,
--	[InLavorazione] [bit] NOT NULL,
--	[Annullato] [bit] NOT NULL,
--	[DataAnnullamento] [datetime] NOT NULL,
--	[Note] [nvarchar](max) NOT NULL,
--	[Bevanda] [nvarchar](max) NOT NULL,
--	[Cliente] [nvarchar](max) NOT NULL,
--	[Patatine] [nvarchar](max) NOT NULL
--) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

--CREATE TABLE [dbo].[Panino](
--	[PaninoID] [uniqueidentifier] NOT NULL,
--	[Nome] [nvarchar](max) NOT NULL,
--	[Descrizione] [nvarchar](max) NOT NULL,
--	[Prezzo] [decimal](10, 2) NOT NULL,
--	[DataCreazione] [datetime] NOT NULL,
--	[PathImage] [nvarchar](max) NOT NULL,
--	[InMenu] [bit] NOT NULL,
--	[PaninoMese] [bit] NOT NULL,
--	[Categoria] [nvarchar](max) NULL
--) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

--CREATE TABLE [dbo].[User](
--	[UserID] [uniqueidentifier] NOT NULL,
--	[UserName] [nvarchar](max) NOT NULL,
--	[Password] [nvarchar](max) NOT NULL,
--	[DataCreazione] [datetime] NOT NULL,
--	[Admin] [bit] NOT NULL,
--	[SuperAdmin] [bit] NOT NULL
--) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

---- Inserimento del record per l'utente sa
--INSERT INTO [User] (UserID, UserName, Password, DataCreazione, Admin, SuperAdmin)
--VALUES ('B054D570-9210-4B3E-9069-71CB122BAF31', 'sa', 'SistemiCloud2023@', '2024-05-15 21:08:50.927', 1, 1);





--STORED PROCEDURE

--CREATE PROCEDURE [dbo].[sp_InsertOrdine]
--(
--    @OrdineID UNIQUEIDENTIFIER,
--    @Cliente NVARCHAR(MAX),
--    @Annullato BIT,
--    @Consegnato BIT,
--    @Bevanda NVARCHAR(MAX),
--    @InLavorazione BIT,
--    @DataAnnullamento DATETIME,
--    @DataPrenotazione DATETIME,
--    @Note NVARCHAR(MAX),
--    @Plus NVARCHAR(MAX),
--    @PaninoID UNIQUEIDENTIFIER,
--    @Patatine NVARCHAR(MAX),
--    @PrezzoFinale decimal(10,2),
--    @NumeroOrdine INT
--	)
--AS
--BEGIN
--    INSERT INTO Ordine (OrdineID, Cliente, Annullato, Consegnato, Bevanda, InLavorazione, DataAnnullamento, DataPrenotazione, Note, Plus, PaninoID, Patatine, PrezzoFinale, NumeroOrdine)
--    VALUES (@OrdineID, @Cliente, @Annullato, @Consegnato, @Bevanda, @InLavorazione, @DataAnnullamento, @DataPrenotazione, @Note, @Plus, @PaninoID, @Patatine, @PrezzoFinale, @NumeroOrdine);
--END

--CREATE PROCEDURE [dbo].[sp_ModificaOrdine](
--@OrdineID uniqueidentifier,
--@Annullato bit,
--@Consegnato bit,
--@NumeroOrdine int
--)
--AS
--BEGIN
--if(@Consegnato = 1)
--begin
--	update Ordine
--	set Consegnato = @Consegnato
--	where 
--	1=1
--	and OrdineID=@OrdineID
--	and NumeroOrdine = @NumeroOrdine
--end

--if(@Annullato = 1)
--begin
--	update Ordine
--	set Annullato = @Annullato,
--	DataAnnullamento = GetDate()
--	where 
--	1=1
--	and OrdineID=@OrdineID
--	and NumeroOrdine = @NumeroOrdine
--end
--END

--CREATE   PROCEDURE [dbo].[sp_ResyncNews]
--    @NewsID UNIQUEIDENTIFIER,
--    @Titolo NVARCHAR(100),
--    @Corpo NVARCHAR(MAX),
--    @Occhiello NVARCHAR(100),
--    @Visibile BIT,
--    @PathImage NVARCHAR(100),
--    @Scaduta BIT,
--    @PrimoPiano BIT,
--    @Add BIT
--AS
--BEGIN
--    SET NOCOUNT ON;

--    BEGIN TRY
--        IF @Add = 1 -- Inserimento di un nuovo record
--        BEGIN
--            INSERT INTO News (NewsID, TitoloNotizia, CorpoNotizia, Occhiello, Visibile, ImmaginePath, Scaduta, InPrimoPiano, DataInserimento)
--            VALUES (NEWID(), @Titolo, @Corpo, @Occhiello, @Visibile, @PathImage, @Scaduta, @PrimoPiano, GETDATE());
--        END
--        ELSE -- Modifica di un record esistente
--        BEGIN
--            UPDATE News
--            SET TitoloNotizia = @Titolo,
--                CorpoNotizia = @Corpo,
--                Occhiello = @Occhiello,
--                Visibile = @Visibile,
--                ImmaginePath = @PathImage,
--                Scaduta = @Scaduta,
--                ScadenzaNotizia = CASE WHEN @Scaduta = 1 THEN GETDATE() ELSE NULL END,
--                InPrimoPiano = @PrimoPiano
--            WHERE NewsID = @NewsID;
--        END
--    END TRY
--    BEGIN CATCH
--        -- Registra l'errore nel log
--        DECLARE @ErrorMessage NVARCHAR(MAX) = ERROR_MESSAGE();
--        DECLARE @ErrorSeverity INT = ERROR_SEVERITY();
--        DECLARE @ErrorState INT = ERROR_STATE();

--        RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState);
--    END CATCH
--END

--CREATE PROCEDURE [dbo].[sp_ResyncPiatto]
--    @PaninoID UNIQUEIDENTIFIER,
--    @Nome NVARCHAR(100),
--    @Prezzo DECIMAL(10, 2),
--    @PaninoMese BIT,
--    @PathImage NVARCHAR(100),
--    @InMenu BIT,
--    @Descrizione NVARCHAR(MAX),
--    @Categoria NVARCHAR(50),
--    @Add BIT
--AS
--BEGIN
--    IF @Add = 1 -- Inserimento di un nuovo record
--    BEGIN
--        INSERT INTO Panino (PaninoID, Nome, Prezzo, PaninoMese, PathImage, InMenu, Descrizione, Categoria,DataCreazione)
--        VALUES (Newid(), @Nome, @Prezzo, @PaninoMese, @PathImage, @InMenu, @Descrizione, @Categoria,GETDATE());
--    END
--    ELSE -- Modifica di un record esistente
--    BEGIN
--        UPDATE Panino
--        SET Nome = @Nome,
--            Prezzo = @Prezzo,
--            PaninoMese = @PaninoMese,
--            PathImage = @PathImage,
--            InMenu = @InMenu,
--            Descrizione = @Descrizione,
--            Categoria = @Categoria
--        WHERE PaninoID = @PaninoID;
--    END
--END


