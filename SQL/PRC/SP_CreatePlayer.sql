
SET ANSI_NULLS ON 
GO
if exists (select top 1 1 from dbo.sysobjects where id = object_id(N'[dbo].[SP_CreatePlyer]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[SP_CreatePlyer]
GO

CREATE PROCEDURE [dbo].[SP_CreatePlyer]
@PlayerName VARCHAR(30),
@NickName VARCHAR(30), 
AS

/* *************************************************************************************************
Object Name: SP_CreatePlyer
Description: Create plyer based on passed parameters PlayerName and NickName
Object Interdependency: Called from webservice

Version History:
Date        Programmer       Modification                                                  
11/14/2015  Genna Motro      Creation.                                                     

***************************************************************************************************/

DECLARE 
        @ErrMessage VARCHAR(1000),
        @Now DATETIME 
		
  SET @Now = GETDATE()
  
  insert into Player (PlayerName, NickName, CreationDate) values (@PlayerName,@NickName, @Now )

  IF (@@Error <> 0) BEGIN
    SET @ErrMessage = 'Error to create the Player'
    GOTO Error
  END
  
  Error:
  
    SET @ErrMessage = 'Source: '+ Object_name(@@ProcID)+ ' ' + @ErrMessage
    RAISERROR (@ErrMessage, 16, 1)
	
	RETURN 1
GO