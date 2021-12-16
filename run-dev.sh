#!/bin/bash

# Starts the project locally with just the SQL server container running.
echo -e "\nStarting up your development services...\n"

if [ $(docker container list | grep -o sql_server) ]
then
    echo -e "SQL Server container is already running...\n"
else
    docker run \
        -e "ACCEPT_EULA=Y" \
        -e "SA_PASSWORD=msSQLpass123" \
        -p 1433:1433 \
        --name sql_server \
        -h sqlserver \
        -v /tmp/mssql:/var/opt/mssql3 \
        -d \
        mcr.microsoft.com/mssql/server:2019-latest
fi

cd Server && dotnet watch run