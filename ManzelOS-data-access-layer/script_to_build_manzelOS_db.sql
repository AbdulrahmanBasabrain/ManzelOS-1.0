use ManzelOSDB;


create table continents
(

continent_id smallint IDENTITY(1,1) PRIMARY KEY,
continent_name nvarchar(100) UNIQUE  not null

);


create table countries
(

country_id smallint IDENTITY(1,1) PRIMARY KEY,
continent_id smallint not null references continents(continent_id),
country_name nvarchar(255) UNIQUE not null


);

create table cities
(

city_id int IDENTITY(1,1) PRIMARY KEY,
country_id smallint not null references countries(country_id),
city_name nvarchar(255),


);

create table marriage_statuses
(
marriage_status_id smallint IDENTITY(1,1) PRIMARY KEY,
marriage_status_name nvarchar(10) not null UNIQUE ,

)

create table people
(

person_id int IDENTITY(1,1) PRIMARY KEY,
first_name nvarchar(255),
father_name nvarchar(255),
grandfather_name nvarchar(255),
last_name nvarchar(255),
national_id int not null  UNIQUE ,
date_of_birth date not null,
gender bit not null,
country_id smallint not null references countries(country_id),
city_id int not null references cities(city_id),
address_district nvarchar(500) not null,
email nvarchar(254) not null UNIQUE ,
phone nvarchar (30) not null UNIQUE ,
personal_image_path nvarchar(max) not null,
marriage_status_id smallint not null references marriage_statuses(marriage_status_id),
created_at datetime2 not null,

)

create table employees
(
employee_id int IDENTITY(1,1) PRIMARY KEY,
person_id int not null UNIQUE  references people(person_id),
salary smallmoney not null,
job nvarchar(500) not null,
created_at datetime2 not null,


)

create table system_users
(
system_user_id int IDENTITY(1,1) PRIMARY KEY,
employee_id int not null UNIQUE  references employees(employee_id),
username nvarchar(200)  UNIQUE not null,
password_hash nvarchar(300) UNIQUE  not null,
is_active bit not null,
persmession int not null,
created_at datetime2 not null,


)

create table standard_users
(
standard_user_id int not null IDENTITY(1,1) PRIMARY KEY,
person_id int not null  UNIQUE references people(person_id),
username nvarchar(255)  UNIQUE not null,
password_hash nvarchar(255) UNIQUE  not null,
is_active bit not null,
permession int not null,
created_at datetime

)

create table property_managers
(
property_manager_id int IDENTITY(1,1) PRIMARY KEY,
employee_id int not null UNIQUE  references employees(employee_id),
system_user_id int not null UNIQUE  references system_users(system_user_id),
created_at datetime2 not null,


)

create table tenants
(
tenant_id int IDENTITY(1,1) PRIMARY KEY,
person_id int not null UNIQUE  references people(person_id),
standard_user_id int not null UNIQUE  references standard_users(standard_user_id),
documents_file_path  nvarchar(max)   not null,
created_at datetime2 not null,

)



create table owners
(
owner_id int IDENTITY(1,1) PRIMARY KEY,
person_id int not null  UNIQUE references people(person_id),
standard_user_id int not null UNIQUE  references standard_users(standard_user_id),
is_business bit not null,
number_of_assets int not null,
created_at datetime2 not null,

)




create table rental_unit_types
(
rental_unit_type_id int IDENTITY(1,1) PRIMARY KEY,
rental_unit_name nvarchar(500)  not null UNIQUE ,
)

create table parent_rental_units
(
parent_rental_unit_id int IDENTITY(1,1) PRIMARY KEY,
number_of_children_units int not null,
)

create table bill_types
(
bill_type_id int IDENTITY(1,1) PRIMARY KEY,
billing_type_name nvarchar(500) UNIQUE  not null,
)

