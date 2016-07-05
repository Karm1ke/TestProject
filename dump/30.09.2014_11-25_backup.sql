-- MySqlBackup.NET 2.0.2
-- Dump Time: 2014-09-30 11:25:45
-- --------------------------------------
-- Server version 5.6.16-64.0-beget-log (LTD BeGet)


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;


-- 
-- Definition of Address
-- 

DROP TABLE IF EXISTS `Address`;
CREATE TABLE IF NOT EXISTS `Address` (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT 'Код',
  `HouseNumber` int(11) NOT NULL COMMENT 'Номер дома',
  `Housing` varchar(5) CHARACTER SET cp1257 DEFAULT NULL COMMENT 'Корпус',
  `Building` int(11) DEFAULT NULL COMMENT 'Строение',
  `Apartment` varchar(5) CHARACTER SET cp1257 DEFAULT NULL COMMENT 'Квартира',
  `Postcode` int(11) NOT NULL COMMENT 'Почтовый индекс',
  `StreetId` int(11) NOT NULL COMMENT 'Код улицы',
  `TownId` int(11) NOT NULL COMMENT 'Код населенного пункта',
  `DistrictId` int(11) NOT NULL COMMENT 'Код района',
  `RegionId` int(11) NOT NULL COMMENT 'Код региона',
  PRIMARY KEY (`Id`),
  KEY `StreetId` (`StreetId`,`TownId`,`DistrictId`,`RegionId`),
  KEY `TownId` (`TownId`),
  KEY `DistrictId` (`DistrictId`,`RegionId`),
  KEY `RegionId` (`RegionId`),
  CONSTRAINT `Address_ibfk_1` FOREIGN KEY (`StreetId`) REFERENCES `Street` (`Id`) ON DELETE NO ACTION ON UPDATE CASCADE,
  CONSTRAINT `Address_ibfk_2` FOREIGN KEY (`TownId`) REFERENCES `Town` (`Id`) ON DELETE NO ACTION ON UPDATE CASCADE,
  CONSTRAINT `Address_ibfk_3` FOREIGN KEY (`DistrictId`) REFERENCES `District` (`Id`) ON DELETE NO ACTION ON UPDATE CASCADE,
  CONSTRAINT `Address_ibfk_4` FOREIGN KEY (`RegionId`) REFERENCES `Region` (`Id`) ON DELETE NO ACTION ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=cp1251;

-- 
-- Dumping data for table Address
-- 

/*!40000 ALTER TABLE `Address` DISABLE KEYS */;
INSERT INTO `Address`(`Id`,`HouseNumber`,`Housing`,`Building`,`Apartment`,`Postcode`,`StreetId`,`TownId`,`DistrictId`,`RegionId`) VALUES
(1,211,'',0,'12',460000,2,1,1,1),
(2,65,'',0,'34',460000,2,1,1,1),
(3,65,'1',0,'2',460000,2,1,1,1),
(4,1,'',0,'9',460001,4,1,1,1),
(5,32,'',0,'23',460010,3,1,1,1);
/*!40000 ALTER TABLE `Address` ENABLE KEYS */;

-- 
-- Definition of Authorization
-- 

DROP TABLE IF EXISTS `Authorization`;
CREATE TABLE IF NOT EXISTS `Authorization` (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT 'Код',
  `DateTime` datetime NOT NULL COMMENT 'Дата, время авторизации',
  `Login` varchar(20) NOT NULL COMMENT 'Логин пользователя',
  PRIMARY KEY (`Id`),
  KEY `Login` (`Login`),
  CONSTRAINT `Authorization_ibfk_2` FOREIGN KEY (`Login`) REFERENCES `User` (`Login`) ON DELETE NO ACTION ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=131 DEFAULT CHARSET=cp1251;

-- 
-- Dumping data for table Authorization
-- 

/*!40000 ALTER TABLE `Authorization` DISABLE KEYS */;
INSERT INTO `Authorization`(`Id`,`DateTime`,`Login`) VALUES
(3,'2014-08-19 08:44:41','Ivanov'),
(4,'2014-08-19 09:09:16','Ivanov'),
(5,'2014-08-19 09:17:32','Kudrin'),
(6,'2014-08-19 09:20:02','Kudrin'),
(7,'2014-08-19 09:22:09','Ivanov'),
(8,'2014-08-19 10:46:20','Ivanov'),
(9,'2014-08-19 10:55:20','Ivanov'),
(10,'2014-08-19 10:56:20','Ivanov'),
(11,'2014-08-19 10:58:29','Ivanov'),
(12,'2014-08-19 10:59:49','Ivanov'),
(13,'2014-08-19 11:03:30','Ivanov'),
(14,'2014-08-19 11:13:31','Ivanov'),
(15,'2014-08-19 11:15:14','Ivanov'),
(16,'2014-08-19 11:17:17','Ivanov'),
(17,'2014-08-19 11:22:21','Ivanov'),
(18,'2014-08-19 11:24:28','Ivanov'),
(19,'2014-08-19 11:28:28','Kudrin'),
(20,'2014-08-19 11:38:12','Kudrin'),
(21,'2014-08-19 11:41:23','Kudrin'),
(22,'2014-08-19 11:46:21','Ivanov'),
(23,'2014-08-19 11:56:36','Kudrin'),
(24,'2014-08-19 12:02:25','Ivanov'),
(25,'2014-08-19 12:03:28','Ivanov'),
(26,'2014-08-19 12:05:57','Ivanov'),
(27,'2014-08-19 12:12:17','Ivanov'),
(28,'2014-08-19 12:14:04','Ivanov'),
(29,'2014-08-19 12:16:53','Ivanov'),
(30,'2014-08-19 12:18:05','Kudrin'),
(31,'2014-08-19 12:19:47','Ivanov'),
(32,'2014-08-19 12:22:28','Ivanov'),
(33,'2014-08-19 12:23:29','Ivanov'),
(34,'2014-08-19 12:24:22','Ivanov'),
(35,'2014-08-19 12:24:57','Ivanov'),
(36,'2014-08-19 12:27:38','Ivanov'),
(37,'2014-08-19 12:30:19','Ivanov'),
(38,'2014-08-19 12:32:03','Ivanov'),
(39,'2014-08-19 12:34:00','Ivanov'),
(40,'2014-08-19 12:37:34','Ivanov'),
(41,'2014-08-19 12:39:48','Ivanov'),
(42,'2014-08-19 12:47:11','Ivanov'),
(43,'2014-08-19 12:51:13','Ivanov'),
(44,'2014-08-19 12:54:19','Ivanov'),
(45,'2014-08-19 14:16:52','Ivanov'),
(46,'2014-08-19 14:19:09','Ivanov'),
(47,'2014-08-19 14:46:51','Ivanov'),
(48,'2014-08-19 14:48:12','Ivanov'),
(49,'2014-08-19 14:55:41','Ivanov'),
(50,'2014-08-19 14:58:58','Ivanov'),
(51,'2014-08-19 15:02:32','Ivanov'),
(52,'2014-08-19 15:03:12','Ivanov'),
(53,'2014-08-19 15:04:15','Ivanov'),
(54,'2014-08-19 15:08:43','Ivanov'),
(55,'2014-08-19 15:24:08','Ivanov'),
(56,'2014-08-19 15:24:45','Ivanov'),
(57,'2014-08-19 15:25:38','Ivanov'),
(58,'2014-08-19 15:29:43','Ivanov'),
(59,'2014-08-19 15:32:22','Ivanov'),
(60,'2014-08-19 15:37:30','Ivanov'),
(61,'2014-08-19 15:44:03','Ivanov'),
(62,'2014-08-19 15:45:47','Ivanov'),
(63,'2014-08-19 15:48:31','Ivanov'),
(64,'2014-08-19 16:10:56','Ivanov'),
(65,'2014-08-20 08:29:25','Ivanov'),
(66,'2014-08-20 08:31:49','Ivanov'),
(67,'2014-08-20 08:37:54','Ivanov'),
(68,'2014-08-20 08:38:45','Ivanov'),
(69,'2014-08-20 08:40:35','Ivanov'),
(70,'2014-08-20 08:43:12','Ivanov'),
(71,'2014-08-20 12:25:36','Ivanov'),
(72,'2014-08-20 14:49:22','Ivanov'),
(73,'2014-08-20 14:53:04','Ivanov'),
(74,'2014-08-20 15:06:14','Ivanov'),
(75,'2014-08-20 15:07:46','Ivanov'),
(76,'2014-08-20 15:10:31','Ivanov'),
(77,'2014-08-20 15:12:53','Ivanov'),
(78,'2014-08-20 15:21:40','Ivanov'),
(79,'2014-08-20 15:54:07','Ivanov'),
(80,'2014-08-20 16:38:10','Ivanov'),
(81,'2014-08-21 11:45:53','Ivanov'),
(82,'2014-08-21 11:48:23','Ivanov'),
(83,'2014-08-21 11:49:37','Ivanov'),
(84,'2014-08-21 12:01:46','Ivanov'),
(85,'2014-08-21 12:07:19','Ivanov'),
(86,'2014-08-21 12:09:16','Ivanov'),
(87,'2014-08-21 12:12:28','Ivanov'),
(88,'2014-08-21 12:23:48','Ivanov'),
(89,'2014-08-21 12:26:04','Ivanov'),
(90,'2014-08-21 12:28:22','Ivanov'),
(91,'2014-08-21 12:31:14','Ivanov'),
(92,'2014-08-21 12:36:15','Ivanov'),
(93,'2014-08-21 12:37:00','Ivanov'),
(94,'2014-08-21 12:37:51','Ivanov'),
(95,'2014-08-21 12:39:58','Ivanov'),
(96,'2014-08-21 12:41:59','Ivanov'),
(97,'2014-08-21 12:43:11','Ivanov'),
(98,'2014-08-21 12:45:03','Ivanov'),
(99,'2014-08-21 12:51:15','Ivanov'),
(100,'2014-08-21 12:53:34','Kudrin'),
(101,'2014-08-21 12:55:44','Kudrin'),
(102,'2014-08-21 12:57:05','Kudrin'),
(103,'2014-08-21 12:59:40','Kudrin'),
(104,'2014-08-21 14:08:02','Ivanov'),
(105,'2014-08-21 14:14:43','Ivanov'),
(106,'2014-08-21 14:16:30','Kudrin'),
(107,'2014-08-21 14:18:00','Kudrin'),
(108,'2014-08-21 14:28:39','Kudrin'),
(109,'2014-08-21 14:52:34','Kudrin'),
(110,'2014-08-21 14:56:44','Kudrin'),
(111,'2014-08-21 14:58:11','Kudrin'),
(112,'2014-08-21 15:02:44','Ivanov'),
(113,'2014-08-21 15:03:39','Ivanov'),
(114,'2014-08-21 15:04:15','Kudrin'),
(115,'2014-08-21 15:06:45','Kudrin'),
(116,'2014-08-21 15:08:59','Ivanov'),
(117,'2014-08-21 15:11:01','Kudrin'),
(118,'2014-08-21 15:13:48','Ivanov'),
(119,'2014-08-21 15:21:37','Kudrin'),
(120,'2014-08-21 15:23:10','Ivanov'),
(121,'2014-08-21 15:24:54','Ivanov'),
(122,'2014-08-21 15:26:22','Ivanov'),
(123,'2014-08-21 15:27:48','Ivanov'),
(124,'2014-08-21 15:47:48','Ivanov'),
(125,'2014-08-21 15:50:57','Ivanov'),
(126,'2014-08-21 16:21:50','Ivanov'),
(127,'2014-08-21 16:24:50','Ivanov'),
(128,'2014-08-21 16:25:37','Ivanov'),
(129,'2014-08-21 16:33:34','Ivanov'),
(130,'2014-08-21 16:34:50','Ivanov');
/*!40000 ALTER TABLE `Authorization` ENABLE KEYS */;

-- 
-- Definition of Client
-- 

DROP TABLE IF EXISTS `Client`;
CREATE TABLE IF NOT EXISTS `Client` (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT 'Код',
  `RegistrationDate` datetime NOT NULL COMMENT 'Дата регистрации',
  `CompanyId` int(11) DEFAULT NULL COMMENT 'Код юридического лица',
  `PersonId` int(11) DEFAULT NULL COMMENT 'Код физического лица',
  `ManagerId` int(11) NOT NULL COMMENT 'Код менеджера',
  PRIMARY KEY (`Id`),
  KEY `CompanyId` (`CompanyId`,`PersonId`,`ManagerId`),
  KEY `PersonId` (`PersonId`),
  KEY `ManagerId` (`ManagerId`),
  CONSTRAINT `Client_ibfk_1` FOREIGN KEY (`CompanyId`) REFERENCES `Company` (`Id`) ON DELETE NO ACTION ON UPDATE CASCADE,
  CONSTRAINT `Client_ibfk_2` FOREIGN KEY (`PersonId`) REFERENCES `Person` (`Id`) ON DELETE NO ACTION ON UPDATE CASCADE,
  CONSTRAINT `Client_ibfk_3` FOREIGN KEY (`ManagerId`) REFERENCES `Manager` (`Id`) ON DELETE NO ACTION ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=cp1251;

-- 
-- Dumping data for table Client
-- 

/*!40000 ALTER TABLE `Client` DISABLE KEYS */;
INSERT INTO `Client`(`Id`,`RegistrationDate`,`CompanyId`,`PersonId`,`ManagerId`) VALUES
(2,'2014-08-07 00:00:00',NULL,1,1),
(3,'2014-08-21 00:00:00',NULL,2,1),
(4,'2014-08-13 00:00:00',NULL,6,1),
(5,'2014-08-14 00:00:00',1,NULL,1),
(6,'2014-08-14 00:00:00',NULL,6,2),
(7,'2014-08-19 00:00:00',1,NULL,2);
/*!40000 ALTER TABLE `Client` ENABLE KEYS */;

-- 
-- Definition of Company
-- 

DROP TABLE IF EXISTS `Company`;
CREATE TABLE IF NOT EXISTS `Company` (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT 'Код',
  `MSRN` varchar(15) NOT NULL COMMENT 'ОГРН',
  `ITN` varchar(12) NOT NULL COMMENT 'ИНН',
  `Name` varchar(100) NOT NULL COMMENT 'Название',
  `ShortName` varchar(70) DEFAULT NULL COMMENT 'Краткое название',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=cp1251 COMMENT='Юридическое лицо';

-- 
-- Dumping data for table Company
-- 

/*!40000 ALTER TABLE `Company` DISABLE KEYS */;
INSERT INTO `Company`(`Id`,`MSRN`,`ITN`,`Name`,`ShortName`) VALUES
(1,'1234567890','1234567890','ООО \"Автоматизированные системы управления\"','ООО \"АСУ\"'),
(2,'1234567890','1234567890','ООО \"Автоматизированные системы управления\"','ООО \"АСУ\"'),
(3,'1234567890','1234567890','ООО \"Автоматизированные системы управления\"','ООО \"АСУ\"'),
(4,'1234567890','1234567890','ООО \"Автоматизированные системы управления\"','ООО \"АСУ\"');
/*!40000 ALTER TABLE `Company` ENABLE KEYS */;

-- 
-- Definition of Contact
-- 

DROP TABLE IF EXISTS `Contact`;
CREATE TABLE IF NOT EXISTS `Contact` (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT 'Код',
  `Value` varchar(50) NOT NULL COMMENT 'Значение',
  `ContactTypeId` int(11) NOT NULL COMMENT 'Код типа контактной информации',
  `PersonId` int(11) DEFAULT NULL COMMENT 'Код физического лица',
  `CompanyId` int(11) DEFAULT NULL COMMENT 'Код юридического лица',
  PRIMARY KEY (`Id`),
  KEY `PersonId` (`PersonId`),
  KEY `CompanyId` (`CompanyId`),
  KEY `ContactTypeId` (`ContactTypeId`),
  CONSTRAINT `Contact_ibfk_1` FOREIGN KEY (`ContactTypeId`) REFERENCES `ContactType` (`Id`) ON DELETE NO ACTION ON UPDATE CASCADE,
  CONSTRAINT `Contact_ibfk_2` FOREIGN KEY (`PersonId`) REFERENCES `Person` (`Id`) ON DELETE NO ACTION ON UPDATE CASCADE,
  CONSTRAINT `Contact_ibfk_3` FOREIGN KEY (`CompanyId`) REFERENCES `Company` (`Id`) ON DELETE NO ACTION ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=cp1251 COMMENT='Контактная информация';

-- 
-- Dumping data for table Contact
-- 

/*!40000 ALTER TABLE `Contact` DISABLE KEYS */;
INSERT INTO `Contact`(`Id`,`Value`,`ContactTypeId`,`PersonId`,`CompanyId`) VALUES
(1,'123458',1,1,NULL),
(2,'blah@mail.ru',2,1,NULL),
(3,'906539',1,1,NULL);
/*!40000 ALTER TABLE `Contact` ENABLE KEYS */;

-- 
-- Definition of ContactType
-- 

DROP TABLE IF EXISTS `ContactType`;
CREATE TABLE IF NOT EXISTS `ContactType` (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT 'Код',
  `Name` varchar(50) NOT NULL COMMENT 'Название',
  `ShortName` varchar(25) DEFAULT NULL COMMENT 'Краткое название',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=cp1251 COMMENT='Тип контактной информации';

-- 
-- Dumping data for table ContactType
-- 

/*!40000 ALTER TABLE `ContactType` DISABLE KEYS */;
INSERT INTO `ContactType`(`Id`,`Name`,`ShortName`) VALUES
(1,'Телефон',''),
(2,'E-mail','');
/*!40000 ALTER TABLE `ContactType` ENABLE KEYS */;

-- 
-- Definition of CreditBureau
-- 

DROP TABLE IF EXISTS `CreditBureau`;
CREATE TABLE IF NOT EXISTS `CreditBureau` (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT 'Код',
  `Name` varchar(100) NOT NULL COMMENT 'Название',
  `ShortName` varchar(70) NOT NULL COMMENT 'Краткое название',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=cp1251 COMMENT='БКИ';

-- 
-- Dumping data for table CreditBureau
-- 

/*!40000 ALTER TABLE `CreditBureau` DISABLE KEYS */;
INSERT INTO `CreditBureau`(`Id`,`Name`,`ShortName`) VALUES
(1,'НБКИ','Национальное Бюро Кредитных Историй'),
(2,'ОБКИ','ОБКИ');
/*!40000 ALTER TABLE `CreditBureau` ENABLE KEYS */;

-- 
-- Definition of District
-- 

DROP TABLE IF EXISTS `District`;
CREATE TABLE IF NOT EXISTS `District` (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT 'Код',
  `Name` varchar(50) NOT NULL COMMENT 'Название',
  `ShortName` varchar(25) DEFAULT NULL COMMENT 'Краткое название',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=cp1251 COMMENT='Район';

-- 
-- Dumping data for table District
-- 

/*!40000 ALTER TABLE `District` DISABLE KEYS */;
INSERT INTO `District`(`Id`,`Name`,`ShortName`) VALUES
(1,'Орский район','Орский'),
(2,'Оренбургский','Оренбургский');
/*!40000 ALTER TABLE `District` ENABLE KEYS */;

-- 
-- Definition of Document
-- 

DROP TABLE IF EXISTS `Document`;
CREATE TABLE IF NOT EXISTS `Document` (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT 'Код',
  `Number` varchar(30) NOT NULL COMMENT 'Номер',
  `Series` varchar(30) DEFAULT NULL COMMENT 'Серия',
  `IssueDate` date NOT NULL COMMENT 'Дата выдачи',
  `Institution` varchar(300) DEFAULT NULL COMMENT 'Кем выдан',
  `PersonId` int(11) NOT NULL COMMENT 'Код физического лица',
  `DocumentTypeId` int(11) NOT NULL COMMENT 'Код типа документа',
  `ReceivePlace` text,
  PRIMARY KEY (`Id`),
  KEY `PersonId` (`PersonId`),
  KEY `DocumentTypeId` (`DocumentTypeId`),
  CONSTRAINT `Document_ibfk_1` FOREIGN KEY (`PersonId`) REFERENCES `Person` (`Id`) ON DELETE NO ACTION ON UPDATE CASCADE,
  CONSTRAINT `Document_ibfk_2` FOREIGN KEY (`DocumentTypeId`) REFERENCES `DocumentType` (`Id`) ON DELETE NO ACTION ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=cp1251 COMMENT='Документ';

-- 
-- Dumping data for table Document
-- 

/*!40000 ALTER TABLE `Document` DISABLE KEYS */;
INSERT INTO `Document`(`Id`,`Number`,`Series`,`IssueDate`,`Institution`,`PersonId`,`DocumentTypeId`,`ReceivePlace`) VALUES
(5,'5305','421134','2014-08-26 00:00:00','ОВД УФМС Промышленного района г. Оренбурга',6,21,NULL),
(6,'541277','3456','2014-09-01 00:00:00','УФМС Оренбугской области',1,21,'Оренбург');
/*!40000 ALTER TABLE `Document` ENABLE KEYS */;

-- 
-- Definition of DocumentType
-- 

DROP TABLE IF EXISTS `DocumentType`;
CREATE TABLE IF NOT EXISTS `DocumentType` (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT 'Код',
  `Name` varchar(50) NOT NULL COMMENT 'Название',
  `ShortName` varchar(25) DEFAULT NULL COMMENT 'Краткое название',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=22 DEFAULT CHARSET=cp1251 COMMENT='Тип документа';

-- 
-- Dumping data for table DocumentType
-- 

/*!40000 ALTER TABLE `DocumentType` DISABLE KEYS */;
INSERT INTO `DocumentType`(`Id`,`Name`,`ShortName`) VALUES
(2,'Индивидуальный налоговый номер','ИНН'),
(3,'Обязательный государственный регистрационный номер','ОГРН'),
(4,'Новый тип',''),
(5,'Новыый',''),
(21,'Паспорт','');
/*!40000 ALTER TABLE `DocumentType` ENABLE KEYS */;

-- 
-- Definition of Manager
-- 

DROP TABLE IF EXISTS `Manager`;
CREATE TABLE IF NOT EXISTS `Manager` (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT 'Код',
  `RegistrationDate` date NOT NULL COMMENT 'Дата регистрации',
  `PersonId` int(11) NOT NULL COMMENT 'Код физического лица',
  PRIMARY KEY (`Id`),
  KEY `PersonId` (`PersonId`),
  CONSTRAINT `Manager_ibfk_1` FOREIGN KEY (`PersonId`) REFERENCES `Person` (`Id`) ON DELETE NO ACTION ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=cp1251;

-- 
-- Dumping data for table Manager
-- 

/*!40000 ALTER TABLE `Manager` DISABLE KEYS */;
INSERT INTO `Manager`(`Id`,`RegistrationDate`,`PersonId`) VALUES
(1,'2014-08-18 00:00:00',1),
(2,'2014-08-18 00:00:00',9),
(3,'2014-08-18 00:00:00',10);
/*!40000 ALTER TABLE `Manager` ENABLE KEYS */;

-- 
-- Definition of Person
-- 

DROP TABLE IF EXISTS `Person`;
CREATE TABLE IF NOT EXISTS `Person` (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT 'Код',
  `Surname` varchar(50) NOT NULL COMMENT 'Фамилия',
  `Name` varchar(15) NOT NULL COMMENT 'Имя',
  `Patronymic` varchar(20) NOT NULL COMMENT 'Отчество',
  `BirthDate` date DEFAULT NULL COMMENT 'Дата рождения',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=21 DEFAULT CHARSET=cp1251 COMMENT='Физическое лицо';

-- 
-- Dumping data for table Person
-- 

/*!40000 ALTER TABLE `Person` DISABLE KEYS */;
INSERT INTO `Person`(`Id`,`Surname`,`Name`,`Patronymic`,`BirthDate`) VALUES
(1,'Иванов','Иван','Иванович','1988-08-30 00:00:00'),
(2,'Попов','Федор','Егорович','2014-08-03 00:00:00'),
(4,'Леонов','Аркадий','Николаевич','2013-11-03 00:00:00'),
(5,'Иванов','Иван','Семенович','2012-04-08 00:00:00'),
(6,'Грека','Иван','Иванович','2012-11-19 00:00:00'),
(7,'Семенова','Ольга','Семеновна','2014-07-14 00:00:00'),
(8,'Иванов','Иван','Сергеевич','1984-12-01 00:00:00'),
(9,'Кудрин','Кудрин','Анатолий',NULL),
(10,'Пряхин','Пряхин','Илья',NULL),
(14,'Кастова','Екатерина','Александровна',NULL),
(15,'Астахов','Федор','Николаевич',NULL),
(16,'Пилюлькин','Павел','Николаевич',NULL),
(17,'1','12','12',NULL),
(18,'Грека','Иван','Иванович','2012-11-19 00:00:00'),
(19,'Грека','Иван','Иванович','2012-11-19 00:00:00'),
(20,'Греков','Артем','Артамоныч','2014-08-31 00:00:00');
/*!40000 ALTER TABLE `Person` ENABLE KEYS */;

-- 
-- Definition of Post
-- 

DROP TABLE IF EXISTS `Post`;
CREATE TABLE IF NOT EXISTS `Post` (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT 'Код',
  `Name` varchar(50) NOT NULL COMMENT 'Название',
  `ShortName` varchar(25) DEFAULT NULL COMMENT 'Краткое название',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=cp1251 COMMENT='Должность';

-- 
-- Dumping data for table Post
-- 

/*!40000 ALTER TABLE `Post` DISABLE KEYS */;
INSERT INTO `Post`(`Id`,`Name`,`ShortName`) VALUES
(1,'Менеджер','Манагер'),
(2,'Программист','Кодер'),
(3,'Менеджер','Манагер');
/*!40000 ALTER TABLE `Post` ENABLE KEYS */;

-- 
-- Definition of QueryJournal
-- 

DROP TABLE IF EXISTS `QueryJournal`;
CREATE TABLE IF NOT EXISTS `QueryJournal` (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT 'Код',
  `DateTime` datetime NOT NULL COMMENT 'Дата запроса',
  `Report` longblob COMMENT 'Кредитный отчет',
  `CreditBureauId` int(11) NOT NULL COMMENT 'Код БКИ',
  `ClientId` int(11) NOT NULL COMMENT 'Код клиента',
  `AuthorizationId` int(11) NOT NULL COMMENT 'Код авторизации',
  PRIMARY KEY (`Id`),
  KEY `CreditBureauId` (`CreditBureauId`),
  KEY `ClientId` (`ClientId`),
  KEY `AuthorizationId` (`AuthorizationId`),
  CONSTRAINT `QueryJournal_ibfk_1` FOREIGN KEY (`CreditBureauId`) REFERENCES `CreditBureau` (`Id`) ON DELETE NO ACTION ON UPDATE CASCADE,
  CONSTRAINT `QueryJournal_ibfk_2` FOREIGN KEY (`ClientId`) REFERENCES `Client` (`Id`) ON DELETE NO ACTION ON UPDATE CASCADE,
  CONSTRAINT `QueryJournal_ibfk_3` FOREIGN KEY (`AuthorizationId`) REFERENCES `Authorization` (`Id`) ON DELETE NO ACTION ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=cp1251 COMMENT='Журнал запросов';

-- 
-- Dumping data for table QueryJournal
-- 

/*!40000 ALTER TABLE `QueryJournal` DISABLE KEYS */;

/*!40000 ALTER TABLE `QueryJournal` ENABLE KEYS */;

-- 
-- Definition of Region
-- 

DROP TABLE IF EXISTS `Region`;
CREATE TABLE IF NOT EXISTS `Region` (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT 'Код',
  `Name` varchar(50) NOT NULL COMMENT 'Название',
  `ShortName` varchar(25) DEFAULT NULL COMMENT 'Краткое название',
  `RegionTypeId` int(11) NOT NULL COMMENT 'Код типа региона',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=cp1251 COMMENT='Регион';

-- 
-- Dumping data for table Region
-- 

/*!40000 ALTER TABLE `Region` DISABLE KEYS */;
INSERT INTO `Region`(`Id`,`Name`,`ShortName`,`RegionTypeId`) VALUES
(1,'Оренбургская','',1);
/*!40000 ALTER TABLE `Region` ENABLE KEYS */;

-- 
-- Definition of RegionType
-- 

DROP TABLE IF EXISTS `RegionType`;
CREATE TABLE IF NOT EXISTS `RegionType` (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT 'Код',
  `Name` varchar(50) NOT NULL COMMENT 'Название',
  `ShortName` varchar(25) DEFAULT NULL COMMENT 'Краткое название',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=cp1251 COMMENT='Тип региона';

-- 
-- Dumping data for table RegionType
-- 

/*!40000 ALTER TABLE `RegionType` DISABLE KEYS */;
INSERT INTO `RegionType`(`Id`,`Name`,`ShortName`) VALUES
(1,'Область','');
/*!40000 ALTER TABLE `RegionType` ENABLE KEYS */;

-- 
-- Definition of Registration
-- 

DROP TABLE IF EXISTS `Registration`;
CREATE TABLE IF NOT EXISTS `Registration` (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT 'Код',
  `Date` date NOT NULL COMMENT 'Дата регистрации',
  `RegistrationTypeId` int(11) NOT NULL COMMENT 'Код типа регистрации',
  `AddressId` int(11) NOT NULL COMMENT 'Код адреса',
  `PersonId` int(11) DEFAULT NULL COMMENT 'Код физического лица',
  `CompanyId` int(11) DEFAULT NULL COMMENT 'Код юридического лица',
  PRIMARY KEY (`Id`),
  KEY `RegistrationTypeId` (`RegistrationTypeId`),
  KEY `AddressId` (`AddressId`),
  KEY `PersonId` (`PersonId`),
  KEY `CompanyId` (`CompanyId`),
  CONSTRAINT `Registration_ibfk_1` FOREIGN KEY (`RegistrationTypeId`) REFERENCES `RegistrationType` (`Id`) ON DELETE NO ACTION ON UPDATE CASCADE,
  CONSTRAINT `Registration_ibfk_2` FOREIGN KEY (`AddressId`) REFERENCES `Address` (`Id`) ON DELETE NO ACTION ON UPDATE CASCADE,
  CONSTRAINT `Registration_ibfk_3` FOREIGN KEY (`PersonId`) REFERENCES `Person` (`Id`) ON DELETE NO ACTION ON UPDATE CASCADE,
  CONSTRAINT `Registration_ibfk_4` FOREIGN KEY (`CompanyId`) REFERENCES `Company` (`Id`) ON DELETE NO ACTION ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=cp1251 COMMENT='Регистрация';

-- 
-- Dumping data for table Registration
-- 

/*!40000 ALTER TABLE `Registration` DISABLE KEYS */;
INSERT INTO `Registration`(`Id`,`Date`,`RegistrationTypeId`,`AddressId`,`PersonId`,`CompanyId`) VALUES
(2,'2014-08-14 00:00:00',2,4,1,NULL),
(3,'2014-08-14 00:00:00',1,5,1,NULL);
/*!40000 ALTER TABLE `Registration` ENABLE KEYS */;

-- 
-- Definition of RegistrationType
-- 

DROP TABLE IF EXISTS `RegistrationType`;
CREATE TABLE IF NOT EXISTS `RegistrationType` (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT 'Код',
  `Name` varchar(50) NOT NULL COMMENT 'Название',
  `ShortName` varchar(25) DEFAULT NULL COMMENT 'Краткое название',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=cp1251 COMMENT='Тип регистрации';

-- 
-- Dumping data for table RegistrationType
-- 

/*!40000 ALTER TABLE `RegistrationType` DISABLE KEYS */;
INSERT INTO `RegistrationType`(`Id`,`Name`,`ShortName`) VALUES
(1,'Фактический адрес',''),
(2,'Юридический адрес','');
/*!40000 ALTER TABLE `RegistrationType` ENABLE KEYS */;

-- 
-- Definition of Representative
-- 

DROP TABLE IF EXISTS `Representative`;
CREATE TABLE IF NOT EXISTS `Representative` (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT 'Код',
  `PostId` int(11) NOT NULL COMMENT 'Код должности',
  `CompanyId` int(11) NOT NULL COMMENT 'Код юридического лица',
  `PersonId` int(11) NOT NULL COMMENT 'Код физического лица',
  PRIMARY KEY (`Id`),
  KEY `PostId` (`PostId`),
  KEY `CompanyId` (`CompanyId`),
  KEY `PersonId` (`PersonId`),
  CONSTRAINT `Representative_ibfk_1` FOREIGN KEY (`PostId`) REFERENCES `Post` (`Id`) ON DELETE NO ACTION ON UPDATE CASCADE,
  CONSTRAINT `Representative_ibfk_2` FOREIGN KEY (`CompanyId`) REFERENCES `Company` (`Id`) ON DELETE NO ACTION ON UPDATE CASCADE,
  CONSTRAINT `Representative_ibfk_3` FOREIGN KEY (`PersonId`) REFERENCES `Person` (`Id`) ON DELETE NO ACTION ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=cp1251 COMMENT='Контактное лицо';

-- 
-- Dumping data for table Representative
-- 

/*!40000 ALTER TABLE `Representative` DISABLE KEYS */;
INSERT INTO `Representative`(`Id`,`PostId`,`CompanyId`,`PersonId`) VALUES
(1,2,1,14),
(2,1,1,15),
(3,1,1,16),
(4,1,1,17);
/*!40000 ALTER TABLE `Representative` ENABLE KEYS */;

-- 
-- Definition of Street
-- 

DROP TABLE IF EXISTS `Street`;
CREATE TABLE IF NOT EXISTS `Street` (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT 'Код',
  `Name` varchar(50) NOT NULL COMMENT 'Название',
  `ShortName` varchar(25) DEFAULT NULL COMMENT 'Краткое название',
  `StreetTypeId` int(11) NOT NULL COMMENT 'Код типа улицы',
  PRIMARY KEY (`Id`),
  KEY `StreetTypeId` (`StreetTypeId`),
  KEY `StreetTypeId_2` (`StreetTypeId`),
  CONSTRAINT `Street_ibfk_2` FOREIGN KEY (`StreetTypeId`) REFERENCES `StreetType` (`Id`) ON DELETE NO ACTION ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=cp1251 COMMENT='Улица';

-- 
-- Dumping data for table Street
-- 

/*!40000 ALTER TABLE `Street` DISABLE KEYS */;
INSERT INTO `Street`(`Id`,`Name`,`ShortName`,`StreetTypeId`) VALUES
(2,'Дзержинского','',1),
(3,'Гагарина','',1),
(4,'Автоматики','',2);
/*!40000 ALTER TABLE `Street` ENABLE KEYS */;

-- 
-- Definition of StreetType
-- 

DROP TABLE IF EXISTS `StreetType`;
CREATE TABLE IF NOT EXISTS `StreetType` (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT 'Код',
  `Name` varchar(50) NOT NULL,
  `ShortName` varchar(25) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=cp1251;

-- 
-- Dumping data for table StreetType
-- 

/*!40000 ALTER TABLE `StreetType` DISABLE KEYS */;
INSERT INTO `StreetType`(`Id`,`Name`,`ShortName`) VALUES
(1,'Проспект','Проспект'),
(2,'Проезд','Пр-д');
/*!40000 ALTER TABLE `StreetType` ENABLE KEYS */;

-- 
-- Definition of Town
-- 

DROP TABLE IF EXISTS `Town`;
CREATE TABLE IF NOT EXISTS `Town` (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT 'Код',
  `Name` varchar(50) NOT NULL COMMENT 'Название',
  `ShortName` varchar(25) DEFAULT NULL COMMENT 'Краткое название',
  `TownTypeId` int(11) NOT NULL COMMENT 'Код типа населенного пункта',
  PRIMARY KEY (`Id`),
  KEY `TownTypeId` (`TownTypeId`),
  CONSTRAINT `Town_ibfk_1` FOREIGN KEY (`TownTypeId`) REFERENCES `TownType` (`Id`) ON DELETE NO ACTION ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=cp1251 COMMENT='Населенный пункт';

-- 
-- Dumping data for table Town
-- 

/*!40000 ALTER TABLE `Town` DISABLE KEYS */;
INSERT INTO `Town`(`Id`,`Name`,`ShortName`,`TownTypeId`) VALUES
(1,'Оренбург','',1);
/*!40000 ALTER TABLE `Town` ENABLE KEYS */;

-- 
-- Definition of TownType
-- 

DROP TABLE IF EXISTS `TownType`;
CREATE TABLE IF NOT EXISTS `TownType` (
  `Id` int(11) NOT NULL AUTO_INCREMENT COMMENT 'Код',
  `Name` varchar(50) NOT NULL COMMENT 'Название',
  `ShortName` varchar(25) DEFAULT NULL COMMENT 'Краткое название',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=cp1251 COMMENT='Тип населенного пункта';

-- 
-- Dumping data for table TownType
-- 

/*!40000 ALTER TABLE `TownType` DISABLE KEYS */;
INSERT INTO `TownType`(`Id`,`Name`,`ShortName`) VALUES
(1,'Город','');
/*!40000 ALTER TABLE `TownType` ENABLE KEYS */;

-- 
-- Definition of User
-- 

DROP TABLE IF EXISTS `User`;
CREATE TABLE IF NOT EXISTS `User` (
  `Login` varchar(20) NOT NULL COMMENT 'Логин',
  `Password` varchar(70) NOT NULL COMMENT 'Пароль',
  `Date` date NOT NULL COMMENT 'Дата выдачи',
  `ManagerId` int(11) NOT NULL COMMENT 'Код менеджера',
  PRIMARY KEY (`Login`),
  KEY `ManagerId` (`ManagerId`),
  CONSTRAINT `User_ibfk_1` FOREIGN KEY (`ManagerId`) REFERENCES `Manager` (`Id`) ON DELETE NO ACTION ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=cp1251 COMMENT='Авторизация';

-- 
-- Dumping data for table User
-- 

/*!40000 ALTER TABLE `User` DISABLE KEYS */;
INSERT INTO `User`(`Login`,`Password`,`Date`,`ManagerId`) VALUES
('Ivanov','827ccb0eea8a706c4c34','2014-08-18 00:00:00',1),
('Kudrin','827ccb0eea8a706c4c34','2014-08-18 00:00:00',2);
/*!40000 ALTER TABLE `User` ENABLE KEYS */;

-- 
-- Definition of doctors
-- 

DROP TABLE IF EXISTS `doctors`;
CREATE TABLE IF NOT EXISTS `doctors` (
  `doctor_id` bigint(30) NOT NULL AUTO_INCREMENT COMMENT 'номер записи врача',
  `doctor_lastname` varchar(50) DEFAULT NULL COMMENT 'фамилия врача',
  `doctor_name` varchar(50) DEFAULT NULL COMMENT 'имя врача',
  `doctor_secondname` varchar(100) DEFAULT NULL COMMENT 'отчество врача',
  `doctor_specialisation` text COMMENT 'адрес врача',
  `doctor_settings` text,
  `doctor_phone` varchar(12) DEFAULT NULL COMMENT 'телефон врача',
  `doctor_email` text COMMENT 'почта врача',
  `doctor_creation_date` datetime DEFAULT NULL COMMENT 'дата создания записи врача',
  `doctor_status` text,
  PRIMARY KEY (`doctor_id`)
) ENGINE=MyISAM AUTO_INCREMENT=17 DEFAULT CHARSET=utf8 COMMENT='Таблица содержит информацию о врачах.';

-- 
-- Dumping data for table doctors
-- 

/*!40000 ALTER TABLE `doctors` DISABLE KEYS */;
INSERT INTO `doctors`(`doctor_id`,`doctor_lastname`,`doctor_name`,`doctor_secondname`,`doctor_specialisation`,`doctor_settings`,`doctor_phone`,`doctor_email`,`doctor_creation_date`,`doctor_status`) VALUES
(1,'Фонтанов','Игорь','Константинович','уролог','{\"Services_Procent\":[[3,20],[1,25]]}','555555','','2014-03-01 22:46:57','off'),
(2,'Иванов','Иван','Иванович','гинеколог','{\"Services_Procent\":[[3,5],[2,12],[11,45],[5,50]]}','666666','','2014-03-01 22:48:07','on'),
(3,'Васильев','Евгений','Олегович','онколог','{\"Services_Procent\":[[4,20],[7,20],[3,10],[6,5]]}','+79032662781','','2014-03-01 17:46:58','on'),
(14,'Карпов','Карп','Карпович','стоматолог','{\"Services_Procent\":[[10,20],[11,10]]}','555666','','2014-03-25 14:46:04','on');
/*!40000 ALTER TABLE `doctors` ENABLE KEYS */;

-- 
-- Definition of doctors_timetable
-- 

DROP TABLE IF EXISTS `doctors_timetable`;
CREATE TABLE IF NOT EXISTS `doctors_timetable` (
  `id_record` int(11) NOT NULL AUTO_INCREMENT,
  `doctor_id` int(11) NOT NULL,
  `timetable_week` text,
  `date_record` date DEFAULT NULL,
  `date_timetable` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`id_record`)
) ENGINE=MyISAM AUTO_INCREMENT=32 DEFAULT CHARSET=utf8;

-- 
-- Dumping data for table doctors_timetable
-- 

/*!40000 ALTER TABLE `doctors_timetable` DISABLE KEYS */;

/*!40000 ALTER TABLE `doctors_timetable` ENABLE KEYS */;


/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;


-- Dump completed on 2014-09-30 11:25:51
-- Total time: 0:0:0:5:120 (d:h:m:s:ms)
