/*
   Tuesday, December 8, 20153:23:09 PM
   User: 
   Server: statler
   Database: Gitle
   Application: 
*/

/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.FilterPreset ADD
	Project_id bigint NULL
GO
ALTER TABLE dbo.FilterPreset SET (LOCK_ESCALATION = TABLE)
GO
COMMIT

ALTER TABLE [dbo].[FilterPreset]  WITH CHECK ADD  CONSTRAINT [FKEA8C755CC4985D11] FOREIGN KEY([Project_id])
REFERENCES [dbo].[Project] ([Id])
GO
