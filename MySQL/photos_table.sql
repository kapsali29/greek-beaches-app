CREATETABLE `photos` (
 `beach_id` int(11)NOTNULL,
 `ph` text NOTNULL,
KEY `beach_id` (`beach_id`),
KEY `beach_id_2` (`beach_id`),
CONSTRAINT `photos_ibfk_1` FOREIGNKEY(`beach_id`)REFERENCES `beach` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8
