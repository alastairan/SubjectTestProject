namespace SubjectTestProject.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
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
                "dbo.CourseDeliveries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DeliveryType = c.Int(nullable: false),
                        Semestar_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Semestars", t => t.Semestar_Id)
                .Index(t => t.Semestar_Id);
            
            CreateTable(
                "dbo.Semestars",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        Code = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SubjectDeliveries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Subject_Id = c.Int(),
                        CourseDelivery_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Subjects", t => t.Subject_Id)
                .ForeignKey("dbo.CourseDeliveries", t => t.CourseDelivery_Id)
                .Index(t => t.Subject_Id)
                .Index(t => t.CourseDelivery_Id);
            
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
                "dbo.CourseUnits",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IsEssential = c.Boolean(),
                        UnitId = c.Int(nullable: false),
                        Course_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Units", t => t.UnitId, cascadeDelete: true)
                .ForeignKey("dbo.Courses", t => t.Course_Id)
                .Index(t => t.UnitId)
                .Index(t => t.Course_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SubjectDeliveries", "CourseDelivery_Id", "dbo.CourseDeliveries");
            DropForeignKey("dbo.SubjectDeliveries", "Subject_Id", "dbo.Subjects");
            DropForeignKey("dbo.Units", "Subject_Id", "dbo.Subjects");
            DropForeignKey("dbo.Subjects", "CourseId", "dbo.Courses");
            DropForeignKey("dbo.CourseUnits", "Course_Id", "dbo.Courses");
            DropForeignKey("dbo.CourseUnits", "UnitId", "dbo.Units");
            DropForeignKey("dbo.Assessments", "Subject_Id", "dbo.Subjects");
            DropForeignKey("dbo.CourseDeliveries", "Semestar_Id", "dbo.Semestars");
            DropForeignKey("dbo.Units", "Assessment_Id", "dbo.Assessments");
            DropIndex("dbo.CourseUnits", new[] { "Course_Id" });
            DropIndex("dbo.CourseUnits", new[] { "UnitId" });
            DropIndex("dbo.Subjects", new[] { "CourseId" });
            DropIndex("dbo.SubjectDeliveries", new[] { "CourseDelivery_Id" });
            DropIndex("dbo.SubjectDeliveries", new[] { "Subject_Id" });
            DropIndex("dbo.CourseDeliveries", new[] { "Semestar_Id" });
            DropIndex("dbo.Units", new[] { "Subject_Id" });
            DropIndex("dbo.Units", new[] { "Assessment_Id" });
            DropIndex("dbo.Assessments", new[] { "Subject_Id" });
            DropTable("dbo.CourseUnits");
            DropTable("dbo.Courses");
            DropTable("dbo.Subjects");
            DropTable("dbo.SubjectDeliveries");
            DropTable("dbo.Semestars");
            DropTable("dbo.CourseDeliveries");
            DropTable("dbo.Units");
            DropTable("dbo.Assessments");
        }
    }
}
