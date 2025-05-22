using FluentMigrator;

namespace SD.ArticlesAnalysis.Analysis.Infrastructure.Dal.Migrations;

[Migration(version:20250524, TransactionBehavior.Default)]
public class AddArticleAnalysisType: Migration 
{
    public override void Up()
    {
        const string sql = @"
DO  $$
    BEGIN
        IF NOT EXISTS (SELECT 1 FROM pg_type WHERE typname = 'article_analysis_type') THEN
            CREATE TYPE article_analysis_type as
            (
                id                      bigint,
                article_id              bigint,
                article_name            varchar(256),
                paragraphs_count        bigint,
                words_count             bigint,
                characters_count        bigint,
                word_cloud_location     varchar(384)
            );
        END IF;
    END
$$;
";
        
        Execute.Sql(sql);
    }

    public override void Down()
    {
        const string sql = @"
DO $$
    BEGIN
        DROP TYPE IF EXISTS article_analysis_type;
    END
$$;
";
        
        Execute.Sql(sql);
    }
}