BEGIN TRANSACTION;
GO

ALTER TABLE [NursingRecipeExecHistory] ADD [LastStatus] int NOT NULL DEFAULT 0;
DECLARE @defaultSchema AS sysname;
SET @defaultSchema = SCHEMA_NAME();
DECLARE @description AS sql_variant;
SET @description = N'操作前状态';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'NursingRecipeExecHistory', 'COLUMN', N'LastStatus';
GO

DECLARE @defaultSchema AS sysname;
SET @defaultSchema = SCHEMA_NAME();
DECLARE @description AS sql_variant;
SET @description = N'关联医嘱表编号';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'NursingRecipeExec', 'COLUMN', N'RecipeId';
GO

DECLARE @defaultSchema AS sysname;
SET @defaultSchema = SCHEMA_NAME();
DECLARE @description AS sql_variant;
SET @description = N'病人标识';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'NursingRecipeExec', 'COLUMN', N'PIID';
GO

ALTER TABLE [NursingRecipeExec] ADD [IsInfusion] bit NOT NULL DEFAULT CAST(0 AS bit);
DECLARE @defaultSchema AS sysname;
SET @defaultSchema = SCHEMA_NAME();
DECLARE @description AS sql_variant;
SET @description = N'是否输液';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'NursingRecipeExec', 'COLUMN', N'IsInfusion';
GO

ALTER TABLE [NursingRecipeExec] ADD [PlatformType] int NOT NULL DEFAULT 0;
DECLARE @defaultSchema AS sysname;
SET @defaultSchema = SCHEMA_NAME();
DECLARE @description AS sql_variant;
SET @description = N'系统标识: 0=急诊，1=院前';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'NursingRecipeExec', 'COLUMN', N'PlatformType';
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220215005806_infusionchanges', N'5.0.15');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [NursingRecipeExecRefund] (
    [Id] uniqueidentifier NOT NULL,
    [ExecId] uniqueidentifier NOT NULL,
    [RecipeNo] nvarchar(20) NOT NULL,
    [PIID] uniqueidentifier NOT NULL,
    [PlatformType] int NOT NULL,
    [IsWithDrawn] bit NOT NULL,
    [NursingRefundStatus] int NOT NULL,
    [RequestTime] datetime2 NOT NULL,
    [Code] nvarchar(20) NOT NULL,
    [Name] nvarchar(200) NOT NULL,
    [Specification] nvarchar(200) NULL,
    [RefundQty] int NOT NULL,
    [Price] decimal(18,4) NOT NULL,
    [NurseCode] nvarchar(20) NULL,
    [NurseName] nvarchar(50) NULL,
    [Reason] nvarchar(500) NULL,
    [ConfirmedTime] datetime2 NULL,
    [ConfirmmerCode] nvarchar(20) NULL,
    [ConfirmmerName] nvarchar(50) NULL,
    [ExtraProperties] nvarchar(max) NULL,
    [ConcurrencyStamp] nvarchar(40) NULL,
    [CreationTime] datetime2 NOT NULL,
    [CreatorId] uniqueidentifier NULL,
    [LastModificationTime] datetime2 NULL,
    [LastModifierId] uniqueidentifier NULL,
    [IsDeleted] bit NOT NULL DEFAULT CAST(0 AS bit),
    [DeleterId] uniqueidentifier NULL,
    [DeletionTime] datetime2 NULL,
    CONSTRAINT [PK_NursingRecipeExecRefund] PRIMARY KEY ([Id])
);
DECLARE @defaultSchema AS sysname;
SET @defaultSchema = SCHEMA_NAME();
DECLARE @description AS sql_variant;
SET @description = N'医嘱执行退款退费表';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'NursingRecipeExecRefund';
SET @description = N'关联医嘱执行表编号';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'NursingRecipeExecRefund', 'COLUMN', N'ExecId';
SET @description = N'医嘱号';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'NursingRecipeExecRefund', 'COLUMN', N'RecipeNo';
SET @description = N'病人标识';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'NursingRecipeExecRefund', 'COLUMN', N'PIID';
SET @description = N'系统标识: 0=急诊，1=院前';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'NursingRecipeExecRefund', 'COLUMN', N'PlatformType';
SET @description = N'是退药';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'NursingRecipeExecRefund', 'COLUMN', N'IsWithDrawn';
SET @description = N'退药退费状态';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'NursingRecipeExecRefund', 'COLUMN', N'NursingRefundStatus';
SET @description = N'申请时间';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'NursingRecipeExecRefund', 'COLUMN', N'RequestTime';
SET @description = N'医嘱编码';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'NursingRecipeExecRefund', 'COLUMN', N'Code';
SET @description = N'医嘱名称';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'NursingRecipeExecRefund', 'COLUMN', N'Name';
SET @description = N'规格';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'NursingRecipeExecRefund', 'COLUMN', N'Specification';
SET @description = N'数量';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'NursingRecipeExecRefund', 'COLUMN', N'RefundQty';
SET @description = N'单价';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'NursingRecipeExecRefund', 'COLUMN', N'Price';
SET @description = N'申请护士编码';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'NursingRecipeExecRefund', 'COLUMN', N'NurseCode';
SET @description = N'申请护士名称';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'NursingRecipeExecRefund', 'COLUMN', N'NurseName';
SET @description = N'原因';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'NursingRecipeExecRefund', 'COLUMN', N'Reason';
SET @description = N'确认时间';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'NursingRecipeExecRefund', 'COLUMN', N'ConfirmedTime';
SET @description = N'确认人编码';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'NursingRecipeExecRefund', 'COLUMN', N'ConfirmmerCode';
SET @description = N'确认人名称';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'NursingRecipeExecRefund', 'COLUMN', N'ConfirmmerName';
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220215010602_refundcreated', N'5.0.15');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[NursingRecipeExecRefund]') AND [c].[name] = N'Code');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [NursingRecipeExecRefund] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [NursingRecipeExecRefund] DROP COLUMN [Code];
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[NursingRecipeExecRefund]') AND [c].[name] = N'Name');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [NursingRecipeExecRefund] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [NursingRecipeExecRefund] DROP COLUMN [Name];
GO

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[NursingRecipeExecRefund]') AND [c].[name] = N'Price');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [NursingRecipeExecRefund] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [NursingRecipeExecRefund] DROP COLUMN [Price];
GO

