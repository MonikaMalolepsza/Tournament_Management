CREATE TABLE `tournament`.`type` (
                                     `id` INT(11) NOT NULL AUTO_INCREMENT,
                                     `type` VARCHAR(100) NULL DEFAULT NULL,
                                     PRIMARY KEY (`id`)
);
CREATE TABLE `tournament`.`team` (
                                     `id` INT(11) NOT NULL AUTO_INCREMENT,
                                     PRIMARY KEY (`id`),
                                     `name` VARCHAR(50) NULL DEFAULT NULL,
                                     `type_id` INT(11) NULL DEFAULT NULL,
                                     INDEX `fk_type_team_id` (`type_id`),
                                     CONSTRAINT `fk_type_team_id` FOREIGN KEY (`type_id`) REFERENCES `type` (`id`)
);
CREATE TABLE `tournament`.`person` (
                                       `id` INT(11) NOT NULL AUTO_INCREMENT,
                                       `name` VARCHAR(50) NULL DEFAULT NULL,
                                       `surname` VARCHAR(50) NULL DEFAULT NULL,
                                       `age` INT NOT NULL,
                                       `active` TINYINT(1) NULL,
                                       PRIMARY KEY (`id`)
);

CREATE TABLE `tournament`.`referee` (
                                        `id` INT(11) NOT NULL AUTO_INCREMENT,
                                        `certificate` VARCHAR(255) NULL,
                                        `type_id` INT(11) NULL DEFAULT NULL,
                                        PRIMARY KEY (`id`),
                                        `person_id` INT(11) NULL DEFAULT NULL,
                                        INDEX `fk_type_ref_id` (`type_id`),
                                        CONSTRAINT `fk_type_ref_id` FOREIGN KEY (`type_id`) REFERENCES `type` (`id`),
                                        INDEX `fk_referee_person` (`person_id`),
                                        CONSTRAINT `fk_referee_person` FOREIGN KEY (`person_id`) REFERENCES `person` (`id`) ON DELETE CASCADE ON UPDATE CASCADE
);

CREATE TABLE `tournament`.`footballPlayer` (
                                               `id` INT(11) NOT NULL AUTO_INCREMENT,
                                               `goals` INT NULL,
                                               `speed` INT NULL,
                                               `type_id` INT(11) NULL DEFAULT NULL,
                                               `person_id` INT(11) NULL DEFAULT NULL,
                                               PRIMARY KEY (`id`),
                                               `team_id` INT(11) NULL DEFAULT NULL,
                                                INDEX `fk_f_team` (`team_id`),
                                                CONSTRAINT `fk_f_team` FOREIGN KEY (`team_id`) REFERENCES `team` (`id`),
                                                INDEX `fk_f_type` (`type_id`),
                                                CONSTRAINT `fk_f_type` FOREIGN KEY (`type_id`) REFERENCES `type` (`id`),
                                                INDEX `fk_f_person` (`person_id`),
                                                CONSTRAINT `fk_f_person` FOREIGN KEY (`person_id`) REFERENCES `person` (`id`) ON DELETE CASCADE ON UPDATE CASCADE

);
CREATE TABLE `tournament`.`handballPlayer` (
                                               `id` INT(11) NOT NULL AUTO_INCREMENT,
                                               `position` VARCHAR(100) NULL DEFAULT NULL,
                                               `speed` INT NULL,
                                               `goals` INT NULL,
                                               `type_id` INT(11) NULL DEFAULT NULL,
                                               `person_id` INT(11) NULL DEFAULT NULL,
                                               PRIMARY KEY (`id`),
                                               `team_id` INT(11) NULL DEFAULT NULL,
                                                INDEX `fk_h_team` (`team_id`),
                                                CONSTRAINT `fk_h_team` FOREIGN KEY (`team_id`) REFERENCES `team` (`id`),
                                                INDEX `fk_h_type` (`type_id`),
                                                CONSTRAINT `fk_h_type` FOREIGN KEY (`type_id`) REFERENCES `type` (`id`),
                                                INDEX `fk_h_person` (`person_id`),
                                                CONSTRAINT `fk_h_person` FOREIGN KEY (`person_id`) REFERENCES `person` (`id`) ON DELETE CASCADE ON UPDATE CASCADE

);
CREATE TABLE `tournament`.`basketballPlayer` (
                                                 `id` INT(11) NOT NULL AUTO_INCREMENT,
                                                 `height` INT(3) NULL DEFAULT NULL,
                                                 `field_goal` INT NULL,
                                                 `speed` INT NULL,
                                                 `type_id` INT(11) NULL DEFAULT NULL,
                                                 `person_id` INT(11) NULL DEFAULT NULL,
                                                 PRIMARY KEY (`id`),
                                               `team_id` INT(11) NULL DEFAULT NULL,
                                                INDEX `fk_b_team` (`team_id`),
                                                CONSTRAINT `fk_b_team` FOREIGN KEY (`team_id`) REFERENCES `team` (`id`),
                                                INDEX `fk_b_type` (`type_id`),
                                                CONSTRAINT `fk_b_type` FOREIGN KEY (`type_id`) REFERENCES `type` (`id`),
                                                INDEX `fk_b_person` (`person_id`),
                                                CONSTRAINT `fk_b_person` FOREIGN KEY (`person_id`) REFERENCES `person` (`id`) ON DELETE CASCADE ON UPDATE CASCADE

);

CREATE TABLE `tournament`.`trainer` (
                                        `id` INT(11) NOT NULL AUTO_INCREMENT,
                                        PRIMARY KEY (`id`),
                                        `person_id` INT(11) NULL DEFAULT NULL,
                                        `type_id` INT(11) NULL DEFAULT NULL,
                                         `team_id` INT(11) NULL DEFAULT NULL,
                                         INDEX `fk_tr_team` (`team_id`),
                                         CONSTRAINT `fk_tr_team` FOREIGN KEY (`team_id`) REFERENCES `team` (`id`),
                                         INDEX `fk_tr_type` (`type_id`),
                                         CONSTRAINT `fk_tr_type` FOREIGN KEY (`type_id`) REFERENCES `type` (`id`),
                                         INDEX `fk_tr_person` (`person_id`),
                                         CONSTRAINT `fk_tr_person` FOREIGN KEY (`person_id`) REFERENCES `person` (`id`) ON DELETE CASCADE ON UPDATE CASCADE
);

CREATE TABLE `tournament`.`physio` (
                                       `id` INT(11) NOT NULL AUTO_INCREMENT,
                                       `person_id` INT(11) NULL DEFAULT NULL,
                                       `experience` INT(2) NULL DEFAULT NULL,
                                       PRIMARY KEY (`id`),
                                       `team_id` INT(11) NULL DEFAULT NULL,
                                       INDEX `fk_ph_team` (`team_id`),
                                       CONSTRAINT `fk_ph_team` FOREIGN KEY (`team_id`) REFERENCES `team` (`id`),
                                       INDEX `fk_ph_person` (`person_id`),
                                       CONSTRAINT `fk_ph_person` FOREIGN KEY (`person_id`) REFERENCES `person` (`id`) ON DELETE CASCADE ON UPDATE CASCADE
);