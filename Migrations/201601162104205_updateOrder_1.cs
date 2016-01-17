namespace Marjani.Peyment.Bitcoin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateOrder_1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Orders", "Adderess");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "Adderess", c => c.String());
        }
    }
}
