namespace RealEstateProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrationName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Offices", "officeCity", c => c.String());
            DropColumn("dbo.Offices", "officeCountry");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Offices", "officeCountry", c => c.String());
            DropColumn("dbo.Offices", "officeCity");
        }
    }
}
