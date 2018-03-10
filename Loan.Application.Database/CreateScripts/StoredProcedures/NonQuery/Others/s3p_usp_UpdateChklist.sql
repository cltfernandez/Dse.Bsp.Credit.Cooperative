
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_UpdateChklist]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_UpdateChklist]
GO
CREATE PROCEDURE [dbo].[usp_UpdateChklist]  
   
AS  
BEGIN  
 UPDATE CHKLIST   
  SET CHKLIST.CLACT = A.CLACT  
 FROM   
  #TEMPCHKLIST A  
  INNER JOIN CHKLIST B   
   ON A.CHKLIST_ID= B.CHKLIST_ID   
END  