
# Api Productos

El repositorio contiene dos proyectos, APIProductos se desarrollo en NetCore y ProductsApp en NodeJS los dos proyectos implementan un API que se encarga de hacer gestion de productos.

Se debe ejecutar el script productost.sql que contiene toda la informacion de la base de datos con la tabla de productos y 4 procedimientos almacenados que se encargan del CRUD.

## Configuracion de ProductsApp

El API de NodeJS se desarrollo sobre la version 22.11.0 despues de clonar el repositorio ejecutamos el siguiente comando

```bash
  npm i
```

Se debe configurar la conexion a la base de datos y el puerto creando un enviroment en la ruta raiz del proyecto con las siguientes propiedades remplazando los valores segun la configuración de base de datos.

```
PORT=9000
DB_USER=USER
DB_PASSWORD=PASSWORD
DB_SERVER=SERVER(localhost)
DB_DATABASE=prueba_productos
```

Una vez creado el enviroment ejecutamos el siguiente comando para iniciar la aplicacion:
npm run dev

```bash
npm run dev
```

## Configuracion de ProductsApp

Para el proyecto de .Net se debe configurar la conexion a la base de datos en el archivo APIProductos\appsettings.json. 

Para la ejecucion de los test se puede utilizar el siguiente comando 

```bash
dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=cobertura
```

## Postman

El archivo ProductosApi.postman_collection.json contiene una coleccion de postman para realizar pruebas del api de .net y NodeJS
