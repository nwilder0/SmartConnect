-- phpMyAdmin SQL Dump
-- version 3.4.11.1deb2+deb7u1
-- http://www.phpmyadmin.net
--
-- Host: localhost
-- Generation Time: Mar 01, 2015 at 06:39 AM
-- Server version: 5.5.41
-- PHP Version: 5.4.36-0+deb7u3

SET SQL_MODE="NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;

--
-- Database: `smart_connect`
--

-- --------------------------------------------------------

--
-- Table structure for table `error`
--

CREATE TABLE IF NOT EXISTS `error` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `message` text CHARACTER SET latin1 COLLATE latin1_general_ci NOT NULL,
  `mac` varchar(50) CHARACTER SET latin1 COLLATE latin1_general_ci NOT NULL,
  `ip` varchar(50) CHARACTER SET latin1 COLLATE latin1_general_ci NOT NULL,
  `os` varchar(255) CHARACTER SET latin1 COLLATE latin1_general_ci NOT NULL,
  `connected_ssid` varchar(50) CHARACTER SET latin1 COLLATE latin1_general_ci NOT NULL,
  `connected_ap` varchar(50) CHARACTER SET latin1 COLLATE latin1_general_ci NOT NULL,
  `connected_time` time NOT NULL,
  `logged` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=79 ;

--
-- Dumping data for table `error`
--

