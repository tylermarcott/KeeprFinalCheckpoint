CREATE TABLE
    IF NOT EXISTS accounts(
        id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
        createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
        updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
        name varchar(255) COMMENT 'User Name',
        email varchar(255) COMMENT 'User Email',
        picture varchar(255) COMMENT 'User Picture'
    ) default charset utf8 COMMENT '';

CREATE TABLE
    IF NOT EXISTS keeps(
        id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
        creatorId VARCHAR(255) NOT NULL,
        name VARCHAR(255) NOT NULL,
        description VARCHAR(2000) NOT NULL,
        img VARCHAR(500) NOT NULL,
        views INT DEFAULT 0,
        kept INT DEFAULT 0,
        FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
    ) default charset utf8 COMMENT '';

CREATE TABLE
    IF NOT EXISTS vaults(
        id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
        creatorId VARCHAR(255) NOT NULL,
        name VARCHAR(255) NOT NULL,
        description VARCHAR(255) NOT NULL,
        img VARCHAR(255) NOT NULL,
        isPrivate BOOLEAN DEFAULT false,
        FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
    ) default charset utf8 COMMENT '';

CREATE TABLE
    IF NOT EXISTS vaultKeeps(
        id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
        creatorId VARCHAR(255) NOT NULL,
        vaultId INT NOT NULL,
        keepId INT NOT NULL
    )

SELECT kps.*, acc.*
FROM keeps kps
    JOIN accounts acc ON acc.id = kps.creatorId

INSERT INTO
    keeps (
        creatorId,
        name,
        description,
        img,
        views,
        kept
    )
VALUES (
        @creatorId,
        @name,
        @description,
        @img,
        @views,
        @kept
    )

SELECT kps.*, acc.*
FROM keeps kps
    JOIN accounts acc ON acc.id = kps.creatorId
WHERE kps.id = LAST_INSERT_ID()

INSERT INTO
    keeps (
        creatorId,
        name,
        description,
        img,
        views,
        kept
    )
VALUES (
        '650a24a4fe35b4c25b2ada9f ',
        'best keep',
        'this is the best keep',
        'https: / / plus.unsplash.com / premium_photo -1677101221533 -52 b45823a2dc ? auto = format & fit = crop & q = 80 & w = 2071 & ixlib = rb -4.0.3 & ixid = M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA % 3 D % 3 D',
        2,
        1
    )

SELECT keeps.*, accounts.*
FROM keeps
    JOIN accounts ON accounts.id = keeps.creatorId
WHERE
    keeps.id = LAST_INSERT_ID()

SELECT accounts.*, keeps.*
FROM keeps
    JOIN accounts on accounts.id = keeps.creatorId
WHERE keeps.id = 1