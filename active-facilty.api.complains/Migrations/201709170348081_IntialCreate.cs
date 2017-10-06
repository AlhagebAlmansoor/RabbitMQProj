namespace active_facilty.api.complains.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IntialCreate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Complainer", "Id", c => c.Guid(nullable: false, identity: true, defaultValueSql: "newsequentialid()"));
        }
        
        public override void Down()
        {
        }
    }
}