INSERT INTO `error` (`id`, `message`, `mac`, `ip`, `os`, `connected_ssid`, `connected_ap`, `connected_time`, `logged`) VALUES
(1, '3/1/2015 05:08:07 AM: Error converting value 10 to type SmartConnect.AP. Path updateInterval, line 2, position 25.', 'E006E6C588BA', 'fe80::c065:cef9:192f:c9c9%12', 'Microsoft Windows NT 6.1.7601 Service Pack 1', 'System.Byte[]', '002191FECE01', '00:00:00', '2015-02-28 23:03:03'),
(2, '3/1/2015 05:08:08 AM: Error converting value WPA2 to type SmartConnect.AP. Path defaultAuthentication, line 2, position 34.', 'E006E6C588BA', 'fe80::c065:cef9:192f:c9c9%12', 'Microsoft Windows NT 6.1.7601 Service Pack 1', 'System.Byte[]', '002191FECE01', '00:00:00', '2015-02-28 23:03:03'),
(3, '3/1/2015 05:08:08 AM: Error converting value WPA2 to type SmartConnect.AP. Path defaultAuthentication, line 2, position 34.', 'E006E6C588BA', 'fe80::c065:cef9:192f:c9c9%12', 'Microsoft Windows NT 6.1.7601 Service Pack 1', 'System.Byte[]', '002191FECE01', '00:00:00', '2015-02-28 23:03:03'),
(4, '3/1/2015 05:08:08 AM: Error converting value 10 to type SmartConnect.SSID. Path updateInterval, line 2, position 25.', 'E006E6C588BA', 'fe80::c065:cef9:192f:c9c9%12', 'Microsoft Windows NT 6.1.7601 Service Pack 1', 'System.Byte[]', '002191FECE01', '00:00:00', '2015-02-28 23:03:03'),
(5, '3/1/2015 05:08:08 AM: Error converting value WPA2 to type SmartConnect.AP. Path defaultAuthentication, line 2, position 34.', 'E006E6C588BA', 'fe80::c065:cef9:192f:c9c9%12', 'Microsoft Windows NT 6.1.7601 Service Pack 1', 'System.Byte[]', '002191FECE01', '00:00:00', '2015-02-28 23:03:03'),
(6, '3/1/2015 05:08:08 AM: Error converting value 10 to type SmartConnect.SSID. Path updateInterval, line 2, position 25.', 'E006E6C588BA', 'fe80::c065:cef9:192f:c9c9%12', 'Microsoft Windows NT 6.1.7601 Service Pack 1', 'System.Byte[]', '002191FECE01', '00:00:00', '2015-02-28 23:03:03'),
(7, '3/1/2015 05:08:08 AM: Input string was not in a correct format.', 'E006E6C588BA', 'fe80::c065:cef9:192f:c9c9%12', 'Microsoft Windows NT 6.1.7601 Service Pack 1', 'System.Byte[]', '002191FECE01', '00:00:00', '2015-02-28 23:03:03'),
(8, '3/1/2015 05:08:18 AM: Input string was not in a correct format.', 'E006E6C588BA', 'fe80::c065:cef9:192f:c9c9%12', 'Microsoft Windows NT 6.1.7601 Service Pack 1', 'System.Byte[]', '002191FECE01', '00:00:00', '2015-02-28 23:03:14'),
(9, '3/1/2015 05:08:29 AM: Input string was not in a correct format.', 'E006E6C588BA', 'fe80::c065:cef9:192f:c9c9%12', 'Microsoft Windows NT 6.1.7601 Service Pack 1', 'System.Byte[]', '002191FECE01', '00:00:00', '2015-02-28 23:03:24'),
(10, '3/1/2015 05:08:39 AM: Input string was not in a correct format.', 'E006E6C588BA', 'fe80::c065:cef9:192f:c9c9%12', 'Microsoft Windows NT 6.1.7601 Service Pack 1', 'System.Byte[]', '002191FECE01', '00:00:00', '2015-02-28 23:03:34'),
(11, '3/1/2015 05:17:24 AM: Error converting value WPA2 to type SmartConnect.AP. Path defaultAuthentication, line 2, position 34.', 'E006E6C588BA', 'fe80::c065:cef9:192f:c9c9%12', 'Microsoft Windows NT 6.1.7601 Service Pack 1', 'System.Byte[]', '002191FECE01', '00:00:00', '2015-02-28 23:12:20'),
(12, '3/1/2015 05:17:24 AM: SSID Update: Unable to reach server for updates - Unexpected character encountered while parsing value: <. Path , line 0, position 0.', 'E006E6C588BA', 'fe80::c065:cef9:192f:c9c9%12', 'Microsoft Windows NT 6.1.7601 Service Pack 1', 'System.Byte[]', '002191FECE01', '00:00:00', '2015-02-28 23:12:20'),
(13, '3/1/2015 05:17:24 AM: SSID Update: Unable to reach server for updates - Unexpected character encountered while parsing value: <. Path , line 0, position 0.', 'E006E6C588BA', 'fe80::c065:cef9:192f:c9c9%12', 'Microsoft Windows NT 6.1.7601 Service Pack 1', 'System.Byte[]', '002191FECE01', '00:00:00', '2015-02-28 23:12:20'),
(14, '3/1/2015 05:17:24 AM: Unexpected character encountered while parsing value: <. Path , line 0, position 0.', 'E006E6C588BA', 'fe80::c065:cef9:192f:c9c9%12', 'Microsoft Windows NT 6.1.7601 Service Pack 1', 'System.Byte[]', '002191FECE01', '00:00:00', '2015-02-28 23:12:20'),
(15, '3/1/2015 05:17:24 AM: SSID Update: Unable to reach server for updates - Unexpected character encountered while parsing value: <. Path , line 0, position 0.', 'E006E6C588BA', 'fe80::c065:cef9:192f:c9c9%12', 'Microsoft Windows NT 6.1.7601 Service Pack 1', 'System.Byte[]', '002191FECE01', '00:00:00', '2015-02-28 23:12:20'),
(16, '3/1/2015 05:17:24 AM: Unexpected character encountered while parsing value: <. Path , line 0, position 0.', 'E006E6C588BA', 'fe80::c065:cef9:192f:c9c9%12', 'Microsoft Windows NT 6.1.7601 Service Pack 1', 'System.Byte[]', '002191FECE01', '00:00:00', '2015-02-28 23:12:20'),
(17, '3/1/2015 05:17:25 AM: AP Update: non-connection error - Unexpected character encountered while parsing value: <. Path , line 0, position 0.', 'E006E6C588BA', 'fe80::c065:cef9:192f:c9c9%12', 'Microsoft Windows NT 6.1.7601 Service Pack 1', 'System.Byte[]', '002191FECE01', '00:00:00', '2015-02-28 23:12:20'),
(18, '3/1/2015 05:17:24 AM: SSID Update: Unable to reach server for updates - Unexpected character encountered while parsing value: <. Path , line 0, position 0.', 'E006E6C588BA', 'fe80::c065:cef9:192f:c9c9%12', 'Microsoft Windows NT 6.1.7601 Service Pack 1', 'System.Byte[]', '002191FECE01', '00:00:00', '2015-02-28 23:12:20'),
(19, '3/1/2015 05:17:24 AM: Unexpected character encountered while parsing value: <. Path , line 0, position 0.', 'E006E6C588BA', 'fe80::c065:cef9:192f:c9c9%12', 'Microsoft Windows NT 6.1.7601 Service Pack 1', 'System.Byte[]', '002191FECE01', '00:00:00', '2015-02-28 23:12:20'),
(20, '3/1/2015 05:17:25 AM: AP Update: non-connection error - Unexpected character encountered while parsing value: <. Path , line 0, position 0.', 'E006E6C588BA', 'fe80::c065:cef9:192f:c9c9%12', 'Microsoft Windows NT 6.1.7601 Service Pack 1', 'System.Byte[]', '002191FECE01', '00:00:00', '2015-02-28 23:12:20'),
(21, '3/1/2015 05:17:25 AM: Link Update: non-connection error - Unexpected character encountered while parsing value: <. Path , line 0, position 0.', 'E006E6C588BA', 'fe80::c065:cef9:192f:c9c9%12', 'Microsoft Windows NT 6.1.7601 Service Pack 1', 'System.Byte[]', '002191FECE01', '00:00:00', '2015-02-28 23:12:20'),
(22, '3/1/2015 05:17:25 AM: Input string was not in a correct format.', 'E006E6C588BA', 'fe80::c065:cef9:192f:c9c9%12', 'Microsoft Windows NT 6.1.7601 Service Pack 1', 'System.Byte[]', '002191FECE01', '00:00:00', '2015-02-28 23:12:20'),
(23, '3/1/2015 05:17:35 AM: Input string was not in a correct format.', 'E006E6C588BA', 'fe80::c065:cef9:192f:c9c9%12', 'Microsoft Windows NT 6.1.7601 Service Pack 1', 'System.Byte[]', '002191FECE01', '00:00:00', '2015-02-28 23:12:31'),
(24, '3/1/2015 05:17:46 AM: Input string was not in a correct format.', 'E006E6C588BA', 'fe80::c065:cef9:192f:c9c9%12', 'Microsoft Windows NT 6.1.7601 Service Pack 1', 'System.Byte[]', '002191FECE01', '00:00:00', '2015-02-28 23:12:41'),
(25, '3/1/2015 05:17:56 AM: Input string was not in a correct format.', 'E006E6C588BA', 'fe80::c065:cef9:192f:c9c9%12', 'Microsoft Windows NT 6.1.7601 Service Pack 1', 'System.Byte[]', '002191FECE01', '00:00:00', '2015-02-28 23:12:51'),
(26, '3/1/2015 05:18:06 AM: Input string was not in a correct format.', 'E006E6C588BA', 'fe80::c065:cef9:192f:c9c9%12', 'Microsoft Windows NT 6.1.7601 Service Pack 1', 'System.Byte[]', '002191FECE01', '00:00:00', '2015-02-28 23:13:02'),
(27, '3/1/2015 05:18:17 AM: Input string was not in a correct format.', 'E006E6C588BA', 'fe80::c065:cef9:192f:c9c9%12', 'Microsoft Windows NT 6.1.7601 Service Pack 1', 'System.Byte[]', '002191FECE01', '00:00:00', '2015-02-28 23:13:12'),
(28, '3/1/2015 05:18:22 AM: Unexpected character encountered while parsing value: <. Path , line 0, position 0.', 'E006E6C588BA', 'fe80::c065:cef9:192f:c9c9%12', 'Microsoft Windows NT 6.1.7601 Service Pack 1', 'System.Byte[]', '002191FECE01', '00:00:00', '2015-02-28 23:13:18'),
(29, '3/1/2015 05:18:22 AM: SSID Update: Unable to reach server for updates - Unexpected character encountered while parsing value: <. Path , line 0, position 0.', 'E006E6C588BA', 'fe80::c065:cef9:192f:c9c9%12', 'Microsoft Windows NT 6.1.7601 Service Pack 1', 'System.Byte[]', '002191FECE01', '00:00:00', '2015-02-28 23:13:18'),
(30, '3/1/2015 05:18:23 AM: Link Update: non-connection error - Unexpected character encountered while parsing value: <. Path , line 0, position 0.', 'E006E6C588BA', 'fe80::c065:cef9:192f:c9c9%12', 'Microsoft Windows NT 6.1.7601 Service Pack 1', 'System.Byte[]', '002191FECE01', '00:00:00', '2015-02-28 23:13:18'),
(31, '3/1/2015 05:18:23 AM: AP Update: non-connection error - Unexpected character encountered while parsing value: <. Path , line 0, position 0.', 'E006E6C588BA', 'fe80::c065:cef9:192f:c9c9%12', 'Microsoft Windows NT 6.1.7601 Service Pack 1', 'System.Byte[]', '002191FECE01', '00:00:00', '2015-02-28 23:13:18'),
(32, '3/1/2015 05:18:27 AM: Input string was not in a correct format.', 'E006E6C588BA', 'fe80::c065:cef9:192f:c9c9%12', 'Microsoft Windows NT 6.1.7601 Service Pack 1', 'System.Byte[]', '002191FECE01', '00:00:00', '2015-02-28 23:13:22'),
(33, '3/1/2015 05:18:37 AM: Input string was not in a correct format.', 'E006E6C588BA', 'fe80::c065:cef9:192f:c9c9%12', 'Microsoft Windows NT 6.1.7601 Service Pack 1', 'System.Byte[]', '002191FECE01', '00:00:00', '2015-02-28 23:13:32'),
(34, '3/1/2015 05:21:44 AM: Error converting value WPA2 to type SmartConnect.AP. Path defaultAuthentication, line 2, position 34.', 'E006E6C588BA', 'fe80::c065:cef9:192f:c9c9%12', 'Microsoft Windows NT 6.1.7601 Service Pack 1', 'System.Byte[]', '002191FECE01', '00:00:00', '2015-02-28 23:16:40'),
(35, '3/1/2015 05:21:44 AM: Error converting value 10 to type SmartConnect.SSID. Path updateInterval, line 2, position 25.', 'E006E6C588BA', 'fe80::c065:cef9:192f:c9c9%12', 'Microsoft Windows NT 6.1.7601 Service Pack 1', 'System.Byte[]', '002191FECE01', '00:00:00', '2015-02-28 23:16:40'),
(36, '3/1/2015 05:21:44 AM: Error converting value 10 to type SmartConnect.SSID. Path updateInterval, line 2, position 25.', 'E006E6C588BA', 'fe80::c065:cef9:192f:c9c9%12', 'Microsoft Windows NT 6.1.7601 Service Pack 1', 'System.Byte[]', '002191FECE01', '00:00:00', '2015-02-28 23:16:40'),
(37, '3/1/2015 05:21:44 AM: Error converting value 10 to type SmartConnect.AP. Path updateInterval, line 2, position 25.', 'E006E6C588BA', 'fe80::c065:cef9:192f:c9c9%12', 'Microsoft Windows NT 6.1.7601 Service Pack 1', 'System.Byte[]', '002191FECE01', '00:00:00', '2015-02-28 23:16:40'),
(38, '3/1/2015 05:21:44 AM: Error converting value 10 to type SmartConnect.SSID. Path updateInterval, line 2, position 25.', 'E006E6C588BA', 'fe80::c065:cef9:192f:c9c9%12', 'Microsoft Windows NT 6.1.7601 Service Pack 1', 'System.Byte[]', '002191FECE01', '00:00:00', '2015-02-28 23:16:40'),
(39, '3/1/2015 05:21:44 AM: Error converting value 10 to type SmartConnect.AP. Path updateInterval, line 2, position 25.', 'E006E6C588BA', 'fe80::c065:cef9:192f:c9c9%12', 'Microsoft Windows NT 6.1.7601 Service Pack 1', 'System.Byte[]', '002191FECE01', '00:00:00', '2015-02-28 23:16:40'),
(40, '3/1/2015 05:21:45 AM: Input string was not in a correct format.', 'E006E6C588BA', 'fe80::c065:cef9:192f:c9c9%12', 'Microsoft Windows NT 6.1.7601 Service Pack 1', 'System.Byte[]', '002191FECE01', '00:00:00', '2015-02-28 23:16:40'),
(41, '3/1/2015 05:21:55 AM: Input string was not in a correct format.', 'E006E6C588BA', 'fe80::c065:cef9:192f:c9c9%12', 'Microsoft Windows NT 6.1.7601 Service Pack 1', 'System.Byte[]', '002191FECE01', '00:00:00', '2015-02-28 23:16:50'),
(42, '3/1/2015 05:22:05 AM: Input string was not in a correct format.', 'E006E6C588BA', 'fe80::c065:cef9:192f:c9c9%12', 'Microsoft Windows NT 6.1.7601 Service Pack 1', 'System.Byte[]', '002191FECE01', '00:00:00', '2015-02-28 23:17:01'),
(43, '3/1/2015 05:22:16 AM: Input string was not in a correct format.', 'E006E6C588BA', 'fe80::c065:cef9:192f:c9c9%12', 'Microsoft Windows NT 6.1.7601 Service Pack 1', 'System.Byte[]', '002191FECE01', '00:00:00', '2015-02-28 23:17:11'),
(44, '3/1/2015 05:22:26 AM: Input string was not in a correct format.', 'E006E6C588BA', 'fe80::c065:cef9:192f:c9c9%12', 'Microsoft Windows NT 6.1.7601 Service Pack 1', 'System.Byte[]', '002191FECE01', '00:00:00', '2015-02-28 23:17:21'),
(45, '3/1/2015 05:24:49 AM: Error converting value 10 to type SmartConnect.AP. Path updateInterval, line 2, position 25.', 'E006E6C588BA', 'fe80::c065:cef9:192f:c9c9%12', 'Microsoft Windows NT 6.1.7601 Service Pack 1', 'System.Byte[]', '002191FECE01', '00:00:00', '2015-02-28 23:19:44'),
(46, '3/1/2015 05:24:49 AM: SSID Update: Unable to reach server for updates - Error getting value from Profile on SmartConnect.SSID.', 'E006E6C588BA', 'fe80::c065:cef9:192f:c9c9%12', 'Microsoft Windows NT 6.1.7601 Service Pack 1', 'System.Byte[]', '002191FECE01', '00:00:00', '2015-02-28 23:19:44'),
(47, '3/1/2015 05:24:49 AM: SSID Update: Unable to reach server for updates - Error getting value from Profile on SmartConnect.SSID.', 'E006E6C588BA', 'fe80::c065:cef9:192f:c9c9%12', 'Microsoft Windows NT 6.1.7601 Service Pack 1', 'System.Byte[]', '002191FECE01', '00:00:00', '2015-02-28 23:19:44'),
(48, '3/1/2015 05:24:49 AM: Input string was not in a correct format.', 'E006E6C588BA', 'fe80::c065:cef9:192f:c9c9%12', 'Microsoft Windows NT 6.1.7601 Service Pack 1', 'System.Byte[]', '002191FECE01', '00:00:00', '2015-02-28 23:19:44'),
(49, '3/1/2015 05:25:00 AM: Input string was not in a correct format.', 'E006E6C588BA', 'fe80::c065:cef9:192f:c9c9%12', 'Microsoft Windows NT 6.1.7601 Service Pack 1', 'System.Byte[]', '002191FECE01', '00:00:00', '2015-02-28 23:19:55'),
(50, '3/1/2015 05:25:10 AM: Input string was not in a correct format.', 'E006E6C588BA', 'fe80::c065:cef9:192f:c9c9%12', 'Microsoft Windows NT 6.1.7601 Service Pack 1', 'System.Byte[]', '002191FECE01', '00:00:00', '2015-02-28 23:20:05'),
(51, '3/1/2015 05:25:20 AM: Input string was not in a correct format.', 'E006E6C588BA', 'fe80::c065:cef9:192f:c9c9%12', 'Microsoft Windows NT 6.1.7601 Service Pack 1', 'System.Byte[]', '002191FECE01', '00:00:00', '2015-02-28 23:20:15'),
(52, '3/1/2015 05:25:30 AM: Input string was not in a correct format.', 'E006E6C588BA', 'fe80::c065:cef9:192f:c9c9%12', 'Microsoft Windows NT 6.1.7601 Service Pack 1', 'System.Byte[]', '002191FECE01', '00:00:00', '2015-02-28 23:20:26'),
(53, '3/1/2015 05:25:52 AM: Error converting value 10 to type SmartConnect.SSID. Path updateInterval, line 2, position 25.', 'E006E6C588BA', 'fe80::c065:cef9:192f:c9c9%12', 'Microsoft Windows NT 6.1.7601 Service Pack 1', 'System.Byte[]', '002191FECE01', '00:00:00', '2015-02-28 23:20:54'),
(54, '3/1/2015 05:31:48 AM: Error converting value 10 to type SmartConnect.SSID. Path updateInterval, line 2, position 25.', 'E006E6C588BA', 'fe80::c065:cef9:192f:c9c9%12', 'Microsoft Windows NT 6.1.7601 Service Pack 1', 'System.Byte[]', '002191FECE01', '00:00:00', '2015-02-28 23:26:48'),
(55, '3/1/2015 05:31:53 AM: Input string was not in a correct format.', 'E006E6C588BA', 'fe80::c065:cef9:192f:c9c9%12', 'Microsoft Windows NT 6.1.7601 Service Pack 1', 'System.Byte[]', '002191FECE01', '00:00:00', '2015-02-28 23:26:49'),
(56, '3/1/2015 05:32:04 AM: Input string was not in a correct format.', 'E006E6C588BA', 'fe80::c065:cef9:192f:c9c9%12', 'Microsoft Windows NT 6.1.7601 Service Pack 1', 'System.Byte[]', '002191FECE01', '00:00:00', '2015-02-28 23:26:59'),
(57, '3/1/2015 05:32:14 AM: Input string was not in a correct format.', 'E006E6C588BA', 'fe80::c065:cef9:192f:c9c9%12', 'Microsoft Windows NT 6.1.7601 Service Pack 1', 'System.Byte[]', '002191FECE01', '00:00:00', '2015-02-28 23:27:09'),
(58, '3/1/2015 05:32:24 AM: Input string was not in a correct format.', 'E006E6C588BA', 'fe80::c065:cef9:192f:c9c9%12', 'Microsoft Windows NT 6.1.7601 Service Pack 1', 'System.Byte[]', '002191FECE01', '00:00:00', '2015-02-28 23:27:19'),
(59, '3/1/2015 05:32:35 AM: Input string was not in a correct format.', 'E006E6C588BA', 'fe80::c065:cef9:192f:c9c9%12', 'Microsoft Windows NT 6.1.7601 Service Pack 1', 'System.Byte[]', '002191FECE01', '00:00:00', '2015-02-28 23:27:30'),
(60, '3/1/2015 05:32:45 AM: Input string was not in a correct format.', 'E006E6C588BA', 'fe80::c065:cef9:192f:c9c9%12', 'Microsoft Windows NT 6.1.7601 Service Pack 1', 'System.Byte[]', '002191FECE01', '00:00:00', '2015-02-28 23:27:40'),
(61, '3/1/2015 05:32:55 AM: Input string was not in a correct format.', 'E006E6C588BA', 'fe80::c065:cef9:192f:c9c9%12', 'Microsoft Windows NT 6.1.7601 Service Pack 1', 'System.Byte[]', '002191FECE01', '00:00:00', '2015-02-28 23:27:50'),
(62, '3/1/2015 05:33:05 AM: Input string was not in a correct format.', 'E006E6C588BA', 'fe80::c065:cef9:192f:c9c9%12', 'Microsoft Windows NT 6.1.7601 Service Pack 1', 'System.Byte[]', '002191FECE01', '00:00:00', '2015-02-28 23:28:00'),
(63, '3/1/2015 05:33:16 AM: Input string was not in a correct format.', 'E006E6C588BA', 'fe80::c065:cef9:192f:c9c9%12', 'Microsoft Windows NT 6.1.7601 Service Pack 1', 'System.Byte[]', '002191FECE01', '00:00:00', '2015-02-28 23:28:11'),
(64, '3/1/2015 05:33:26 AM: Input string was not in a correct format.', 'E006E6C588BA', 'fe80::c065:cef9:192f:c9c9%12', 'Microsoft Windows NT 6.1.7601 Service Pack 1', 'System.Byte[]', '002191FECE01', '00:00:00', '2015-02-28 23:28:21'),
(65, '3/1/2015 05:33:36 AM: Input string was not in a correct format.', 'E006E6C588BA', 'fe80::c065:cef9:192f:c9c9%12', 'Microsoft Windows NT 6.1.7601 Service Pack 1', 'System.Byte[]', '002191FECE01', '00:00:00', '2015-02-28 23:28:31'),
(66, '3/1/2015 05:33:46 AM: Input string was not in a correct format.', 'E006E6C588BA', 'fe80::c065:cef9:192f:c9c9%12', 'Microsoft Windows NT 6.1.7601 Service Pack 1', 'System.Byte[]', '002191FECE01', '00:00:00', '2015-02-28 23:28:42'),
(67, '3/1/2015 05:33:57 AM: Input string was not in a correct format.', 'E006E6C588BA', 'fe80::c065:cef9:192f:c9c9%12', 'Microsoft Windows NT 6.1.7601 Service Pack 1', 'System.Byte[]', '002191FECE01', '00:00:00', '2015-02-28 23:28:52'),
(68, '3/1/2015 05:34:07 AM: Input string was not in a correct format.', 'E006E6C588BA', 'fe80::c065:cef9:192f:c9c9%12', 'Microsoft Windows NT 6.1.7601 Service Pack 1', 'System.Byte[]', '002191FECE01', '00:00:00', '2015-02-28 23:29:02'),
(69, '3/1/2015 06:16:03 AM: Input string was not in a correct format.', 'E006E6C588BA', 'fe80::c065:cef9:192f:c9c9%12', 'Microsoft Windows NT 6.1.7601 Service Pack 1', 'System.Byte[]', '002191FECE01', '00:00:00', '2015-03-01 00:10:58'),
(70, '3/1/2015 06:19:02 AM: LinkSSIDs: KeyNotFound - The given key was not present in the dictionary.', 'E006E6C588BA', 'fe80::c065:cef9:192f:c9c9%12', 'Microsoft Windows NT 6.1.7601 Service Pack 1', 'System.Byte[]', '002191FECE01', '13:15:54', '2015-03-01 00:14:02'),
(71, '3/1/2015 06:19:05 AM: LinkSSIDs: KeyNotFound - The given key was not present in the dictionary.', 'E006E6C588BA', 'fe80::c065:cef9:192f:c9c9%12', 'Microsoft Windows NT 6.1.7601 Service Pack 1', 'System.Byte[]', '002191FECE01', '13:15:54', '2015-03-01 00:14:02'),
(72, '3/1/2015 06:19:05 AM: LinkSSIDs: KeyNotFound - The given key was not present in the dictionary.', 'E006E6C588BA', 'fe80::c065:cef9:192f:c9c9%12', 'Microsoft Windows NT 6.1.7601 Service Pack 1', 'System.Byte[]', '002191FECE01', '13:15:54', '2015-03-01 00:14:02'),
(73, '3/1/2015 06:19:07 AM: Input string was not in a correct format.', 'E006E6C588BA', 'fe80::c065:cef9:192f:c9c9%12', 'Microsoft Windows NT 6.1.7601 Service Pack 1', 'System.Byte[]', '002191FECE01', '13:15:54', '2015-03-01 00:14:02'),
(74, '3/1/2015 06:20:53 AM: Input string was not in a correct format.', 'E006E6C588BA', 'fe80::c065:cef9:192f:c9c9%12', 'Microsoft Windows NT 6.1.7601 Service Pack 1', 'System.Byte[]', '002191FECE01', '00:00:00', '2015-03-01 00:15:48'),
(75, '3/1/2015 06:35:11 AM: LinkSSIDs: KeyNotFound - The given key was not present in the dictionary.', 'E006E6C588BA', 'fe80::c065:cef9:192f:c9c9%12', 'Microsoft Windows NT 6.1.7601 Service Pack 1', 'System.Byte[]', '002191FECE01', '13:25:19', '2015-03-01 00:30:07'),
(76, '3/1/2015 06:35:12 AM: LinkSSIDs: KeyNotFound - The given key was not present in the dictionary.', 'E006E6C588BA', 'fe80::c065:cef9:192f:c9c9%12', 'Microsoft Windows NT 6.1.7601 Service Pack 1', 'System.Byte[]', '002191FECE01', '13:25:19', '2015-03-01 00:30:07'),
(77, '3/1/2015 06:36:47 AM: LinkSSIDs: KeyNotFound - The given key was not present in the dictionary.', 'E006E6C588BA', 'fe80::c065:cef9:192f:c9c9%12', 'Microsoft Windows NT 6.1.7601 Service Pack 1', 'System.Byte[]', '002191FECE01', '13:26:16', '2015-03-01 00:31:44'),
(78, '3/1/2015 06:36:49 AM: LinkSSIDs: KeyNotFound - The given key was not present in the dictionary.', 'E006E6C588BA', 'fe80::c065:cef9:192f:c9c9%12', 'Microsoft Windows NT 6.1.7601 Service Pack 1', 'System.Byte[]', '002191FECE01', '13:26:16', '2015-03-01 00:31:44');

