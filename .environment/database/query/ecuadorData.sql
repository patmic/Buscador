USE SAE;
GO

IF OBJECT_ID('vwBusqueda', 'V') IS NOT NULL
    DROP VIEW vwBusqueda;
IF OBJECT_ID('Area', 'U') IS NOT NULL
    DROP TABLE Area;
IF OBJECT_ID('Ubicacion', 'U') IS NOT NULL
    DROP TABLE Ubicacion;
IF OBJECT_ID('UbicacionTipo', 'U') IS NOT NULL
    DROP TABLE UbicacionTipo;
IF OBJECT_ID('AreaAcreditacionActividad', 'U') IS NOT NULL
    DROP TABLE AreaAcreditacionActividad;
IF OBJECT_ID('AreaAcreditacion', 'U') IS NOT NULL
    DROP TABLE AreaAcreditacion;

CREATE TABLE UbicacionTipo (
    UbicacionTipoId     INT PRIMARY KEY,
    UbicacionTipo       VARCHAR(50)
);
CREATE TABLE Ubicacion (
    UbicacionId         INT PRIMARY KEY,
    UbicacionIdPadre    INT,
    UbicacionTipoId     INT,
    Ubicacion           VARCHAR(100),
    FOREIGN KEY (UbicacionTipoId) REFERENCES UbicacionTipo(UbicacionTipoId)
);
CREATE TABLE AreaAcreditacion (
    AreaAcreditacionId INT PRIMARY KEY,
    Nombre              VARCHAR(50) NOT NULL
);
CREATE TABLE AreaAcreditacionActividad (
    AreaAcreditacionActividadId         INT PRIMARY KEY,
    AreaAcreditacionId                  INT,
    Nombre                              VARCHAR(100) NOT NULL,
    FOREIGN KEY (AreaAcreditacionId) REFERENCES AreaAcreditacion(AreaAcreditacionId)
);
CREATE TABLE Area (
    AreaId                      INT PRIMARY KEY identity(1,1),
    AreaAcreditacionId          INT,
    AreaAcreditacionActividadId INT,
    UbicacionId                 INT,
    AreaEstado                  VARCHAR(50),
    AreaCodigoAcreditacion      VARCHAR(50),
    AreaRazonSocial             VARCHAR(200),
    FOREIGN KEY (UbicacionId)   REFERENCES Ubicacion(UbicacionId),
    FOREIGN KEY (AreaAcreditacionId) REFERENCES AreaAcreditacion(AreaAcreditacionId),
    FOREIGN KEY (AreaAcreditacionActividadId) REFERENCES AreaAcreditacionActividad(AreaAcreditacionActividadId)
);

INSERT INTO UbicacionTipo (UbicacionTipoId, UbicacionTipo)
VALUES
    (1, 'Pais'),
    (2, 'Provincia'),
    (3, 'Canton'),
    (4, 'Ciudad');
-- Inserta provincias de Ecuador
INSERT INTO Ubicacion (UbicacionId, UbicacionIdPadre, UbicacionTipoId, Ubicacion)
VALUES
    -- Provincia de Azuay
    (1, 1, 2, 'Azuay'),
    -- Provincia de Bolívar
    (2, 1, 2, 'Bolívar'),
    -- Provincia de Cañar
    (3, 1, 2, 'Cañar'),
    -- Provincia de Carchi
    (4, 1, 2, 'Carchi'),
    -- Provincia de Chimborazo
    (5, 1, 2, 'Chimborazo'),
    -- Provincia de Cotopaxi
    (6, 1, 2, 'Cotopaxi'),
    -- Provincia de El Oro
    (7, 1, 2, 'El Oro'),
    -- Provincia de Esmeraldas
    (8, 1, 2, 'Esmeraldas'),
    -- Provincia de Galápagos
    (9, 1, 2, 'Galápagos'),
    -- Provincia de Guayas
    (10, 1, 2, 'Guayas'),
    -- Provincia de Imbabura
    (11, 1, 2, 'Imbabura'),
    -- Provincia de Loja
    (12, 1, 2, 'Loja'),
    -- Provincia de Los Ríos
    (13, 1, 2, 'Los Ríos'),
    -- Provincia de Manabí
    (14, 1, 2, 'Manabí'),
    -- Provincia de Morona Santiago
    (15, 1, 2, 'Morona Santiago'),
    -- Provincia de Napo
    (16, 1, 2, 'Napo'),
    -- Provincia de Orellana
    (17, 1, 2, 'Orellana'),
    -- Provincia de Pastaza
    (18, 1, 2, 'Pastaza'),
    -- Provincia de Pichincha
    (19, 1, 2, 'Pichincha'),
    -- Provincia de Santa Elena
    (20, 1, 2, 'Santa Elena'),
    -- Provincia de Santo Domingo de los Tsáchilas
    (21, 1, 2, 'Santo Domingo de los Tsáchilas'),
    -- Provincia de Sucumbíos
    (22, 1, 2, 'Sucumbíos'),
    -- Provincia de Tungurahua
    (23, 1, 2, 'Tungurahua'),
    -- Provincia de Zamora Chinchipe
    (24, 1, 2, 'Zamora Chinchipe');
