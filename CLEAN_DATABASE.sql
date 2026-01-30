-- Run this in Supabase SQL Editor to clean up the database
-- This will delete all tables so we can recreate them fresh

DROP TABLE IF EXISTS "Users" CASCADE;
DROP TABLE IF EXISTS "Admins" CASCADE;
DROP TABLE IF EXISTS "__EFMigrationsHistory" CASCADE;

-- Verify tables are gone (should return no rows)
SELECT table_name FROM information_schema.tables WHERE table_schema = 'public';
