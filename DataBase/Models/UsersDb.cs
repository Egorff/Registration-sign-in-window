using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Models
{
    public class UsersDb : DbContext
    {
        #region DbSets

        public DbSet<User> User { get; set; }

        public DbSet<Role> Role { get; set; }

        public DbSet<Salt> Salt { get; set; }

        public DbSet<Users_Role> Users_Role { get; set; }

        #endregion



        #region Methods

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDb;Initial Catalog=Users;Integrated Security=True");

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasOne(U => U.Salt).WithOne(S => S.User).HasForeignKey<Salt>(S => S.UserId);

            modelBuilder.Entity<Users_Role>().HasOne(UR => UR.Role).WithMany(R => R.User_Role).HasForeignKey(FK => FK.RoleId);

            modelBuilder.Entity<Users_Role>().HasOne(UR => UR.User).WithMany(R => R.User_Role).HasForeignKey(FK => FK.UserId);

            

            //modelBuilder.Entity<Role>().HasData(
            //    new Role(1, "ordinary"), new Role(2, "VIP")
            //    );

            //modelBuilder.Entity<User>().HasData(
            //    new User(Guid.Parse("911c1098-273e-4311-9247-438751a7332f"), "Admin", "egorolinek@gmail.com", "egorpvp1",
            //    new Salt(Guid.Parse("946a41e5-56fc-4c25-8bbe-50ea355e9747"), "egjebjrhtgb"))
            //    );

            //modelBuilder.Entity<Users_Role>().HasData(new Users_Role() 
            //{ Id = Guid.Parse("4791c465-03b5-47bf-a170-69302f0a244f"), RoleId = 2, 
            //    UserId = Guid.Parse("911c1098-273e-4311-9247-438751a7332f") });

            //modelBuilder.Entity<Salt>().HasData(new Salt() { Id = Guid.Parse() });

            base.OnModelCreating(modelBuilder); 
        }

        #endregion

        public UsersDb()
        {

        }
    }
}
