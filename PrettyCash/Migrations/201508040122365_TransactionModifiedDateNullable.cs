namespace PrettyCash.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TransactionModifiedDateNullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Transactions", "ModifiedDateTime", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Transactions", "ModifiedDateTime", c => c.DateTime(nullable: false));
        }
    }
}
