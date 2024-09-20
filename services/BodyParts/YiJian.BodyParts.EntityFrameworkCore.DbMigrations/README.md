EntityFrameworkCore.Migrations
===========================
该类库用来实现数据库迁移工作。

****

	依赖 .EntityFrameworkCore 项目,需要引用模型配置。


相关命令：
	Add-Migration Initial 来创建初始迁移
	Add-Migration "Version" 创建新的版本并指定标识符
	Remove-Migration 移除迁移信息
	Update-Database 更新数据库


迁移备注：
    迁移数据，需要迁移模型中的注释到数据库表，那么需要在生成的迁移文件中添加以下语句。
	 migrationBuilder.ApplyDatabaseDescription(this);

