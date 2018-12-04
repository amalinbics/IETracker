namespace IETracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedTransactionType : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO TransactionTypes (ID,Type) VALUES (1,'Income')");
            Sql("INSERT INTO TransactionTypes (ID,Type) VALUES (2,'Expense')");
        }
        
        public override void Down()
        {
        }
    }
}
