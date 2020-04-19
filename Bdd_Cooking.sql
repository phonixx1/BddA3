-- Initialisation de la bdd
DROP DATABASE `cooking`;
CREATE DATABASE cooking;

USE `cooking`;

CREATE TABLE `cooking`.`cuisinier` (
  `idCuisinier` VARCHAR(4) NOT NULL,
  `salaire` FLOAT NOT NULL,
  PRIMARY KEY (`idCuisinier`) );
        
CREATE TABLE `cooking`.`fournisseur` (
  `refFournisseur` VARCHAR(4) NOT NULL,
  `nomF` VARCHAR(25) NOT NULL,
  `numeroTel` VARCHAR(10) NULL,
  PRIMARY KEY (`refFournisseur`) );

CREATE TABLE `cooking`.`client` (
  `idCompte` VARCHAR(25) NOT NULL,
  `nom` VARCHAR(20) NOT NULL,
  `numeroTel` VARCHAR(10) NULL,
  `mdp` VARCHAR(20) NOT NULL,
  `soldeCook` INT NULL,
  PRIMARY KEY (`idCompte`) );
 
CREATE TABLE `cooking`.`produit` (
  `idProduit` VARCHAR(4) NOT NULL,
  `nomProduit` VARCHAR(25) NOT NULL,
  `categorie` VARCHAR(20) NOT NULL,
  `uniteDeQuantite` VARCHAR(20) NOT NULL,
  `stockActuel` INT NOT NULL,
  `stockMin` INT NOT NULL,
  `stockMax` INT NOT NULL,
  `refFournisseur` VARCHAR(4) NOT NULL,
  PRIMARY KEY (`idProduit`),
   CONSTRAINT `idProduit1` FOREIGN KEY (`refFournisseur`)
		REFERENCES `cooking`.`fournisseur` (`refFournisseur`)
		ON DELETE CASCADE
		ON UPDATE CASCADE );
        
CREATE TABLE `cooking`.`recette` (
  `idRecette` VARCHAR(4) NOT NULL,
  `nomRecette` VARCHAR(50) NOT NULL,
  `type` VARCHAR(20) NOT NULL,
  `prixDeVente` INT NOT NULL,
  `descriptif` VARCHAR(256) NOT NULL,
  `compteur` INT NULL,
  `idCompte` VARCHAR(25) NOT NULL,
  `idCuisinier` VARCHAR(4) NOT NULL,
  PRIMARY KEY (`idRecette`),
   CONSTRAINT `idRecette1` FOREIGN KEY (`idCompte`)
		REFERENCES `cooking`.`client` (`idCompte`)
		ON DELETE CASCADE
		ON UPDATE CASCADE,
   CONSTRAINT `idRecette2` FOREIGN KEY (`idCuisinier`)
		REFERENCES `cooking`.`cuisinier` (`idCuisinier`)
		ON DELETE CASCADE
		ON UPDATE CASCADE );
 
 CREATE TABLE `cooking`.`commande` (
  `idRecette` VARCHAR(4) NOT NULL,
  `idCompte` VARCHAR(25) NOT NULL,
  `quantite` INT NOT NULL,
  PRIMARY KEY (`idRecette`,`idCompte`),
   CONSTRAINT `idCompte2` FOREIGN KEY (`idRecette`)
		REFERENCES `cooking`.`recette` (`idRecette`)
		ON DELETE CASCADE
		ON UPDATE CASCADE,
   CONSTRAINT `idRecette4` FOREIGN KEY (`idCompte`)
		REFERENCES `cooking`.`client` (`idCompte`)
		ON DELETE CASCADE
		ON UPDATE CASCADE );
        
CREATE TABLE `cooking`.`estConstitue` (
  `idRecette` VARCHAR(4) NOT NULL,
  `idProduit` VARCHAR(4) NOT NULL,
  `quantiteUtilisee` INT NOT NULL,
  PRIMARY KEY (`idRecette`,`idProduit`),
   CONSTRAINT `idRecette3` FOREIGN KEY (`idProduit`)
		REFERENCES `cooking`.`produit` (`idProduit`)
		ON DELETE CASCADE
		ON UPDATE CASCADE,
  CONSTRAINT `idProduit2` FOREIGN KEY (`idRecette`)
		REFERENCES `cooking`.`recette` (`idRecette`)
		ON DELETE CASCADE
		ON UPDATE CASCADE );

