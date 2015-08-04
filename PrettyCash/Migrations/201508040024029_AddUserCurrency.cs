namespace PrettyCash.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserCurrency : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserCurrencies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ApplicationUser_Id = c.String(maxLength: 128),
                        Currency_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .ForeignKey("dbo.Currencies", t => t.Currency_Id)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.Currency_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserCurrencies", "Currency_Id", "dbo.Currencies");
            DropForeignKey("dbo.UserCurrencies", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.UserCurrencies", new[] { "Currency_Id" });
            DropIndex("dbo.UserCurrencies", new[] { "ApplicationUser_Id" });
            DropTable("dbo.UserCurrencies");
        }
    }
}
