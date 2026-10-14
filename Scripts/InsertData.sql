/* DML: Run this second, after CreateTables.sql. It inserts 34 sample records. */
USE GameLibraryWebDB;
GO
INSERT INTO Franchises (FranchiseName, Publisher) VALUES
('Mario', 'Nintendo'), ('The Legend of Zelda', 'Nintendo'), ('Fallout', 'Bethesda Softworks'), ('Halo', 'Xbox Game Studios'),
('Final Fantasy', 'Square Enix'), ('Pokemon', 'Nintendo'), ('The Elder Scrolls', 'Bethesda Softworks'), ('Sonic the Hedgehog', 'Sega');
INSERT INTO Platforms (PlatformName, Manufacturer) VALUES
('Nintendo Switch', 'Nintendo'), ('PlayStation 5', 'Sony'), ('Xbox Series X S', 'Microsoft'), ('PC', 'Various'),
('Nintendo 3DS', 'Nintendo'), ('PlayStation 4', 'Sony'), ('Xbox One', 'Microsoft'), ('Nintendo Wii U', 'Nintendo');
INSERT INTO Games (GameTitle, ReleaseYear, FranchiseId, PlatformId) VALUES
('Super Mario Odyssey', 2017, 1, 1), ('Super Mario Bros Wonder', 2023, 1, 1), ('The Legend of Zelda Breath of the Wild', 2017, 2, 1),
('The Legend of Zelda Tears of the Kingdom', 2023, 2, 1), ('Fallout 4', 2015, 3, 6), ('Fallout 76', 2018, 3, 3),
('Halo Infinite', 2021, 4, 3), ('Halo The Master Chief Collection', 2014, 4, 7), ('Final Fantasy VII Rebirth', 2024, 5, 2),
('Final Fantasy XVI', 2023, 5, 2), ('Pokemon Scarlet', 2022, 6, 1), ('Pokemon Legends Arceus', 2022, 6, 1),
('The Elder Scrolls V Skyrim', 2011, 7, 4), ('The Elder Scrolls Online', 2014, 7, 4), ('Sonic Frontiers', 2022, 8, 1),
('Sonic Superstars', 2023, 8, 1), ('Super Mario 3D World', 2013, 1, 8), ('The Legend of Zelda A Link Between Worlds', 2013, 2, 5);
GO
