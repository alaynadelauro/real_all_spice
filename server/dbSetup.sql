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
    IF NOT EXISTS recipes(
        id int NOT NULL PRIMARY KEY AUTO_INCREMENT,
        createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
        updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
        title CHAR(50) NOT NULL,
        instructions VARCHAR(1000) NOT NULL,
        img VARCHAR(500) NOT NULL,
        category CHAR(50) NOT NULL,
        creatorId VARCHAR(255) NOT NULL,
        FOREIGN KEY (creatorId) REFERENCES accounts(id)
    ) DEFAULT charset utf8 COMMENT '';

DROP TABLE recipes;

CREATE TABLE
    IF NOT EXISTS ingredients(
        id int NOT NULL PRIMARY KEY AUTO_INCREMENT,
        name CHAR(50) NOT NULL,
        quantity CHAR(255) NOT NULL,
        recipeId INT NOT NULL,
        FOREIGN KEY (recipeId) REFERENCES recipes(id)
    ) DEFAULT charset utf8 COMMENT '';

DROP TABLE ingredients;

CREATE TABLE
    IF NOT EXISTS favorites(
        id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
        accountID VARCHAR(255) NOT NULL,
        recipeId int NOT NULL,
        Foreign Key (accountId) REFERENCES accounts(id) ON DELETE CASCADE,
        FOREIGN KEY (recipeId) REFERENCES recipes(id) ON DELETE CASCADE,
        UNIQUE(accountId, recipeId)
    ) DEFAULT charset utf8 COMMENT '';

DROP TABLE favorites;

INSERT INTO
    recipes (
        title,
        category,
        instructions,
        img,
        creatorId
    )
VALUES (
        'test',
        'test',
        'test',
        'test',
        '6567cbc6ac990d55b2b2e3fa'
    );