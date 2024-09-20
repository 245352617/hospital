using MasterDataService;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Guids;
using Volo.Abp.ObjectMapping;
using Volo.Abp.Uow;
using YiJian.Recipes.Basic;
using YiJian.Recipes.Basic.Recipes;

namespace YiJian.Basic
{
    /// <summary>
    /// RecipeProjectGrpcAppService
    /// </summary>
    public class RecipeProjectGrpcAppService : ApplicationService
    {
        private readonly GrpcMasterData.GrpcMasterDataClient _grpcMasterDataClient;
        private readonly ILogger<RecipeProjectGrpcAppService> _logger;
        private readonly IGuidGenerator _guidGenerator;
        private readonly IObjectMapper _objectMapper;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IRecipeProjectRepository _recipeProjectRepository;

        /// <summary>
        /// RecipeProjectGrpcAppService
        /// </summary> 
        public RecipeProjectGrpcAppService(GrpcMasterData.GrpcMasterDataClient grpcMasterDataClient,
            ILogger<RecipeProjectGrpcAppService> logger,
            IAbpLazyServiceProvider lazyServiceProvider,
            IRecipeProjectRepository recipeProjectRepository)
        {
            this._grpcMasterDataClient = grpcMasterDataClient;
            this._logger = logger;
            this._guidGenerator = lazyServiceProvider.LazyGetRequiredService<IGuidGenerator>();
            this._objectMapper = lazyServiceProvider.LazyGetRequiredService<IObjectMapper>();
            this._unitOfWorkManager = lazyServiceProvider.LazyGetRequiredService<IUnitOfWorkManager>();
            this._recipeProjectRepository = recipeProjectRepository;
        }

        /// <summary>
        /// 同步医嘱项目
        /// </summary>
        /// <returns></returns>
        public async Task SyncRecipeProjectAsync()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("--> Sync Medicine From Grpc Begin");
            Console.ResetColor();

            using var uow = this._unitOfWorkManager.Begin();
            try
            {
                var list = new List<RecipeProject>();
                // 同步药品信息
                var medicineList = GetMedicines();
                list.AddRange(medicineList);
                // 同步检验项目信息
                var labProjects = GetLabProjects();
                list.AddRange(labProjects);
                // 同步检查项目信息
                var examProjects = GetExamProjects();
                list.AddRange(examProjects);
                // 同步诊疗项目信息
                var treatProjects = GetTreats();
                list.AddRange(treatProjects);

                await this._recipeProjectRepository.InsertOrUpdateAsync(list);

                await uow.CompleteAsync();
            }
            catch (Exception)
            {
                await uow.RollbackAsync();
                throw;
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("--> Sync Medicine From Grpc Completed");
            Console.ResetColor();
        }

        /// <summary>
        /// 通过编码获取药品 Demo
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<GrpcMedicineModel> GetMedicineAsync(int id)
        {
            var request = new GetMedicineByIdRquest()
            {
                Id = id
            };
            var grpcMedicine = await this._grpcMasterDataClient.GetMedicineByIdAsync(request);

            return grpcMedicine.Medicine;
        }

        /// <summary>
        /// 通过编码获取检验项目 Demo
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<GrpcLabProjectModel> GetLabProjectAsync(int id)
        {
            var request = new GetLabProjectByIdRequest()
            {
                Id = id
            };
            var grpcLabProject = await this._grpcMasterDataClient.GetLabProjectByIdAsync(request);

            return grpcLabProject.LabProject;
        }

        /// <summary>
        /// 通过编码获取检查项目 Demo
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public async Task<GrpcExamProjectModel> GetExamProjectAsync(string code)
        {
            var request = new GetExamProjectByCodeRequest
            {
                Code = code
            };
            var grpcExamProject = await this._grpcMasterDataClient.GetExamProjectByCodeAsync(request);

            return grpcExamProject.ExamProject;
        }

        /// <summary>
        /// 通过编码获取诊疗项目 Demo
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<GrpcTreatProjectModel> GetTreatAsync(int id)
        {
            var request = new GetTreatProjectByIdRequest
            {
                Id = id
            };
            var grpcTreatProject = await this._grpcMasterDataClient.GetTreatProjectByIdAsync(request);

            return grpcTreatProject.TreatProject;
        }

        #region Private Method

        private List<RecipeProject> GetExamProjects()
        {
            List<RecipeProject> list = new();
            var request = new GetAllExamProjectsRequest();
            var examProjects = this._grpcMasterDataClient.GetAllExamProjects(request).ExamProjects;
            foreach (var examProject in examProjects)
            {
                var id = this._guidGenerator.Create();
                var recipeProject = new RecipeProject(id)
                {
                    ExamProp = new RecipeExamProp(id),
                };
                this._objectMapper.Map(examProject, recipeProject);
                list.Add(recipeProject);
            }

            return list;
        }

        private List<RecipeProject> GetLabProjects()
        {
            List<RecipeProject> list = new
                List<RecipeProject>();
            var request = new GetAllLabProjectsRequest();
            var labProjects = this._grpcMasterDataClient.GetAllLabProjects(request).LabProjects;
            foreach (var labProject in labProjects)
            {
                var id = this._guidGenerator.Create();
                var recipeProject = new RecipeProject(id)
                {
                    LabProp = new RecipeLabProp(id),
                };
                this._objectMapper.Map(labProject, recipeProject);
                list.Add(recipeProject);
            }

            return list;
        }

        private List<RecipeProject> GetMedicines()
        {
            List<RecipeProject> list = new();
            var request = new GetAllMedicinesRequest();
            var medicines = this._grpcMasterDataClient.GetAllMedicines(request).Medicines;
            foreach (var medicine in medicines)
            {
                var id = this._guidGenerator.Create();
                var recipeProject = new RecipeProject(id)
                {
                    MedicineProp = new RecipeMedicineProp(id),
                };
                this._objectMapper.Map(medicine, recipeProject);
                list.Add(recipeProject);
            }

            return list;
        }

        private List<RecipeProject> GetTreats()
        {
            List<RecipeProject> list = new();
            var request = new GetAllTreatProjectsRequest();
            var treatProjects = this._grpcMasterDataClient.GetAllTreatProjects(request).TreatProjects;
            foreach (var treatProject in treatProjects)
            {
                var id = this._guidGenerator.Create();
                var recipeProject = new RecipeProject(id)
                {
                };
                this._objectMapper.Map(treatProject, recipeProject);
                list.Add(recipeProject);
            }

            return list;
        }

        #endregion
    }
}
