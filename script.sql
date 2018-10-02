-- --------------------------------------------------------
-- Servidor:                     127.0.0.1
-- Versão do servidor:           5.7.23-log - MySQL Community Server (GPL)
-- OS do Servidor:               Win64
-- HeidiSQL Versão:              9.5.0.5196
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;


-- Copiando estrutura do banco de dados para api.livraria.auth
CREATE DATABASE IF NOT EXISTS `api.livraria.auth` /*!40100 DEFAULT CHARACTER SET latin1 */;
USE `api.livraria.auth`;

-- Copiando estrutura para tabela api.livraria.auth.usuarios
CREATE TABLE IF NOT EXISTS `usuarios` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Senha` varchar(50) DEFAULT '0',
  `DataCriacao` datetime DEFAULT NULL,
  `DataAtualizacao` datetime DEFAULT NULL,
  `Nome` varchar(50) DEFAULT '0',
  `UserName` varchar(10) DEFAULT '0',
  `Email` varchar(30) DEFAULT '0',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=latin1;

-- Copiando dados para a tabela api.livraria.auth.usuarios: ~3 rows (aproximadamente)
/*!40000 ALTER TABLE `usuarios` DISABLE KEYS */;
INSERT INTO `usuarios` (`Id`, `Senha`, `DataCriacao`, `DataAtualizacao`, `Nome`, `UserName`, `Email`) VALUES
	(2, 'string', '2018-09-30 21:30:41', '2018-09-30 21:30:41', 'string', 'string', 'string'),
	(3, 'string', '2018-09-30 19:06:04', '0001-01-01 00:00:00', 'string', 'string', 'string'),
	(4, '12345678', '2018-09-30 20:10:55', '0001-01-01 00:00:00', 'Pedro', 'pedropefj', 'pedro.pefj@hotmail.com');
/*!40000 ALTER TABLE `usuarios` ENABLE KEYS */;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
