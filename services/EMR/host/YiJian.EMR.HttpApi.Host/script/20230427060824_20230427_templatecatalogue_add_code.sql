BEGIN TRANSACTION;
GO

ALTER TABLE [EmrTemplateCatalogue] ADD [Code] nvarchar(200) NULL;
DECLARE @defaultSchema AS sysname;
SET @defaultSchema = SCHEMA_NAME();
DECLARE @description AS sql_variant;
SET @description = N'目录名称编码';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'EmrTemplateCatalogue', 'COLUMN', N'Code';
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[EmrMergeTemplateWhiteList]') AND [c].[name] = N'TemplateName');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [EmrMergeTemplateWhiteList] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [EmrMergeTemplateWhiteList] ALTER COLUMN [TemplateName] nvarchar(100) NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230427060824_20230427_templatecatalogue_add_code', N'6.0.14');
GO

COMMIT;
GO

