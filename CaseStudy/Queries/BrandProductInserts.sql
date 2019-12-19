--INSERT INTO [dbo].[Brand] ([Name]) VALUES ('Rolex')
--INSERT INTO [dbo].[Brand] ([Name]) VALUES ('MVMT')
--INSERT INTO [dbo].[Brand] ([Name]) VALUES ('Bulova');

INSERT INTO [dbo].[Product] ([Id], [BrandId], [CostPrice], [Description], [GraphicName], [MSRP], [ProductName], [QtyOnBackOrder], [QtyOnHand] ) VALUES ('Rolex', 1, 40000, 'By operating its own exclusive foundry, Rolex has the unrivalled ability to cast the highest quality 18 ct gold alloys.
According to the proportion of silver, copper, platinum or palladium added, different types of 18 ct gold are obtained: yellow, pink or white.
They are made with only the purest metals and meticulously inspected in an in-house laboratory with state-of-the-art equipment, before the gold is formed and shaped with the same painstaking attention to quality.
Rolexs commitment to excellence begins at the source.', 'rolex.jpg', 15000, 'Day-Date 40', 10, 5)

INSERT INTO [dbo].[Product] ([Id], [BrandId], [CostPrice], [Description], [GraphicName], [MSRP], [ProductName], [QtyOnBackOrder], [QtyOnHand] ) VALUES ('Rolex1', 1, 60000, 'Gem-setters, like sculptors, finely carve the precious metal to hand-shape the seat in which each gemstone will be perfectly lodged. With the art and craft of a jeweller, the stone is placed and meticulously aligned with the others, then firmly secured in its gold or platinum setting. Besides the intrinsic quality of the stones, several other criteria contribute to the beauty of Rolex gem-setting: the precise alignment of the height of the gems, their orientation and position, the regularity, strength and proportions of the setting as well as the intricate finishing of the metalwork. 
A sparkling symphony to enhance the watch and enchant the wearer.', 'rolex1.jpg', 1000, 'PearlMaster 39', 103, 25)

INSERT INTO [dbo].[Product] ([Id], [BrandId], [CostPrice], [Description], [GraphicName], [MSRP], [ProductName], [QtyOnBackOrder], [QtyOnHand] ) VALUES ('Rolex2', 1, 6000, 'Rolex uses Oystersteel for its steel watch cases. Specially developed by the brand, Oystersteel belongs to the 904L steel family, superalloys most commonly used in high-technology and in the aerospace and chemical industries, where maximum resistance to corrosion is essential.
Oystersteel is extremely resistant, offers an exceptional finish once polished and maintains its beauty even in the harshest environments.', 'rolex2.jpg', 900, 'OysterSteel', 1053, 225)


INSERT INTO [dbo].[Product] ([Id], [BrandId], [CostPrice], [Description], [GraphicName], [MSRP], [ProductName], [QtyOnBackOrder], [QtyOnHand] ) VALUES ('Rolex3', 1, 7100, 'Model features
The Air-King dial features a distinctive black dial with a combination of large 3, 6 and 9 numerals marking the hours and a prominent minute scale for navigational time readings.
It bears the name Air-King in the same lettering that was designed specially for the model in the 1950s.', 'rolex3.jpg', 1930, 'OysterSteel', 1152, 25)

INSERT INTO [dbo].[Product] ([Id], [BrandId], [CostPrice], [Description], [GraphicName], [MSRP], [ProductName], [QtyOnBackOrder], [QtyOnHand] ) VALUES ('Rolex4', 1, 17160, 'The dial is the distinctive face of a Rolex watch, the feature most responsible for its identity and readability.
Characterised by hour markers fashioned from 18 ct gold to prevent tarnishing, every Rolex dial is designed and manufactured in-house, largely by hand to ensure perfection.', 'rolex4.jpg', 11930, 'OysterSteel', 253, 255);


