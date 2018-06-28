namespace Refaction.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateProducts : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Products (Id, Name, Description, Price, DeliveryPrice) VALUES ('8f2e9176-35ee-4f0a-ae55-83023d2db1a3', 'Samsung Galaxy S7', 'Newest mobile product from Samsung.', 1024.99, 16.99)");
            Sql("INSERT INTO Products (Id, Name, Description, Price, DeliveryPrice) VALUES ('de1287c0-4b15-4a7b-9d8a-dd21b3cafec3', 'Apple iPhone 6S', 'Newest mobile product from Apple.', 1299.99, 15.99)");
           
        }

        public override void Down()
        {
        }
    }
}
