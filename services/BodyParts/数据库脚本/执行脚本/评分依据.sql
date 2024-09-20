INSERT INTO [dbo].[IcuScoreStandard]([Id], [Name], [Code], [Describe], [ScoreType], [Formula], [FormulaPare], [FormName], [Remark]) VALUES ('F51135BC-30A7-4F8A-BA1C-0DC69BC3E62B', '患者自理评估(Barthel指数)', 'AS.001', '患者自理评估', 0, NULL, NULL, '', '123');
INSERT INTO [dbo].[IcuScoreStandard]([Id], [Name], [Code], [Describe], [ScoreType], [Formula], [FormulaPare], [FormName], [Remark]) VALUES ('4846ADE5-919E-0554-BDCE-39F938900852', '数字疼痛评分', 'AS.002', 'NRS', NULL, NULL, NULL, NULL, '测试111');
INSERT INTO [dbo].[IcuScoreStandard]([Id], [Name], [Code], [Describe], [ScoreType], [Formula], [FormulaPare], [FormName], [Remark]) VALUES ('A1565F64-1D1A-8DF2-960F-39F94373C7D1', '导管滑脱评分', 'AS.003', '导管滑脱评分', NULL, NULL, NULL, NULL, '测试222');
INSERT INTO [dbo].[IcuScoreStandard]([Id], [Name], [Code], [Describe], [ScoreType], [Formula], [FormulaPare], [FormName], [Remark]) VALUES ('494738D1-7C24-4DA7-B302-40021773EBD1', 'CPOT', 'AS.004', 'CPOT', 0, '', '', '', '测试111');
INSERT INTO [dbo].[IcuScoreStandard]([Id], [Name], [Code], [Describe], [ScoreType], [Formula], [FormulaPare], [FormName], [Remark]) VALUES ('1FD7C86D-D1A2-4D61-89F3-4859CE4E6203', '压疮风险评估(Braden)', 'AS.005', '压疮风险评估', 0, NULL, NULL, '', '122222');
INSERT INTO [dbo].[IcuScoreStandard]([Id], [Name], [Code], [Describe], [ScoreType], [Formula], [FormulaPare], [FormName], [Remark]) VALUES ('2CAC5843-C273-4A8C-BACD-9475A383FF39', '跌倒/坠床评估', 'AS.006', '跌倒/坠床评估', 0, NULL, NULL, NULL, '测试111');
INSERT INTO [dbo].[IcuScoreStandard]([Id], [Name], [Code], [Describe], [ScoreType], [Formula], [FormulaPare], [FormName], [Remark]) VALUES ('3C48296E-8118-4241-B2AB-EAA82532F9C8', 'GCS', 'AS.007', 'GCS', 0, '', '', '', '测试111');
INSERT INTO [dbo].[IcuScoreStandard]([Id], [Name], [Code], [Describe], [ScoreType], [Formula], [FormulaPare], [FormName], [Remark]) VALUES ('23C742FE-D511-485F-882C-F4FFF6982FE8', 'DVT评分（AUTAR）', 'AS.008', 'DVT评分', 0, NULL, NULL, NULL, '1.评分原则：评估表项目单选，以各项目的高分计入（如患者存在脊柱创伤、骨盆创伤，则以高分项骨盆创伤分值计入）。

 2.风险等级及护理指引：
分值< 6 ---无风险，无需特别措施，尽早活动；
分值 7-10---低危凤险，给予基础预防；
分值 11-14---中危风险给予基础预防+物理预防；
分值> 15---高危险，在中等风险措施基础上加用药物预防措施。

 3.评估频率：针对DVT易患人群，入院时、手术后、病情变化时、活动能力改变、BADL评分下降时需及时评估；高风险患者启用DVT护理评估单并每日至少评估记录一次。
');