-- Inserta Cantones de Ecuador
INSERT INTO Ubicacion (UbicacionId, UbicacionIdPadre, UbicacionTipoId, Ubicacion)
VALUES
    (25, 1, 3, 'Cuenca'), -- Canton Azuay
    (26, 1, 3, 'Gualaceo'),
    (27, 2, 3, 'Guaranda'), -- Canton Bolívar
    (28, 2, 3, 'Chillanes'),
    (29, 3, 3, 'Azogues'), -- Canton Cañar
    (30, 3, 3, 'Biblián'),
    (31, 4, 3, 'Tulcán'), -- Canton Carchi
    (32, 4, 3, 'Montúfar'),
    (33, 5, 3, 'Riobamba'), -- Canton Chimborazo
    (34, 5, 3, 'Guano'),
    (35, 6, 3, 'Latacunga'), -- Canton Cotopaxi
    (36, 6, 3, 'Saquisilí'),
    (37, 7, 3, 'Machala'), -- Canton El Oro
    (38, 7, 3, 'Pasaje'),
    (183, 7, 3, 'Piñas'),
    (39, 8, 3, 'Esmeraldas'), -- Canton Esmeraldas
    (40, 8, 3, 'Atacames'),
    (41, 9, 3, 'San Cristóbal'), -- Canton Galápagos
    (42, 9, 3, 'Santa Cruz'),
    (43, 10, 3, 'Guayaquil'), -- Canton Guayas
    (44, 10, 3, 'Daule'),
    (180, 10, 3, 'Durán'),
    (45, 11, 3, 'Ibarra'), -- Canton Imbabura
    (46, 11, 3, 'Otavalo'),
    (47, 12, 3, 'Loja'), -- Canton Loja
    (48, 12, 3, 'Catamayo'),
    (49, 13, 3, 'Babahoyo'), -- Canton Los Ríos
    (50, 13, 3, 'Quevedo'),
    (51, 14, 3, 'Portoviejo'), -- Canton Manabí
    (52, 14, 3, 'Manta'),
    (53, 15, 3, 'Macas'), -- Canton Morona Santiago
    (54, 15, 3, 'Gualaquiza'),
    (55, 16, 3, 'Tena'), -- Canton Napo
    (56, 16, 3, 'Quijos'),
    (57, 17, 3, 'Orellana'), -- Canton Orellana
    (58, 17, 3, 'La Joya de los Sachas'),
    (59, 18, 3, 'Puyo'), -- Canton Pastaza
    (60, 18, 3, 'Mera'),
    (61, 19, 3, 'Quito'), -- Canton Pichincha
    (62, 19, 3, 'Rumiñahui'),
    (182, 19, 3, 'Mejía'),
    (63, 20, 3, 'Santa Elena'), -- Canton Santa Elena
    (64, 20, 3, 'La Libertad'),
    (65, 21, 3, 'Santo Domingo'), -- Canton Santo Domingo de los Tsáchilas
    (66, 21, 3, 'La Concordia'),
    (67, 22, 3, 'Lago Agrio'), -- Canton Sucumbíos
    (68, 22, 3, 'Gonzalo Pizarro'),
    (69, 23, 3, 'Ambato'), -- Canton Tungurahua
    (70, 23, 3, 'Baños de Agua Santa'),
    (71, 24, 3, 'Zamora'), -- Canton Zamora Chinchipe
    (72, 24, 3, 'Yantzaza');
