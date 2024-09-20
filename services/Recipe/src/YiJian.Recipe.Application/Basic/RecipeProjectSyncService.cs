using DotNetCore.CAP;
using MasterDataService;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Guids;
using Volo.Abp.ObjectMapping;
using Volo.Abp.Uow;
using YiJian.Recipes.Basic;
using YiJian.Recipes.Basic.Recipes;

namespace YiJian.Basic
{
    /// <summary>
    /// 医嘱项目同步服务
    /// Author: ywlin
    /// Date: 2021-12-04
    /// </summary>
    public class RecipeProjectSyncService : IScopedDependency, ICapSubscribe
    {
        private readonly AbpLazyServiceProvider _lazyServiceProvider;
        private readonly IObjectMapper _objectMapper;
        private readonly IGuidGenerator _guidGenerator;
        private readonly IRecipeProjectRepository _recipeProjectRepository;
        /// <summary>
        /// UnitOfWorkManager
        /// </summary>
        public IUnitOfWorkManager UnitOfWorkManager => _lazyServiceProvider.LazyGetRequiredService<IUnitOfWorkManager>();
        /// <summary>
        /// Logger
        /// </summary>
        public ILogger Logger => _lazyServiceProvider.LazyGetRequiredService<ILogger>();

        /// <summary>
        /// 医嘱项目同步服务
        /// </summary> 
        public RecipeProjectSyncService(
            AbpLazyServiceProvider lazyServiceProvider,
            IObjectMapper ObjectMapper, IGuidGenerator guidGenerator,
            IRecipeProjectRepository recipeProjectRepository)
        {
            this._lazyServiceProvider = lazyServiceProvider;
            this._objectMapper = ObjectMapper;
            this._guidGenerator = guidGenerator;
            this._recipeProjectRepository = recipeProjectRepository;
        }

        /// <summary>
        /// 同步药品
        /// </summary>
        /// <param name="eto"></param>
        /// <returns></returns>
        [CapSubscribe("sync.masterdata.medicine")]
        public async Task SyncMedicineAsync(GrpcMedicineModel eto)
        {
            try
            {
                using var uow = UnitOfWorkManager.Begin();

                var recipeProject = await (await this._recipeProjectRepository.GetQueryableAsync())
                    .Include(x => x.MedicineProp)
                    .FirstOrDefaultAsync(x => x.CategoryCode == "Medicine" && x.SourceId == eto.Id);
                if (recipeProject == null)
                {
                    var id = _guidGenerator.Create();
                    recipeProject = new RecipeProject(id)
                    {
                        MedicineProp = new RecipeMedicineProp(id),
                    };
                    _objectMapper.Map(eto, recipeProject);
                    await this._recipeProjectRepository.InsertAsync(recipeProject);
                }
                else
                {
                    _objectMapper.Map(eto, recipeProject);
                    await this._recipeProjectRepository.UpdateAsync(recipeProject);
                }

                await uow.CompleteAsync();
            }
            catch (Exception exception)
            {
                Logger.LogError(exception.Message);
                throw;
            }
        }

        /// <summary>
        /// 同步检验项目
        /// </summary>
        /// <param name="eto"></param>
        /// <returns></returns>
        [CapSubscribe("sync.masterdata.labproject")]
        public async Task SyncLabProjectAsync(GrpcLabProjectModel eto)
        {
            try
            {
                using var uow = UnitOfWorkManager.Begin();

                var recipeProject = await (await this._recipeProjectRepository.GetQueryableAsync())
                    .Include(x => x.LabProp)
                    .FirstOrDefaultAsync(x => x.CategoryCode == "Lab" && x.SourceId == eto.Id);
                if (recipeProject == null)
                {
                    var id = _guidGenerator.Create();
                    recipeProject = new RecipeProject(id)
                    {
                        LabProp = new RecipeLabProp(id),
                    };
                    _objectMapper.Map(eto, recipeProject);
                    await this._recipeProjectRepository.InsertAsync(recipeProject);
                }
                else
                {
                    _objectMapper.Map(eto, recipeProject);
                    await this._recipeProjectRepository.UpdateAsync(recipeProject);
                }

                await uow.CompleteAsync();
            }
            catch (Exception exception)
            {
                Logger.LogError(exception.Message);
                throw;
            }
        }

        /// <summary>
        /// 同步检查项目
        /// </summary>
        /// <param name="eto"></param>
        /// <returns></returns>
        [CapSubscribe("sync.masterdata.examproject")]
        public async Task SyncExamProjectAsync(GrpcExamProjectModel eto)
        {
            try
            {
                using var uow = UnitOfWorkManager.Begin();

                var recipeProject = await (await this._recipeProjectRepository.GetQueryableAsync())
                    .Include(x => x.ExamProp)
                    .FirstOrDefaultAsync(x => x.CategoryCode == "Exam" && x.SourceId == eto.Id);
                if (recipeProject == null)
                {
                    var id = _guidGenerator.Create();
                    recipeProject = new RecipeProject(id)
                    {
                        ExamProp = new RecipeExamProp(id),
                    };
                    _objectMapper.Map(eto, recipeProject);
                    await this._recipeProjectRepository.InsertAsync(recipeProject);
                }
                else
                {
                    _objectMapper.Map(eto, recipeProject);
                    await this._recipeProjectRepository.UpdateAsync(recipeProject);
                }

                await uow.CompleteAsync();
            }
            catch (Exception exception)
            {
                Logger.LogError(exception.Message);
                throw;
            }
        }
    }
}
