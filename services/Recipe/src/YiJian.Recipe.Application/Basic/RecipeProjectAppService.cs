using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using YiJian.Basic.RecipeProjects;
using YiJian.Basic.RecipeProjects.Dto;
using YiJian.Recipes.Basic;
using YiJian.Recipes.Basic.Recipes;

namespace YiJian.Basic
{
    /// <summary>
    /// 医嘱项目接口
    /// Author: ywlin
    /// Date: 2021-12-04
    /// </summary>
    public class RecipeProjectAppService : ApplicationService, IRecipeProjectAppService
    {
        private readonly IRecipeProjectRepository _recipeProjectRepository;

        /// <summary>
        /// 医嘱项目接口
        /// </summary>
        /// <param name="recipeProjectRepository"></param>
        public RecipeProjectAppService(IRecipeProjectRepository recipeProjectRepository)
        {
            this._recipeProjectRepository = recipeProjectRepository;
        }

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<RecipeProjectData>> GetListAsync()
        {
            var list = await (await this._recipeProjectRepository.GetQueryableAsync())
                .Include(x => x.MedicineProp)
                .Include(x => x.ExamProp)
                .Include(x => x.LabProp)
                .ToListAsync();

            return ObjectMapper.Map<IEnumerable<RecipeProject>, IEnumerable<RecipeProjectData>>(list);
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<RecipeProjectData>> GetPagedListAsync(RecipeProjectPagedInput input)
        {
            var query = (await this._recipeProjectRepository.GetQueryableAsync())
                .Include(x => x.MedicineProp)
                .Include(x => x.ExamProp)
                .Include(x => x.LabProp)
                .WhereIf(!string.IsNullOrEmpty(input.CategoryCode), x => x.CategoryCode.Trim() == input.CategoryCode.Trim());
            var list = await query
                .WhereIf(!string.IsNullOrEmpty(input.Filter), x => x.Name.Contains(input.Filter) || x.Code.Contains(input.Filter))
                .PageBy(input.SkipCount, input.Size)
                .ToListAsync();
            var count = await query.LongCountAsync();
            var dtoList = ObjectMapper.Map<List<RecipeProject>, List<RecipeProjectData>>(list);

            return new PagedResultDto<RecipeProjectData>(count, dtoList.AsReadOnly());
        }
    }
}
