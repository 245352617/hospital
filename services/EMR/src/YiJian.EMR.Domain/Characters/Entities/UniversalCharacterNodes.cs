using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Domain.Entities;
using YiJian.EMR.Enums;

namespace YiJian.EMR.Characters.Entities
{

    /// <summary>
    /// 通用字符节点
    /// </summary>
    public class UniversalCharacterNode : Entity<int>
    {
        public UniversalCharacterNode(string character, int sort, int universalCharacterId)
        {
            Character = character;
            Sort = sort;
            UniversalCharacterId = universalCharacterId; 
        }


        /// <summary>
        /// 字符
        /// </summary>
        [Comment("字符")]
        [StringLength(50)]
        public string Character { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        [Comment("排序")]
        public int Sort { get; set; }
         
        /// <summary>
        /// 目录对象
        /// </summary>
        public int UniversalCharacterId { get; set; }

        /// <summary>
        /// 目录对象
        /// </summary>
        public virtual UniversalCharacter UniversalCharacter { get; set; }
         
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="character"></param>
        /// <param name="sort"></param>
        /// <param name="universalCharacterId"></param> 
        public void Update(string character, int sort, int universalCharacterId)
        {
            Character = character;
            Sort = sort;
            UniversalCharacterId = universalCharacterId;
        }

    }


}