-- Remplissage des tables
-- Table cuisinier
INSERT INTO `cooking`.`cuisinier` (`idCuisinier`, `salaire`) VALUES ('C001', 1200);
INSERT INTO `cooking`.`cuisinier` (`idCuisinier`, `salaire`) VALUES ('C002', 1300);
INSERT INTO `cooking`.`cuisinier` (`idCuisinier`, `salaire`) VALUES ('C003', 1250);

-- Table fournisseur
INSERT INTO `cooking`.`fournisseur` (`refFournisseur`, `nomF`,`numeroTel`) VALUES ('F001', 'Maraicher Benoit et fils','0124589578');
INSERT INTO `cooking`.`fournisseur` (`refFournisseur`, `nomF`,`numeroTel`) VALUES ('F002', 'Condiment Marie','0692124632');
INSERT INTO `cooking`.`fournisseur` (`refFournisseur`, `nomF`,`numeroTel`) VALUES ('F003', 'Boucher Petitpas','0132282879');
INSERT INTO `cooking`.`fournisseur` (`refFournisseur`, `nomF`,`numeroTel`) VALUES ('F004', 'Epicier Jean Marc','0609273520');
INSERT INTO `cooking`.`fournisseur` (`refFournisseur`, `nomF`,`numeroTel`) VALUES ('F005', 'Laiterie Michel','0134189660');

 -- Table client
INSERT INTO `cooking`.`client` (`idCompte`, `nom`, `numeroTel`, `mdp`, `soldeCook`) VALUES ('K001', 'Revol', '0687351223', 'azerty',NULL);
INSERT INTO `cooking`.`client` (`idCompte`, `nom`, `numeroTel`, `mdp`, `soldeCook`) VALUES ('K002', 'Rezmer', '0650958028', 'bonjour', 20);
INSERT INTO `cooking`.`client` (`idCompte`, `nom`, `numeroTel`, `mdp`, `soldeCook`) VALUES ('K003', 'Dupont', '0608992315', 'qwerty', 5);

