USE [AbnieSoftDB]
GO
/****** Object:  StoredProcedure [dbo].[GetMaterialCirculationData]    Script Date: 01/10/1403 02:57:17 ب.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[GetMaterialCirculationData]
    @BusinessId INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT *
    FROM [AbnieSoftDB].[PRO].[MaterialCirculation]
    WHERE BusinessId = @BusinessId AND IsDelete = 0 AND IsUpdate = 0;;
END
