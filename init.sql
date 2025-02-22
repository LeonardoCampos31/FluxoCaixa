CREATE DATABASE fluxocaixa_lancamento;

\c fluxocaixa_lancamento;

CREATE EXTENSION IF NOT EXISTS "pgcrypto";

CREATE TABLE lancamento (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    valor DECIMAL(18,2) NOT NULL,
    tipo VARCHAR(10) CHECK (tipo IN ('Credito', 'Debito')) NOT NULL,
    data TIMESTAMP WITH TIME ZONE NOT NULL
);

CREATE DATABASE fluxocaixa_consolidado;

\c fluxocaixa_consolidado;

CREATE EXTENSION IF NOT EXISTS "pgcrypto";

CREATE TABLE consolidado (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    data DATE NOT NULL UNIQUE,
    saldo DECIMAL(18,2) NOT NULL
);