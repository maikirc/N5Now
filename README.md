# N5Now
Inicializar Base de Datos MS Sql Server
=======================================
Cargar el respaldo N5NowBackup ubicado en la carpeta \\N5Now\N5NowApiBD, o ejecutar los scripts ubicados en la misma carpeta en el siguiente orden:
1. DATABASE.sql
2. TABLES.sql
3. INSERT.sql

Inicializar N5Now.Api
=====================
Modificar las siguientes keys del archivo appsettings.json:
1. Serilog:WriteTo:path
2. ConnectionStrings:N5Now

Inicializar N5Now.PermissionConsumer
====================================
Modificar las siguientes keys del archivo appsettings.json:
1. Serilog:WriteTo:path
2. ConnectionStrings:N5Now
3. Kafka:BootstrapServers

Inicializar N5Now.PermissionProducer
====================================
Modificar las siguientes keys del archivo appsettings.json:
1. Serilog:WriteTo:path
2. Kafka:BootstrapServers

Forma de Consumir 1:
====================
1. Front - End (envia petición)
2. N5Now.Api (recibe petición y procesa)
3. N5Now.Service
4. N5Now.Logic
5. N5Now.Repository
6. N5Now.Data

Forma de Consumir 2:
====================
1. Front - End (envia petición)
2. N5Now.Producer (recibe petición y deposita el mensaje en la cola del elasticsearch por medio de Kafka)
3. N5Now.Consumer (lee el mensaje de la cola del elasticsearch por medio de Kafka y procesa)
4. N5Now.Service
5. N5Now.Logic
6. N5Now.Repository
7. N5Now.Data