create table rental_units
(

rental_unit_id int IDENTITY(1,1) PRIMARY KEY,
rental_unit_number_order int not null,
rental_unit_name nvarchar(500) not null,
rental_unit_space decimal(12,2) not null,
rental_unit_location_name nvarchar(500) not null,
rental_unit_location_latitude DECIMAL(9, 6) not null,
rental_unit_location_longitude DECIMAL(9, 6) not null,
rental_unit_type_id int not null references rental_unit_types(rental_unit_type_id),
parent_rental_unit_id int null references parent_rental_units(parent_rental_unit_id),
is_available bit not null,
property_manager_id int not null references property_managers(property_manager_id),
created_at datetime2 not null,
representative_owner_id int not null references owners(owner_id),


)



create table billing_periods
(

billing_period_type_id int IDENTITY(1,1) PRIMARY KEY,
billing_period int not null UNIQUE ,

)

create table rental_contracts
(

rental_contract_id int IDENTITY(1,1) PRIMARY KEY,
rent_start_date date not null,
rent_end_date date not null,
created_date date not null,
representative_owner_id_at_the_time int not null references owners(owner_id),
tenant_id int not null references tenants(tenant_id),
billing_period_type_id int not null references billing_periods(billing_period_type_id),
rental_unit_id int not null references rental_units(rental_unit_id),
is_active bit not null,
rent_amount smallmoney not null,
bill_type_id int not null references bill_types(bill_type_id),
created_at datetime2 not null,

)

create table consumption_measuring_units
(
consumption_unit_type_id smallint not null IDENTITY(1,1) PRIMARY KEY,
consumption_measuring_unit nvarchar(200) UNIQUE 
)

create table utility_readers
(
utility_reader_id int not null IDENTITY(1,1) PRIMARY KEY,
rental_contract_id int not null references rental_contracts(rental_contract_id),
rental_unit_id int not null references rental_units(rental_unit_id),
consumption_unit_type_id smallint not null references consumption_measuring_units(consumption_unit_type_id),
billing_period_type_id int not null references billing_periods(billing_period_type_id),
consumption_amount decimal(12,2) not null,
date_transfered_to_tenant date not null,
bill_type_id int not null references bill_types(bill_type_id),
price smallmoney not null,
created_at datetime2 not null,

)

create table electricity_readers
(
electricity_reader_id int not null IDENTITY(1,1) PRIMARY KEY,
utility_reader_id int not null references utility_readers(utility_reader_id),
electricity_reader_number int not null
)

create table water_readers
(
water_reader_id int not null IDENTITY(1,1) PRIMARY KEY,
utility_reader_id int not null references utility_readers(utility_reader_id),
water_reader_number int not null


)

create table bill_payment_statuses
(
bill_payment_status_id smallint not null IDENTITY(1,1) PRIMARY KEY,
bill_payment_status_name nvarchar(100) not null,

)

create table generated_bills
(

generated_bill_id int not null IDENTITY(1,1) PRIMARY KEY,
generated_bill_fees smallmoney not null,
bill_type_id int not null references bill_types(bill_type_id),
rental_contract_id int not null references rental_contracts(rental_contract_id),
rental_unit_id int not null references rental_units(rental_unit_id),
generated_date datetime2 not null,
created_at datetime2 not null,
bill_payment_status_id smallint not null references bill_payment_statuses(bill_payment_status_id),
expected_payment_date datetime2 not null,
is_paid bit not null,

)

create table payment_methods
(
payment_method_id smallint not null IDENTITY(1,1) PRIMARY KEY,
payment_status_name nvarchar(150) not null,

)

create table payments
(
payment_id int not null IDENTITY(1,1) PRIMARY KEY,
payment_amonut smallmoney not null,
paid_by_tenant_id int not null references tenants(tenant_id),
generated_bill_id int not null references generated_bills(generated_bill_id),
recorded_by_system_user_id int not null references system_users(system_user_id),
received_by_rental_unit_owner_id int not null references owners(owner_id),
payment_date datetime2 not null,
payment_method_id smallint not null references payment_methods(payment_method_id),
payment_status_id smallint not null references bill_payment_statuses(bill_payment_status_id),
created_at datetime2 not null,
)