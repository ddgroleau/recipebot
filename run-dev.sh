#!/bin/bash

# Starts the project locally with just the SQL server container running.
echo -e "\nStarting up your development services...\n"

if [ $(docker container list | grep -o sql_server) ]
then
    echo -e "SQL Server container is already running...\n"
else
    echo -e "Starting your SQL Server container...\n"

    docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=msSQLpass123" -p 1433:1433 --name sql_server -h sqlserver -v /tmp/mssql:/var/opt/mssql3 -d mcr.microsoft.com/mssql/server:2019-latest
fi

echo -e "Starting the application...\n"

cd Server 

echo -e "Navigate to --> \n\e[1;4;32mhttp://localhost:4000\a\e\a"

dotnet watch run

