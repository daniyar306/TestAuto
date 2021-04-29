# TestAuto
Тестовое задания 




Script of database: 


CREATE DATABASE "Car"
    WITH 
    OWNER = postgres
    ENCODING = 'UTF8'
    LC_COLLATE = 'Russian_Kazakhstan.1251'
    LC_CTYPE = 'Russian_Kazakhstan.1251'
    TABLESPACE = pg_default
    CONNECTION LIMIT = -1;

COMMENT ON DATABASE "Car"
    IS 'DB for car and car''s owner';
    
CREATE TABLE public.auto
(
    id integer NOT NULL DEFAULT nextval('auto_id_seq'::regclass),
    brand character varying(50) COLLATE pg_catalog."default" NOT NULL,
    model character varying(50) COLLATE pg_catalog."default" NOT NULL,
    CONSTRAINT auto_pkey PRIMARY KEY (id)
)
    
CREATE TABLE public.driver
(
    id bigint NOT NULL DEFAULT nextval('car_id_seq'::regclass),
    name character varying(50) COLLATE pg_catalog."default" NOT NULL,
    auto_id integer,
    CONSTRAINT car_pkey PRIMARY KEY (id),
    CONSTRAINT car_auto_id_fkey FOREIGN KEY (auto_id)
        REFERENCES public.auto (id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE SET NULL
)

TABLESPACE pg_default;

ALTER TABLE public.driver
    OWNER to postgres;
