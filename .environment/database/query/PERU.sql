-- Active: 1710399827327@@127.0.0.1@3306@PERU
-- Crear la tabla ORGANIZACION
CREATE TABLE ORGANIZACION (
    id INT AUTO_INCREMENT PRIMARY KEY,
    nombre VARCHAR(100),
    direccion VARCHAR(100),
    telefono VARCHAR(20),
    email VARCHAR(100)
);

-- Insertar 20 registros en la tabla ORGANIZACION
INSERT INTO ORGANIZACION (nombre, direccion, telefono, email) VALUES
('Empresa 1', 'Calle 123', '123456789', 'empresa1@example.com'),
('Empresa 2', 'Avenida 456', '987654321', 'empresa2@example.com'),
('Empresa 3', 'Plaza 789', '111222333', 'empresa3@example.com'),
('Empresa 4', 'Ruta 101', '444555666', 'empresa4@example.com'),
('Empresa 5', 'Camino 202', '777888999', 'empresa5@example.com'),
('Empresa 6', 'Autopista 303', '123123123', 'empresa6@example.com'),
('Empresa 7', 'Calle Principal', '456456456', 'empresa7@example.com'),
('Empresa 8', 'Avenida Central', '789789789', 'empresa8@example.com'),
('Empresa 9', 'Plaza Mayor', '000111222', 'empresa9@example.com'),
('Empresa 10', 'Ruta Secundaria', '333444555', 'empresa10@example.com'),
('Empresa 11', 'Camino de la Montaña', '666777888', 'empresa11@example.com'),
('Empresa 12', 'Avenida del Mar', '999000111', 'empresa12@example.com'),
('Empresa 13', 'Plaza del Sol', '222333444', 'empresa13@example.com'),
('Empresa 14', 'Calle del Cielo', '555666777', 'empresa14@example.com'),
('Empresa 15', 'Avenida de las Flores', '888999000', 'empresa15@example.com'),
('Empresa 16', 'Plaza de los Ángeles', '111222333', 'empresa16@example.com'),
('Empresa 17', 'Calle de los Sueños', '444555666', 'empresa17@example.com'),
('Empresa 18', 'Avenida de la Esperanza', '777888999', 'empresa18@example.com'),
('Empresa 19', 'Plaza de la Paz', '000111222', 'empresa19@example.com'),
('Empresa 20', 'Ruta de la Felicidad', '333444555', 'empresa20@example.com');

-- Crear la tabla CERTIFICADOS
CREATE TABLE CERTIFICADOS (
    id INT AUTO_INCREMENT PRIMARY KEY,
    nombre VARCHAR(100),
    fecha DATE,
    organizacion_id INT,
    FOREIGN KEY (organizacion_id) REFERENCES ORGANIZACION(id)
);

-- Insertar 20 registros en la tabla CERTIFICADOS
INSERT INTO CERTIFICADOS (nombre, fecha, organizacion_id) VALUES
('Certificado 1', '2024-01-01', 1),
('Certificado 2', '2024-01-02', 2),
('Certificado 3', '2024-01-03', 3),
('Certificado 4', '2024-01-04', 4),
('Certificado 5', '2024-01-05', 5),
('Certificado 6', '2024-01-06', 6),
('Certificado 7', '2024-01-07', 7),
('Certificado 8', '2024-01-08', 8),
('Certificado 9', '2024-01-09', 9),
('Certificado 10', '2024-01-10', 10),
('Certificado 11', '2024-01-11', 11),
('Certificado 12', '2024-01-12', 12),
('Certificado 13', '2024-01-13', 13),
('Certificado 14', '2024-01-14', 14),
('Certificado 15', '2024-01-15', 15),
('Certificado 16', '2024-01-16', 16),
('Certificado 17', '2024-01-17', 17),
('Certificado 18', '2024-01-18', 18),
('Certificado 19', '2024-01-19', 19),
('Certificado 20', '2024-01-20', 20);


SELECT * FROM ORGANIZACION;
SELECT * FROM CERTIFICADOS;


