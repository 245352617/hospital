namespace YiJian.MasterData.VitalSign
{
    using System;
    using System.Threading.Tasks;
    using Volo.Abp.Data;
    using Volo.Abp.DependencyInjection;
    using Volo.Abp.Domain.Repositories;
    using Volo.Abp.Guids;

    /// <summary>
    /// 评分项 DataSeedContributor
    /// </summary>
    public class VitalSignExpressionDataSeedContributor : IDataSeedContributor, ITransientDependency
    {
        public async Task SeedAsync(DataSeedContext context)
        {
			if (await _vitalSignExpressionRepository.GetCountAsync() > 0)
			{
				return;
			}            
            /*
            // 
            Guid id = default;
            // 评分项
            string itemName = "";
            // Ⅰ级评分表达式
            string stLevelExpression = "";
            // Ⅱ级评分表达式
            string ndLevelExpression = "";
            // Ⅲ级评分表达式
            string rdLevelExpression = "";
            // Ⅳa级评分表达式
            string thALevelExpression = "";
            // Ⅳb级评分表达式
            string thBLevelExpression = "";
            // 默认Ⅰ级评分表达式
            string defaultStLevelExpression = "";
            // 默认Ⅱ级评分表达式
            string defaultNdLevelExpression = "";
            // 默认Ⅲ级评分表达式
            string defaultRdLevelExpression = "";
            // 默认Ⅳa级评分表达式
            string defaultThALevelExpression = "";
            // 默认Ⅳb级评分表达式
            string defaultThBLevelExpression = "";

            var vitalSignExpression = new VitalSignExpression(_guidGenerator.Create(), 
                itemName,// 评分项
                stLevelExpression,// Ⅰ级评分表达式
                ndLevelExpression,// Ⅱ级评分表达式
                rdLevelExpression,// Ⅲ级评分表达式
                thALevelExpression,// Ⅳa级评分表达式
                thBLevelExpression,// Ⅳb级评分表达式
                defaultStLevelExpression,// 默认Ⅰ级评分表达式
                defaultNdLevelExpression,// 默认Ⅱ级评分表达式
                defaultRdLevelExpression,// 默认Ⅲ级评分表达式
                defaultThALevelExpression,// 默认Ⅳa级评分表达式
                defaultThBLevelExpression// 默认Ⅳb级评分表达式
                );

			await _vitalSignExpressionRepository.InsertAsync(vitalSignExpression); 
            */
        }

        #region constructor
        public VitalSignExpressionDataSeedContributor(
            IVitalSignExpressionRepository vitalSignExpressionRepository, 
            VitalSignExpressionManager vitalSignExpressionManager,
            IGuidGenerator guidGenerator)
        {
            _vitalSignExpressionRepository = vitalSignExpressionRepository;
            _vitalSignExpressionManager = vitalSignExpressionManager;

            _guidGenerator = guidGenerator;
        }
        #endregion

        #region Private Fields
        private readonly IVitalSignExpressionRepository _vitalSignExpressionRepository;
        private readonly VitalSignExpressionManager _vitalSignExpressionManager;
        private readonly IGuidGenerator _guidGenerator;
        #endregion
    }
}
