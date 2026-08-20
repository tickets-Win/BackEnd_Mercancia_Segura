-- Respaldo de Tipo_Siniestro tomado antes de reemplazar el catalogo
SET IDENTITY_INSERT Tipo_Siniestro ON;
INSERT INTO Tipo_Siniestro (Tipo_Siniestro_ID, Tipo) VALUES (1, N'Robo total');
INSERT INTO Tipo_Siniestro (Tipo_Siniestro_ID, Tipo) VALUES (2, N'Robo parcial');
INSERT INTO Tipo_Siniestro (Tipo_Siniestro_ID, Tipo) VALUES (3, N'Faltante');
INSERT INTO Tipo_Siniestro (Tipo_Siniestro_ID, Tipo) VALUES (4, N'Pérdida total');
INSERT INTO Tipo_Siniestro (Tipo_Siniestro_ID, Tipo) VALUES (5, N'Daño por manejo');
INSERT INTO Tipo_Siniestro (Tipo_Siniestro_ID, Tipo) VALUES (6, N'Daño por estiba');
INSERT INTO Tipo_Siniestro (Tipo_Siniestro_ID, Tipo) VALUES (7, N'Mojadura');
INSERT INTO Tipo_Siniestro (Tipo_Siniestro_ID, Tipo) VALUES (8, N'Contaminación');
INSERT INTO Tipo_Siniestro (Tipo_Siniestro_ID, Tipo) VALUES (9, N'Volcadura');
INSERT INTO Tipo_Siniestro (Tipo_Siniestro_ID, Tipo) VALUES (10, N'Colisión');
INSERT INTO Tipo_Siniestro (Tipo_Siniestro_ID, Tipo) VALUES (11, N'Incendio');
INSERT INTO Tipo_Siniestro (Tipo_Siniestro_ID, Tipo) VALUES (12, N'Caída de la carga');
SET IDENTITY_INSERT Tipo_Siniestro OFF;
-- Los siniestros 5 y 6 apuntaban a: 5 -> Robo parcial, 6 -> Volcadura
