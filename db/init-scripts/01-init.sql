-- Tabela de Vendas
CREATE TABLE sales (
    id UUID PRIMARY KEY,
    number VARCHAR NOT NULL,
    customerid UUID NOT NULL,
    branchid UUID NOT NULL,
    createdat TIMESTAMP WITHOUT TIME ZONE NOT NULL,
    status TEXT NOT NULL,
    discount DECIMAL(18,2) NOT NULL
);

-- Itens da Venda (com FK e deleção em cascata)
CREATE TABLE saleitem (
    id SERIAL PRIMARY KEY, -- Novo campo de ID
    productid UUID NOT NULL,
    saleid UUID NOT NULL,
    quantity INTEGER NOT NULL,
    unitprice DECIMAL(18,2) NOT NULL,
    discount DECIMAL(18,2) NOT NULL,
    CONSTRAINT fk_sale FOREIGN KEY (saleid) REFERENCES sales(id) ON DELETE CASCADE
);
