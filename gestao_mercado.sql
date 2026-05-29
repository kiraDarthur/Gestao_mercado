-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Tempo de geração: 29-Maio-2026 às 23:23
-- Versão do servidor: 10.4.32-MariaDB
-- versão do PHP: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Banco de dados: `gestao_mercado`
--

-- --------------------------------------------------------

--
-- Estrutura da tabela `categorias`
--

CREATE TABLE `categorias` (
  `id` int(11) NOT NULL,
  `nome` varchar(100) NOT NULL,
  `descricao` varchar(255) DEFAULT NULL,
  `corredor` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Extraindo dados da tabela `categorias`
--

INSERT INTO `categorias` (`id`, `nome`, `descricao`, `corredor`) VALUES
(1, 'Mercearia', 'Alimentos secos, arroz e massas', 1),
(2, 'Laticínios', 'Leite, iogurtes e queijos', 2),
(3, 'Bebidas', 'Sumos, águas e refrigerantes', 3),
(4, 'Talho e Peixaria', 'Carnes frescas e peixe', 4),
(5, 'Higiene e Limpeza', 'Detergentes e champôs', 5);

-- --------------------------------------------------------

--
-- Estrutura da tabela `detalhes_venda`
--

CREATE TABLE `detalhes_venda` (
  `id` int(11) NOT NULL,
  `venda_id` int(11) DEFAULT NULL,
  `produto_id` int(11) DEFAULT NULL,
  `quantidade` int(11) NOT NULL,
  `preco_unitario` decimal(10,2) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Extraindo dados da tabela `detalhes_venda`
--

INSERT INTO `detalhes_venda` (`id`, `venda_id`, `produto_id`, `quantidade`, `preco_unitario`) VALUES
(1, 1, 1, 2, 1.25),
(2, 1, 4, 2, 0.89);

-- --------------------------------------------------------

--
-- Estrutura da tabela `funcionarios`
--

CREATE TABLE `funcionarios` (
  `id` int(11) NOT NULL,
  `nome` varchar(100) NOT NULL,
  `cargo` varchar(50) NOT NULL,
  `salario` decimal(10,2) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Extraindo dados da tabela `funcionarios`
--

INSERT INTO `funcionarios` (`id`, `nome`, `cargo`, `salario`) VALUES
(1, 'Ana Silva', 'Operadora de Caixa', 850.00),
(2, 'Carlos Souza', 'Gerente de Loja', 1350.00),
(3, 'Pedro Santos', 'Repositor de Stock', 820.00);

-- --------------------------------------------------------

--
-- Estrutura da tabela `produtos`
--

CREATE TABLE `produtos` (
  `id` int(11) NOT NULL,
  `nome` varchar(100) NOT NULL,
  `preco` decimal(10,2) NOT NULL,
  `stock` int(11) NOT NULL,
  `imagem_path` varchar(255) DEFAULT NULL,
  `categoria_id` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Extraindo dados da tabela `produtos`
--

INSERT INTO `produtos` (`id`, `nome`, `preco`, `stock`, `imagem_path`, `categoria_id`) VALUES
(1, 'Arroz Carolino 1kg', 1.26, 120, '', 1),
(2, 'Massa Esparguete 500g', 0.90, 200, '', 1),
(3, 'Atum em Azeite 3x', 2.99, 85, '', 1),
(4, 'Leite UHT Meio Gordo 1L', 0.89, 300, 'C:\\ImagensMercado\\leite.jpg', 2),
(5, 'Queijo Flamengo Fatiado', 2.10, 45, 'C:\\ImagensMercado\\queijo.jpg', 2),
(6, 'Água Mineral 1.5L', 0.35, 500, '', 3),
(7, 'Sumo de Laranja 1L', 1.65, 110, 'C:\\ImagensMercado\\sumo_laranja.jpg', 3),
(8, 'Peito de Frango 1kg', 5.99, 30, 'C:\\ImagensMercado\\frango.jpg', 4),
(9, 'Detergente de Roupa 50D', 8.99, 40, 'C:\\ImagensMercado\\detergente.jpg', 5),
(10, 'Papel Higiénico 12 Rolos', 3.15, 90, 'C:\\ImagensMercado\\papel_hig.jpg', 5),
(11, '2132', 212.00, 1000, '', 4),
(13, 'açucar', 1.00, 1000, '', 1),
(14, 'arth', 123.00, 12, '', 2),
(15, 'Arroz Carolino kg', 1.26, 120, 'C:\\Users\\F40784.FORMACAO\\Pictures\\chrono-trigger-group-portrait-yb319gdzkluhrpub.jpg', 1),
(16, 'Arroz Carolino', 1.26, 120, 'C:\\Users\\F40784.FORMACAO\\Pictures\\1_9JuhzN4ogjP6QBICwX9loQ.jpg', 1);

-- --------------------------------------------------------

--
-- Estrutura da tabela `vendas`
--

CREATE TABLE `vendas` (
  `id` int(11) NOT NULL,
  `data_venda` datetime DEFAULT current_timestamp(),
  `valor_total` decimal(10,2) NOT NULL,
  `funcionario_id` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Extraindo dados da tabela `vendas`
--

INSERT INTO `vendas` (`id`, `data_venda`, `valor_total`, `funcionario_id`) VALUES
(1, '2026-05-25 10:15:00', 4.28, 1);

--
-- Índices para tabelas despejadas
--

--
-- Índices para tabela `categorias`
--
ALTER TABLE `categorias`
  ADD PRIMARY KEY (`id`);

--
-- Índices para tabela `detalhes_venda`
--
ALTER TABLE `detalhes_venda`
  ADD PRIMARY KEY (`id`),
  ADD KEY `venda_id` (`venda_id`),
  ADD KEY `produto_id` (`produto_id`);

--
-- Índices para tabela `funcionarios`
--
ALTER TABLE `funcionarios`
  ADD PRIMARY KEY (`id`);

--
-- Índices para tabela `produtos`
--
ALTER TABLE `produtos`
  ADD PRIMARY KEY (`id`),
  ADD KEY `categoria_id` (`categoria_id`);

--
-- Índices para tabela `vendas`
--
ALTER TABLE `vendas`
  ADD PRIMARY KEY (`id`),
  ADD KEY `funcionario_id` (`funcionario_id`);

--
-- AUTO_INCREMENT de tabelas despejadas
--

--
-- AUTO_INCREMENT de tabela `categorias`
--
ALTER TABLE `categorias`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- AUTO_INCREMENT de tabela `detalhes_venda`
--
ALTER TABLE `detalhes_venda`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT de tabela `funcionarios`
--
ALTER TABLE `funcionarios`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT de tabela `produtos`
--
ALTER TABLE `produtos`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=17;

--
-- AUTO_INCREMENT de tabela `vendas`
--
ALTER TABLE `vendas`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- Restrições para despejos de tabelas
--

--
-- Limitadores para a tabela `detalhes_venda`
--
ALTER TABLE `detalhes_venda`
  ADD CONSTRAINT `detalhes_venda_ibfk_1` FOREIGN KEY (`venda_id`) REFERENCES `vendas` (`id`) ON DELETE CASCADE,
  ADD CONSTRAINT `detalhes_venda_ibfk_2` FOREIGN KEY (`produto_id`) REFERENCES `produtos` (`id`) ON DELETE SET NULL;

--
-- Limitadores para a tabela `produtos`
--
ALTER TABLE `produtos`
  ADD CONSTRAINT `produtos_ibfk_1` FOREIGN KEY (`categoria_id`) REFERENCES `categorias` (`id`) ON DELETE SET NULL;

--
-- Limitadores para a tabela `vendas`
--
ALTER TABLE `vendas`
  ADD CONSTRAINT `vendas_ibfk_1` FOREIGN KEY (`funcionario_id`) REFERENCES `funcionarios` (`id`) ON DELETE SET NULL;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
