namespace IETracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class AddUserandRole : DbMigration
    {
        public override void Up()
        {
            Sql(@"INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'15b521a9-997e-4dd6-9f79-c7157b23db8e', N'Admin')");

            Sql(@"INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) 
                VALUES (N'339d3fb1-bc0a-4e07-bda0-e8249514b457', N'admin@gmail.com', 0, N'ALmni16evWalaD8U7TSPfbi2UbXiAzgeQtUSMkTsSm78XC/fGqBfyWzF4lhTbGCLjw==', N'fdfc1590-f595-48ac-bc9f-befb5e2db148', NULL, 0, 0, NULL, 1, 0, N'admin@gmail.com')");

            Sql(@"INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) 
                VALUES (N'd96c75a9-c3f4-48ce-8a52-ff494607ad58', N'guest@gmail.com', 0, N'AG5xMNfEZCTI+s9m44vYmF68BRjOG73fVw3okgn45eBU/NbZmH7QTS3TVE0UGiVILw==', N'9a8ac904-c4ea-4158-bdf9-c6ac7eff346b', NULL, 0, 0, NULL, 1, 0, N'guest@gmail.com')");

            Sql(@"INSERT INTO[dbo].[AspNetUserRoles]
                ([UserId], [RoleId]) VALUES(N'339d3fb1-bc0a-4e07-bda0-e8249514b457', N'15b521a9-997e-4dd6-9f79-c7157b23db8e')");

        }

        public override void Down()
        {
        }
    }
}
