CREATE TABLE `beach` (
 `id` int(11)NOTNULL AUTO_INCREMENT,
 `lat` decimal(9,6)NOTNULL,
`lon` decimal(9,6)NOTNULL,
 `name` text NOTNULL,
 `region` text NOTNULL,
 `photo` text NOTNULL,
PRIMARY KEY(`id`)
) ENGINE=InnoDB AUTO_INCREMENT=3274DEFAULT CHARSET=utf8
