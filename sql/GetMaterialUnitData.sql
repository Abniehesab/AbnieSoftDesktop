USE [AbnieSoftDB]
GO
/****** Object:  StoredProcedure [dbo].[GetMaterialUnitData]    Script Date: 01/10/1403 02:58:05 ب.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[GetMaterialUnitData]
    @BusinessId INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT *
    FROM [AbnieSoftDB].[PRO].[MaterialUnit]
    WHERE BusinessId = @BusinessId AND IsDelete = 0;
END
