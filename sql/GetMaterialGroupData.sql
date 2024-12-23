USE [AbnieSoftDB]
GO
/****** Object:  StoredProcedure [dbo].[GetMaterialGroupData]    Script Date: 01/10/1403 02:57:49 ب.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[GetMaterialGroupData]
    @BusinessId INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT *
    FROM [AbnieSoftDB].[PRO].[MaterialGroup]
    WHERE BusinessId = @BusinessId AND IsDelete = 0;
END
