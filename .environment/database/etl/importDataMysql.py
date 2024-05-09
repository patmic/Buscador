import csv
import pymysql

def import_from_csv(host, port, user, password, database, table, input_file):
    connection = None
    
    try:
        # Conexión a la base de datos MySQL
        connection = pymysql.connect(host=host,
                                     port=port,
                                     user=user,
                                     password=password,
                                     database=database)

        cursor = connection.cursor()

        # Trunca la tabla existente para limpiar los datos
        cursor.execute(f"TRUNCATE TABLE {table}")

        # Lee los datos del archivo CSV
        with open(input_file, 'r', newline='', encoding='utf-8') as csvfile:
            reader = csv.DictReader(csvfile)
            
            # Inserta cada fila en la tabla
            for row in reader:
                columns = ', '.join(row.keys())
                values = ', '.join([f"'{value}'" for value in row.values()])
                insert_query = f"INSERT INTO {table} ({columns}) VALUES ({values})"
                cursor.execute(insert_query)
                
        connection.commit()
        print(f"Los datos se han importado correctamente desde '{input_file}' a la tabla '{table}' en la base de datos '{database}'.")
        
    except Exception as e:
        connection.rollback()
        print(f"Error: {e}")
        
    finally:
        if connection:
            connection.close()

# Ejemplo de uso
import_from_csv(host='tu_host',
                port=3306,  # Puerto MySQL predeterminado
                user='tu_usuario',
                password='tu_contraseña',
                database='tu_base_de_datos',
                table='tu_tabla',
                input_file='datos_exportados.csv')