-- Inserta ciudades por cada cantón
INSERT INTO Ubicacion (UbicacionId, UbicacionIdPadre, UbicacionTipoId, Ubicacion)
VALUES
    (73, 25, 4, 'Cuenca'),      -- Ciudades del Canton Azuay
    (74, 25, 4, 'Girón'), 
    (75, 27, 4, 'Guaranda'),        -- Ciudades del Canton Bolívar
    (76, 27, 4, 'San Miguel de Bolívar'), 
    (77, 29, 4, 'Azogues'),         -- Ciudades del Canton Cañar
    (78, 29, 4, 'Biblián'), 
    (79, 31, 4, 'Tulcán'),      -- Ciudades del Canton Carchi
    (80, 31, 4, 'Montúfar'), 
    (81, 33, 4, 'Riobamba'),        -- Ciudades del Canton Chimborazo
    (82, 33, 4, 'Guano'), 
    (83, 35, 4, 'Latacunga'),       -- Ciudades del Canton Cotopaxi
    (84, 35, 4, 'Saquisilí'), 
    (85, 37, 4, 'Machala'),         -- Ciudades del Canton El Oro
    (86, 37, 4, 'Pasaje'), 
    (87, 39, 4, 'Esmeraldas'),      -- Ciudades del Canton Esmeraldas
    (88, 39, 4, 'Atacames'), 
    (89, 41, 4, 'Puerto Baquerizo Moreno'),         -- Ciudades del Canton Galápagos
    (90, 41, 4, 'Puerto Ayora'), 
    (91, 43, 4, 'Guayaquil'),       -- Ciudades del Canton Guayas
    (92, 43, 4, 'Daule'), 
    (93, 45, 4, 'Ibarra'),      -- Ciudades del Canton Imbabura
    (94, 45, 4, 'Otavalo'), 
    (95, 47, 4, 'Loja'),        -- Ciudades del Canton Loja
    (96, 47, 4, 'Catamayo'), 
    (97, 49, 4, 'Babahoyo'),        -- Ciudades del Canton Los Ríos
    (98, 49, 4, 'Quevedo'), 
    (99, 51, 4, 'Portoviejo'),      -- Ciudades del Canton Manabí
    (100, 51, 4, 'Manta'), 
    (101, 53, 4, 'Macas'),      -- Ciudades del Canton Morona Santiago
    (102, 53, 4, 'Gualaquiza'), 
    (103, 55, 4, 'Tena'),       -- Ciudades del Canton Napo
    (104, 55, 4, 'Quijos'), 
    (105, 57, 4, 'Orellana'),       -- Ciudades del Canton Orellana
    (106, 57, 4, 'La Joya de los Sachas'), 
    (107, 59, 4, 'Puyo'),       -- Ciudades del Canton Pastaza
    (108, 59, 4, 'Mera'), 
    (109, 61, 4, 'Quito'),      -- Ciudades del Canton Pichincha
    (110, 61, 4, 'Rumiñahui'), 
    (111, 63, 4, 'Santa Elena'),        -- Ciudades del Canton Santa Elena
    (112, 63, 4, 'La Libertad'), 
    (113, 65, 4, 'Santo Domingo'),      -- Ciudades del Canton Santo Domingo de los Tsáchilas
    (114, 65, 4, 'La Concordia'), 
    (115, 67, 4, 'Lago Agrio'),         -- Ciudades del Canton Sucumbíos
    (116, 67, 4, 'Gonzalo Pizarro'), 
    (117, 69, 4, 'Ambato'),         -- Ciudades del Canton Tungurahua
    (118, 69, 4, 'Baños de Agua Santa'), 
    (119, 71, 4, 'Zamora'),         -- Ciudades del Canton Zamora Chinchipe
    (120, 71, 4, 'Yantzaza');

-- Insertar datos en la tabla "AreaAcreditacion-Actividad"
INSERT INTO AreaAcreditacion (AreaAcreditacionId, Nombre)
VALUES
    (1, 'LABORATORIOS'),
    (2, 'CERTIFICACIÓN'),
    (3, 'INSPECCIÓN'),
    (4, 'VALIDACIÓN Y VERIFICACIÓN');
