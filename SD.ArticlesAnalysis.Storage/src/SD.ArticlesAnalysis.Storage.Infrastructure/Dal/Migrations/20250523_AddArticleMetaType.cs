using FluentMigrator;

namespace SD.ArticlesAnalysis.Storage.Infrastructure.Dal.Migrations;

[Migration(version:20250523, TransactionBehavior.Default)]
public class AddArticleMetaType: Migration
{
    public override void Up()
    {
        const string sql = @"
DO  $$
    BEGIN
        IF NOT EXISTS (SELECT 1 FROM pg_type WHERE typname = 'article_meta_type') THEN
            CREATE TYPE article_meta_type as
            (
                id          bigint,
                name        varchar(256),
                hash        varchar(128),
                location    varchar(384)
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
        DROP TYPE IF EXISTS article_meta_type;
    END
$$;
";
        
        Execute.Sql(sql);
    }
}