DECLARE @defaultSchema AS sysname;
SET @defaultSchema = SCHEMA_NAME();
DECLARE @description AS sql_variant;
EXEC sp_dropextendedproperty 'MS_Description', 'SCHEMA', @defaultSchema, 'TABLE', N'NursingRecipeExecRefund', 'COLUMN', N'IsWithDrawn';
SET @description = N'是否退药退费';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'NursingRecipeExecRefund', 'COLUMN', N'IsWithDrawn';
GO

DECLARE @defaultSchema AS sysname;
SET @defaultSchema = SCHEMA_NAME();
DECLARE @description AS sql_variant;
EXEC sp_dropextendedproperty 'MS_Description', 'SCHEMA', @defaultSchema, 'TABLE', N'NursingRecipeExecRefund', 'COLUMN', N'ConfirmmerName';
SET @description = N'审批人名称';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'NursingRecipeExecRefund', 'COLUMN', N'ConfirmmerName';
GO

DECLARE @defaultSchema AS sysname;
SET @defaultSchema = SCHEMA_NAME();
DECLARE @description AS sql_variant;
EXEC sp_dropextendedproperty 'MS_Description', 'SCHEMA', @defaultSchema, 'TABLE', N'NursingRecipeExecRefund', 'COLUMN', N'ConfirmmerCode';
SET @description = N'审批人编码';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'NursingRecipeExecRefund', 'COLUMN', N'ConfirmmerCode';
GO

DECLARE @defaultSchema AS sysname;
SET @defaultSchema = SCHEMA_NAME();
DECLARE @description AS sql_variant;
EXEC sp_dropextendedproperty 'MS_Description', 'SCHEMA', @defaultSchema, 'TABLE', N'NursingRecipeExecRefund', 'COLUMN', N'ConfirmedTime';
SET @description = N'审批时间';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'NursingRecipeExecRefund', 'COLUMN', N'ConfirmedTime';
GO

ALTER TABLE [NursingRecipeExecRefund] ADD [RefundType] int NOT NULL DEFAULT 0;
DECLARE @defaultSchema AS sysname;
SET @defaultSchema = SCHEMA_NAME();
DECLARE @description AS sql_variant;
SET @description = N'退药退费类型';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'NursingRecipeExecRefund', 'COLUMN', N'RefundType';
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220216100200_reciperefund', N'5.0.15');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var3 sysname;
SELECT @var3 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Duct_Dict]') AND [c].[name] = N'CreationTime');
IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [Duct_Dict] DROP CONSTRAINT [' + @var3 + '];');
ALTER TABLE [Duct_Dict] DROP COLUMN [CreationTime];
GO

DECLARE @var4 sysname;
SELECT @var4 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Duct_Dict]') AND [c].[name] = N'CreatorId');
IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [Duct_Dict] DROP CONSTRAINT [' + @var4 + '];');
ALTER TABLE [Duct_Dict] DROP COLUMN [CreatorId];
GO

DECLARE @var5 sysname;
SELECT @var5 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Duct_Dict]') AND [c].[name] = N'DeleterId');
IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [Duct_Dict] DROP CONSTRAINT [' + @var5 + '];');
ALTER TABLE [Duct_Dict] DROP COLUMN [DeleterId];
GO

DECLARE @var6 sysname;
SELECT @var6 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Duct_Dict]') AND [c].[name] = N'DeletionTime');
IF @var6 IS NOT NULL EXEC(N'ALTER TABLE [Duct_Dict] DROP CONSTRAINT [' + @var6 + '];');
ALTER TABLE [Duct_Dict] DROP COLUMN [DeletionTime];
GO

DECLARE @var7 sysname;
SELECT @var7 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Duct_Dict]') AND [c].[name] = N'IsDeleted');
IF @var7 IS NOT NULL EXEC(N'ALTER TABLE [Duct_Dict] DROP CONSTRAINT [' + @var7 + '];');
ALTER TABLE [Duct_Dict] DROP COLUMN [IsDeleted];
GO

DECLARE @var8 sysname;
SELECT @var8 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Duct_Dict]') AND [c].[name] = N'LastModificationTime');
IF @var8 IS NOT NULL EXEC(N'ALTER TABLE [Duct_Dict] DROP CONSTRAINT [' + @var8 + '];');
ALTER TABLE [Duct_Dict] DROP COLUMN [LastModificationTime];
GO

DECLARE @var9 sysname;
SELECT @var9 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Duct_Dict]') AND [c].[name] = N'LastModifierId');
IF @var9 IS NOT NULL EXEC(N'ALTER TABLE [Duct_Dict] DROP CONSTRAINT [' + @var9 + '];');
ALTER TABLE [Duct_Dict] DROP COLUMN [LastModifierId];
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220305073349_Update_Dict', N'5.0.15');
GO

COMMIT;
GO

