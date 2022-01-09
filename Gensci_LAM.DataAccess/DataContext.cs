using System.Linq;
using System.Threading.Tasks;
using Gensci_LAM.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using WalkingTec.Mvvm.Core;

namespace Gensci_LAM.DataAccess
{
    public class DataContext : FrameworkContext
    {
        public DbSet<FrameworkUser> FrameworkUsers { get; set; }

        //学校
        public DbSet<School> Schools { get; set; }


        //学生
        public DbSet<Student> Students { get; set; }

        //书
        public DbSet<Book> Books { get; set; }


        public DbSet<School2> School2s { get; set; }




        #region 多对多 测试
        public DbSet<SchoolMdl> SchoolMdls { get; set; }
        public DbSet<StudentMdl> StudentMdls { get; set; }
        public DbSet<Major> Majors { get; set; }
        public DbSet<StudentMajor> StudentMajors { get; set; }
        #endregion




        public DataContext(CS cs)
             : base(cs)
        {
        }

        public DataContext(string cs, DBTypeEnum dbtype)
            : base(cs, dbtype)
        {
        }

        public DataContext(string cs, DBTypeEnum dbtype, string version = null)
            : base(cs, dbtype, version)
        {
        }


        public DataContext(DbContextOptions<DataContext> options) : base(options) { }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //指定索引
            //modelBuilder.Entity<Student>().HasIndex(x => x.StudentName);

            base.OnModelCreating(modelBuilder);
        }





        public override async Task<bool> DataInit(object allModules, bool IsSpa)
        {
            var state = await base.DataInit(allModules, IsSpa);
            bool emptydb = false;
            try
            {
                emptydb = Set<FrameworkUser>().Count() == 0 && Set<FrameworkUserRole>().Count() == 0;
            }
            catch { }
            if (state == true || emptydb == true)
            {
                //when state is true, means it's the first time EF create database, do data init here
                //当state是true的时候，表示这是第一次创建数据库，可以在这里进行数据初始化
                var user = new FrameworkUser
                {
                    ITCode = "admin",
                    Password = Utils.GetMD5String("000000"),
                    IsValid = true,
                    Name = "Admin"
                };

                var userrole = new FrameworkUserRole
                {
                    UserCode = user.ITCode,
                    RoleCode = "001"
                };

                Set<FrameworkUser>().Add(user);
                Set<FrameworkUserRole>().Add(userrole);
                await SaveChangesAsync();
            }
            return state;
        }

    }

    /// <summary>
    /// DesignTimeFactory for EF Migration, use your full connection string,
    /// EF will find this class and use the connection defined here to run Add-Migration and Update-Database
    /// </summary>
    public class DataContextFactory : IDesignTimeDbContextFactory<DataContext>
    {
        public DataContext CreateDbContext(string[] args)
        {
            return new DataContext("Server=.;User Id=sa;Password=123456;Database=Gensci_LAM_db;Trusted_Connection=True;", DBTypeEnum.SqlServer);
        }
    }

}
