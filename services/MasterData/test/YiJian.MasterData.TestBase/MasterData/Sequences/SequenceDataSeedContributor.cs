namespace YiJian.MasterData.Sequences
{
    using System;
    using System.Threading.Tasks;
    using Volo.Abp.Data;
    using Volo.Abp.DependencyInjection;
    using Volo.Abp.Domain.Repositories;
    using Volo.Abp.Guids;

    /// <summary>
    /// 序列 DataSeedContributor
    /// </summary>
    public class SequenceDataSeedContributor : IDataSeedContributor, ITransientDependency
    {
        public async Task SeedAsync(DataSeedContext context)
        {
            if (await _sequenceRepository.GetCountAsync() > 0)
            {
                return;
            }

            // 编码
            string code = "Recipe";
            // 名称
            string name = "医嘱号";
            // 序列值
            int value = 0;
            // 格式
            string format = null; //"YYYYMMDD";
            // 序列值长度
            int length = 4;
            // 日期
            DateTime date = DateTime.Now;
            // 备注
            string memo = "医嘱";

            var sequence = new Sequence(code,   // 编码
                name,           // 名称
                value,          // 序列值
                format,         // 格式
                length,         // 序列值长度
                date,           // 日期
                memo            // 备注
                );

            await _sequenceRepository.InsertAsync(sequence);

        }

        #region constructor
        public SequenceDataSeedContributor(
            ISequenceRepository sequenceRepository,
            SequenceManager sequenceManager,
            IGuidGenerator guidGenerator)
        {
            _sequenceRepository = sequenceRepository;
            _sequenceManager = sequenceManager;

            _guidGenerator = guidGenerator;
        }
        #endregion

        #region Private Fields
        private readonly ISequenceRepository _sequenceRepository;
        private readonly SequenceManager _sequenceManager;
        private readonly IGuidGenerator _guidGenerator;
        #endregion
    }
}
