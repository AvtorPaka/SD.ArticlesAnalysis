using FluentMigrator;

namespace SD.ArticlesAnalysis.Analysis.Infrastructure.Dal.Migrations;

[Migration(version:20250523, TransactionBehavior.Default)]
public class InitSchema: Migration
{
    public override void Up()
    {
        Create.Table("articles_analysis")
            .WithColumn("id").AsInt64().PrimaryKey("articles_analysis_pk").Identity()
            .WithColumn("article_id").AsInt64().NotNullable()
            .WithColumn("article_name").AsString(256).NotNullable()
            .WithColumn("paragraphs_count").AsInt64().NotNullable()
            .WithColumn("words_count").AsInt64().NotNullable()
            .WithColumn("characters_count").AsInt64().NotNullable()
            .WithColumn("word_cloud_location").AsString(384).NotNullable();

        Create.Index("articles_analysis_article_id_uidx")
            .OnTable("articles_analysis")
            .OnColumn("article_id")
            .Unique();
        
        Create.Index("articles_analysis_wc_location_uidx")
            .OnTable("articles_analysis")
            .OnColumn("word_cloud_location")
            .Unique();
    }

    public override void Down()
    {
        Delete.Table("articles_analysis");
    }
}