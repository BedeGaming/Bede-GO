namespace Bede.Go.Contracts.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class InitialSetup : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Games",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        Distance = c.Decimal(nullable: false, precision: 18, scale: 2),
                        StartTime = c.DateTime(nullable: false),
                        Location_Longitude = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Location_Latitude = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Location_Accuracy = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("Games");
        }
    }
}
