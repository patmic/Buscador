# pip install faker
from faker import Faker
import pyodbc

fake = Faker('es_ES')
conn = pyodbc.connect('DRIVER={SQL Server};SERVER=PAT-PC;DATABASE=CAN_DB;UID=pat_mic;PWD=pat_mic_PASS;TrustServerCertificate=True')
# conn = pyodbc.connect('DRIVER={SQL Server};SERVER=localhost;DATABASE=CAN;UID=SA;PWD=pat_mic_DBKEY;TrustServerCertificate=True')

cursor = conn.cursor()
for i in range(19000):
    Nombre      = str(fake.name())
    Direccion   = str(fake.address())
    Celular     = str(fake.phone_number())
    Fecha       = str(fake.date_of_birth()) 
    Email       = str(fake.email())
    CodigoPostal= str(fake.postcode())
    Localidad   = str(fake.city())
    street_address   = str(fake.street_address())

    # print(Nombre, Direccion, Celular, Fecha)
    cursor.execute("INSERT INTO Persona (Nombre, Direccion, Celular, Fecha) VALUES (?, ?, ?, ?)",
                    (Nombre, Direccion, Celular, Fecha))
    cursor.commit()

    cursor.execute("SELECT @@IDENTITY AS NewID;")
    new_id = cursor.fetchone()[0]
    organizacionFullText = Nombre +" "+ Direccion +" "+ Celular +" "+ Fecha +" "+ street_address +" "+ Localidad +" "+ CodigoPostal + " "+ Email
    
    print(new_id, organizacionFullText)
    cursor.execute("INSERT INTO organizacion (organizacionId, organizacionFullText) VALUES (?, ?)",
                   (new_id, organizacionFullText)) 
# Commit the changes
conn.commit()
cursor.close()
conn.close()