-- Table produit
INSERT INTO `cooking`.`produit` (`idProduit`, `nomProduit`, `categorie`, `uniteDeQuantite`, `stockActuel`,`stockMin`,`stockMax`,`refFournisseur`) VALUES ('P001', 'Salade', 'legume', 'piece', 10, 2, 20, 'F001');
INSERT INTO `cooking`.`produit` (`idProduit`, `nomProduit`, `categorie`, `uniteDeQuantite`, `stockActuel`,`stockMin`,`stockMax`,`refFournisseur`) VALUES ('P002', 'Tomate', 'legume', 'piece', 40, 10, 80, 'F001');
INSERT INTO `cooking`.`produit` (`idProduit`, `nomProduit`, `categorie`, `uniteDeQuantite`, `stockActuel`,`stockMin`,`stockMax`,`refFournisseur`) VALUES ('P003', 'Baie de goji', 'fruit', 'piece', 80, 20, 200, 'F002');
INSERT INTO `cooking`.`produit` (`idProduit`, `nomProduit`, `categorie`, `uniteDeQuantite`, `stockActuel`,`stockMin`,`stockMax`,`refFournisseur`) VALUES ('P004', 'Pommes de terre', 'legume', 'piece', 150, 60, 200, 'F001');
INSERT INTO `cooking`.`produit` (`idProduit`, `nomProduit`, `categorie`, `uniteDeQuantite`, `stockActuel`,`stockMin`,`stockMax`,`refFournisseur`) VALUES ('P005', 'Olive', 'fruit', 'piece', 150, 60, 200, 'F002');
INSERT INTO `cooking`.`produit` (`idProduit`, `nomProduit`, `categorie`, `uniteDeQuantite`, `stockActuel`,`stockMin`,`stockMax`,`refFournisseur`) VALUES ('P006', 'Veau', 'viande', 'grammes' ,2000, 8000, 20000, 'F003');
INSERT INTO `cooking`.`produit` (`idProduit`, `nomProduit`, `categorie`, `uniteDeQuantite`, `stockActuel`,`stockMin`,`stockMax`,`refFournisseur`) VALUES ('P007', 'Farine T80','farine', 'grammes' ,10000, 20000, 100000, 'F004');
INSERT INTO `cooking`.`produit` (`idProduit`, `nomProduit`, `categorie`, `uniteDeQuantite`, `stockActuel`,`stockMin`,`stockMax`,`refFournisseur`) VALUES ('P008', 'Oeufs', 'viande', 'piece', 80, 15, 160, 'F003');
INSERT INTO `cooking`.`produit` (`idProduit`, `nomProduit`, `categorie`, `uniteDeQuantite`, `stockActuel`,`stockMin`,`stockMax`,`refFournisseur`) VALUES ('P009', 'Sucre', 'autre', 'kilos' ,35, 10, 100, 'F004');
INSERT INTO `cooking`.`produit` (`idProduit`, `nomProduit`, `categorie`, `uniteDeQuantite`, `stockActuel`,`stockMin`,`stockMax`,`refFournisseur`) VALUES ('P010', 'Beurre', 'autre', 'grammes', 10000, 20000, 50000, 'F005');
INSERT INTO `cooking`.`produit` (`idProduit`, `nomProduit`, `categorie`, `uniteDeQuantite`, `stockActuel`,`stockMin`,`stockMax`,`refFournisseur`) VALUES ('P011', 'Vinaigrette', 'huile', 'cuilleres', 10, 2, 20, 'F002');
INSERT INTO `cooking`.`produit` (`idProduit`, `nomProduit`, `categorie`, `uniteDeQuantite`, `stockActuel`,`stockMin`,`stockMax`,`refFournisseur`) VALUES ('P012', 'Chocolat', 'autre', 'carres', 80, 10, 120, 'F004');
INSERT INTO `cooking`.`produit` (`idProduit`, `nomProduit`, `categorie`, `uniteDeQuantite`, `stockActuel`,`stockMin`,`stockMax`,`refFournisseur`) VALUES ('P013', 'Cheddar', 'fromage', 'grammes', 2000, 8000, 14000, 'F005');
INSERT INTO `cooking`.`produit` (`idProduit`, `nomProduit`, `categorie`, `uniteDeQuantite`, `stockActuel`,`stockMin`,`stockMax`,`refFournisseur`) VALUES ('P014', 'Parmesan', 'fromage', 'grammes', 2000, 8000, 14000, 'F005');
INSERT INTO `cooking`.`produit` (`idProduit`, `nomProduit`, `categorie`, `uniteDeQuantite`, `stockActuel`,`stockMin`,`stockMax`,`refFournisseur`) VALUES ('P015', 'Mozzarela', 'fromage', 'grammes', 2000, 8000, 14000, 'F005');
INSERT INTO `cooking`.`produit` (`idProduit`, `nomProduit`, `categorie`, `uniteDeQuantite`, `stockActuel`,`stockMin`,`stockMax`,`refFournisseur`) VALUES ('P016', 'Fromage de chèvre', 'fromage', 'grammes', 8000, 2000, 14000, 'F005');
INSERT INTO `cooking`.`produit` (`idProduit`, `nomProduit`, `categorie`, `uniteDeQuantite`, `stockActuel`,`stockMin`,`stockMax`,`refFournisseur`) VALUES ('P017', 'Citron', 'fruit', 'piece', 25, 40, 80, 'F001');

-- Table recette 
INSERT INTO `cooking`.`recette` (`idRecette`, `nomRecette`, `type`, `prixDeVente`, `descriptif`,`compteur`,`idCompte`,`idCuisinier`) VALUES ('R001', 'Salade printanniere', 'Entrée', 4.00, 'Salade printaniere composee de tomates, olives, baie de goji',2,'K003','C002');
INSERT INTO `cooking`.`recette` (`idRecette`, `nomRecette`, `type`, `prixDeVente`, `descriptif`,`compteur`,`idCompte`,`idCuisinier`) VALUES ('R002', 'Fondant au chocolat', 'Dessert', 3.00, 'Fondant au chocolat individuel',0,'K002','C001');
INSERT INTO `cooking`.`recette` (`idRecette`, `nomRecette`, `type`, `prixDeVente`, `descriptif`,`compteur`,`idCompte`,`idCuisinier`) VALUES ('R003', 'Galette de pommes de terre et son roti de veau', 'Plat', 12.50, 'Galette de pommes de terre de Noirmoutier accompagné d une tranche de filet de veau et de sa sauce',0,'K003','C003');
INSERT INTO `cooking`.`recette` (`idRecette`, `nomRecette`, `type`, `prixDeVente`, `descriptif`,`compteur`,`idCompte`,`idCuisinier`) VALUES ('R004', 'Pizza aux 4 fromages', 'Plat', 12.50, 'Pizza au cheddar, mozzarella, parmesan et au fromage de chèvre',14,'K002','C001');
INSERT INTO `cooking`.`recette` (`idRecette`, `nomRecette`, `type`, `prixDeVente`, `descriptif`,`compteur`,`idCompte`,`idCuisinier`) VALUES ('R005', 'Tarte au citron', 'Dessert', 4.00, 'Tarte au citron individuelle',4,'K003','C002');

