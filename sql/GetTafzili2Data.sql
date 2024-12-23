USE [AbnieSoftDB]
GO
/****** Object:  StoredProcedure [dbo].[GetTafzili2Data]    Script Date: 01/10/1403 02:59:44 ب.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[GetTafzili2Data]
    @BusinessId INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT *
    FROM [AbnieSoftDB].[ACC].[Tafzili2]
    WHERE BusinessId = @BusinessId AND IsDelete = 0;
END
