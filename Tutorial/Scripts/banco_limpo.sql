-- ══════════════════════════════════════════════════════
--   Atividades Práticas - Entity Framework com C# e MySQL
--   Prof. Escobar
-- ══════════════════════════════════════════════════════

-- Apaga e recria o banco do zero
DROP DATABASE IF EXISTS escola;

CREATE DATABASE escola
    CHARACTER SET utf8mb4
    COLLATE utf8mb4_unicode_ci;

USE escola;

-- ══════════════════════════════════════
--         ATIVIDADE 1 - PRODUTOS
-- ══════════════════════════════════════
CREATE TABLE Produtos (
    Id    INT          NOT NULL AUTO_INCREMENT,
    Nome  VARCHAR(255) NOT NULL,
    Preco DOUBLE       NOT NULL,
    PRIMARY KEY (Id)
);

INSERT INTO Produtos (Nome, Preco) VALUES ('Notebook', 3500.00);
INSERT INTO Produtos (Nome, Preco) VALUES ('Mouse', 89.90);
INSERT INTO Produtos (Nome, Preco) VALUES ('Teclado', 149.90);

SELECT * FROM Produtos;

SET SQL_SAFE_UPDATES = 0;
UPDATE Produtos SET Nome = 'Mouse Gamer', Preco = 199.90 WHERE Id = 2;
SET SQL_SAFE_UPDATES = 1;

DELETE FROM Produtos WHERE Id = 3;

-- Desafio: listar acima de R$ 100
SELECT * FROM Produtos WHERE Preco > 100.00 ORDER BY Preco;


-- ══════════════════════════════════════
--         ATIVIDADE 2 - CLIENTES
-- ══════════════════════════════════════
CREATE TABLE Clientes (
    Id    INT          NOT NULL AUTO_INCREMENT,
    Nome  VARCHAR(255) NOT NULL,
    Email VARCHAR(255) NOT NULL,
    CONSTRAINT chk_email CHECK (Email LIKE '%@%.%'),
    PRIMARY KEY (Id)
);

INSERT INTO Clientes (Nome, Email) VALUES ('Ana Silva', 'ana@email.com');
INSERT INTO Clientes (Nome, Email) VALUES ('Bruno Costa', 'bruno@email.com');
-- INSERT INTO Clientes (Nome, Email) VALUES ('Teste', 'emailinvalido'); -- bloqueado pelo CHECK

SELECT * FROM Clientes;

SET SQL_SAFE_UPDATES = 0;
UPDATE Clientes SET Nome = 'Ana Souza', Email = 'ana.souza@email.com' WHERE Id = 1;
SET SQL_SAFE_UPDATES = 1;

DELETE FROM Clientes WHERE Id = 2;

-- Desafio: buscar por email
SELECT * FROM Clientes WHERE Email = 'ana.souza@email.com';


-- ══════════════════════════════════════
--         ATIVIDADE 3 - CURSOS
-- ══════════════════════════════════════
CREATE TABLE Cursos (
    Id           INT          NOT NULL AUTO_INCREMENT,
    Nome         VARCHAR(255) NOT NULL,
    CargaHoraria INT          NOT NULL,
    ProfessorId  INT          NULL,
    PRIMARY KEY (Id)
);

INSERT INTO Cursos (Nome, CargaHoraria) VALUES ('Python', 40);
INSERT INTO Cursos (Nome, CargaHoraria) VALUES ('C#', 60);
INSERT INTO Cursos (Nome, CargaHoraria) VALUES ('Banco de Dados', 30);
INSERT INTO Cursos (Nome, CargaHoraria) VALUES ('Java', 80);
INSERT INTO Cursos (Nome, CargaHoraria) VALUES ('HTML e CSS', 20);

-- Filtrar por carga horária >= 40h
SELECT * FROM Cursos WHERE CargaHoraria >= 40;

-- Ordenar por nome
SELECT * FROM Cursos ORDER BY Nome ASC;


-- ══════════════════════════════════════
--         ATIVIDADE 4 - ESTOQUE
-- ══════════════════════════════════════
CREATE TABLE Estoque (
    Id         INT          NOT NULL AUTO_INCREMENT,
    Nome       VARCHAR(255) NOT NULL,
    Quantidade INT          NOT NULL CHECK (Quantidade >= 0),
    PRIMARY KEY (Id)
);

