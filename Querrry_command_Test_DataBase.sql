-- CREATE TABLE users (
--   userID UUID PRIMARY KEY,
--   name TEXT,
--   surname TEXT,
--   email TEXT,
--   password TEXT
-- );

-- CREATE TABLE task (
--   taskID UUID,
--   Numb SERIAL,
--   topic TEXT,
--   description TEXT,
--   start_time TIMESTAMP,
--   work_time INTERVAL,
--   status TEXT,
--   user_ID UUID,
--   FOREIGN KEY (user_guid) REFERENCES users (guid)
-- );

--------------------------------------------------------------CREATE

--ALTER TABLE users RENAME COLUMN guid TO user_Id;
--ALTER TABLE users DROP CONSTRAINT guid;
--ALTER TABLE users ADD PRIMARY KEY guid;
--SELECT * FROM users
--CREATE EXTENSION IF NOT EXISTS "uuid-ossp";
--INSERT INTO users (guid, name, surname, email, password)
--VALUES (UUID_GENERATE_V4(), 'Jan', 'Kowalski', 'jan.kowalski@example.com', 'haslo123');

-------------------------------------------------------------EDIT USER

-- ALTER TABLE task RENAME COLUMN user_guid TO user_Id;
-- ALTER TABLE task RENAME COLUMN taskid TO task_Id;
--ALTER TABLE task RENAME COLUMN ID TO Number_task;
--SELECT * FROM task
-- INSERT INTO mytask (id, topic, description, data, status) 
-- VALUES (3, 'zad2', 'opis2', null, null);public.mytask
--------------------------------------------------------------EDIT TASKS

--ALTER Table task RENAME TO tasks



























