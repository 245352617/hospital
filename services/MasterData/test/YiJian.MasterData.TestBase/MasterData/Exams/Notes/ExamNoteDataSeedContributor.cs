namespace YiJian.MasterData
{
    using System;
    using System.Threading.Tasks;
    using Volo.Abp.Data;
    using Volo.Abp.DependencyInjection;
    using Volo.Abp.Domain.Repositories;
    using Volo.Abp.Guids;

    /// <summary>
    /// 检查申请注意事项 DataSeedContributor
    /// </summary>
    public class ExamNoteDataSeedContributor : IDataSeedContributor, ITransientDependency
    {
        public async Task SeedAsync(DataSeedContext context)
        {
			if (await _examNoteRepository.GetCountAsync() > 0)
			{
				return;
			}            
            /*
            // 注意事项编码
            string code = "";
            // 注意事项名称
            string name = "";
            // 科室编码
            string deptCode = "";
            // 科室
            string dept = "";
            // 显示名称(申请单) eg: CT检查申请单、MRI检查申请单、X线检查申请单
            string displayName = "";
            // 检查申请描述模板
            string descTemplate = "";
            // 是否启用
            bool isActive = true;

            var examNote = new ExamNote(code,   // 注意事项编码
                name,           // 注意事项名称
                deptCode,       // 科室编码
                dept,           // 科室
                displayName,    // 显示名称(申请单) eg: CT检查申请单、MRI检查申请单、X线检查申请单
                descTemplate,   // 检查申请描述模板
                isActive        // 是否启用
                );

			await _examNoteRepository.InsertAsync(examNote); 
            */
        }

        #region constructor
        public ExamNoteDataSeedContributor(
            IExamNoteRepository examNoteRepository, 
            IGuidGenerator guidGenerator)
        {
            _examNoteRepository = examNoteRepository;

            _guidGenerator = guidGenerator;
        }
        #endregion

        #region Private Fields
        private readonly IExamNoteRepository _examNoteRepository;
        private readonly IGuidGenerator _guidGenerator;
        #endregion
    }
}
