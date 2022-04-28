namespace BITCollege_RS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class storedProcedure : DbMigration
    {
        public override void Up()
        {
            this.Sql(Properties.Resource.create_next_number);
        }
        
        public override void Down()
        {
            this.Sql(Properties.Resource.drop_next_number);
        }
    }
}
