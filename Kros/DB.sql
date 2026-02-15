-- INIT database

DROP TABLE IF EXISTS firma;
DROP TABLE IF EXISTS oddelenie_zamestnanci;
DROP TABLE IF EXISTS oddelenie;
DROP TABLE IF EXISTS projekt;
DROP TABLE IF EXISTS divizia;
DROP TABLE IF EXISTS zamestnanec;

CREATE TABLE zamestnanec (
  id INT IDENTITY(1, 1) PRIMARY KEY NOT NULL,
  titul VARCHAR(10) NOT NULL,
  meno VARCHAR(255) NOT NULL,
  priezvisko VARCHAR(255) NOT NULL,
  telefon VARCHAR(35),
  email VARCHAR(255),
  
  CONSTRAINT uc_zamestnanec UNIQUE (titul, meno, priezvisko, telefon, email)
);

CREATE TABLE firma (
  ico INT PRIMARY KEY NOT NULL,
  riaditel_id INT NOT NULL,
  nazov VARCHAR(255) NOT NULL,
  
  CONSTRAINT uc_firma UNIQUE (nazov),
  CONSTRAINT fk_firma_riaditel FOREIGN KEY (riaditel_id) REFERENCES zamestnanec (id) -- Firma nemoze byt bez riaditela
);

CREATE TABLE divizia (
  id INT IDENTITY(1, 1) PRIMARY KEY NOT NULL,
  veduci_id INT NOT NULL,
  nazov VARCHAR(255) NOT NULL,
  
  CONSTRAINT uc_divizia UNIQUE (nazov),
  CONSTRAINT fk_divizia_veduci_zamestnanec FOREIGN KEY (veduci_id) REFERENCES zamestnanec (id) -- Divizia nemoze byt bez veduceho
);

CREATE TABLE projekt (
  id INT IDENTITY(1, 1) PRIMARY KEY NOT NULL,
  veduci_id INT NOT NULL,
  divizia_id INT NOT NULL,
  nazov VARCHAR(255) NOT NULL,
  
  CONSTRAINT uc_projekt UNIQUE (nazov),
  CONSTRAINT fk_projekt_veduci_zamestnanec FOREIGN KEY (veduci_id) REFERENCES zamestnanec (id), -- Projekt nemoze byt bez veduceho
  CONSTRAINT fk_projekt_divizia FOREIGN KEY (divizia_id) REFERENCES divizia (id) ON DELETE CASCADE -- Ked zanikne divizia, tak zaniknu aj projekty
);

CREATE TABLE oddelenie (
  id INT IDENTITY(1, 1) PRIMARY KEY NOT NULL,
  veduci_id INT NOT NULL,
  projekt_id INT NOT NULL,
  nazov VARCHAR(255) NOT NULL,
  
  CONSTRAINT fk_oddelenie_veduci_zamestnanec FOREIGN KEY (veduci_id) REFERENCES zamestnanec (id), -- Oddelenie nemoze byt bez veduceho
  CONSTRAINT fk_oddelenie_projekt FOREIGN KEY (projekt_id) REFERENCES projekt (id) ON DELETE CASCADE --Ked zanikne projekt, tak zaniknu aj oddelenie co ho malo robit
); 

CREATE TABLE oddelenie_zamestnanci (
  oddelenie_id INT NOT NULL,
  zamestnanec_id INT NOT NULL,
  
  CONSTRAINT uc_oddelenie_zamestnanci UNIQUE (oddelenie_id,zamestnanec_id),
  CONSTRAINT fk_vymazanie_oddelenia_z_oddelenie_zamestnanci FOREIGN KEY (oddelenie_id) REFERENCES oddelenie (id) ON DELETE CASCADE,
  CONSTRAINT fk_vymazanie_zamestnanca_z_oddelenie_zamestnanci FOREIGN KEY (zamestnanec_id) REFERENCES zamestnanec (id) ON DELETE CASCADE
);


/*
CREATE TRIGGER INSTEADOF_firma 
ON dbo.firma
  INSTEAD OF DELETE AS
  BEGIN
    TRUNCATE TABLE divizia;
    TRUNCATE TABLE oddelenie;
    TRUNCATE TABLE projekt;
    TRUNCATE TABLE zamestnanec;
    TRUNCATE TABLE firma;
    TRUNCATE TABLE oddelenie_zamestnanci;
    TRUNCATE TABLE oddelenie_projekty;
    TRUNCATE TABLE divizia_oddelenia;
  END
;
*/
INSERT INTO zamestnanec (titul, meno, priezvisko, telefon, email) VALUES ('Ing.', 'Matej', 'Guran', '+421800400400', 'guran@example.com');
INSERT INTO zamestnanec (titul, meno, priezvisko, telefon, email) VALUES ('Mgr.', 'Ferdinand', 'Pekar', '+421800600600', 'pekar@example.com');
INSERT INTO zamestnanec (titul, meno, priezvisko, telefon, email) VALUES ('', 'Miroslav', 'Pikolik', '+421800700700', 'pikolik@example.com');
INSERT INTO zamestnanec (titul, meno, priezvisko, telefon, email) VALUES ('Bc.', 'Zdeno', 'Kralik', '+421800900900', 'kralik@example.com');
INSERT INTO zamestnanec (titul, meno, priezvisko, telefon, email) VALUES ('MSc.', 'Gabriel', 'Belanyi', '+421800900903', 'belanyik@example.com');
INSERT INTO zamestnanec (titul, meno, priezvisko, telefon, email) VALUES ('PhDr.', 'Viktor', 'Pleva', '+421800700705', 'pleva@example.com');

INSERT INTO firma (ico, riaditel_id, nazov) VALUES (123456, 1, 'Pekáreň Včela');
INSERT INTO divizia (veduci_id, nazov) VALUES (2, 'Divizia IT');
INSERT INTO projekt (veduci_id, divizia_id, nazov) VALUES (5, 1, 'ERP System');
INSERT INTO oddelenie (veduci_id, projekt_id, nazov) VALUES (4, 1, 'Oddelenie vyvoja ERP');
INSERT INTO oddelenie_zamestnanci (oddelenie_id, zamestnanec_id) VALUES (1, 3);
INSERT INTO oddelenie_zamestnanci (oddelenie_id, zamestnanec_id) VALUES (1, 6);
