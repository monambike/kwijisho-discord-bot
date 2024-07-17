/**************************************************************************************
Copyright(c) 2024 Vinicius Gabriel Marques de Melo. All rights reserved.
Contact: @monambike for more information.
For license information, please see the LICENSE file in the root directory.
-
Script created by @monambike. Please check https://github.com/monambike for more details,
including the latest licensing information.

**************************************************************************************/

/* This trigger inserts default entries into tables that depend on the Server table
   whenever a new row is added to Server. */

CREATE TRIGGER TR_InsertServer_DDL
AFTER INSERT ON Server
FOR EACH ROW
BEGIN
	/* Inserting into the ServerChannel table. */
	INSERT INTO ServerChannel (ServerId) VALUES (NEW.ServerId);
	
	/* Inserting into the ServerRole table. */
	INSERT INTO ServerRole (ServerId) VALUES (NEW.ServerId);
END;
