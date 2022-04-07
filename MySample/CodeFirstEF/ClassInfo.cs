using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace CodeFirstEF
{
    public class MUserInfo
    {
        [Key]
        public int ID { get; set; }
        [Required,MaxLength(50)]
        public string Name { get; set; }
        public int Age { get; set; }
        public  DateTime StarTime { get; set; }
    }

    // [DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))] 
// 可以配置文件进行配置(codeConfigurationType)
    public class SwartzUserUserContext : DbContext
    {

        //使用UserContext connectionString
        public SwartzUserUserContext():base("UserContext")
        {

        }

        public DbSet<MUserInfo> UserInfo { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //移除将表名称设置为实体名称的约定
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            //表名加swartz前缀
            modelBuilder.Types().Configure(f => f.ToTable("swartz" + f.ClrType.Name));
        }
    }

    public class DataModelInitializer : DropCreateDatabaseIfModelChanges<SwartzUserUserContext>
    {
        protected override void Seed(SwartzUserUserContext context)
        {
            //初始化数据
            context.UserInfo.Add(new MUserInfo
            {
                ID = 1,
                Age = 12,
                Name = "dzjx"
            });

            context.SaveChanges();
            base.Seed(context);
        }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            var context = new SwartzUserUserContext();
          var init=  new DataModelInitializer();
          init.InitializeDatabase(context);
        }
    }
}