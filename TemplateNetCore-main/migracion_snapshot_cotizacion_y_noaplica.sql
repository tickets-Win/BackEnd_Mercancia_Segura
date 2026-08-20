-- 7) La palomita "No aplica" de las cuotas de mercancias especiales deja de
--    deducirse de que las cuatro cuotas esten vacias y se guarda de verdad.
IF COL_LENGTH('Poliza_Mercancia','No_Aplica_Cuotas_Especiales') IS NULL
    ALTER TABLE Poliza_Mercancia ADD No_Aplica_Cuotas_Especiales bit NULL;

-- 8) Condiciones especiales y exclusiones se congelan en la cotizacion, como ya
--    lo hace Deducibles: se prellenan de la poliza pero quedan guardadas aqui.
IF COL_LENGTH('Cotizacion_Mercancia','Condiciones_Especiales') IS NULL
    ALTER TABLE Cotizacion_Mercancia ADD Condiciones_Especiales nvarchar(max) NULL;
IF COL_LENGTH('Cotizacion_Mercancia','Exclusiones') IS NULL
    ALTER TABLE Cotizacion_Mercancia ADD Exclusiones nvarchar(max) NULL;

-- Arranque: lo existente conserva el sentido que hasta hoy se deducia.
UPDATE Poliza_Mercancia
   SET No_Aplica_Cuotas_Especiales =
       CASE WHEN Medicamentos IS NULL
             AND Cobre_Aluminio_Acero IS NULL
             AND Medicamentos_Controlados IS NULL
             AND EQ_Contratistas IS NULL
            THEN 1 ELSE 0 END
 WHERE No_Aplica_Cuotas_Especiales IS NULL;

-- Para revertir:
-- ALTER TABLE Poliza_Mercancia DROP COLUMN No_Aplica_Cuotas_Especiales;
-- ALTER TABLE Cotizacion_Mercancia DROP COLUMN Condiciones_Especiales, Exclusiones;
