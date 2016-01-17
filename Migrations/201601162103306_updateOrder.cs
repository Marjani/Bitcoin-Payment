namespace Marjani.Peyment.Bitcoin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateOrder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "TransactionHash", c => c.String());
            AddColumn("dbo.Orders", "Address", c => c.String());
            AddColumn("dbo.Orders", "confirmations", c => c.String());
            AddColumn("dbo.Orders", "Value", c => c.String());
            AddColumn("dbo.Orders", "PayOn", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "PayOn");
            DropColumn("dbo.Orders", "Value");
            DropColumn("dbo.Orders", "confirmations");
            DropColumn("dbo.Orders", "Address");
            DropColumn("dbo.Orders", "TransactionHash");
        }
    }
}
