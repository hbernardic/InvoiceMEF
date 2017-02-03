namespace InvoiceMEF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeUserId : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Invoices", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Invoices", new[] { "ApplicationUser_Id" });
            RenameColumn(table: "dbo.Invoices", name: "ApplicationUser_Id", newName: "UserId");
            AlterColumn("dbo.Invoices", "UserId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Invoices", "UserId");
            AddForeignKey("dbo.Invoices", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Invoices", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Invoices", new[] { "UserId" });
            AlterColumn("dbo.Invoices", "UserId", c => c.String(maxLength: 128));
            RenameColumn(table: "dbo.Invoices", name: "UserId", newName: "ApplicationUser_Id");
            CreateIndex("dbo.Invoices", "ApplicationUser_Id");
            AddForeignKey("dbo.Invoices", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
