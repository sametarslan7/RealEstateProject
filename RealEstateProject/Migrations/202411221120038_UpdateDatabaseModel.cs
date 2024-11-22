namespace RealEstateProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDatabaseModel : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Builds", "buildPrice", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Builds", "buildPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
