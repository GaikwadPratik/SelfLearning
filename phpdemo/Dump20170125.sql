CREATE DATABASE  IF NOT EXISTS `demo_schema` /*!40100 DEFAULT CHARACTER SET latin1 */;
USE `demo_schema`;
-- MySQL dump 10.13  Distrib 5.7.17, for Linux (x86_64)
--
-- Host: localhost    Database: demo_schema
-- ------------------------------------------------------
-- Server version	5.7.17-0ubuntu0.16.04.1

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `addresses`
--

DROP TABLE IF EXISTS `addresses`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `addresses` (
  `addresseId` int(11) NOT NULL AUTO_INCREMENT COMMENT 'Primary key of the table. To be used as FK in University and Employer respectively',
  `personId` int(11) NOT NULL COMMENT 'FK to person table',
  `addressType` int(11) NOT NULL COMMENT 'FK to AddressType',
  `firstLine` varchar(45) NOT NULL COMMENT 'First line of the address',
  `lastLine` varchar(45) DEFAULT NULL COMMENT 'last line of the address',
  `city` varchar(45) NOT NULL COMMENT 'city of the address',
  `state` varchar(45) NOT NULL COMMENT 'state of the address',
  `zip` varchar(45) NOT NULL COMMENT 'zip of the address',
  `createDt` datetime NOT NULL,
  `updateDt` datetime NOT NULL,
  `isDeleted` tinyint(1) NOT NULL,
  PRIMARY KEY (`addresseId`),
  KEY `personaddressFK_idx` (`personId`),
  KEY `addresstypeaddressFK_idx` (`addressType`),
  CONSTRAINT `addresstypeaddressFK` FOREIGN KEY (`addressType`) REFERENCES `addresstypes` (`addressTypeId`) ON UPDATE CASCADE,
  CONSTRAINT `personaddressFK` FOREIGN KEY (`personId`) REFERENCES `person` (`personId`) ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=latin1 COMMENT='table containing address information of the person, respective employers and universities';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `addresses`
--

LOCK TABLES `addresses` WRITE;
/*!40000 ALTER TABLE `addresses` DISABLE KEYS */;
INSERT INTO `addresses` VALUES (1,1,1,'453 Palisade Ave','Apartment 4','Jersey City','NJ','07307','2017-01-22 15:30:35','2017-01-24 11:14:11',0),(2,2,0,'','','','','','2017-01-23 14:50:53','2017-01-23 14:51:00',1),(3,3,1,'sdf','sd','dfs','sd','123','2017-01-23 21:58:23','2017-01-23 21:58:23',0),(4,4,1,'sdfg','sdfg','sdfg','sd','234','2017-01-23 21:59:41','2017-01-23 22:05:46',1),(5,5,1,'A-501 Lords CHSL','Near Swastik Park','Mumbai','MH','40007','2017-01-24 00:44:23','2017-01-24 00:45:13',0),(6,6,1,'123','123','123','12','123','2017-01-24 11:49:51','2017-01-24 11:49:51',0);
/*!40000 ALTER TABLE `addresses` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `addresstypes`
--

DROP TABLE IF EXISTS `addresstypes`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `addresstypes` (
  `addressTypeId` int(11) NOT NULL AUTO_INCREMENT COMMENT 'Address type id. Primary key. To be used as FK in Address table.',
  `addressTypeName` varchar(45) NOT NULL COMMENT 'Type of the address. For now it will contain 1. Home, 2. University, 3. Employer',
  `createDt` datetime NOT NULL,
  `updateDt` datetime NOT NULL,
  `isDeleted` tinyint(1) NOT NULL,
  PRIMARY KEY (`addressTypeId`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `addresstypes`
--

LOCK TABLES `addresstypes` WRITE;
/*!40000 ALTER TABLE `addresstypes` DISABLE KEYS */;
INSERT INTO `addresstypes` VALUES (0,'','2017-01-16 16:04:53','2017-01-16 16:04:53',0),(1,'Home','2017-01-16 16:04:53','2017-01-16 16:04:53',0),(2,'Employer','2017-01-16 16:04:53','2017-01-16 16:04:53',0),(3,'University','2017-01-16 16:04:53','2017-01-16 16:04:53',0),(4,'sdf','2017-01-21 11:05:05','2017-01-21 11:05:05',0),(8,'addresstype','2017-01-21 11:11:07','2017-01-21 11:11:07',0),(9,'addresstype1','2017-01-21 11:12:37','2017-01-21 11:12:37',0),(10,'addresstype2','2017-01-21 11:13:40','2017-01-21 11:13:40',0);
/*!40000 ALTER TABLE `addresstypes` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `emails`
--

DROP TABLE IF EXISTS `emails`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `emails` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `emailId` varchar(200) NOT NULL,
  `personId` int(11) NOT NULL,
  `createDt` datetime NOT NULL,
  `updateDt` datetime NOT NULL,
  `emailtypeid` int(11) NOT NULL,
  `isDeleted` tinyint(1) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `emailpersonFK_idx` (`personId`),
  KEY `typeemailFK_idx` (`emailtypeid`),
  CONSTRAINT `emailpersonFK` FOREIGN KEY (`personId`) REFERENCES `person` (`personId`) ON UPDATE CASCADE,
  CONSTRAINT `emailtypeFK` FOREIGN KEY (`emailtypeid`) REFERENCES `emailtypes` (`emailTypeid`) ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=latin1 COMMENT='table containing email ids of the user.';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `emails`
--

LOCK TABLES `emails` WRITE;
/*!40000 ALTER TABLE `emails` DISABLE KEYS */;
INSERT INTO `emails` VALUES (1,'pratik.gaikwad19@gmail.com',1,'2017-01-22 15:30:35','2017-01-22 15:30:35',1,0),(2,'asdf',1,'2017-01-22 16:40:07','2017-01-23 21:30:01',1,1),(4,'234',4,'2017-01-23 21:59:41','2017-01-23 22:05:46',1,1),(5,'mdgaikwad@gmail.com',5,'2017-01-24 00:44:23','2017-01-24 00:44:23',1,0),(6,'123',6,'2017-01-24 11:49:51','2017-01-24 11:49:51',1,0);
/*!40000 ALTER TABLE `emails` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `emailtypes`
--

DROP TABLE IF EXISTS `emailtypes`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `emailtypes` (
  `emailTypeid` int(11) NOT NULL AUTO_INCREMENT,
  `emailTypeName` varchar(45) NOT NULL,
  `createDt` datetime NOT NULL,
  `updateDt` datetime NOT NULL,
  `isDeleted` tinyint(1) NOT NULL,
  PRIMARY KEY (`emailTypeid`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `emailtypes`
--

LOCK TABLES `emailtypes` WRITE;
/*!40000 ALTER TABLE `emailtypes` DISABLE KEYS */;
INSERT INTO `emailtypes` VALUES (1,'Home','2017-01-19 16:27:26','2017-01-19 16:27:26',0),(2,'Work','2017-01-19 16:27:26','2017-01-19 16:27:26',0),(3,'Personal','2017-01-19 16:27:26','2017-01-19 16:27:26',0);
/*!40000 ALTER TABLE `emailtypes` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `employers`
--

DROP TABLE IF EXISTS `employers`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `employers` (
  `employersId` int(11) NOT NULL AUTO_INCREMENT COMMENT 'Primary key of the table.',
  `personId` int(11) NOT NULL COMMENT 'FK to person table',
  `employerName` varchar(45) NOT NULL,
  `fromDate` datetime NOT NULL COMMENT 'Joining month/year ',
  `toDate` datetime DEFAULT NULL COMMENT 'ending month/year ',
  `createDt` datetime NOT NULL,
  `updateDt` datetime NOT NULL,
  `isDeleted` tinyint(1) NOT NULL,
  PRIMARY KEY (`employersId`),
  KEY `personemployerFK_idx` (`personId`),
  CONSTRAINT `personemployerFK` FOREIGN KEY (`personId`) REFERENCES `person` (`personId`) ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=latin1 COMMENT='table containing employer information of the person';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `employers`
--

LOCK TABLES `employers` WRITE;
/*!40000 ALTER TABLE `employers` DISABLE KEYS */;
INSERT INTO `employers` VALUES (1,1,'asdf','2017-01-09 00:00:00','2017-01-30 00:00:00','2017-01-22 16:40:07','2017-01-23 21:30:01',1);
/*!40000 ALTER TABLE `employers` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `person`
--

DROP TABLE IF EXISTS `person`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `person` (
  `personId` int(11) NOT NULL AUTO_INCREMENT COMMENT 'Primary key of the table.',
  `firstName` varchar(45) NOT NULL COMMENT 'first name of the person',
  `lastName` varchar(45) DEFAULT NULL COMMENT 'last name of the person',
  `sex` varchar(1) NOT NULL COMMENT 'should contain M, F',
  `createDt` datetime NOT NULL,
  `updateDt` datetime NOT NULL,
  `isDeleted` tinyint(1) NOT NULL,
  PRIMARY KEY (`personId`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=latin1 COMMENT='This table will contain first name and last name of the person';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `person`
--

LOCK TABLES `person` WRITE;
/*!40000 ALTER TABLE `person` DISABLE KEYS */;
INSERT INTO `person` VALUES (1,'Pratik','Gaikwad','M','2017-01-22 15:30:35','2017-01-24 11:14:11',0),(2,'','','','2017-01-23 14:50:53','2017-01-23 14:51:00',1),(3,'Aadi','Gaikwad','M','2017-01-23 21:58:23','2017-01-23 21:58:23',0),(4,'Test','TEst','F','2017-01-23 21:59:41','2017-01-23 22:05:46',1),(5,'Mayur','Gaikwad','M','2017-01-24 00:44:23','2017-01-24 00:45:13',0),(6,'Test','TEs','M','2017-01-24 11:49:51','2017-01-24 11:49:51',0);
/*!40000 ALTER TABLE `person` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `phonenos`
--

DROP TABLE IF EXISTS `phonenos`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `phonenos` (
  `phonenoId` int(11) NOT NULL AUTO_INCREMENT,
  `personId` int(11) NOT NULL COMMENT 'FK to Person table',
  `phoneTypeId` int(11) NOT NULL COMMENT 'FK to phone type table',
  `phoneno` varchar(45) NOT NULL,
  `createDt` datetime NOT NULL,
  `updateDt` datetime NOT NULL,
  `isDeleted` tinyint(1) NOT NULL,
  PRIMARY KEY (`phonenoId`),
  KEY `personphoneFK_idx` (`personId`),
  KEY `phonetypephoneFK_idx` (`phoneTypeId`),
  CONSTRAINT `personphoneFK` FOREIGN KEY (`personId`) REFERENCES `person` (`personId`) ON UPDATE CASCADE,
  CONSTRAINT `phonetypephoneFK` FOREIGN KEY (`phoneTypeId`) REFERENCES `phonetypes` (`phoneTypesId`) ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=latin1 COMMENT='table containing phone information';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `phonenos`
--

LOCK TABLES `phonenos` WRITE;
/*!40000 ALTER TABLE `phonenos` DISABLE KEYS */;
INSERT INTO `phonenos` VALUES (1,1,1,'201.742.3440','2017-01-22 15:30:35','2017-01-24 11:14:11',0),(2,1,1,'1234','2017-01-22 16:11:53','2017-01-23 21:30:01',1),(3,1,3,'213','2017-01-22 17:00:29','2017-01-22 17:01:40',1),(4,3,1,'234','2017-01-23 21:58:23','2017-01-23 21:58:23',0),(5,4,1,'234','2017-01-23 21:59:41','2017-01-23 22:05:46',1),(6,5,3,'9960609964','2017-01-24 00:44:23','2017-01-24 00:45:13',0),(7,6,1,'123','2017-01-24 11:49:51','2017-01-24 11:49:51',0);
/*!40000 ALTER TABLE `phonenos` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `phonetypes`
--

DROP TABLE IF EXISTS `phonetypes`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `phonetypes` (
  `phoneTypesId` int(11) NOT NULL AUTO_INCREMENT,
  `phoneTypesName` varchar(45) NOT NULL COMMENT 'Type of the phones. For now it will contain 1. Mobile, 2. work, 3. home',
  `createDt` datetime NOT NULL,
  `updateDt` datetime NOT NULL,
  `isDeleted` tinyint(1) NOT NULL,
  PRIMARY KEY (`phoneTypesId`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=latin1 COMMENT='table containing phone types: 1. work, 2. home, 3. mobile';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `phonetypes`
--

LOCK TABLES `phonetypes` WRITE;
/*!40000 ALTER TABLE `phonetypes` DISABLE KEYS */;
INSERT INTO `phonetypes` VALUES (1,'Mobile','2017-01-16 16:14:42','2017-01-16 16:14:42',0),(2,'Work','2017-01-16 16:14:42','2017-01-16 16:14:42',0),(3,'Home','2017-01-16 16:14:42','2017-01-16 16:14:42',0);
/*!40000 ALTER TABLE `phonetypes` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `researches`
--

DROP TABLE IF EXISTS `researches`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `researches` (
  `researcheId` int(11) NOT NULL AUTO_INCREMENT,
  `personId` int(11) NOT NULL COMMENT 'FK to person table',
  `researcheName` varchar(45) NOT NULL,
  `researchesDesc` varchar(100) DEFAULT NULL,
  `createDt` datetime NOT NULL,
  `updateDt` datetime NOT NULL,
  `isDeleted` tinyint(1) NOT NULL,
  PRIMARY KEY (`researcheId`),
  KEY `personresearchFK_idx` (`personId`),
  CONSTRAINT `personresearchFK` FOREIGN KEY (`personId`) REFERENCES `person` (`personId`) ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=latin1 COMMENT='table containing research information ';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `researches`
--

LOCK TABLES `researches` WRITE;
/*!40000 ALTER TABLE `researches` DISABLE KEYS */;
INSERT INTO `researches` VALUES (1,1,'TEst','TEst','2017-01-22 16:27:07','2017-01-23 21:30:01',1),(2,5,'Test','Test','2017-01-24 00:45:13','2017-01-24 00:45:13',0);
/*!40000 ALTER TABLE `researches` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `universities`
--

DROP TABLE IF EXISTS `universities`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `universities` (
  `universityId` int(11) NOT NULL AUTO_INCREMENT,
  `personId` int(11) NOT NULL COMMENT 'FK to person table',
  `universityType` int(11) NOT NULL COMMENT 'FK to univeristy type',
  `universityName` varchar(45) NOT NULL COMMENT 'name of the university',
  `fromDate` datetime NOT NULL COMMENT 'Joining month/year ',
  `toDate` datetime DEFAULT NULL COMMENT 'ending month/year ',
  `createDt` datetime NOT NULL,
  `updateDt` datetime NOT NULL,
  `isDeleted` tinyint(1) NOT NULL,
  PRIMARY KEY (`universityId`),
  KEY `personuniversityFK_idx` (`personId`),
  KEY `universitytypeuniversityFK_idx` (`universityType`),
  CONSTRAINT `personuniversityFK` FOREIGN KEY (`personId`) REFERENCES `person` (`personId`) ON UPDATE CASCADE,
  CONSTRAINT `universitytypeuniversityFK` FOREIGN KEY (`universityType`) REFERENCES `universitytype` (`universitytypeId`) ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=latin1 COMMENT='table containing information of universities attended';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `universities`
--

LOCK TABLES `universities` WRITE;
/*!40000 ALTER TABLE `universities` DISABLE KEYS */;
INSERT INTO `universities` VALUES (1,1,2,'Stevens Institute of Technology','2017-01-01 00:00:00',NULL,'2017-01-22 15:30:35','2017-01-24 11:14:11',0),(2,3,2,'asdf','2017-01-03 00:00:00',NULL,'2017-01-23 21:58:23','2017-01-23 21:58:23',0),(3,4,2,'sf','2017-01-04 00:00:00',NULL,'2017-01-23 21:59:41','2017-01-23 22:05:46',1),(4,5,3,'Pune University','2017-01-01 00:00:00','2017-01-31 00:00:00','2017-01-24 00:44:23','2017-01-24 00:45:13',0),(5,6,1,'123','2017-01-22 00:00:00',NULL,'2017-01-24 11:49:51','2017-01-24 11:49:51',0);
/*!40000 ALTER TABLE `universities` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `universitytype`
--

DROP TABLE IF EXISTS `universitytype`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `universitytype` (
  `universitytypeId` int(11) NOT NULL AUTO_INCREMENT COMMENT 'Address type id. Primary key. To be used as FK in University table.',
  `universitytypeName` varchar(45) NOT NULL COMMENT 'Type of the univesity. For now it will contain 1. Undergraduate, 2. Graduate, 3. PostDoctorate',
  `createDt` datetime NOT NULL,
  `updateDt` datetime NOT NULL,
  `isDeleted` tinyint(1) NOT NULL,
  PRIMARY KEY (`universitytypeId`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `universitytype`
--

LOCK TABLES `universitytype` WRITE;
/*!40000 ALTER TABLE `universitytype` DISABLE KEYS */;
INSERT INTO `universitytype` VALUES (1,'Undergraduation','2017-01-16 16:12:41','2017-01-16 16:12:41',0),(2,'Graduation','2017-01-16 16:12:41','2017-01-16 16:12:41',0),(3,'PostDoctorate','2017-01-16 16:12:41','2017-01-16 16:12:41',0);
/*!40000 ALTER TABLE `universitytype` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping routines for database 'demo_schema'
--
/*!50003 DROP PROCEDURE IF EXISTS `sp_DeleteAddress` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_DeleteAddress`(
	IN p_addressId INT,
    IN p_personId INT
)
BEGIN
	UPDATE employers 
    SET addressId = 0,
    updateDt = NOW()
    WHERE personId = p_personId && addressId = p_addressId;
    
    UPDATE universities
    SET addressId = 0,
    updateDt = NOW()
    WHERE personId = p_personId && addressId = p_addressId;
    
    UPDATE addresses 
    SET isDeleted = 1, updateDt = NOW()
    WHERE p_personId = p_personId && addressId = p_addressId;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_DeleteAddressType` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_DeleteAddressType`(
	IN p_addressType INT,
    OUT p_message VARCHAR(10)
)
BEGIN

	SELECT phoneTypesId INTO @typeid FROM addresstypes 
	WHERE addressTypeId <> p_addressType
	LIMIT 1;
    
    IF @typeid IS NOT NULL
    THEN
        
		UPDATE addresses
        SET addressType = @typeid
        WHERE addressType = p_addressType;
        
        UPDATE addresstypes 
        SET isDeleted = 1, updateDt = NOW()
        WHERE addressTypeId = p_addressType;
        
        SET p_message = 'SUCCESS';
	
    ELSE
		SET p_message = 'ERROR';
	END IF;

END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_DeleteEmail` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_DeleteEmail`(
	IN p_id INT,
    IN p_personId INT
)
BEGIN
	UPDATE emails 
    SET isDeleted = 1, updateDt = NOW()
    WHERE p_personId && id = p_id;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_DeleteEmailType` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_DeleteEmailType`(
	IN p_emailIdType INT,
    OUT p_message VARCHAR(10)
)
BEGIN

	SELECT emailTypeid INTO @typeid FROM emailtypes
        WHERE emailTypeid <> p_emailIdType
        LIMIT 1;
    
    IF @typeid IS NOT NULL
    THEN
		        
		UPDATE emails
        SET emailtypeid = @typeid, updateDt = NOW()
        WHERE emailtypeid = p_emailIdType;
        
        UPDATE emailtypes 
        SET isDeleted = 1, updateDt = NOW()
        WHERE emailTypeid = p_emailIdType;
        
        SET p_message = 'SUCCESS';
	
    ELSE
		SET p_message = 'ERROR';
	END IF;

END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_DeleteEmployer` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_DeleteEmployer`(
	IN p_employerId INT,
    IN p_personId INT    
)
BEGIN

    UPDATE employers
    SET isDeleted = 1, updateDt = NOW()
	WHERE personId = p_personId 
    && employersId = p_employerId;
    
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_DeletePerson` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_DeletePerson`(
	IN p_personId INT,
    OUT p_deletedCount INT
)
BEGIN
	UPDATE phonenos 
    SET isDeleted = 1, updateDt = NOW()
    WHERE personId = p_personId;
    
    UPDATE emails 
    SET isDeleted = 1, updateDt = NOW()
    WHERE personId = p_personId;
    
    UPDATE researches 
    SET isDeleted = 1, updateDt = NOW()
    WHERE personId = p_personId;
    
    UPDATE universities 
    SET isDeleted = 1, updateDt = NOW()
    WHERE personId = p_personId;
    
    UPDATE employers 
    SET isDeleted = 1, updateDt = NOW()
    WHERE personId = p_personId;
    
    UPDATE addresses 
    SET isDeleted = 1, updateDt = NOW()
    WHERE personId = p_personId;
    
    UPDATE person 
    SET isDeleted = 1, updateDt = NOW()
    WHERE personId = p_personId;
    
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_DeletePhoneno` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_DeletePhoneno`(
	IN p_phonenoId INT,
    IN p_personId INT
)
BEGIN

	UPDATE phonenos
    SET isDeleted = 1, updateDt = NOW()
    WHERE phonenoId = p_phonenoId
    && personId = p_personId;

END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_DeletePhoneType` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_DeletePhoneType`(
	IN p_phoneType INT,
    OUT p_message VARCHAR(10)
)
BEGIN

	SELECT phoneTypesId INTO @typeid FROM phonetypes 
	WHERE phoneTypesId <> p_phoneType
    LIMIT 1;
    
    IF @typeid IS NOT NULL
    THEN
        
		UPDATE phonenos
        SET phoneTypeId = @typeid
        WHERE phoneTypeId = p_phoneType;
        
        UPDATE phonetypes
        SET isDeleted = 1, updateDt = NOW()
        WHERE phoneTypesId = p_phoneType;
        
        SET p_message = 'SUCCESS';
	
    ELSE
		SET p_message = 'ERROR';
	END IF;

END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_DeleteResearch` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_DeleteResearch`(
	IN p_researchId INT,
    IN p_personId INT    
)
BEGIN

	UPDATE researches
    SET isDeleted = 1, updateDt = NOW()
	WHERE personId = p_personId 
    && researcheId = p_researchId;

END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_DeleteUniversity` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_DeleteUniversity`(
	IN p_universityId INT,
    IN p_personId INT    
)
BEGIN

    UPDATE employers
    SET isDeleted = 1, updateDt = NOW()
	WHERE personId = p_personId 
    && universityId = p_employerId;

END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_DeleteUniversityType` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_DeleteUniversityType`(
	IN p_universityType INT,
    OUT p_message VARCHAR(10)
)
BEGIN

	SELECT universitytypeId INTO @typeid FROM universitytype 
        WHERE universitytypeId <> p_universityType
        LIMIT 1;
    
    IF @typeid IS NOT NULL
    THEN
		
		UPDATE universities
        SET universityType = @typeid
        WHERE universitytypeId = p_universityType;
        
        UPDATE universitytype 
        SET isDeleted = 1, updateDt = NOW()
        WHERE universitytypeId = p_universityType;
        
        SET p_message = 'SUCCESS';
	
    ELSE
		SET p_message = 'ERROR';
	END IF;

END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_DynamicExecution` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_DynamicExecution`(
	IN InTableName VARCHAR(45),
    IN InColumnName VARCHAR(45),
    IN InColumnValue VARCHAR(45),
    IN OutTableName VARCHAR(45),
    IN OutColumnName1 VARCHAR(45),
    IN OutColumnName2 VARCHAR(45)
)
BEGIN
	SET @statement = CONCAT("SELECT ", @OUTCOLUMN1," , ", @OUTCOLUMN2, " FROM ", @OutTableName, 
						   " WHERE !isDeleted && personId in ( SELECT personId FROM ", @InTableName,
												" WHERE !isDeleted && ", @InColumnName, " like '%", @InColumnValue, "%');");
	
    /*SET @statement = CONCAT("SELECT ?, ? FROM ? WHERE !isDeleted && personId IN (SELECT personId FROM ? WHERE !isDeleted && ? LIKE '%?%')", 
							@OUTCOLUMN1, @OUTCOLUMN2, @OutTableName, @InTableName, @InColumnName, @InColumnValue);*/
						
                                                
	PREPARE executionStatement FROM @statement;
    EXECUTE executionStatement;
    DEALLOCATE PREPARE executionStatement;
    
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_GetAllPersons` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_GetAllPersons`()
BEGIN
	SELECT per.personid, per.firstName, per.lastName, per.sex FROM person as per;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_GetTableInformation` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_GetTableInformation`()
BEGIN
	SELECT TABLE_NAME AS tblName,COLUMN_NAME AS colName, DATA_TYPE AS colType FROM information_schema.COLUMNS AS col
	WHERE TABLE_NAME in (SELECT TABLE_NAME FROM information_schema.TABLES WHERE TABLE_SCHEMA = 'demo_schema')
	&& (col.COLUMN_NAME <> 'updateDt' && col.COLUMN_NAME <> 'createDt' && col.COLUMN_NAME <> 'isDeleted')
    && col.TABLE_NAME NOT LIKE '%type%' 
	GROUP BY col.TABLE_NAME, col.COLUMN_NAME, col.DATA_TYPE;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_InserNewPhoneType` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_InserNewPhoneType`(
	IN p_Type VARCHAR(45),
    OUT p_message VARCHAR(20)
)
BEGIN

	SET @typeId = NULL;
    
	SELECT phoneTypesId INTO @typeId FROM phonetypes
    WHERE LOWER(phoneTypesName) = LOWER(p_Type)
    && isDeleted = 0
    LIMIT 1;
    
    IF @typeId IS NULL
    THEN

		INSERT INTO phonetypes
		(
			phoneTypesName,
			createDt,
			updateDt,
            isDeleted
		)
		VALUES
		(
			p_Type,
			NOW(),
			NOW(),
            0
		);
    
		SET p_message = 'SUCCESS';
    ELSE
		SET p_message = 'ALREADYEXISTS';
	END IF;

END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_InsertNewAddress` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_InsertNewAddress`(
	IN p_personId INT,
    IN p_addressType INT,
    IN p_firstLine VARCHAR(45),
    IN p_lastLine VARCHAR(45),
    IN p_city VARCHAR(45),
    IN p_state VARCHAR(45),
    IN p_zip VARCHAR(45),
    OUT p_addressId INT
)
BEGIN
	INSERT INTO addresses
    (
		personId,
        addressType,
        firstLine,
        lastLine,
        city,
        state,
        zip,
        createDt,
        updateDt,
        isDeleted
    )
    VALUES
    (
		p_personId,
        p_addressType,
        p_firstLine,
        p_lastLine,
        p_city,
        p_state,
        p_zip,
        NOW(),
        NOW(),
        0
	);
    
    SELECT LAST_INSERT_ID() INTO p_addressId;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_InsertNewAddressType` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_InsertNewAddressType`(
	IN p_Type VARCHAR(45),
    OUT p_message VARCHAR(20)
)
BEGIN

	SET @typeId = NULL;

	SELECT addressTypeId INTO @typeId FROM addresstypes
    WHERE LOWER(addressTypeName) = LOWER(p_Type)
    && isDeleted = 0
    LIMIT 1;
    
    IF @typeId IS NULL
    THEN

		INSERT INTO addresstypes
		(
			addressTypeName,
			createDt,
			updateDt,
            isDeleted
		)
		VALUES
		(
			p_Type,
			NOW(),
			NOW(),
            0
		);
    
		SET p_message = 'SUCCESS';
    ELSE
		SET p_message = 'ALREADYEXISTS';
	END IF;

END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_InsertNewEmail` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_InsertNewEmail`(
	IN p_emailId VARCHAR(200),
    IN p_personId INT,
    IN p_emailtypeid INT,
    OUT p_Id INT
)
BEGIN

	INSERT INTO emails
    (
		emailId,
        personId,
        createDt,
        updateDt,
        emailtypeid,
		isDeleted
	)
    VALUES
    (
		p_emailId,
        p_personId,
        NOW(),
        NOW(),
        p_emailtypeid,
        0
	);
	
    SELECT LAST_INSERT_ID() INTO p_Id;

END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_InsertNewEmailType` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_InsertNewEmailType`(
	IN p_Type VARCHAR(45),
    OUT p_message VARCHAR(20)
)
BEGIN

	SET @typeId = NULL;
    
	SELECT emailTypeid INTO @typeId FROM emailtypes
    WHERE LOWER(emailTypeName) = LOWER(p_Type)
    && isDeleted = 0
    LIMIT 1;
    
    IF @typeId IS NULL
    THEN

		INSERT INTO emailtypes
		(
			emailTypeName,
			createDt,
			updateDt,
            isDeleted
		)
		VALUES
		(
			p_Type,
			NOW(),
			NOW(),
            0
		);
    
		SET p_message = 'SUCCESS';
    ELSE
		SET p_message = 'ALREADYEXISTS';
	END IF;

END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_InsertNewEmployer` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_InsertNewEmployer`(
	IN p_personId INT,
    IN p_name VARCHAR(45),
    IN p_fromDate DATETIME,
    IN p_toDate DATETIME,
    OUT p_employersId INT
)
BEGIN
	    	
    INSERT INTO employers
    (
		personId,
        employerName,
        fromDate,
        toDate,
        createDt,
        updateDt,
		isDeleted
	)
    VALUES
    (
		p_personId,
        p_name,
        p_fromDate,
        p_toDate,
        NOW(),
        NOW(),
        0
	);
	
    SELECT LAST_INSERT_ID() INTO p_employersId;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_InsertNewPerson` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_InsertNewPerson`(
	IN p_firstName VARCHAR(45),
    IN p_lastName VARCHAR(45),
    IN p_sex VARCHAR(1),
    OUT p_personId INT
)
BEGIN
	INSERT INTO person
    (
		firstName,
        lastName,
        sex,
        createDt,
        updateDt,
        isDeleted
    )
    VALUES
    (
		p_firstName,
        p_lastName,
        p_sex,
        NOW(),
        NOW(),
        0
    );
    
    SELECT LAST_INSERT_ID() INTO p_personId;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_InsertNewPhoneno` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_InsertNewPhoneno`(
	IN p_personId INT,
    IN p_phoneTypeId INT,
    IN p_phoneno VARCHAR(45),
    OUT p_phonenoId INT
)
BEGIN

	INSERT INTO phonenos
    (
		personId,
        phoneTypeId,
        phoneno,
        createDt,
        updateDt,
		isDeleted
	)
    VALUES
    (
		p_personId,
        p_phoneTypeId,
        p_phoneno,
        NOW(),
        NOW(),
        0
	);
    
    SELECT LAST_INSERT_ID() INTO p_phonenoId;

END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_InsertNewRecord` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_InsertNewRecord`(
	IN p_firstName VARCHAR(45),
    IN p_lastName VARCHAR(45),
    IN p_sex VARCHAR(1),
    IN p_firstLine VARCHAR(45),
    IN p_lastLine VARCHAR(45),
    IN p_city VARCHAR(45),
    IN p_state VARCHAR(45),
    IN p_zip VARCHAR(45),
    IN p_phoneTypeId INT,
    IN p_phoneno VARCHAR(45),
    OUT p_personId INT
)
BEGIN
	
    CALL sp_InsertNewPerson
    (
		p_firstName, 
        p_lastName, 
        p_sex, 
        p_personId
	);
    
    SELECT addressTypeId INTO @addressType FROM addresstypes
    WHERE LOWER(addressTypeName) = LOWER('HOME');
    
    CALL sp_InsertNewAddress
    (
		p_personId, 
        @addressType,
        p_firstLine,
        p_lastLine,
        p_city,
        p_state,
        p_zip,
		@homeAddressId
	);
    
    CALL sp_InsertNewPhoneno
    (
		p_personId,
		p_phoneTypeId,
        p_phoneno,
        @phonenoId
	);
    
    IF @homeAddressId IS NOT NULL 
    && @phonenoId IS NOT NULL
    THEN
		SELECT LAST_INSERT_ID() INTO p_personId;
	END IF;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_InsertNewResearch` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_InsertNewResearch`(
	IN p_personId INT,
    IN p_researchName VARCHAR(45),
    IN p_researchDescription VARCHAR(45),
    OUT p_researchId INT
)
BEGIN
	
    INSERT INTO researches
    (
		personId,
        researcheName,
        researchesDesc,
        createDt,
        updateDt,
		isDeleted
	)
    VALUES
    (
		p_personId,
        p_researchName,
        p_researchDescription,
        NOW(),
        NOW(),
        0
	);
    
    SELECT LAST_INSERT_ID() INTO p_researchId;
    
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_InsertNewUniversity` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_InsertNewUniversity`(
	IN p_personId INT,
    IN p_universityType INT,
    IN p_universityName VARCHAR(45),
    IN p_fromDate DATETIME,
    IN p_toDate DATETIME,
    OUT p_universityId INT
)
BEGIN
	
    INSERT INTO universities
    (
		personId,
        universityType,
        universityName,
        fromDate,
        toDate,
        createDt,
        updateDt,
		isDeleted
	)
    VALUES
    (
		p_personId,
        p_universityType, 
        p_universityName,
        p_fromDate,
        p_toDate,
        NOW(),
        NOW(),
        0
	);
	
    SELECT LAST_INSERT_ID() INTO p_universityId;

END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_InsertNewUniversityType` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_InsertNewUniversityType`(
	IN p_Type VARCHAR(45),
    OUT p_message VARCHAR(20)
)
BEGIN

	SET @typeId = NULL;

	SELECT universitytypeId INTO @typeId FROM universitytype
    WHERE LOWER(universitytypeName) = LOWER(p_Type)
    && isDeleted = 0
    LIMIT 1;
    
    IF @typeId IS NULL
    THEN

		INSERT INTO universitytype
		(
			universitytypeName,
			createDt,
			updateDt,
            isDeleted
		)
		VALUES
		(
			p_Type,
			NOW(),
			NOW(),
            0
		);
    
		SET p_message = 'SUCCESS';
    ELSE
		SET p_message = 'ALREADYEXISTS';
	END IF;

END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_UpdateAddress` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_UpdateAddress`(
	IN p_personId INT,
    IN p_addressType INT,
    IN p_firstLine VARCHAR(45),
    IN p_lastLine VARCHAR(45),
    IN p_city VARCHAR(45),
    IN p_state VARCHAR(45),
    IN p_zip VARCHAR(45),
    IN p_addressId INT
)
BEGIN
	UPDATE addresses
	SET
        addressType = p_addressType,
        firstLine = p_firstLine,
        lastLine = p_lastLine,
        city = p_city,
        state = p_state,
        zip = p_zip,
        updateDt = NOW()
	WHERE
		addresseId = P_addressId
        && personId = p_personId;
    
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_UpdateAddressType` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_UpdateAddressType`(
	IN p_typeId INT,
    IN p_typeName VARCHAR(45)
)
BEGIN

	UPDATE addresstypes
    SET addressTypeName = p_typeName, updateDt = NOW()
    WHERE addressTypeId = p_typeId;

END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_UpdateEmail` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_UpdateEmail`(
    IN p_personId INT,
    IN p_emailtypeid INT,
    IN p_emailId VARCHAR(200),
    IN p_Id INT    
)
BEGIN
	
    UPDATE emails
    SET emailId = p_emailId, emailtypeid = p_emailtypeid
    WHERE personId = p_personId && id = p_Id;
    
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_UpdateEmailType` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_UpdateEmailType`(
	IN p_typeId INT,
    IN p_typeName VARCHAR(45)
)
BEGIN

	UPDATE emailtypes
    SET emailTypeName = p_typeName, updateDt = NOW()
    WHERE emailTypeid = p_typeId;

END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_UpdateEmployer` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_UpdateEmployer`(
	IN p_personId INT,
    IN p_name VARCHAR(45),
    IN p_fromDate DATETIME,
    IN p_toDate DATETIME,
    IN p_employersId INT
)
BEGIN
    
    UPDATE employers
    SET employerName = p_name, fromDate = p_fromDate, toDate = p_toDate, updateDt = NOW()
    WHERE personId = p_personId
    && employersId = p_employersId;
    
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_UpdatePerson` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_UpdatePerson`(
	IN p_firstName VARCHAR(45),
    IN p_lastName VARCHAR(45),
    IN p_sex VARCHAR(1),
    IN p_personId INT
)
BEGIN
	UPDATE person
    SET firstName = p_firstName, lastName = p_lastName, sex = p_sex, updateDt = NOW()
    WHERE personId = p_personId;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_UpdatePhoneno` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_UpdatePhoneno`(
	IN p_personId INT,
    IN p_phoneTypeId INT,
    IN p_phoneno VARCHAR(45),
    IN p_phonenoId INT
)
BEGIN

	UPDATE phonenos
    SET phoneTypeId = p_phoneTypeId, phoneno = p_phoneno, updateDt = NOW()
    WHERE personId = p_personId 
    && phonenoId = p_phonenoId;

END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_UpdatePhoneType` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_UpdatePhoneType`(
	IN p_typeId INT,
    IN p_typeName VARCHAR(45)
)
BEGIN

	UPDATE phonetypes
    SET phoneTypesName = p_typeName, updateDt = NOW()
    WHERE phoneTypesId = p_typeId;

END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_UpdateResearch` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_UpdateResearch`(
	IN p_personId INT,
    IN p_researchName VARCHAR(45),
    IN p_researchDescription VARCHAR(45),
    IN p_researchId INT
)
BEGIN

	UPDATE researches
    SET researcheName = p_researchName, researchesDesc = p_researchDescription, updateDt = NOW()
    WHERE personId = p_personId
    && researcheId = p_researchId;

END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_UpdateUniversity` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_UpdateUniversity`(
	IN p_personId INT,
    IN p_universityType VARCHAR(45),
    IN p_universityName VARCHAR(45),
    IN p_fromDate DATETIME,
    IN p_toDate DATETIME,
    IN p_universityId INT
)
BEGIN
    
    UPDATE universities
    SET universityType = p_universityType, universityName = p_universityName, fromDate = p_fromDate, toDate = p_toDate, updateDt = NOW()
    WHERE personId = p_personId
    && universityId = p_universityId;

END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sp_UpdateUniversityType` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_UpdateUniversityType`(
	IN p_typeId INT,
    IN p_typeName VARCHAR(45)
)
BEGIN

	UPDATE universitytype
    SET universitytypeName = p_typeName, updateDt = NOW()
    WHERE universitytypeId = p_typeId;

END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2017-01-25 19:00:44
