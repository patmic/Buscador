import pyodbc
# Cadena de conexión a la base de datos
driver  = 'SQL Server'
server  = 'PAT-PC'
db1     = 'CAN_DB'
tcon    = 'yes'
uname   = 'pat_mic'
pword   = 'michael'

# Establecer la conexión
try:
    conn = pyodbc.connect(driver='{SQL Server}', host=server, database=db1, trusted_connection=tcon, user=uname, password=pword)
    cursor = conn.cursor()

    # Consulta SQL para seleccionar datos de la tabla Homologacion
    query = "SELECT * FROM Homologacion"

    # Ejecutar la consulta
    cursor.execute(query)

    # Obtener los resultados
    rows = cursor.fetchall()

    # Imprimir los resultados
    for row in rows:
        print(row)

    # Cerrar la conexión
    conn.close()

except pyodbc.Error as e:
    print("Error al conectar a la base de datos:", e)
