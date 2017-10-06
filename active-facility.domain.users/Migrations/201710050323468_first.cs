namespace active_facility.domain.users.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class first : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.User", "Company_Id", "dbo.Company");
            DropIndex("dbo.User", new[] { "Company_Id" });
            AddColumn("dbo.User", "CompanyId", c => c.Guid(nullable: false));
        }
        
        public override void Down()
        {
            AddColumn("dbo.User", "Company_Id", c => c.Guid());
            CreateIndex("dbo.User", "Company_Id");
            AddForeignKey("dbo.User", "Company_Id", "dbo.Company", "Id");
        }
    }
}
