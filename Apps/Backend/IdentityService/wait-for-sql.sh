#!/bin/sh

echo "Waiting for mySQL to be ready..."

until nc -z -v -w30 $SQLSERVER_SERVER $SQLSERVER_PORT
do
  echo $SQLSERVER_SERVER
  echo "Waiting 30 seconds..."
  sleep 30
done

echo "SQL Server is up — starting service"
exec "$@"