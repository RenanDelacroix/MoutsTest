
--tabela de filiais
CREATE TABLE branches (
    id UUID PRIMARY KEY,
    name VARCHAR NOT NULL
);

-- Tabela de Vendas
CREATE TABLE sales (
    id UUID PRIMARY KEY,
    number bigserial NOT NULL,
    customerid UUID NOT NULL,
    branchid UUID NOT NULL,
    createdat TIMESTAMP WITHOUT TIME ZONE NOT NULL,
    status TEXT NOT NULL,
    discount DECIMAL(18,2) NOT NULL,
    CONSTRAINT fk_branches FOREIGN KEY (branchid) REFERENCES branches(id) ON DELETE CASCADE
);

-- Tabela de produtos
CREATE TABLE products (
    id UUID PRIMARY KEY,
    name VARCHAR NOT NULL,
    price DECIMAL(18,2) NOT NULL
);


-- carga da tabela de produtos
INSERT INTO products (id, name, price) VALUES
('9a1f3d5d-54c4-4e11-9fd7-1f9a4a2a7a01', 'Mouse Gamer', 150.00),
('c6d9f773-e6f1-4c25-9ef0-66baf35f15c7', 'Teclado Mecânico', 300.00),
('f345bb37-dc2e-4b50-91b0-6b91e8576452', 'Monitor 27"', 1200.00),
('a14dced3-0c4c-4e6e-9a7b-972015ae7f80', 'Cadeira Ergonômica', 850.00),
('7f9a2e3c-5ad7-45d7-b92a-5e7e672a5c1f', 'Notebook i7', 5500.00);

-- carga da tabela de filiais
INSERT INTO branches (id, name) VALUES
('afb0ed5c-dfc9-4c05-ae77-7859bcfe1359', 'São Paulo'),
('1bf1e38b-d7ab-410a-895c-2b6da5fecff0', 'Santa Catarina');


-- Itens da Venda (com FK e deleção em cascata)
CREATE TABLE saleitem (
    id SERIAL PRIMARY KEY,
    productid UUID NOT NULL,
    saleid UUID NOT NULL,
    quantity INTEGER NOT NULL,
    unitprice DECIMAL(18,2) NOT NULL,
    discount DECIMAL(18,2) NOT NULL,
    CONSTRAINT fk_sale FOREIGN KEY (saleid) REFERENCES sales(id) ON DELETE CASCADE,
    CONSTRAINT fk_product FOREIGN KEY (productid) REFERENCES products(id) ON DELETE CASCADE
    
);
