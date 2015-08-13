namespace PrettyCash.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTransDateToTransaction : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Transactions", "TransDate", c => c.DateTime(nullable: false));
            Sql("update dbo.transactions set transdate = createddatetime");
        }
        
        public override void Down()
        {
            DropColumn("dbo.Transactions", "TransDate");
        }
    }
}
