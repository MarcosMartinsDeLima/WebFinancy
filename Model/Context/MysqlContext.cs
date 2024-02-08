using Microsoft.EntityFrameworkCore;

namespace WebFinancy.Model.Context
{
    public class MysqlContext : DbContext
    {
        public MysqlContext(){}
        public MysqlContext(DbContextOptions<MysqlContext> options):base(options){}
        public DbSet<Financy> Financy {get;set;}
        public DbSet<User> User{get;set;}
    }
}