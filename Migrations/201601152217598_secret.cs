namespace Marjani.Peyment.Bitcoin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class secret : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "Secret", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "Secret");
        }
    }
}
