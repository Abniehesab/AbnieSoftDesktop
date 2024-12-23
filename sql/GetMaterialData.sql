USE [AbnieSoftDB]
GO
/****** Object:  StoredProcedure [dbo].[GetMaterialData]    Script Date: 01/10/1403 02:57:33 ب.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[GetMaterialData]
    @BusinessId INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT *
    FROM [AbnieSoftDB].[PRO].[Material]
    WHERE BusinessId = @BusinessId AND IsDelete = 0;
END
