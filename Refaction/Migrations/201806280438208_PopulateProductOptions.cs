namespace Refaction.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateProductOptions : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO ProductOptions VALUES ('0643ccf0-ab00-4862-b3c5-40e2731abcc9', 'White', 'White Samsung Galaxy S7', '8f2e9176-35ee-4f0a-ae55-83023d2db1a3')");
            Sql("INSERT INTO ProductOptions VALUES ('a21d5777-a655-4020-b431-624bb331e9a2', 'Black', 'Black Samsung Galaxy S7', '8f2e9176-35ee-4f0a-ae55-83023d2db1a3')");
            Sql("INSERT INTO ProductOptions VALUES ('5c2996ab-54ad-4999-92d2-89245682d534', 'Rose Gold', 'Gold Apple iPhone 6S', 'de1287c0-4b15-4a7b-9d8a-dd21b3cafec3')");
            Sql("INSERT INTO ProductOptions VALUES ('9ae6f477-a010-4ec9-b6a8-92a85d6c5f03', 'White', 'White Apple iPhone 6S', 'de1287c0-4b15-4a7b-9d8a-dd21b3cafec3')");
            Sql("INSERT INTO ProductOptions VALUES ('4e2bc5f2-699a-4c42-802e-ce4b4d2ac0ef', 'Black', 'Black Apple iPhone 6S', 'de1287c0-4b15-4a7b-9d8a-dd21b3cafec3')");

        }

        public override void Down()
        {
        }
    }
}
