-- phpMyAdmin SQL Dump
-- version 5.1.0
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: יולי 19, 2021 בזמן 02:27 PM
-- גרסת שרת: 10.4.18-MariaDB
-- PHP Version: 8.0.3

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `store`
--

-- --------------------------------------------------------

--
-- מבנה טבלה עבור טבלה `customers`
--

CREATE TABLE `customers` (
  `id` int(10) NOT NULL,
  `FirstName` varchar(20) NOT NULL,
  `LastName` varchar(20) NOT NULL,
  `Phone` varchar(20) NOT NULL,
  `Address` varchar(30) NOT NULL,
  `Email` varchar(30) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- הוצאת מידע עבור טבלה `customers`
--

INSERT INTO `customers` (`id`, `FirstName`, `LastName`, `Phone`, `Address`, `Email`) VALUES
(1, 'Liel', 'Barouch', '0545934134', 'Azar 11', 'liel@gmail.com'),
(3, 'Yuval', 'Yeiny', '0503525', 'Azar 11', 'yuval@sds.com'),
(4, 'Eliseo', 'Gold', '3523532', 'Naharia', 'djfdsnlfsd');

-- --------------------------------------------------------

--
-- מבנה טבלה עבור טבלה `items`
--

CREATE TABLE `items` (
  `Id` int(10) NOT NULL,
  `ItemName` varchar(30) NOT NULL,
  `Price` int(10) NOT NULL,
  `UnitsInStock` int(10) NOT NULL,
  `QtySold` int(10) NOT NULL,
  `SupplierId` int(20) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- הוצאת מידע עבור טבלה `items`
--

INSERT INTO `items` (`Id`, `ItemName`, `Price`, `UnitsInStock`, `QtySold`, `SupplierId`) VALUES
(1, 'Toy', 90, 1, 4, 1),
(5, 'Batman', 300, 0, 6, 1),
(9, 'Iron man', 120, 8, 0, 5);

-- --------------------------------------------------------

--
-- מבנה טבלה עבור טבלה `orders`
--

CREATE TABLE `orders` (
  `id` int(10) NOT NULL,
  `Price` int(20) NOT NULL,
  `OrderDate` varchar(20) NOT NULL,
  `Count` int(20) NOT NULL,
  `CustomerId` int(10) NOT NULL,
  `ItemId` int(10) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- הוצאת מידע עבור טבלה `orders`
--

INSERT INTO `orders` (`id`, `Price`, `OrderDate`, `Count`, `CustomerId`, `ItemId`) VALUES
(1, 90, '7/10/2021 12:00:00 A', 1, 1, 1),
(2, 900, '7/10/2021 12:00:00 A', 3, 1, 5),
(3, 9726, '10/07/2021', 3, 1, 8),
(4, 600, '10/07/2021', 2, 4, 5),
(5, 300, '10/07/2021', 1, 3, 5);

-- --------------------------------------------------------

--
-- מבנה טבלה עבור טבלה `suppliers`
--

CREATE TABLE `suppliers` (
  `id` int(10) NOT NULL,
  `SupplierName` varchar(30) NOT NULL,
  `Phone` varchar(20) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- הוצאת מידע עבור טבלה `suppliers`
--

INSERT INTO `suppliers` (`id`, `SupplierName`, `Phone`) VALUES
(1, 'Lego', '054444444'),
(5, 'Marvel Toys', '0775544321'),
(6, 'Pazels', '0987654');

--
-- Indexes for dumped tables
--

--
-- אינדקסים לטבלה `customers`
--
ALTER TABLE `customers`
  ADD PRIMARY KEY (`id`);

--
-- אינדקסים לטבלה `items`
--
ALTER TABLE `items`
  ADD PRIMARY KEY (`Id`);

--
-- אינדקסים לטבלה `orders`
--
ALTER TABLE `orders`
  ADD PRIMARY KEY (`id`);

--
-- אינדקסים לטבלה `suppliers`
--
ALTER TABLE `suppliers`
  ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `customers`
--
ALTER TABLE `customers`
  MODIFY `id` int(10) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT for table `items`
--
ALTER TABLE `items`
  MODIFY `Id` int(10) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=10;

--
-- AUTO_INCREMENT for table `orders`
--
ALTER TABLE `orders`
  MODIFY `id` int(10) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- AUTO_INCREMENT for table `suppliers`
--
ALTER TABLE `suppliers`
  MODIFY `id` int(10) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
