CREATE TABLE users (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    EMAIL VARCHAR(255) NOT NULL,
    PASSWORD VARCHAR(255) NOT NULL,
    FIRSTNAME VARCHAR(255) NOT NULL,
    LASTNAME VARCHAR(255) NOT NULL,
    GENDER CHAR(1) NOT NULL,
    DATEOFBIRTH DATE NOT NULL,
    CREATEDATE DATE
);