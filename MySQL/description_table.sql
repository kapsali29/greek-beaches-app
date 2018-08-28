CREATE TABLE `description` (
 `beach_id` int(11)NOTNULL,
 `text` text NOTNULL,
 `source` text NOTNULL,
KEY `beach_id` (`beach_id`),
CONSTRAINT `description_ibfk_1` FOREIGNKEY(`beach_id`)REFERENCES `beach` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8
