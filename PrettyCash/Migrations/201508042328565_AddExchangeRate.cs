namespace PrettyCash.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddExchangeRate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ExchangeRates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateTime = c.DateTime(nullable: false),
                        Data = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ExchangeRates");
        }
    }
}