INSERT INTO Estoque (Nome, Quantidade) VALUES ('Arroz', 50);
INSERT INTO Estoque (Nome, Quantidade) VALUES ('Feijão', 3);
INSERT INTO Estoque (Nome, Quantidade) VALUES ('Macarrão', 2);
INSERT INTO Estoque (Nome, Quantidade) VALUES ('Óleo', 20);
-- INSERT INTO Estoque (Nome, Quantidade) VALUES ('Açúcar', -5); -- bloqueado pelo CHECK

SELECT * FROM Estoque;

-- Baixa de estoque
SET SQL_SAFE_UPDATES = 0;
UPDATE Estoque SET Quantidade = Quantidade - 10 WHERE Id = 1 AND Quantidade - 10 >= 0;
SET SQL_SAFE_UPDATES = 1;

-- Listar estoque baixo (<= 5)
SELECT * FROM Estoque WHERE Quantidade <= 5 ORDER BY Quantidade ASC;


-- ══════════════════════════════════════
--      ATIVIDADE 5 - PROFESSOR E CURSOS
-- ══════════════════════════════════════
CREATE TABLE Professores (
    Id   INT          NOT NULL AUTO_INCREMENT,
    Nome VARCHAR(255) NOT NULL,
    PRIMARY KEY (Id)
);

ALTER TABLE Cursos
    ADD CONSTRAINT fk_professor
    FOREIGN KEY (ProfessorId) REFERENCES Professores(Id)
    ON DELETE SET NULL;

INSERT INTO Professores (Nome) VALUES ('Carlos Silva');
INSERT INTO Professores (Nome) VALUES ('Fernanda Lima');

SET SQL_SAFE_UPDATES = 0;
UPDATE Cursos SET ProfessorId = 1 WHERE Nome IN ('C#', 'Banco de Dados');
UPDATE Cursos SET ProfessorId = 2 WHERE Nome IN ('Python', 'Java');
SET SQL_SAFE_UPDATES = 1;

-- Listar com JOIN (Include)
SELECT
    p.Id   AS ProfessorId,
    p.Nome AS Professor,
    c.Id   AS CursoId,
    c.Nome AS Curso,
    c.CargaHoraria
FROM Professores p
LEFT JOIN Cursos c ON c.ProfessorId = p.Id
ORDER BY p.Nome, c.Nome;


-- ══════════════════════════════════════
--       ATIVIDADE 6 - PEDIDO E ITENS
-- ══════════════════════════════════════
CREATE TABLE Pedidos (
    Id   INT      NOT NULL AUTO_INCREMENT,
    Data DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (Id)
);

CREATE TABLE ItensPedido (
    Id         INT          NOT NULL AUTO_INCREMENT,
    Produto    VARCHAR(255) NOT NULL,
    Quantidade INT          NOT NULL,
    PedidoId   INT          NOT NULL,
    PRIMARY KEY (Id),
    CONSTRAINT fk_pedido
        FOREIGN KEY (PedidoId) REFERENCES Pedidos(Id)
        ON DELETE CASCADE
);

INSERT INTO Pedidos (Data) VALUES (NOW());

INSERT INTO ItensPedido (Produto, Quantidade, PedidoId) VALUES ('Notebook', 1, 1);
INSERT INTO ItensPedido (Produto, Quantidade, PedidoId) VALUES ('Mouse',    2, 1);
INSERT INTO ItensPedido (Produto, Quantidade, PedidoId) VALUES ('Teclado',  3, 1);

-- Listar pedidos com itens (Include)
SELECT
    p.Id   AS PedidoId,
    p.Data,
    i.Produto,
    i.Quantidade
FROM Pedidos p
INNER JOIN ItensPedido i ON i.PedidoId = p.Id
ORDER BY p.Id;

-- Desafio: total de itens por pedido
SELECT
    p.Id AS PedidoId,
    p.Data,
    SUM(i.Quantidade) AS TotalItens
FROM Pedidos p
INNER JOIN ItensPedido i ON i.PedidoId = p.Id
GROUP BY p.Id, p.Data;
