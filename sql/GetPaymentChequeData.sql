USE [AbnieSoftDB]
GO
/****** Object:  StoredProcedure [dbo].[GetPaymentChequeData]    Script Date: 01/10/1403 02:58:46 ب.ظ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[GetPaymentChequeData]
    @BusinessId INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT *
    FROM [AbnieSoftDB].[FIN].[PaymentCheque]
    WHERE BusinessId = @BusinessId AND IsDelete = 0  AND IsUpdate = 0;
END
