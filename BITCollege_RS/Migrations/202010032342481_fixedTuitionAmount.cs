namespace BITCollege_RS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixedTuitionAmount : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Courses", "TuitionAmount", c => c.Double(nullable: false));
            DropColumn("dbo.Courses", "TutionAmount");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Courses", "TutionAmount", c => c.Double(nullable: false));
            DropColumn("dbo.Courses", "TuitionAmount");
        }
    }
}
