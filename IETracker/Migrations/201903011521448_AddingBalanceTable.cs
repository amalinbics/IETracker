namespace IETracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingBalanceTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Balances",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BalanceDate = c.DateTime(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Balances");
        }
    }
}
