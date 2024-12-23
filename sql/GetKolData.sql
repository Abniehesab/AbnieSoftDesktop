USE [AbnieSoftDB]
GO
/****** Object:  StoredProcedure [dbo].[GetKolData]    Script Date: 01/10/1403 02:56:55 ب.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[GetKolData]
    @BusinessId INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT *
    FROM [AbnieSoftDB].[ACC].[Kol]
    WHERE BusinessId = @BusinessId AND IsDelete = 0;
END
