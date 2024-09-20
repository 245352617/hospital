using Mapster;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Szyjian.Ecis.Patient.Application.Contracts;
using Szyjian.Ecis.Patient.Security;
using Volo.Abp.Security.Claims;
using Volo.Abp.Users;
using Xunit;

namespace Szyjian.Ecis.Patient.Application.Test
{
    public class ReportCardAppServiceTests : SzyjianEcisPatientApplicationTestBase
    {
        private readonly IReportCardAppService _reportCardAppService;
        ////private readonly ICurrentPrincipalAccessor _currentPrincipalAccessor;
        ////private readonly ICurrentUser _currentUser;

        public ReportCardAppServiceTests()
        {
            //_currentPrincipalAccessor = ServiceProvider.GetRequiredService<ICurrentPrincipalAccessor>();
            //_currentUser = ServiceProvider.GetRequiredService<ICurrentUser>();
            _reportCardAppService = ServiceProvider.GetRequiredService<IReportCardAppService>();
        }

        /// <summary>
        /// 创建报卡，测试是否返回新建报卡的GUID
        /// </summary>
        [Fact]
        public async Task ShouldReturnReportCardIDAsync()
        {
            //var fakeUserId = "1";
            //var fakeUserName = "FakeUser";
            //var identity = new ClaimsIdentity();
            //identity.AddClaim(new Claim(AbpClaimTypes.UserId, fakeUserId));
            //identity.AddClaim(new Claim(AbpClaimTypes.UserName, fakeUserName));

            //var principal = new ClaimsPrincipal(identity);
            //var currentPrincipalAccessor = GetRequiredService<ICurrentPrincipalAccessor>();
            //currentPrincipalAccessor.CurrentPrincipal = principal;

            Guid guid = Guid.NewGuid(); //使用GUID作为变数，以确保插入的Code和Name不会和数据库里的一致
            var dto = new ReportCardCreateDto() { Code = $"kuanquan_weec_{guid}", Name = $"狂犬病_weec_{guid}", IsActived = true, Sort = 11 };
            var result = await _reportCardAppService.CreateReportCardAsync(dto, new System.Threading.CancellationToken());
            Assert.Equal(200, (int)result.Code);
            Assert.True(Guid.TryParse(result.Data, out Guid newGuid));
        }

        /// <summary>
        /// 创建报卡，测试是否能插入相同Code或Name的报卡
        /// </summary>
        [Fact]
        public async Task ReportCardShouldNotCreateAsync()
        {
            //await _reportCardAppService.DeleteReportCardAsync(Guid.Empty);

            Guid guid = Guid.NewGuid();
            var dto = new ReportCardCreateDto() { Code = $"kuanquan_weec_{guid}", Name = $"狂犬病_weec_{guid}", IsActived = true, Sort = 11 };
            var result = await _reportCardAppService.CreateReportCardAsync(dto, new System.Threading.CancellationToken());
            Assert.Equal(200, (int)result.Code);

            var dtoSameCode = dto.BuildAdapter().AdaptToType<ReportCardCreateDto>();
            dtoSameCode.Name += Guid.NewGuid(); 
            var resultSameCode = await _reportCardAppService.CreateReportCardAsync(dtoSameCode, new System.Threading.CancellationToken());
            Assert.Equal(400, (int)resultSameCode.Code);
            Console.WriteLine(resultSameCode.Msg);

            var dtoSameName = dto.BuildAdapter().AdaptToType<ReportCardCreateDto>();
            dtoSameName.Code += Guid.NewGuid();
            var resultSameName = await _reportCardAppService.CreateReportCardAsync(dtoSameName, new System.Threading.CancellationToken());
            Assert.Equal(400, (int)resultSameName.Code);
            Console.WriteLine(resultSameName.Msg);
        }

        //[Fact]
        //public async Task GetReportCardListAsync()
        //{
        //    var result = await _reportCardAppService.GetReportCardListAsync(new System.Threading.CancellationToken());
        //    Assert.NotNull(result);
        //    Assert.Equal(200, (int)result.Code);
        //}

        //[Fact]
        //public async Task GetReportCardAsync()
        //{
        //    var result = await _reportCardAppService.GetReportCardAsync(reportCardId, new System.Threading.CancellationToken());
        //    Assert.NotNull(result);
        //    Assert.Equal(200, (int)result.Code);
        //}
    }
}
