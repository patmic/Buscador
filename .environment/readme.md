
 image: mcr.microsoft.com/mssql/server:2019-CU3-ubuntu-18.04

estando en la carpeta .devcontainer se debe correr el comando para que ejecute la configuracion del docker-compose.yml
docker-compose up -d

 ------------------------------------
 docker volume ls


 Dato	Valor
hostname	localhost
port	1433
dbname	master
user	sa
pass	Password12345   pat_mic_DBKEY

------------------


        {
            "server": "localhost",
            "database": "master",
            "authenticationType": "SqlLogin",
            "user": "sa",
            "password": "",
            "emptyPasswordInput": false,
            "savePassword": true,
            "profileName": "DB_CAN",
            "encrypt": "Mandatory",
            "trustServerCertificate": true,
            "connectTimeout": 15,
            "commandTimeout": 30,
            "applicationName": "vscode-mssql"
        }




------------------


ingresar al contedor para validar la instancia

en un powershell
docker exec -it mssql-container /bin/bash

powerShell
docker exec -it mssql-container bash


/opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P 'pat_mic_DBKEY' -Q "SELECT @@VERSION"


docker exec -it mssql-container /bin/bash/opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P 'pat_mic_DBKEY' -Q "SELECT @@VERSION"

/opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P 'pat_mic_DBKEY' -Q "CREATE DATABASE SAE ON PRIMARY (NAME = 'SAE_data',
    FILENAME = '/var/opt/sqlserver/data/SAE.mdf',
    SIZE = 100MB,
    MAXSIZE = UNLIMITED,
    FILEGROWTH = 10MB)
LOG ON
    (NAME = 'SAE_log',
    FILENAME = '/var/opt/sqlserver/log/SAE.ldf',
    SIZE = 50MB,
    MAXSIZE = 200MB,
    FILEGROWTH = 5MB);
"
/opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P 'Password12345' -Q "SELECT name, physical_name FROM sys.master_files WHERE type_desc IN ('ROWS', 'LOG')"

/opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P 'Password12345' -Q "CREATE DATABASE SAE ON PRIMARY (NAME = 'SAE_data', FILENAME = '/var/opt/mssql/data/SAE.mdf', SIZE = 100MB, MAXSIZE = UNLIMITED, FILEGROWTH = 10MB) LOG ON (NAME = 'SAE_log', FILENAME = '/var/opt/mssql/data/SAE.ldf', SIZE = 50MB, MAXSIZE = 200MB, FILEGROWTH = 5MB);"





---------------------------------

version: '3.9'

networks:
  mssql-network:
    driver: bridge
  mysql-network:
    driver: bridge

volumes:
  mssqldata:
    driver: local
  sqldata:
    driver: local
  sqllog:
    driver: local
  sqlbackup:
    driver: local

services:
  mssql:
    image: mcr.microsoft.com/mssql/server:2019-CU3-ubuntu-18.04
    container_name: mssql-container
    env_file:
      - mssql.env
      - sapassword.env
    ports:
      - '1433:1433'
    networks:
      - mssql-network
    restart: always
    volumes:
      - mssqldata:/var/opt/mssql/
      - sqldata:/var/opt/sqlserver/data
      - sqllog:/var/opt/sqlserver/log
      - sqlbackup:/var/opt/sqlserver/backup
      - /c/docker/shared:/usr/shared

  mysql:
    image: mysql:latest
    container_name: mysql-container
    environment:
      MYSQL_ROOT_PASSWORD: "pat_mic_DBKEY"
      MYSQL_DATABASE: "PERU"
    ports:
      - "3306:3306"
    volumes:
      - ./mysql-data:/var/lib/mysql:rw
    networks:
      - mysql-network

-----------------------------

sqlite:
    image: nouchka/sqlite3:latest alpine:latest
    container_name: sqlite-container
    command: sh -c "apk add --no-cache sqlite && mkdir /db && cp /Colombia.sqlite /db/Colombia.sqlite && sqlite3 /db/Colombia.sqlite"
    # ports:
    #   - '1433:1433'
    volumes:
      - ./Colombia.sqlite:/Colombia.sqlite
      - ./sqlite-data:/db
    networks:
      - sqlite-network
- -------------------------------------------------------------------
entrar como root;

docker exec -u 0 -it mssql-container bash
apt -y install mssql-server-fts
systemctl restart mssql-server
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P pat_mic_DBKEY -Q "SHUTDOWN WITH NOWAIT"
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P pat_mic_DBKEY


docker exec -u 0 -it mssql-container bash
apt-get update
apt -y install mssql-server mssql-tools unixodbc-dev
apt-get install -y mssql-server-fts
apt-get update && apt-get install -y mssql-server-fts

/opt/mssql/bin/mssql-conf setup
/opt/mssql/bin/mssql-conf list

dpkg -l | grep mssql-server-fts
apt-get update && apt-get install -y mssql-server-fts


apt-get install -yq curl apt-transport-https gnupg
# Get official Microsoft repository configuration
curl https://packages.microsoft.com/keys/microsoft.asc | apt-key add - 
curl https://packages.microsoft.com/config/ubuntu/20.04/packages-microsoft-prod.deb --output packages-microsoft-prod.deb && dpkg -i packages-microsoft-prod.deb 
curl https://packages.microsoft.com/config/ubuntu/20.04/mssql-server-2022.list | tee /etc/apt/sources.list.d/mssql-server.list 
apt-get update  
# Install SQL Server from apt
    apt-get install -y mssql-server 
# Install optional packages
    apt-get install -y mssql-server-fts 
    ACCEPT_EULA=Y apt-get install -y mssql-tools  
    apt-get clean
    rm -rf /var/lib/apt/lists
# Run SQL Server process
CMD /opt/mssql/bin/sqlservr

/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P pat_mic_DBKEY -Q "SHUTDOWN WITH NOWAIT"


/opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P pat_mic_DBKEY -Q "EXEC sp_fulltext_service 'load_os_resources', 1;"

/opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P pat_mic_DBKEY


