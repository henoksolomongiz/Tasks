namespace Task.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Transaction",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FromDate = c.DateTime(),
                        ToDate = c.DateTime(),
                        Position = c.String(maxLength: 50, unicode: false),
                        CompanyName = c.String(maxLength: 50, unicode: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(maxLength: 50, unicode: false),
                        LastName = c.String(maxLength: 50, unicode: false),
                        EmailId = c.String(maxLength: 50, unicode: false),
                        Address = c.String(maxLength: 50, unicode: false),
                        DateofBirth = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateStoredProcedure(
                "dbo.Transaction_Insert",
                p => new
                    {
                        FromDate = p.DateTime(),
                        ToDate = p.DateTime(),
                        Position = p.String(maxLength: 50, unicode: false),
                        CompanyName = p.String(maxLength: 50, unicode: false),
                        UserId = p.Int(),
                    },
                body:
                    @"INSERT [dbo].[Transaction]([FromDate], [ToDate], [Position], [CompanyName], [UserId])
                      VALUES (@FromDate, @ToDate, @Position, @CompanyName, @UserId)
                      
                      DECLARE @Id int
                      SELECT @Id = [Id]
                      FROM [dbo].[Transaction]
                      WHERE @@ROWCOUNT > 0 AND [Id] = scope_identity()
                      
                      SELECT t0.[Id]
                      FROM [dbo].[Transaction] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Id] = @Id"
            );
            
            CreateStoredProcedure(
                "dbo.Transaction_Update",
                p => new
                    {
                        Id = p.Int(),
                        FromDate = p.DateTime(),
                        ToDate = p.DateTime(),
                        Position = p.String(maxLength: 50, unicode: false),
                        CompanyName = p.String(maxLength: 50, unicode: false),
                        UserId = p.Int(),
                    },
                body:
                    @"UPDATE [dbo].[Transaction]
                      SET [FromDate] = @FromDate, [ToDate] = @ToDate, [Position] = @Position, [CompanyName] = @CompanyName, [UserId] = @UserId
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.Transaction_Delete",
                p => new
                    {
                        Id = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[Transaction]
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.User_Insert",
                p => new
                    {
                        FirstName = p.String(maxLength: 50, unicode: false),
                        LastName = p.String(maxLength: 50, unicode: false),
                        EmailId = p.String(maxLength: 50, unicode: false),
                        Address = p.String(maxLength: 50, unicode: false),
                        DateofBirth = p.DateTime(),
                    },
                body:
                    @"INSERT [dbo].[User]([FirstName], [LastName], [EmailId], [Address], [DateofBirth])
                      VALUES (@FirstName, @LastName, @EmailId, @Address, @DateofBirth)
                      
                      DECLARE @Id int
                      SELECT @Id = [Id]
                      FROM [dbo].[User]
                      WHERE @@ROWCOUNT > 0 AND [Id] = scope_identity()
                      
                      SELECT t0.[Id]
                      FROM [dbo].[User] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Id] = @Id"
            );
            
            CreateStoredProcedure(
                "dbo.User_Update",
                p => new
                    {
                        Id = p.Int(),
                        FirstName = p.String(maxLength: 50, unicode: false),
                        LastName = p.String(maxLength: 50, unicode: false),
                        EmailId = p.String(maxLength: 50, unicode: false),
                        Address = p.String(maxLength: 50, unicode: false),
                        DateofBirth = p.DateTime(),
                    },
                body:
                    @"UPDATE [dbo].[User]
                      SET [FirstName] = @FirstName, [LastName] = @LastName, [EmailId] = @EmailId, [Address] = @Address, [DateofBirth] = @DateofBirth
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.User_Delete",
                p => new
                    {
                        Id = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[User]
                      WHERE ([Id] = @Id)"
            );
            
        }
        
        public override void Down()
        {
            DropStoredProcedure("dbo.User_Delete");
            DropStoredProcedure("dbo.User_Update");
            DropStoredProcedure("dbo.User_Insert");
            DropStoredProcedure("dbo.Transaction_Delete");
            DropStoredProcedure("dbo.Transaction_Update");
            DropStoredProcedure("dbo.Transaction_Insert");
            DropForeignKey("dbo.Transaction", "UserId", "dbo.User");
            DropIndex("dbo.Transaction", new[] { "UserId" });
            DropTable("dbo.User");
            DropTable("dbo.Transaction");
        }
    }
}
