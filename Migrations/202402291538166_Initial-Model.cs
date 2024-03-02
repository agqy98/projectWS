namespace Project_WS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.staffs",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    FirstName = c.String(nullable: false, maxLength: 50, storeType: "nvarchar"),
                    LastName = c.String(nullable: false, maxLength: 50, storeType: "nvarchar"),
                    Dob = c.DateTime(precision: 0),
                })
                .PrimaryKey(t => t.Id);

        }

        public override void Down()
        {
            //DropTable("dbo.staffs");
        }
    }
}
