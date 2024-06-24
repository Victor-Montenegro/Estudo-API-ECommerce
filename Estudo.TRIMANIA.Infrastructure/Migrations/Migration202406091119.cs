using FluentMigrator;

namespace Estudo.TRIMANIA.Infrastructure.Migrations
{
    [Migration(202406091119)]
    public class Migration202406091119 : Migration
    {
        public override void Down()
        {
        }

        public override void Up()
        {
            Create.Table("User")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("creation_date").AsDateTime2().NotNullable()
                .WithColumn("UpdatedAt").AsDateTime2().NotNullable()
                .WithColumn("DeletedAt").AsDateTime2().Nullable()
                .WithColumn("Birthday").AsDateTime2().Nullable()
                .WithColumn("CPF").AsString(11).NotNullable()
                .WithColumn("Name").AsString(255).NotNullable()
                .WithColumn("Login").AsString(255).NotNullable().Unique()
                .WithColumn("Email").AsString(255).NotNullable()
                .WithColumn("Identification").AsGuid().NotNullable()
                .WithColumn("Password").AsString(500).NotNullable();

            Create.Table("Address")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("UserId").AsInt32().NotNullable()
                .WithColumn("creation_date").AsDateTime2().NotNullable()
                .WithColumn("UpdatedAt").AsDateTime2().NotNullable() 
                .WithColumn("DeletedAt").AsDateTime2().Nullable()
                .WithColumn("City").AsString(50).NotNullable()
                .WithColumn("State").AsString(50).NotNullable()
                .WithColumn("Street").AsString(255).NotNullable()
                .WithColumn("Number").AsString(30).NotNullable()
                .WithColumn("Neighborhood").AsString(255).NotNullable();

            Create.Table("Order")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("UserId").AsInt32().NotNullable()
                .WithColumn("creation_date").AsDateTime2().NotNullable()
                .WithColumn("UpdatedAt").AsDateTime2().NotNullable()
                .WithColumn("DeletedAt").AsDateTime2().Nullable()
                .WithColumn("total_value").AsDecimal(15,5).Nullable()
                .WithColumn("Status").AsInt16().Nullable()
                .WithColumn("cancel_date").AsDateTime2().Nullable()
                .WithColumn("finished_date").AsDateTime2().Nullable()
                ;

            Create.Table("Product")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("creation_date").AsDateTime2().NotNullable()
                .WithColumn("UpdatedAt").AsDateTime2().NotNullable() 
                .WithColumn("DeletedAt").AsDateTime2().Nullable()
                .WithColumn("Name").AsString(50).Nullable()
                .WithColumn("Quantity").AsInt32().Nullable()
                .WithColumn("Price").AsDecimal(15,5).Nullable()
                .WithColumn("Description").AsString(255).Nullable();

            Create.Table("OrderItem")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("creation_date").AsDateTime2().NotNullable()
                .WithColumn("UpdatedAt").AsDateTime2().NotNullable()
                .WithColumn("DeletedAt").AsDateTime2().Nullable()
                .WithColumn("OrderId").AsInt32().Nullable()
                .WithColumn("product_id").AsInt32().Nullable()
                .WithColumn("Quantity").AsInt32().Nullable()
                .WithColumn("Price").AsDecimal(15, 5).Nullable();

            Create.ForeignKey("fk_Address_UserId_User_Id")
                .FromTable("Address")
                .ForeignColumn("UserId")
                .ToTable("User")
                .PrimaryColumn("Id");

            Create.ForeignKey("fk_Order_UserId_User_Id")
                .FromTable("Order")
                .ForeignColumn("UserId")
                .ToTable("User")
                .PrimaryColumn("Id");

            Create.ForeignKey("fk_OrderItem_OrderId_Order_Id")
                .FromTable("OrderItem")
                .ForeignColumn("OrderId")
                .ToTable("Order")
                .PrimaryColumn("Id");  
            
            Create.ForeignKey("fk_OrderItem_product_id_Product_Id")
                .FromTable("OrderItem")
                .ForeignColumn("product_id")
                .ToTable("Product")
                .PrimaryColumn("Id");

            Execute.Sql(@"
                CREATE TRIGGER [dbo].[OrderItem_UPDATE] ON [dbo].[OrderItem]
                    AFTER UPDATE
                AS
                BEGIN
                    SET NOCOUNT ON;

                    IF ((SELECT TRIGGER_NESTLEVEL()) > 1) RETURN;

                    UPDATE oi
                    SET UpdatedAt = GETUTCDATE()
                    FROM dbo.OrderItem AS oi
                    INNER JOIN INSERTED AS I
                        ON oi.Id = I.Id
                END");

            Execute.Sql(@"
                CREATE TRIGGER [dbo].[OrderItem_INSERT] ON [dbo].[OrderItem]
                   AFTER INSERT
               AS
               BEGIN
                   SET NOCOUNT ON;

                   IF ((SELECT TRIGGER_NESTLEVEL()) > 1) RETURN;

                   UPDATE oi
                   SET UpdatedAt = GETUTCDATE(), creation_date = GETUTCDATE()
                   FROM dbo.OrderItem AS oi
                   INNER JOIN INSERTED AS I
                       ON oi.Id = I.Id
               END");

            Execute.Sql(@"
                CREATE TRIGGER [dbo].[Address_UPDATE] ON [dbo].[Address]
                    AFTER UPDATE
                AS
                BEGIN
                    SET NOCOUNT ON;

                    IF ((SELECT TRIGGER_NESTLEVEL()) > 1) RETURN;

                    UPDATE B
                    SET UpdatedAt = GETUTCDATE()
                    FROM dbo.Address AS a
                    INNER JOIN INSERTED AS I
                        ON a.Id = I.Id
                END");    
            
            Execute.Sql(@"
                CREATE TRIGGER [dbo].[Address_INSERT] ON [dbo].[Address]
                    AFTER UPDATE
                AS
                BEGIN
                    SET NOCOUNT ON;

                    IF ((SELECT TRIGGER_NESTLEVEL()) > 1) RETURN;

                    UPDATE a
                    SET UpdatedAt = GETUTCDATE(), creation_date = GETUTCDATE()
                    FROM dbo.Address AS a
                    INNER JOIN INSERTED AS I
                        ON a.Id = I.Id
                END");
        }
    }
}
