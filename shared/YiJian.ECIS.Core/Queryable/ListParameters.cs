using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace YiJian.ECIS.Core
{
    public class ListParameters<T> : Criteria<T>
    {
        public object Parameters { get; set; }

        public ListParameters(Expression<Func<T, bool>> initCriteria = null) : base(initCriteria)
        {
        }
        [Newtonsoft.Json.JsonIgnore]
        public long RecordCount { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        public Expression<Func<T, object>> Asc { get; set; }
        [Newtonsoft.Json.JsonIgnore]

        public Expression<Func<T, object>> Desc { get; set; }
        [Newtonsoft.Json.JsonIgnore]
        public IEnumerable<Expression<Func<T, object>>> Includes { get; set; }

        /// <summary>
        /// 包含
        /// </summary>
        /// <param name="includeExpr"></param>
        /// <returns></returns>
        public ListParameters<T> Include(Expression<Func<T, object>> includeExpr)
        {
            IList<Expression<Func<T, object>>> includes = null;
            if (this.Includes == null) Includes = includes = new List<Expression<Func<T, object>>>();
            else
            {
                includes = this.Includes as IList<Expression<Func<T, object>>>;
                if (includes == null) Includes = includes = new List<Expression<Func<T, object>>>(this.Includes);

            }
            includes.Add(includeExpr);
            this.Includes = includes;
            return this;
        }

        IList<T> _Items;
        /// <summary>
        /// 数据集合
        /// </summary>
        public IList<T> Items
        {
            get
            {
                //if (this.RecordCount == 0) return null;
                if (_Items == null) _Items = new List<T>();
                return _Items;
            }
            set
            {
                this._Items = value;
                if (this.RecordCount == 0 && value != null) this.RecordCount = value.Count;
            }
        }


    }
}
