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
        views INT NOT NULL,
        kept INT NOT NULL,
        FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE,
        FOREIGN KEY (kept) REFERENCES vaultKeeps(id) ON DELETE CASCADE
    ) default charset utf8 COMMENT '';

-- CREATE TABLE

--     IF NOT EXISTS vaultKeeps(

--         id INT AUTO_INCREMENT PRIMARY KEY,

--         creatorId VARCHAR(255) NOT NULL,

--         vaultId INT NOT NULL,

--         keepId INT NOT NULL

--     )

SELECT kps.*, acc.*
FROM keeps kps
    JOIN accounts acc ON acc.id = kps.creatorId