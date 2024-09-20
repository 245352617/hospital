using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using System.Collections.Generic;
using System.Linq;
using Szyjian.Ecis.Patient.Application.Contracts;
using Szyjian.Ecis.Patient.Domain;
using Szyjian.Ecis.Patient.Domain.Shared;

namespace Szyjian.Ecis.Patient.Application
{
    /// <summary>
    /// 床卡配置服务
    /// </summary>
    [Authorize]
    public class BedCardAppService : EcisPatientAppService, IBedCardAppService
    {
        private readonly IFreeSql _freeSql;

        public BedCardAppService(IFreeSql freeSql)
        {
            _freeSql = freeSql;
        }

        /// <summary>
        /// 添加床卡配置
        /// </summary>
        /// <param name="bedCard"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseResult<bool> AddBedCard(BedCard bedCard)
        {
            if (bedCard == null) return RespUtil.Error<bool>(msg: "请求参数为空");

            List<BedCard> bedCardList = _freeSql.Select<BedCard>().ToList();
            int sort = bedCard.Sort;
            if (bedCardList.Any(x => x.Sort == sort))
            {
                bedCard.Sort += 1;
                sort = bedCard.Sort;
            }

            List<BedCard> sortList = bedCardList.Where(x => x.Sort >= sort).OrderBy(x => x.Sort).ToList();
            List<BedCard> updateList = new List<BedCard>();
            foreach (BedCard item in sortList)
            {
                if (item.Sort == sort)
                {
                    item.Sort += 1;
                    sort++;
                    updateList.Add(item);
                }
            }

            var id = _freeSql.Insert(bedCard).ExecuteIdentity();
            foreach (var item in updateList)
            {
                _freeSql.Update<BedCard>(item.Id).Set(x => x.Sort, item.Sort).ExecuteAffrows();
            }

            if (bedCard.IsDefault)
            {
                _freeSql.Update<BedCard>().Set(x => x.IsDefault, false).Where(x => x.Id != id).ExecuteAffrows();
            }

            return RespUtil.Ok(data: true);
        }

        /// <summary>
        /// 编辑床卡配置
        /// </summary>
        /// <param name="bedCard"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseResult<bool> EditBedCard(BedCard bedCard)
        {
            if (bedCard == null) return RespUtil.Error<bool>(msg: "请求参数为空");
            List<BedCard> bedCardList = _freeSql.Select<BedCard>().ToList();
            BedCard oldBedCard = bedCardList.FirstOrDefault(x => x.Id == bedCard.Id);
            if (oldBedCard == null) return RespUtil.Error<bool>(msg: "没找到要更新的记录");

            int sort = bedCard.Sort;
            if (bedCardList.Any(x => x.Sort == sort && x.Id != bedCard.Id))
            {
                bedCard.Sort += 1;
                sort = bedCard.Sort;
            }

            List<BedCard> sortList = bedCardList.Where(x => x.Sort >= sort && x.Id != bedCard.Id).OrderBy(x => x.Sort).ToList();
            List<BedCard> updateList = new List<BedCard>();
            foreach (BedCard item in sortList)
            {
                if (item.Sort == sort)
                {
                    item.Sort += 1;
                    sort++;
                    updateList.Add(item);
                }
            }

            _freeSql.Update<BedCard>(bedCard.Id)
                .Set(x => x.Sort, bedCard.Sort)
                .Set(x => x.BedColor, bedCard.BedColor)
                .Set(x => x.ColorRGB, bedCard.ColorRGB)
                .Set(x => x.Label, bedCard.Label)
                .Set(x => x.TriageLevel, bedCard.TriageLevel)
                .Set(x => x.NurseLevel, bedCard.NurseLevel)
                .Set(x => x.IsActive, bedCard.IsActive)
                .Set(x => x.IsDefault, bedCard.IsDefault)
                .ExecuteAffrows();
            foreach (var item in updateList)
            {
                _freeSql.Update<BedCard>(item.Id).Set(x => x.Sort, item.Sort).ExecuteAffrows();
            }

            if (bedCard.IsDefault)
            {
                _freeSql.Update<BedCard>().Set(x => x.IsDefault, false).Where(x => x.Id != bedCard.Id).ExecuteAffrows();
            }

            return RespUtil.Ok(data: true);
        }

        /// <summary>
        /// 删除床卡配置
        /// </summary>
        /// <param name="bedCard"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseResult<bool> DeleteBedCard(BedCard bedCard)
        {
            if (bedCard == null) return RespUtil.Error<bool>(msg: "请求参数为空");

            _freeSql.Delete<BedCard>(bedCard).ExecuteAffrows();
            return RespUtil.Ok(data: true);
        }

        /// <summary>
        /// 获取床卡配置列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ResponseResult<List<BedCard>> GetBedCardList()
        {
            List<BedCard> bedCardList = _freeSql.Select<BedCard>().ToList();

            bedCardList = bedCardList.OrderBy(x => x.Sort).ToList(); ;
            return RespUtil.Ok(data: bedCardList);
        }
    }
}
