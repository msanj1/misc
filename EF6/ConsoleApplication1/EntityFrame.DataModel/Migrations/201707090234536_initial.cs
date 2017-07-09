namespace EntityFrame.DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Clans",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClainName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Ninjas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ServedInOniwaban = c.Boolean(nullable: false),
                        ClainId = c.Int(nullable: false),
                        Clan_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clans", t => t.Clan_Id)
                .Index(t => t.Clan_Id);
            
            CreateTable(
                "dbo.NinjaEquipments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Type = c.Int(nullable: false),
                        Ninja_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Ninjas", t => t.Ninja_Id)
                .Index(t => t.Ninja_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.NinjaEquipments", "Ninja_Id", "dbo.Ninjas");
            DropForeignKey("dbo.Ninjas", "Clan_Id", "dbo.Clans");
            DropIndex("dbo.NinjaEquipments", new[] { "Ninja_Id" });
            DropIndex("dbo.Ninjas", new[] { "Clan_Id" });
            DropTable("dbo.NinjaEquipments");
            DropTable("dbo.Ninjas");
            DropTable("dbo.Clans");
        }
    }
}