-- Table est constitue
INSERT INTO `cooking`.`estConstitue` (`idRecette`, `idProduit`,`quantiteUtilisee`) VALUES ('R001', 'P001', 1);
INSERT INTO `cooking`.`estConstitue` (`idRecette`, `idProduit`,`quantiteUtilisee`) VALUES ('R001', 'P002', 3);
INSERT INTO `cooking`.`estConstitue` (`idRecette`, `idProduit`,`quantiteUtilisee`) VALUES ('R001', 'P003', 8);
INSERT INTO `cooking`.`estConstitue` (`idRecette`, `idProduit`,`quantiteUtilisee`) VALUES ('R001', 'P005', 10);
INSERT INTO `cooking`.`estConstitue` (`idRecette`, `idProduit`,`quantiteUtilisee`) VALUES ('R001', 'P011', 1);
INSERT INTO `cooking`.`estConstitue` (`idRecette`, `idProduit`,`quantiteUtilisee`) VALUES ('R002', 'P012', 8);
INSERT INTO `cooking`.`estConstitue` (`idRecette`, `idProduit`,`quantiteUtilisee`) VALUES ('R002', 'P007',250);
INSERT INTO `cooking`.`estConstitue` (`idRecette`, `idProduit`,`quantiteUtilisee`) VALUES ('R002', 'P008', 3);
INSERT INTO `cooking`.`estConstitue` (`idRecette`, `idProduit`,`quantiteUtilisee`) VALUES ('R002', 'P009', 5);
INSERT INTO `cooking`.`estConstitue` (`idRecette`, `idProduit`,`quantiteUtilisee`) VALUES ('R002', 'P010', 100);
INSERT INTO `cooking`.`estConstitue` (`idRecette`, `idProduit`,`quantiteUtilisee`) VALUES ('R003', 'P004', 6);
INSERT INTO `cooking`.`estConstitue` (`idRecette`, `idProduit`,`quantiteUtilisee`) VALUES ('R003', 'P006', 500);
INSERT INTO `cooking`.`estConstitue` (`idRecette`, `idProduit`,`quantiteUtilisee`) VALUES ('R004', 'P002', 5);
INSERT INTO `cooking`.`estConstitue` (`idRecette`, `idProduit`,`quantiteUtilisee`) VALUES ('R004', 'P013', 80);
INSERT INTO `cooking`.`estConstitue` (`idRecette`, `idProduit`,`quantiteUtilisee`) VALUES ('R004', 'P014', 80);
INSERT INTO `cooking`.`estConstitue` (`idRecette`, `idProduit`,`quantiteUtilisee`) VALUES ('R004', 'P015', 50);
INSERT INTO `cooking`.`estConstitue` (`idRecette`, `idProduit`,`quantiteUtilisee`) VALUES ('R004', 'P016', 70);
INSERT INTO `cooking`.`estConstitue` (`idRecette`, `idProduit`,`quantiteUtilisee`) VALUES ('R004', 'P007', 250);
INSERT INTO `cooking`.`estConstitue` (`idRecette`, `idProduit`,`quantiteUtilisee`) VALUES ('R005', 'P007', 150);
INSERT INTO `cooking`.`estConstitue` (`idRecette`, `idProduit`,`quantiteUtilisee`) VALUES ('R005', 'P008', 3);
INSERT INTO `cooking`.`estConstitue` (`idRecette`, `idProduit`,`quantiteUtilisee`) VALUES ('R005', 'P017', 4);

-- Table commande
INSERT INTO `cooking`.`commande` (`idRecette`, `idCompte`,`quantite`) VALUES ('R001', 'K001', 1);
INSERT INTO `cooking`.`commande` (`idRecette`, `idCompte`,`quantite`) VALUES ('R001', 'K002', 2);
INSERT INTO `cooking`.`commande` (`idRecette`, `idCompte`,`quantite`) VALUES ('R003', 'K003', 1);

-- Tests 
select * from client;
select * from recette;
select * from produit;
select * from fournisseur;
select * from cuisinier;
select * from estConstitue;
select * from commande;