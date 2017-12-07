namespace Bede.Go.Contracts.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class AddLocations : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Locations",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Longitude = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Latitude = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Accuracy = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("Games", "Location_Id", c => c.Long());
            AddForeignKey("Games", "Location_Id", "Locations", "Id");
            CreateIndex("Games", "Location_Id");
            DropColumn("Games", "Location_Longitude");
            DropColumn("Games", "Location_Latitude");
            DropColumn("Games", "Location_Accuracy");
        }
        
        public override void Down()
        {
            AddColumn("Games", "Location_Accuracy", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("Games", "Location_Latitude", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("Games", "Location_Longitude", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropIndex("Games", new[] { "Location_Id" });
            DropForeignKey("Games", "Location_Id", "Locations");
            DropColumn("Games", "Location_Id");
            DropTable("Locations");
        }
    }
}
