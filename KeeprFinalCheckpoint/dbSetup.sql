CREATE TABLE
    IF NOT EXISTS accounts(
        id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
        createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
        updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
        name varchar(255) COMMENT 'User Name',
        email varchar(255) COMMENT 'User Email',
        picture varchar(255) COMMENT 'User Picture',
        coverImg varchar(255) COMMENT 'User Cover Image'
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
        'https://plus.unsplash.com/premium_photo-1677101221533-52b45823a2dc?auto=format&fit=crop&q=80&w=2071&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D',
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

INSERT INTO
    vaults (
        creatorId,
        name,
        description,
        img,
        isPrivate
    )
VALUES (
        '650a24a4fe35b4c25b2ada9f',
        'CatVault',
        'Many Cats',
        'https://images.unsplash.com/photo-1514888286974-6c03e2ca1dba?auto=format&fit=crop&q=80&w=2043&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D',
        false
    );

SELECT vaults.*, accounts.*
FROM vaults
    JOIN accounts ON accounts.id = vaults.creatorId
WHERE
    vaults.id = LAST_INSERT_ID()

SELECT vaults.*, accounts.*
FROM vaults
    JOIN accounts ON accounts.id = vaults.creatorId
WHERE vaults.id = @vaultId

INSERT INTO
    vaultKeeps (creatorId, vaultId, keepId)
VALUES (
        '650a24a4fe35b4c25b2ada9f',
        15,
        6
    );

SELECT
    vaultKeeps.*,
    accounts.*
FROM vaultKeeps
    JOIN accounts ON accounts.id = vaultKeeps.creatorId
WHERE
    vaultKeeps.id = LAST_INSERT_ID()

SELECT
    keeps.*,
    accounts.*,
    vaults.*,
    vaultKeeps.*
FROM keeps
    JOIN accounts ON accounts.id = vaultKeeps.creatorId
    JOIN vaults ON vaults.id = vaultKeeps.vaultId
    JOIN vaultKeeps ON vaultKeeps.WHERE vaultKeeps.vaultId = 1

SELECT keeps.*, profiles.*
FROM keeps
    JOIN accounts profiles ON profiles.id = keeps.creatorId
WHERE keeps.creatorId = '65271b81e57f826489d44109'

SELECT
    vaultKeeps.*,
    keeps.*,
    vaults.*,
    profiles.*
FROM vaultKeeps
    JOIN keeps ON keeps.id = vaultKeeps.keepId
    JOIN vaults ON vaults.id = vaultKeeps.vaultId
    JOIN accounts profiles ON profiles.id = vaultKeeps.creatorId
WHERE vaultKeeps.vaultId = 15

--NOTE: ok, so Miles is saying this is wrong, that I need to select from keeps, and join vaultKeeps, instead of the other way around. I am going to give this a shot and see if I can get all keeps in vault through this method.

SELECT
    keeps.*,
    vaultKeeps.*,
    vaults.*,
    profiles.*
FROM keeps
    JOIN vaultKeeps ON vaultKeeps.keepId = keeps.id
    JOIN vaults ON vaults.id = vaultKeeps.vaultId
    JOIN accounts profiles ON profiles.id = vaultKeeps.creatorId
WHERE vaultKeeps.vaultId = 1

SELECT vaultKeeps.* FROM vaultKeeps WHERE vaultKeeps.Id = 2 

UPDATE accounts
SET
    name = 'catman',
    picture = 'https://images.unsplash.com/photo-1519052537078-e6302a4968d4?auto=format&fit=crop&q=60&w=500&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTR8fGNhdHxlbnwwfHwwfHx8MA%3D%3D',
    coverImg = 'https://images.unsplash.com/photo-1592194996308-7b43878e84a6?auto=format&fit=crop&q=60&w=500&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTB8fGNhdHxlbnwwfHwwfHx8MA%3D%3D'
WHERE id = '65271b81e57f826489d44109 '