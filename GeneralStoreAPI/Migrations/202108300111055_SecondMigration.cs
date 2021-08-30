namespace GeneralStoreAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SecondMigration : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Transactions", "ProductSKU", c => c.String(maxLength: 128));
            CreateIndex("dbo.Transactions", "CustomerId");
            CreateIndex("dbo.Transactions", "ProductSKU");
            AddForeignKey("dbo.Transactions", "CustomerId", "dbo.Customers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Transactions", "ProductSKU", "dbo.Products", "SKU");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Transactions", "ProductSKU", "dbo.Products");
            DropForeignKey("dbo.Transactions", "CustomerId", "dbo.Customers");
            DropIndex("dbo.Transactions", new[] { "ProductSKU" });
            DropIndex("dbo.Transactions", new[] { "CustomerId" });
            AlterColumn("dbo.Transactions", "ProductSKU", c => c.String());
        }
    }
}
