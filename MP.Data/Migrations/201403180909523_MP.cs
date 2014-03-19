namespace MP.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MP : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Items", "ItemCode", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Items", "ItemCode");
        }
    }
}
