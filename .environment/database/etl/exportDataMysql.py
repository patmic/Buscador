import csv
import pymysql
# pip install pymysql
def export_to_csv(host, port, user, password, database, table, output_file):
    connection = None
    
    try:
        # Conexión a la base de datos MySQL
        connection = pymysql.connect(host=host,
                                     port=port,
                                     user=user,
                                     password=password,
                                     database=database,
                                     cursorclass=pymysql.cursors.DictCursor)

        cursor = connection.cursor()

        # Consulta SQL para seleccionar todos los datos de la tabla
        sql_query = f"SELECT * FROM {table}"

        cursor.execute(sql_query)

        # Obtener todos los resultados de la consulta
        rows = cursor.fetchall()

        # Nombre de las columnas
        field_names = [field[0] for field in cursor.description]

        # Escribir los datos en el archivo CSV
        with open(output_file, 'w', newline='', encoding='utf-8') as csvfile:  # Especifica la codificación utf-8
            writer = csv.DictWriter(csvfile, fieldnames=field_names)
            
            # Escribir el encabezado
            writer.writeheader()
            
            # Escribir los datos
            for row in rows:
                writer.writerow(row)
                
        print(f"Los datos de la tabla '{table}' se han exportado exitosamente a '{output_file}'.")
        
    except Exception as e:
        print(f"Error: {e}")
        
    finally:
        if connection:
            connection.close()

# Ejemplo de uso
# export_to_csv(host='24.199.115.134',
#               port=3306,
#               user='saevistas',
#               password='JMgC3baeQKzD18rl',
#               database='sae_db_prod',
#               table='accreditationoac_v',
#               output_file='accreditationoac_v.csv')

export_to_csv(host='24.199.115.134',
              port=3306,
              user='saevistas',
              password='JMgC3baeQKzD18rl',
              database='sae_db_prod',
              table='alcances_acreditados',
              output_file='alcances_acreditados.csv')

export_to_csv(host='24.199.115.134',
              port=3306,
              user='saevistas',
              password='JMgC3baeQKzD18rl',
              database='sae_db_prod',
              table='alcances_acreditados_nacionales',
              output_file='alcances_acreditados_nacionales.csv')

export_to_csv(host='24.199.115.134',
              port=3306,
              user='saevistas',
              password='JMgC3baeQKzD18rl',
              database='sae_db_prod',
              table='dataoa_v',
              output_file='dataoa_v.csv')