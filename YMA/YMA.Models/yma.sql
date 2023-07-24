create table addresses(
	id text primary key,
	title text,
	full_address text,
	province text,
	district text,
	neighbourhood text,
	create_date timestamp,
	is_disabled boolean
)

create table accounts(
	id text primary key,
	first_name text,
	last_name text,
	email text,
	phone text,
	default_address_id text,
	create_date timestamp,
	is_disabled boolean
)

create table logs(
	id text primary key,
	type text,
	message text,
	data text,
	create_date timestamp
)

create table ads(
	id text primary key,
	image_url text,
	order_number int,
	is_disabled boolean
)

create table exchanges(
	id text primary key,
	currency text,
	unit_price float,
	exchange_ratio float,
	exchange_amount float,
	is_increased boolean,
	order_number int,
	is_disabled boolean
)

create table categories(
	id text primary key,
	name text,
	icon_url text,
	is_disabled boolean
)

create table featured_categories(
	id text primary key,
	category_id text,
	order_counter int
)

create table brands(
	id text primary key,
	name text,
	image_url text,
	is_disabled boolean
)

create table featured_brands(
	id text primary key,
	brand_id text,
	order_counter int
)

create table versions(
	id text primary key,
	name text,
	version text,
	download_url text
)

create table companies(
	id text primary key,
	name text,
	image_url text,
	email text,
	phone text,
	web text,
	address text,
	theme_color text,
	create_date timestamp,
	is_disabled boolean
)

create table featured_companies(
	id text primary key,
	company_id text,
	order_counter int
)

create table products(
	id text primary key,
	name text,
	model text,
	year text,
	description text,
	image_url text,
	code text,
	oem_no text,
	price float,
	discount float,
	stock_counter int,
	brand_id text,
	category_id text,
	company_id text,
	create_date timestamp,
	is_disabled boolean
)

create table featured_products(
	id text primary key,
	product_id text,
	order_counter int
)

create table company_invites(
	id text primary key,
	receiver_id text,
	sender_id text,
	is_buying boolean,
	is_selling boolean,
	is_current_account_registration boolean,
	is_accepted boolean,
	reply_date timestamp,
	create_date timestamp
)

create table favorite_products(
	id text primary key,
	product_id text,
	account_id text,
	create_date timestamp
)
