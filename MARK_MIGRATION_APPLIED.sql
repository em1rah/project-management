-- Run this SQL in Supabase to mark migration as applied
-- Go to Supabase Dashboard → SQL Editor → New Query → paste this

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20260130012429_InitialCreatePostgres', '9.0.0')
ON CONFLICT DO NOTHING;

-- Verify migration was recorded
SELECT * FROM "__EFMigrationsHistory";
