USE [AbnieSoftDB]
GO
/****** Object:  StoredProcedure [dbo].[GetTenderData]    Script Date: 01/10/1403 03:00:50 ب.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[GetTenderData]
    @BusinessId INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT *
    FROM [AbnieSoftDB].[PRO].[Tender]
    WHERE BusinessId = @BusinessId AND IsDelete = 0;
END
