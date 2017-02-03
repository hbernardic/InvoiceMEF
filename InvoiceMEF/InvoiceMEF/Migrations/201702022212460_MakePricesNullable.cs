namespace InvoiceMEF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MakePricesNullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Invoices", "TotalPrice", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Invoices", "TotalPriceAfterTax", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Invoices", "TotalPriceAfterTax", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Invoices", "TotalPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
