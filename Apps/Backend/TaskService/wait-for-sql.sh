#!/bin/sh

echo "Waiting for SQL Server to be ready..."

until nc -z -v -w30 $POSTGRESQL_SERVER $POSTGRESQL_PORT
do
  echo $POSTGRESQL_SERVER
  echo "Waiting 30 seconds..."
  sleep 30
done

echo "SQL Server is up — starting service"
exec "$@"