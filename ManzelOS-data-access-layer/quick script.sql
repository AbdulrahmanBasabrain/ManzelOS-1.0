use ManzelOSDB;

-- Delete all records
DELETE FROM employees;

-- Reset identity seed to 0 (next insert will be 0)
DBCC CHECKIDENT('employees', RESEED, -1);

-- Delete all records
DELETE FROM people;

-- Reset identity seed to 0 (next insert will be 0)
DBCC CHECKIDENT('people', RESEED, -1);



select * from people 
select * from employees
select * from property_managers
