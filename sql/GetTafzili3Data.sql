USE [AbnieSoftDB]
GO
/****** Object:  StoredProcedure [dbo].[GetTafzili3Data]    Script Date: 01/10/1403 02:59:57 ب.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[GetTafzili3Data]
    @BusinessId INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT *
    FROM [AbnieSoftDB].[ACC].[Tafzili3]
    WHERE BusinessId = @BusinessId AND IsDelete = 0;
END
