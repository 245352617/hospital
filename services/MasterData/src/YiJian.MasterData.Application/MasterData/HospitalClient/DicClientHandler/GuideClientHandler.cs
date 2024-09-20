using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.EventBus.Distributed;
using YiJian.MasterData.MasterData.HospitalClient;

namespace YiJian.MasterData.MasterData
{
    /// <summary>
    /// 描    述 ：组套指引同步
    /// 创 建 人 ：杨凯
    /// 创建时间 ：2023/6/17 10:10:17
    /// </summary>
    public class GuideClientHandler : MasterDataAppService, IDistributedEventHandler<GuideDicEto>,
    ITransientDependency
    {
        private readonly IExamNoteRepository _examNoteRepository;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="examNoteRepository"></param>
        public GuideClientHandler(IExamNoteRepository examNoteRepository)
        {
            _examNoteRepository = examNoteRepository;
        }

        /// <summary>
        /// 事件处理
        /// </summary>
        /// <param name="eventData"></param>
        /// <returns></returns>
        public async Task HandleEventAsync(GuideDicEto eventData)
        {
            if (eventData == null) return;
            List<GuideDicEto> list = new List<GuideDicEto>() { eventData };

            //获取已存在的信息
            List<ExamNote> examNoteList = await _examNoteRepository.ToListAsync();
            //查询新增的信息
            List<ExamNote> examNote = list
                .Select(s => new ExamNote
                {
                    NoteCode = s.GuideCode,
                    NoteName = s.GuideName
                        .Replace("~r", "\r")
                        .Replace("~n", "\n")
                        .Replace("~R", "\r")
                        .Replace("~N", "\n"),
                    DisplayName = s.GuideName.Replace("~r", "\r").Replace("~n", "\n").Replace("~R", "\r")
                        .Replace("~N", "\n"),
                    DescTemplate = s.GuideName.Replace("~r", "\r").Replace("~n", "\n").Replace("~R", "\r")
                        .Replace("~N", "\n")
                }).ToList();
            //新增的
            List<ExamNote> addExamNote = examNote.Where(x => examNoteList.All(a => a.NoteCode != x.NoteCode))
                .ToList();
            //删除的
            List<ExamNote> deleteExamNote = examNoteList.Where(x => examNote.All(a => a.NoteCode != x.NoteCode))
                .ToList();
            //修改
            List<ExamNote> updateExamNote = new List<ExamNote>();
            examNote.RemoveAll(addExamNote);
            examNote.RemoveAll(deleteExamNote);
            examNote.ForEach(x =>
            {
                ExamNote data = examNoteList.FirstOrDefault(
                    g => x.NoteCode == g.NoteCode && x.NoteName != g.NoteName);
                if (data != null)
                {
                    data.Update(x.NoteName, x.NoteName, x.NoteName);
                    updateExamNote.Add(data);
                }
            });
            if (addExamNote.Any())
            {
                await _examNoteRepository.InsertManyAsync(addExamNote);
            }

            if (updateExamNote.Any())
            {
                await _examNoteRepository.UpdateManyAsync(updateExamNote);
            }

            if (deleteExamNote.Any())
            {
                await _examNoteRepository.DeleteManyAsync(deleteExamNote);
            }
        }
    }
}
