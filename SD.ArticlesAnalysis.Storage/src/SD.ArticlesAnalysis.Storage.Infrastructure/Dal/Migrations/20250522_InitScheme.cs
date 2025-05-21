using FluentMigrator;

namespace SD.ArticlesAnalysis.Storage.Infrastructure.Dal.Migrations;

[Migration(version:20250522, TransactionBehavior.Default)]
public class InitScheme: Migration
{
    public override void Up()
    {
        Create.Table("articles_meta")
            .WithColumn("id").AsInt64().PrimaryKey("articles_meta_pk").Identity()
            .WithColumn("name").AsString(256).NotNullable()
            .WithColumn("hash").AsString(128).NotNullable()
            .WithColumn("location").AsString(384).NotNullable();

        Create.Index("articles_meta_hash_uidx")
            .OnTable("articles_meta")
            .OnColumn("hash")
            .Unique();
        
        Create.Index("articles_meta_location_uidx")
            .OnTable("articles_meta")
            .OnColumn("location")
            .Unique();
    }

    public override void Down()
    {
        Delete.Table("articles_meta");
    }
}