-- --------------------------------------------------------

--
-- Table structure for table `net_data`
--

CREATE TABLE IF NOT EXISTS `net_data` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `session` bigint(20) NOT NULL,
  `ap` varchar(50) CHARACTER SET latin1 COLLATE latin1_general_ci NOT NULL,
  `signal_strength` varchar(50) CHARACTER SET latin1 COLLATE latin1_general_ci NOT NULL,
  PRIMARY KEY (`id`),
  KEY `session` (`session`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=35 ;

--
-- Dumping data for table `net_data`
--

INSERT INTO `net_data` (`id`, `session`, `ap`, `signal_strength`) VALUES
(3, 8, 'C8:3A:35:1E:26:70', '-72'),
(4, 8, 'C0:4A:00:7C:43:DC', '-72'),
(5, 8, '00:21:91:FE:CE:01', '-49'),
(6, 8, 'F8:1A:67:F8:7D:88', '-70'),
(7, 9, 'C8:3A:35:1E:26:70', '-72'),
(8, 9, 'C0:4A:00:7C:43:DC', '-72'),
(9, 9, '00:21:91:FE:CE:01', '-49'),
(10, 9, 'F8:1A:67:F8:7D:88', '-70'),
(11, 10, 'C8:3A:35:1E:26:70', '-72'),
(12, 10, 'C0:4A:00:7C:43:DC', '-72'),
(13, 10, '00:21:91:FE:CE:01', '-49'),
(14, 10, 'F8:1A:67:F8:7D:88', '-70'),
(15, 11, 'C8:3A:35:1E:26:70', '-72'),
(16, 11, 'C0:4A:00:7C:43:DC', '-72'),
(17, 11, '00:21:91:FE:CE:01', '-48'),
(18, 11, 'F8:1A:67:F8:7D:88', '-70'),
(19, 12, 'C8:3A:35:1E:26:70', '-72'),
(20, 12, 'C0:4A:00:7C:43:DC', '-72'),
(21, 12, '00:21:91:FE:CE:01', '-48'),
(22, 12, 'F8:1A:67:F8:7D:88', '-70'),
(23, 13, 'C8:3A:35:1E:26:70', '-72'),
(24, 13, 'C0:4A:00:7C:43:DC', '-72'),
(25, 13, '00:21:91:FE:CE:01', '-49'),
(26, 13, 'F8:1A:67:F8:7D:88', '-70'),
(27, 14, 'C8:3A:35:1E:26:70', '-72'),
(28, 14, 'C0:4A:00:7C:43:DC', '-72'),
(29, 14, '00:21:91:FE:CE:01', '-49'),
(30, 14, 'F8:1A:67:F8:7D:88', '-70'),
(31, 15, 'C8:3A:35:1E:26:70', '-72'),
(32, 15, 'C0:4A:00:7C:43:DC', '-72'),
(33, 15, '00:21:91:FE:CE:01', '-49'),
(34, 15, 'F8:1A:67:F8:7D:88', '-70');

-- --------------------------------------------------------

--
-- Table structure for table `net_session`
--

CREATE TABLE IF NOT EXISTS `net_session` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `logged` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `mac` varchar(50) CHARACTER SET latin1 COLLATE latin1_general_ci NOT NULL,
  `ip` varchar(50) CHARACTER SET latin1 COLLATE latin1_general_ci NOT NULL,
  `os` varchar(255) CHARACTER SET latin1 COLLATE latin1_general_ci NOT NULL,
  `connected_ssid` varchar(50) CHARACTER SET latin1 COLLATE latin1_general_ci NOT NULL,
  `connected_ap` varchar(50) CHARACTER SET latin1 COLLATE latin1_general_ci NOT NULL,
  `connected_time` time NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=16 ;

--
-- Dumping data for table `net_session`
--

INSERT INTO `net_session` (`id`, `logged`, `mac`, `ip`, `os`, `connected_ssid`, `connected_ap`, `connected_time`) VALUES
(1, '2015-03-01 00:17:20', 'E006E6C588BA', 'fe80::c065:cef9:192f:c9c9%12', 'Microsoft Windows NT 6.1.7601 Service Pack 1', 'System.Byte[]', '002191FECE01', '13:17:52'),
(2, '2015-03-01 00:17:30', 'E006E6C588BA', 'fe80::c065:cef9:192f:c9c9%12', 'Microsoft Windows NT 6.1.7601 Service Pack 1', 'System.Byte[]', '002191FECE01', '00:00:00'),
(3, '2015-03-01 00:30:07', 'E006E6C588BA', 'fe80::c065:cef9:192f:c9c9%12', 'Microsoft Windows NT 6.1.7601 Service Pack 1', 'System.Byte[]', '002191FECE01', '13:25:19'),
(4, '2015-03-01 00:30:17', 'E006E6C588BA', 'fe80::c065:cef9:192f:c9c9%12', 'Microsoft Windows NT 6.1.7601 Service Pack 1', 'System.Byte[]', '002191FECE01', '13:25:29'),
(5, '2015-03-01 00:31:44', 'E006E6C588BA', 'fe80::c065:cef9:192f:c9c9%12', 'Microsoft Windows NT 6.1.7601 Service Pack 1', 'System.Byte[]', '002191FECE01', '13:26:14'),
(6, '2015-03-01 00:31:54', 'E006E6C588BA', 'fe80::c065:cef9:192f:c9c9%12', 'Microsoft Windows NT 6.1.7601 Service Pack 1', 'System.Byte[]', '002191FECE01', '13:26:26'),
(7, '2015-03-01 00:37:43', 'E006E6C588BA', 'fe80::c065:cef9:192f:c9c9%12', 'Microsoft Windows NT 6.1.7601 Service Pack 1', 'System.Byte[]', '002191FECE01', '00:00:00'),
(8, '2015-03-01 00:37:54', 'E006E6C588BA', 'fe80::c065:cef9:192f:c9c9%12', 'Microsoft Windows NT 6.1.7601 Service Pack 1', 'System.Byte[]', '002191FECE01', '00:00:00'),
(9, '2015-03-01 00:38:04', 'E006E6C588BA', 'fe80::c065:cef9:192f:c9c9%12', 'Microsoft Windows NT 6.1.7601 Service Pack 1', 'System.Byte[]', '002191FECE01', '00:00:00'),
(10, '2015-03-01 00:38:14', 'E006E6C588BA', 'fe80::c065:cef9:192f:c9c9%12', 'Microsoft Windows NT 6.1.7601 Service Pack 1', 'System.Byte[]', '002191FECE01', '13:30:06'),
(11, '2015-03-01 00:38:25', 'E006E6C588BA', 'fe80::c065:cef9:192f:c9c9%12', 'Microsoft Windows NT 6.1.7601 Service Pack 1', 'System.Byte[]', '002191FECE01', '13:30:17'),
(12, '2015-03-01 00:38:35', 'E006E6C588BA', 'fe80::c065:cef9:192f:c9c9%12', 'Microsoft Windows NT 6.1.7601 Service Pack 1', 'System.Byte[]', '002191FECE01', '13:30:27'),
(13, '2015-03-01 00:38:45', 'E006E6C588BA', 'fe80::c065:cef9:192f:c9c9%12', 'Microsoft Windows NT 6.1.7601 Service Pack 1', 'System.Byte[]', '002191FECE01', '13:30:37'),
(14, '2015-03-01 00:38:55', 'E006E6C588BA', 'fe80::c065:cef9:192f:c9c9%12', 'Microsoft Windows NT 6.1.7601 Service Pack 1', 'System.Byte[]', '002191FECE01', '13:30:47'),
(15, '2015-03-01 00:39:06', 'E006E6C588BA', 'fe80::c065:cef9:192f:c9c9%12', 'Microsoft Windows NT 6.1.7601 Service Pack 1', 'System.Byte[]', '002191FECE01', '13:30:58');

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
