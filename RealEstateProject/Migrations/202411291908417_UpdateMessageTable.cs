namespace RealEstateProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateMessageTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Messages", "messageDetail", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Messages", "messageDetail");
        }
    }
}
