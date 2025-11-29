using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GymManagmentSystem.DataBase
{
    public class GymDb:IdentityDbContext<User>
    {


        public DbSet<Membership> Memberships { get; set; }
        public DbSet<Trainer> Trainers { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<WorkOutPlan> TrainingPlans { get; set; }
        public DbSet<WorkOut> Workouts { get; set; }

        public DbSet<MemberMembership> MemberMemberships { get; set; }

        public DbSet<Payment> Payments { get; set; }







        public GymDb(DbContextOptions options):base(options)
        {
           
            
        }







        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<MemberMembership>()
                .HasKey(mm => new { mm.MemberId, mm.MembershipId });


            SeedRoles(builder);
            SeedUsers(builder);
            SeedUserRoles(builder);
            SeedMemberships(builder);
            SeedMemberMembership(builder);



        }



        private void SeedRoles(ModelBuilder builder)
        {
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Id = "role-admin",
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole
                {
                    Id = "role-trainer",
                    Name = "Trainer",
                    NormalizedName = "TRAINER"
                },
                new IdentityRole
                {
                    Id = "role-member",
                    Name = "Member",
                    NormalizedName = "MEMBER"
                }
            );
        }

     
        
        private void SeedUsers(ModelBuilder builder)
        {
            var hasher = new PasswordHasher<User>();

            var admin = new User
            {
                Id = "user-admin-1",
                UserName = "admin@gym.com",
                NormalizedUserName = "ADMIN@GYM.COM",
                Email = "admin@gym.com",
                NormalizedEmail = "ADMIN@GYM.COM",
                EmailConfirmed = true,
                FirstName = "Admin",
                SecName="Admin",
                SecurityStamp = Guid.NewGuid().ToString()
            };

            admin.PasswordHash = hasher.HashPassword(admin, "Admin123!");

            var trainer = new Trainer
            {
                Id = "trainer-1",
                UserName = "trainer1@gym.com",
                NormalizedUserName = "TRAINER1@GYM.COM",
                Email = "trainer1@gym.com",
                NormalizedEmail = "TRAINER1@GYM.COM",
                EmailConfirmed = true,
                FirstName = "Mohamed",
                SecName = "Mohsen"
            };
            trainer.PasswordHash = hasher.HashPassword(trainer, "Trainer123!");

            var member = new Member
            {
                Id = "member-1",
                UserName = "member1@gym.com",
                NormalizedUserName = "MEMBER1@GYM.COM",
                Email = "member1@gym.com",
                NormalizedEmail = "MEMBER1@GYM.COM",
                EmailConfirmed = true,
                FirstName = "Mohamed",
                SecName = "Ibrahim"
            };
            member.PasswordHash = hasher.HashPassword(member, "Member123!");

            builder.Entity<Member>().HasData( member);
            builder.Entity<Trainer>().HasData(trainer);
            builder.Entity<User>().HasData(admin);
        }

       
     
        private void SeedUserRoles(ModelBuilder builder)
        {
            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    UserId = "trainer-1",
                    RoleId = "role-trainer"
                },
                new IdentityUserRole<string>
                {
                    UserId = "member-1",
                    RoleId = "role-member"
                },

                new IdentityUserRole<string>
                {
                    UserId = "user-admin-1",
                    RoleId = "role-admin"
                }
            );
        }

   
        // SEED MEMBERSHIP PLANS
        private void SeedMemberships(ModelBuilder builder)
        {
            builder.Entity<Membership>().HasData(
                new Membership { Id = 1, Name = "Basic", DurationInDays = 30, Price = 20 },
                new Membership { Id = 2, Name = "Standard", DurationInDays = 90, Price = 50 },
                new Membership { Id = 3, Name = "Premium", DurationInDays = 365, Price = 150 }
            );
        }

        private void SeedMemberMembership(ModelBuilder builder)
        {
            builder.Entity<MemberMembership>().HasData(
                new MemberMembership
                {
                   
                    MemberId = "member-1",
                    MembershipId = 1,
                    StartDate = DateTime.UtcNow,
                    EndDate = DateTime.UtcNow.AddMonths(1),
                    IsActive = true
                }
            );
        }


    }
}
