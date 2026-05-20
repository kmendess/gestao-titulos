DROP SCHEMA IF EXISTS gestao_titulos CASCADE;

CREATE SCHEMA gestao_titulos;

CREATE TABLE gestao_titulos.titulo (
    id UUID PRIMARY KEY,
    numero_titulo VARCHAR(50) NOT NULL UNIQUE,
    nome_devedor VARCHAR(150) NOT NULL,
    cpf_devedor VARCHAR(11) NOT NULL,
    percentual_juros NUMERIC(5,2) NOT NULL,
    percentual_multa NUMERIC(5,2) NOT NULL,
    criado_em TIMESTAMPTZ NOT NULL DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE gestao_titulos.parcela (
    id UUID PRIMARY KEY,
    titulo_id UUID NOT NULL,
    numero_parcela INT NOT NULL,
    data_vencimento DATE NOT NULL,
    valor NUMERIC(15,2) NOT NULL,

    CONSTRAINT fk_parcela_titulo
        FOREIGN KEY (titulo_id)
        REFERENCES gestao_titulos.titulo(id)
        ON DELETE CASCADE
);

CREATE INDEX idx_titulo_numero ON gestao_titulos.titulo(numero_titulo);
CREATE INDEX idx_titulo_cpf ON gestao_titulos.titulo(cpf_devedor);
CREATE INDEX idx_parcela_titulo_id ON gestao_titulos.parcela(titulo_id);