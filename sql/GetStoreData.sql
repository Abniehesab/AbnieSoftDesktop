USE [AbnieSoftDB]
GO
/****** Object:  StoredProcedure [dbo].[GetStoreData]    Script Date: 01/10/1403 02:59:30 ب.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[GetStoreData]
    @BusinessId INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT *
    FROM [AbnieSoftDB].[PRO].[Store]
    WHERE BusinessId = @BusinessId AND IsDelete = 0;
END
