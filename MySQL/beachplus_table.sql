CREATE TABLE `beachplus` (
 `address` text NOTNULL,
 `source` text NOTNULL,
 `beach_id` int(11)DEFAULTNULL,
 `alternate` text NOTNULL,
 PRIMARY KEY `beach_id` (`beach_id`),
CONSTRAINT `beachplus_ibfk_1` FOREIGNKEY(`beach_id`)REFERENCES `beach` (`id`)ONDELETECASCADEONUPDATECASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8

