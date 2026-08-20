-- Poliza_Mercancia: tres textos que solo se imprimen al generar la poliza.
-- Aditivo y nulo: lo que ya existe no se toca.
IF COL_LENGTH('Poliza_Mercancia','Especiales') IS NULL
    ALTER TABLE Poliza_Mercancia ADD Especiales nvarchar(max) NULL;
IF COL_LENGTH('Poliza_Mercancia','Exclusiones_Particulares') IS NULL
    ALTER TABLE Poliza_Mercancia ADD Exclusiones_Particulares nvarchar(max) NULL;
IF COL_LENGTH('Poliza_Mercancia','Medidas_De_Seguridad') IS NULL
    ALTER TABLE Poliza_Mercancia ADD Medidas_De_Seguridad nvarchar(max) NULL;

-- Para revertir:
-- ALTER TABLE Poliza_Mercancia DROP COLUMN Especiales, Exclusiones_Particulares, Medidas_De_Seguridad;
