using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualBasic;

namespace DateStruct
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Version version = new Version()
            {
                EffectDate = new DateTime(2020, 01, 10)
            };
            Version updateHeaderDateTime = new Version()
            {
                EffectDate = new DateTime(2020, 01, 1)
            };
            Version correctHeaderDateTime = new Version()
            {
                EffectDate = new DateTime(2020, 01, 1)
            };
            Version shot1 = new Version()
            {
                EffectDate = new DateTime(2020, 03, 01)
            };
            Version shot2 = new Version()
            {
                EffectDate = new DateTime(2020, 02, 15)
            };
            Version shot3 = new Version()
            {
                EffectDate = new DateTime(2020, 09, 15)
            };
            Version shot4 = new Version()
            {
                EffectDate = new DateTime(2020, 09, 15)
            };
            Version shot5 = new Version()
            {
                EffectDate = new DateTime(2020, 11, 15)
            };
            Period period = new Period();
            period.AddVersion(version);
            period.AddVersion(correctHeaderDateTime);
            period.CorrectVersion(version.EffectDate,correctHeaderDateTime);
            period.AddVersion(shot1);
            period.AddVersion(shot2);
            period.AddVersion(shot3);
            period.AddVersion(shot4);
            period.AddVersion(shot5);

            var eshot = period.getEffectShot(new DateTime(2020, 12, 15));
            period.DeleteTailVersion();
            period.DeleteTailVersion();
            period.DeleteTailVersion();
            period.DeleteTailVersion();
            period.DeleteTailVersion();
        }
    }

    class Version
    {
        public DateTime EffectDate { get; set; }
        public DateTime? ExpireDate { get; set; }
    }

    class Period
    {
        private List<Version> ListShot = new List<Version>();
        private DateTime? HeaderDateTime => ListShot.OrderByDescending(x => x.EffectDate).FirstOrDefault()?.EffectDate;

        /// <summary>
        /// 添加版本
        /// </summary>
        /// <param name="version"></param>
        public void AddVersion(Version version)
        {
            if (ListShot.Count > 0)
            {
                var maxShot = ListShot.OrderByDescending(x => x.EffectDate).FirstOrDefault(x =>
                    x.EffectDate < version.EffectDate);
                if (maxShot == null)
                {
                    Console.WriteLine("不能小于头日期！");
                    return;
                }
                /// Correct的Case
                if (maxShot.ExpireDate != null && maxShot.ExpireDate == version.EffectDate.AddDays(-1))
                {
                    CorrectVersion(version.EffectDate, version);
                    return;
                }
                var tmpExpireDate = maxShot.ExpireDate;
                maxShot.ExpireDate = version.EffectDate.AddDays(-1);
                version.ExpireDate = tmpExpireDate;
                ListShot.Add(version);
            }
            else
                ListShot.Add(version);
        }

        /// <summary>
        /// 删除版本
        /// </summary>
        /// <param name="shot"></param>
        public void DeleteVersion(DateTime? date)
        {
            ListShot = ListShot.OrderByDescending(x => x.EffectDate).ToList();
            var targetVersion = getEffectShot(date ?? DateTime.Today);
            if (targetVersion.EffectDate == HeaderDateTime)
            {       
                Console.WriteLine("不能删除头版本！");
                return;
            }
            int index = ListShot.IndexOf(targetVersion);
            ListShot[index].ExpireDate = targetVersion.ExpireDate;
            ListShot.Remove(targetVersion);
        }

        /// <summary>
        /// 修正版本
        /// </summary>
        /// <param name="date">修正日期</param>
        /// <param name="targetVersion">目标版本</param>
        public void CorrectVersion(DateTime? date, Version targetVersion)
        {
            date = date ?? DateTime.Today;
            var toChanged = ListShot.OrderByDescending(x => x.EffectDate <= date).FirstOrDefault();
            if (toChanged == null)
            {
                Console.WriteLine("不能修正小于最早版本日期！");
            }
            else
            {
                Console.WriteLine("已修正！");
            }
        }

        /// <summary>
        /// 扩展方法
        /// </summary>
        public void DeleteTailVersion()
        {
            if (ListShot.Count == 1)
            {
                Console.WriteLine("Remain One");
                return;
            }
            var maxShot = ListShot.OrderByDescending(x => x.EffectDate).FirstOrDefault();
            var tempEffectDate = maxShot.EffectDate;
            var ToChangeShot = ListShot.FirstOrDefault(x => x.ExpireDate == tempEffectDate.AddDays(-1));
            ToChangeShot.ExpireDate = null;
            ListShot.Remove(maxShot);
        }

        /// <summary>
        /// 获取有效版本
        /// </summary>
        /// <param name="searchDate"></param>
        /// <returns></returns>
        public Version getEffectShot(DateTime? searchDate = null)
        {
            searchDate = searchDate ?? DateTime.Today;
            return ListShot.FirstOrDefault(x =>
                x.EffectDate < searchDate && (x.ExpireDate == null || x.ExpireDate > searchDate));
        }
    }
}
