USE [AbnieSoftDB]
GO
/****** Object:  StoredProcedure [dbo].[GetTafziliData]    Script Date: 01/10/1403 03:00:10 ب.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[GetTafziliData]
    @BusinessId INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT *
    FROM [AbnieSoftDB].[ACC].[Tafzili]
    WHERE BusinessId = @BusinessId AND IsDelete = 0;
END