INSERT INTO AreaAcreditacionActividad (AreaAcreditacionActividadId, AreaAcreditacionId, Nombre)
VALUES
    -- LABORATORIOS
    (1, 1, 'CALIBRACIÓN'),
    (2, 1, 'CLÍNICOS'),
    (3, 1, 'INVESTIGACIÓN Y CONTROL DE CALIDAD'),
    (4, 1, 'PROVEEDORES DE ENSAYOS DE APTITUD'),
    (5, 1, 'ENSAYOS'),
    -- CERTIFICACIÓN
    (6, 2, 'SISTEMAS DE GESTIÓN DE CALIDAD'),
    (7, 2, 'PERSONAS'),
    (8, 2, 'PRODUCTOS'),
    (9, 2, 'SISTEMAS DE INOCUIDAD ALIMENTARIA'),
    (10, 2, 'SISTEMAS DE GESTIÓN AMBIENTAL'),
    (11, 2, 'SISTEMAS DE GESTIÓN ANTISOBORNO'),
    (12, 2, 'SISTEMAS DE GESTIÓN PARA LA CALIDAD DE DISPOSITIVOS MÉDICOS'),
    -- INSPECCIÓN
    (13, 3, 'INSPECCIÓN'),
    (14, 3, 'AMBIENTAL'),
    (15, 3, 'AGRÍCOLA'),
    -- VALIDACIÓN Y VERIFICACIÓN
    (16, 4, 'VERIFICACIÓN');

