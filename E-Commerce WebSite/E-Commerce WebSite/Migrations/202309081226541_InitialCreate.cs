namespace E_Commerce_WebSite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 100),
                    Description = c.String(nullable: false, maxLength: 500),
                    // Add other Category table columns here...
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Products",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Image = c.String(),
                    Brand = c.String(nullable: false, maxLength: 50),
                    ProductName = c.String(nullable: false, maxLength: 50),
                    // Add other Product table columns here...
                    CategoryId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId);

            // You may add additional actions here, such as adding indexes, seeding data, etc.
        }

        public override void Down()
        {
            // Define how to revert the changes made in the Up method, such as dropping tables or removing indexes.
            DropForeignKey("dbo.Products", "CategoryId", "dbo.Categories");
            DropIndex("dbo.Products", new[] { "CategoryId" });
            DropTable("dbo.Products");
            DropTable("dbo.Categories");
        }

    }
}
