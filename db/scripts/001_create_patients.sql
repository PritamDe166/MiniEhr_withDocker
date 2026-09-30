CREATE TABLE IF NOT EXISTS patients (
    id              INTEGER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    first_name      VARCHAR(100)  NOT NULL,
    last_name       VARCHAR(100)  NOT NULL,
    date_of_birth   DATE          NOT NULL,
    email           VARCHAR(255),
    phone           VARCHAR(20),
    created_at      TIMESTAMPTZ   NOT NULL DEFAULT now()
);