export PGPASSWORD=$1
$2pg_restore -i -h localhost -p 5432 -U postgres -Fc -d postgis_locust -c -t locustreports -v $3
