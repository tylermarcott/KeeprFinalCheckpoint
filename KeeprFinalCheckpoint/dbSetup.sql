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
        1,
        1
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

-- get keeps by vault prototype sql

SELECT
    keeps.*,
    vaults.*,
    accounts.*,
    vaultKeeps.*
FROM keeps
    JOIN vaultKeeps ON vaultKeeps.creatorId = keeps.creatorId
    JOIN vaults ON vaults.creatorId = keeps.creatorId
    JOIN accounts ON accounts.id = keeps.creatorId
WHERE vaultKeeps.vaultId = 15

-- prototype #2

SELECT
    keeps.*,
    keepCreator.*,
    vaults.*,
    vaultCreator.*
FROM keeps
    JOIN vaultKeeps keepCreator ON keepCreator.creatorId = keeps.creatorId
    JOIN vaults ON vaults.creatorId = keeps.creatorId
    JOIN vaultKeeps vaultCreator ON vaultCreator.creatorId = vaults.creatorId
WHERE keeps.creatorId = '65271b81e57f826489d44109'

-- prototype 3: vault 1 and keep 1 have a vaultKeep. Try to use this.

--NOTE we already HAVE the vault. We just need to get the vaultKeep creatorId and vaultKeep keepId

SELECT
    keeps.*,
    vaultKeepCreator.*,
    vaultKeepVault.*
FROM keeps
    JOIN vaultKeeps vaultKeepCreator ON vaultKeepCreator.creatorId = '650a24a4fe35b4c25b2ada9f'
    JOIN vaultKeeps vaultKeepVault ON vaultKeepVault.vaultId = 1
WHERE keeps.creatorId = '650a24a4fe35b4c25b2ada9f'

-- prototype 4

SELECT
    keeps.*,
    keepCreator.*,
    vaults.*,
    vaultCreator.*
FROM keeps
    JOIN vaultKeeps keepCreator ON keepCreator.id = keeps.creatorId
    JOIN vaults ON vaults.id = vaultCreator.vaultId
    JOIN vaultKeeps vaultCreator ON vaultCreator.id = vaults.creatorId
WHERE vaultKeeps.vaultId = 1