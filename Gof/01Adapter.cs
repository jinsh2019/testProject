using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gof
{
    interface IHelper
    {
        void Query();
        void Add();
        void Delete();
    }

    class Oracle : IHelper
    {
        public void Add()
        {
            throw new NotImplementedException();
        }

        public void Delete()
        {
            throw new NotImplementedException();
        }

        public void Query()
        {
            throw new NotImplementedException();
        }
    }

    class Mysql : IHelper
    {
        public void Add()
        {
            throw new NotImplementedException();
        }

        public void Delete()
        {
            throw new NotImplementedException();
        }

        public void Query()
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// 类适配器
    /// </summary>
    class RedisHelperInherit : RedisHelper, IHelper
    {
        public void Add()
        {
            base.RedisAdd();
        }

        public void Delete()
        {
            base.RedisDelete();
        }

        public void Query()
        {
            base.RedisQuery();
        }
    }
    /// <summary>
    /// 对象适配器
    /// </summary>
    class RedisHelperCombination : IHelper
    {
        /// <summary>
        /// 内置字段直接初始化-- 构造函数注入、方法注入
        /// </summary>
        private RedisHelper _redisHelper = new RedisHelper();

        public RedisHelperCombination()
        {
        }

        public void Add()
        {
            this._redisHelper.RedisAdd();
        }

        public void Delete()
        {
            this._redisHelper.RedisAdd();
        }

        public void Query()
        {
            this._redisHelper.RedisAdd();
        }
    }

    /// <summary>
    /// 第三方类库 ，不可变动
    /// </summary>
    class RedisHelper
    {
        public void RedisAdd()
        {
        }

        public void RedisDelete()
        {
        }
        public void RedisQuery()
        {
        }
    }
    /// <summary>
    /// Wrapper 适配器
    /// </summary>
    class Adapter
    {
    }
}