INSERT INTO AREA (AreaAcreditacionId, AreaAcreditacionActividadId, AreaEstado, AreaCodigoAcreditacion, UbicacionId, AreaRazonSocial)
VALUES
(1, 1, 'Acreditado', 'SAE LC 22-004', 91, 'CENTAURO LOGÍSTICA ECUADOR LOGINCEN S.A.'),
(1, 1, 'Acreditado', 'SAE LC 14-001', 91, '2 METROLÓGICA CERTMETROL C.A.'),
(1, 1, 'Acreditado', 'SAE LC 24-001', 91, 'COMPUHELP S.A.'),
(1, 1, 'Acreditado', 'SAE LC 23-003', 91, 'CSMETROIN S.A.S.'),
(1, 1, 'Acreditado', 'SAE LC 07-009', 91, 'METROLAB S.A.'),
(1, 1, 'Acreditado', 'SAE LC 23-002', 91, 'SISTAGROSA SA'),
(1, 1, 'Acreditado', 'SAE LC 10-009', 91, 'ELICROM CIA. LTDA.'),
(1, 1, 'Acreditado', 'SAE LC 17-004', 91, 'SERVICIO LATINOAMERICANO DE METROLOGIA SERLAM S.A.'),
(1, 1, 'Acreditado', 'SAE-ACR-0041-202', 91, 'RIVALESA S.A.'),
(1, 1, 'Acreditado', 'SAE LC 10-005', 109, 'AGRUPAMIENTO DE COMUNICACIONES Y GUERRA ELECTRÓNICA DE LA FUERZA TERRESTRE'),
(1, 1, 'Acreditado', 'SAE LC 20-001', 109, 'ALVAREZ LARREA EQUIPOS MEDICOS ALEM CIA. LTDA.'),
(1, 1, 'Acreditado', 'SAE LC 20-003', 109, 'CALPELAB cíA. LTDA.'),
(1, 1, 'Acreditado', 'SAE LC 21-001', 109, 'DEGSO CIA. LTDA'),
(1, 1, 'Acreditado', 'SAE LC 19-001', 109, 'COMPAÑIA DE AUTOMATIZACIONINDUSTRIAL EUROINSTRUMENTSINGENIERIA CIA. LTDA.'),
(1, 1, 'Acreditado', 'SAE LC 22-003', 109, 'EMCOMEDICAL ELECTRONICA MEDICA Y CONTROL CIA. LTDA'),
(1, 1, 'Acreditado', 'SAE LC 18-004', 109, 'EMPRESA PÚBLICA METROPOLITANA DE AGUA POTABLE Y SANEAMIENTO'),
(1, 1, 'Acreditado', '', 109, 'GSTINGENIERÍA S.A.'),
(1, 1, 'Acreditado', 'SAE LC 22-001', 109, 'LABORATORIO DE ENERGÍA Y 1 DEL ECUADOR LABENERGY CÍA. LTDA'),
(1, 1, 'Acreditado', 'SAE LC 23-001', 109, 'LABORATORIO DE METROLOGÍA INDUSTRIAL LAB-METRO CÍA. LTDA.'),
(1, 1, 'Acreditado', 'SAE LC 17-002', 109, 'LABORATORIO METROSENS CIA. LTDA.'),
(1, 1, 'Acreditado', 'SAE LC 21-002', 109, 'LUIS EDUARDO ALBAN LOPEZ LEAL IMPORTACIONES COMPAÑIA LIMITADA'),
(1, 1, 'Acreditado', 'SAE LC 10-004', 109, 'METROLOGIC S.A.'),
(1, 1, 'Acreditado', 'SAE LC 17-001', 109, 'METROLOGOS ASOCIADOS DEL ECUADOR COMPAÑIA DE 1.'),
(1, 1, 'Acreditado', 'SAE LC 16-002', 109, 'MINGA S.A.'),
(1, 1, 'Acreditado', 'SAE LC 15-002', 109, 'PINPREXAT PRECISION EXACTITUD SOPORTE Y AUTOMATIZACION CIA. LTDA.'),
(1, 1, 'Acreditado', 'SAE LC 14-002', 109, 'PRECISION Y CONTROL PRECITROL S.A.'),
(1, 1, 'Acreditado', 'SAE LC 15-001', 109, 'SECALMET SOLUCIONES ESPECIALIZADAS EN CALIDAD Y METROLOGÍA CÍA. LTDA.'),
(1, 1, 'Acreditado', 'SAE LC 23-004', 65, '2 PROFESIONAL DE INSTRUMENTOS CEPROINSA S.A.S.'),
(1, 1, 'Acreditado', 'SAE LC 16-001', 57, 'SEROIL INTRUMENTS CIA LTDA'),
(1, 1, 'Acreditado', 'SAE LC 22-002', 57, 'SOLUCIONES INDUSTRIALES Y METROLOGICAS METRICSERV CIA. LTDA'),
(1, 5, 'Acreditado', 'SAE LEN 16-013', 109, 'ABGES LABORATORIO ANALITICO AMBIENTAL CIA. LTDA.'),
(1, 5, 'Acreditado', 'SAE LEN 07-001', 109, 'ABRUS INGENIERÍA Y MEDIO AMBIENTE CÍA. LTDA.'),
(1, 5, 'Acreditado', 'SAE LEN 22-013', 81, 'ACERIA DEL ECUADOR CA ADELCA'),
(1, 5, 'Acreditado', 'SAE LEN 23-008', 109, 'ACLAB ASESORÍA, CONSULTORÍA Y LABORATORIO AMBIENTAL S.A.S. B.I.C.'),
(1, 5, 'Acreditado', 'SAE LEN 15-009', 109, 'AGENCIA DE REGULACIÓN Y CONTROL DE ENERGÍA Y RECURSOS NATURALES NO RENOVABLES'),
(1, 5, 'Acreditado', 'SAE LEN 09-003', 109, 'AGENCIA DE REGULACIÓN Y CONTROL FITO Y ZOOSANITARIO'),
(1, 5, 'Acreditado', 'SAE LEN 19-014', 109, 'AGRARPROJEKT S.A'),
(1, 5, 'Acreditado', 'SAE LEN 20-002', 180, 'AGRIPAC S.A'),
(1, 5, 'Acreditado', 'SAE LEN 21-007', 180, 'AGROANALISIS S.A.'),
(1, 5, 'Acreditado', 'SAE LEN 10-006', 109, 'AGRUPAMIENTO DE COMUNICACIONES Y GUERRA ELECTRÓNICA DE LA FUERZA TERRESTRE'),
(1, 5, 'Acreditado', 'SAE LEN 05-005', 109, 'ALSECU S.A.'),
(1, 5, 'Acreditado', 'SAE LEN 19-009', 109, 'AMBIENLAB SERVICIOS AMBIENTALES Y LABORALES CÍA. LTDA.'),
(1, 5, 'Acreditado', 'SAE-LEN-06-013', 109, 'AMBIGEST GESTION AMBIENTAL CIA LTDA'),
(1, 5, 'Acreditado', 'SAE LEN 22-010', 109, 'AMBIGETEC LABORATORIO AMBIENTAL Y GESTIÓN TÉCNICA CÍA. LTDA. B.I.C.'),
(1, 5, 'Acreditado', 'SAE LEN 13-006', 109, 'ANALITICA AVANZADA - ASESORIA Y 1 ANAVANLAB CIA. LTDA.'),
(1, 5, 'Acreditado', 'SAE LEN 19-013', 109, 'ASOCIACION HOLSTEIN FRIESIAN DEL ECUADOR'),
(1, 5, 'Acreditado', 'SAE LEN 10-016', 109, 'ASSAYLAB CIA LTDA'),
(1, 5, 'Acreditado', 'SAE LEN 21-003', 180, 'BIOFACTOR S.A.'),
(1, 5, 'Acreditado', 'SAE LEN 20-004', 180, 'BRITRANSFORMADORES S.A.'),
(1, 5, 'Acreditado', 'SAE LEN 19-007', 183, 'ALBEXXUS CÍA. LTDA.'),
(1, 5, 'Acreditado', 'SAE LEN 20-010', 37, 'AGUIBULAB S.A'),
(1, 5, 'Acreditado', 'SAE LEN 14-003', 91, 'AGENCIA NACIONAL DE REGULACIÓN, CONTROL Y VIGILANCIA SANITARIA - ARCSA, DOCTOR LEOPOLDO IZQUIETA PEREZ'),
(1, 5, 'Acreditado', 'SAE LEN 19-015', 91, 'AGRORUM S.A.'),
(1, 5, 'Acreditado', 'SAE LEN 17-014', 91, 'ACERÍAS NACIONALES DEL ECUADOR SOCIEDAD ANÓNIMA A.N.D.E.C.'),
(1, 5, 'Acreditado', 'SAE LEN 05-004', 91, 'AVILÉS Y VÉLEZ AVVE LABORATORIO DE ANÁLISIS DE ALIMENTOS S.A.'),
(1, 5, 'Acreditado', 'SAE LC 20-002', 91, 'BUREAU VERITAS ECUADOR S.A.'),
(1, 5, 'Acreditado', 'SAE LEN 14-009', 57, 'AQLAB 1 ACOSTA Y COMPAÑIA'),
(1, 5, 'Acreditado', 'SAE LEN 15-011', 57, 'ANDES PETROLEUM ECUADOR LTD.'),
(1, 5, 'Acreditado', 'SAE LEN 08-006', 67, 'CALEB BRETT ECUADOR S.A.'),
(1, 5, 'Acreditado', '', 33, 'BMTLAB 1 E INGENIERIA SOCIEDAD POR ACCIONES SIMPLIFICADA'),
(1, 2, 'Acreditado', 'SAE LCL 17-002', 109, 'CENTRO CLINICO QUIRURGICO AMBULATORIO HOSPITAL DEL DIA CENTRAL 109'),
(1, 2, 'Acreditado', 'SAE LCL 14-001', 109, 'CORPORACION MEDICA PAZMIÑO NARVAEZ CIA LTDA'),
(1, 2, 'Acreditado', 'SAE LCL 17-003', 91, 'INSTITUTO NACIONAL DE INVESTIGACIÓN EN SALUD PUBLICA INSPI DR. LEOPOLDO IZQUIETA PÉREZ'),
(1, 2, 'Acreditado', 'SAE LCL 17-001', 91, 'INTERNATIONAL LABORATORIES SERVICES INTERLAB S.A.'),
(1, 2, 'Acreditado', 'SAE LCL 15-001', 91, 'LAB CENTRO ILLINGWORTH (LCI) S.A.'),
(1, 2, 'Acreditado', 'SAE LCL 16-001', 33, 'LABORATORIO CLINICO E HISTOPATOLOGICO SUCRE'),
(1, 2, 'Acreditado', 'SAE LCL 17-004', 37, 'LABORATORIO CLÍNICO SOLIDARIO LOGROÑO & MUÑOZ CIA. LTDA.'),
(1, 2, 'Acreditado', 'SAE LCL 14-002', 109, 'SYNLAB SAS'),
(1, 3, 'Acreditado', 'SAE LEN 12-001', 109, 'CENTRO DE SOLUCIONES ANALITICAS INTEGRALES CENTROCESAL CIA LTDA.'),
(1, 4, 'Acreditado', 'SAE-ACR-0125-2022', 109, 'PONTIFICIA UNIVERSIDAD CATOLICA DEL ECUADOR'),
(4, 16, 'Acreditado', 'SAE OVV 23-001', 91, 'S.G.S. DEL ECUADOR S.A.'),
(2, 8, 'Acreditado', 'SAE CP 21-001', 109, 'ASIAMBUSINESS DEL ECUADOR S.A.'),
(2, 8, 'Acreditado', 'SAE CP 14-001', 109, 'BUREAU VERITAS ECUADOR S.A.'),
(2, 8, 'Acreditado', 'SAE CP 07-003', 109, 'CERTIFICADORA ECUATORIANA DE ESTANDARES CERESECUADOR CIA. LTDA.'),
(2, 8, 'Acreditado', 'SAE CP 19-002', 109, 'CONSERVACION Y DESARROLLO CYDCERTIFIED S.A.'),
(2, 8, 'Acreditado', 'SAE CP 11-002', 109, 'ICEA ECUADOR'),
(2, 8, 'Acreditado', 'SAE CP 15-001', 109, 'LENOR ECUADOR CIA. LTDA.'),
(2, 8, 'Acreditado', 'SAE CP 22-002', 109, 'MAYACERT S.A.S.'),
(2, 8, 'Acreditado', 'SAE CP 10-001', 109, 'QUALITY CERTIFICATION SERVICES CERTIFICACIONES DEL ECUADOR QCS CIA. LTDA.'),
(2, 8, 'Acreditado', 'SAE CP 14-004', 109, 'SERVICIO ECUATORIANO DE NORMALIZACION'),
(2, 8, 'Acreditado', 'SAE CP 19-001', 109, 'SERVICIOS DE INGENIERIA DE CALIDAD SICALECUADOR S.A.'),
(2, 8, 'Acreditado', 'SAE CP 22-001', 62, 'PCN DEL ECUADOR PDE CIA. LTDA.'),
(2, 8, 'Acreditado', 'SAE CP 07-002', 33, 'KIWA BCS ECUADOR CIA. LTDA.'),
(2, 8, 'Acreditado', '', 91, 'INTERTEK INTERNATIONAL LIMITED'),
(3, 13, 'Acreditado', 'SAE OI 23-005', 91, 'ADEMINSA DEL ECUADOR S.A.'),
(3, 13, 'Acreditado', 'SAE OI 22-010', 91, 'ASIASURVEYORS S.A.'),
(3, 13, 'Acreditado', 'SAE OI 11-003', 91, 'CALEB BRETT ECUADOR S.A.'),
(3, 13, 'Acreditado', 'SAE OI 19-002', 91, 'CONSORCIO SGS - REVISIONES TÉCNICAS'),
(3, 13, 'Acreditado', 'SAE OI 13-015', 91, ''),
(3, 13, 'Acreditado', 'SAE OI 17-014', 91, 'ELITE SURVEY ELISURV S.A'),
(3, 13, 'Acreditado', 'SAE OI 18-027', 33, 'CEDINAP CENTRO DE INGENIERIA APLICADA PACE & SINCON CIA.LTDA.'),
(3, 13, 'Acreditado', 'SAE OI 22-004', 37, 'ASOCIACION DE SERVICIOS AGRICOLAS GREEN FORCE FUERZA VERDE ASOSERAGREFOR'),
(3, 13, 'Acreditado', 'SAE OI 15-005', 25, 'CONSORCIO REVISION VEHICULAR DANTON'),
(3, 13, 'Acreditado', 'SAE OI 19-005', 109, 'ACOSSAND GESTOR AMBIENTAL CIA. LTDA.'),
(3, 13, 'Acreditado', 'SAE OI 12-006', 109, 'AENORECUADOR S.A.'),
(3, 13, 'Acreditado', 'SAE OI 22-002', 109, 'ALLIANZ QUALITÁ CIA. LTDA.'),
(3, 13, 'Acreditado', 'SAE OI 15-001', 109, 'AMSPEC DE ECUADOR S.A.'),
(3, 13, 'Acreditado', 'SAE OI 13-020', 109, 'ASIAMBUSINESS DEL ECUADOR S.A'),
(3, 13, 'Acreditado', 'SAE OI 15-006', 109, 'BCIBUREAU CERTIFICACION & 13 DE EQUIPOS S.A.'),
(3, 13, 'Acreditado', 'SAE OI 22-007', 109, 'BEST-INSPECTION S A'),
(3, 13, 'Acreditado', 'SAE OI 13-013', 109, 'BUREAU VERITAS ECUADOR S.A.'),
(3, 13, 'Acreditado', 'SAE OI 13-005', 109, 'CAMINCARGO CONTROL ECUADOR S.A.'),
(3, 13, 'Acreditado', 'SAE OI 18-024', 109, 'CENTRO DE INFORMACION INTERNACIONAL EMPRESARIAL SOSTENIBLE CIIESOST S.A.'),
(3, 13, 'Acreditado', 'SAE OI 23-001', 109, 'COMPAÑIA CERTIFICADORA DE ALIMENTOS Y ALMACENAMIENTO CHENOA COAACH S.A.S.'),
(3, 13, 'Acreditado', 'SAE OI 22-006', 109, 'CRANES ENGINEERING & OPERATOR ECUADOR CIA. LTDA.'),
(3, 13, 'Acreditado', 'SAE OI 20-004', 109, 'DANTON S.A.'),
(3, 13, 'Acreditado', 'SAE OI 16-012', 109, 'DE LA TORRE ASESORIA ALIMENTARIA AGDR CIA LTDA'),
(3, 13, 'Acreditado', 'SAE OI 21-003', 109, 'ECI PRUEBAS&13 CIA. LITDA.'),
(3, 13, 'Acreditado', 'SAE OI 14-001', 109, 'EMPRESA PÚBLICA DE BIENES Y SERVICIOS UCE PROYECTOS EP'),
(3, 13, 'Acreditado', 'SAE OI 16-004', 109, 'ENGIPETROL S.A.'),
(3, 13, 'Acreditado', 'SAE OI 17-001', 109, '5 NO DESTRUCTIVOS DEL ECUADOR ENDE CIA. LTDA.'),
(3, 13, 'Acreditado', 'SAE OI 20-003', 109, 'ESTANDARES NORMATIVAS & 13 ENII SA'),
(3, 13, 'Acreditado', 'SAE OI 16-001', 58, 'ECUASUPERVISIONS S.A. ECUASUPERSA'),
(3, 13, 'Acreditado', 'SAE OI 16-010', 57, 'COMPAÑIA TECNOLOGIA Y PETROLEO TECNOLPET S.A.');
GO

CREATE VIEW vwBusqueda AS
SELECT 
    a.AreaId,
    aa.Nombre                       Acreditacion,
    aaa.Nombre                      AcreditacionActividad,
    u.Ubicacion                     Ubicacion,
    ut.UbicacionTipo                UbicacionTipo,
    a.AreaEstado                    Estado,
    a.AreaCodigoAcreditacion        CodigoAcreditacion,
    a.AreaRazonSocial               RazonSocial
FROM    Area                        a
JOIN    Ubicacion                   u   ON a.UbicacionId                    = u.UbicacionId
JOIN    UbicacionTipo               ut  ON u.UbicacionTipoId                = ut.UbicacionTipoId
JOIN    AreaAcreditacion            aa  ON a.AreaAcreditacionId             = aa.AreaAcreditacionId
JOIN    AreaAcreditacionActividad   aaa ON a.AreaAcreditacionActividadId    = aaa.AreaAcreditacionActividadId;
GO
SELECT * FROM Area;
SELECT * FROM UbicacionTipo;
SELECT * FROM Ubicacion;
SELECT * FROM AreaAcreditacionActividad;
SELECT * FROM AreaAcreditacion;
SELECT * FROM vwBusqueda;