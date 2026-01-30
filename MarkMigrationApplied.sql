-- MARK MIGRATION AS APPLIED IN SUPABASE
-- Copy all lines below and paste in Supabase SQL Editor

-- Insert migration history record
INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20260130012429_InitialCreatePostgres', '9.0.0')
ON CONFLICT ("MigrationId") DO NOTHING;

-- Verify migration was recorded (optional - run this to check)
SELECT * FROM "__EFMigrationsHistory" ORDER BY "MigrationId";
