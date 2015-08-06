namespace PrettyCash.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNotesFieldToTransaction : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Transactions", "Notes", c => c.String(maxLength: 500));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Transactions", "Notes");
        }
    }
}
