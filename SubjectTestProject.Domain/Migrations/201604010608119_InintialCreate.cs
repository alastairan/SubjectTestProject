namespace SubjectTestProject.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InintialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Assessments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(),
                        Type = c.Int(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        Subject_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Subjects", t => t.Subject_Id)
                .Index(t => t.Subject_Id);
            
            CreateTable(
                "dbo.Units",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(),
                        Name = c.String(),
                        AssessmentRequirements = c.String(),
                        Elements = c.String(),
                        Assessment_Id = c.Int(),
                        Subject_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Assessments", t => t.Assessment_Id)
                .ForeignKey("dbo.Subjects", t => t.Subject_Id)
                .Index(t => t.Assessment_Id)
                .Index(t => t.Subject_Id);
            
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(),
                        Name = c.String(),
                        ParentCode = c.String(),
                        ParentTitle = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Subjects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CourseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
                .Index(t => t.CourseId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Units", "Subject_Id", "dbo.Subjects");
            DropForeignKey("dbo.Subjects", "CourseId", "dbo.Courses");
            DropForeignKey("dbo.Assessments", "Subject_Id", "dbo.Subjects");
            DropForeignKey("dbo.Units", "Assessment_Id", "dbo.Assessments");
            DropIndex("dbo.Subjects", new[] { "CourseId" });
            DropIndex("dbo.Units", new[] { "Subject_Id" });
            DropIndex("dbo.Units", new[] { "Assessment_Id" });
            DropIndex("dbo.Assessments", new[] { "Subject_Id" });
            DropTable("dbo.Subjects");
            DropTable("dbo.Courses");
            DropTable("dbo.Units");
            DropTable("dbo.Assessments");
        }
    }
}
