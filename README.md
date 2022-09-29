# CatalogoAranda

Prueba t�cnica de Arandasoft

#### Instrucciones de uso

1. Modificar el campo "LocalConnection" del grupo "ConnectionStrings" del archivo appsettings.json del proyecto CatalogoAranda.WebApi. Use su conexi�n a una base de datos tipo SQL Server (Se prob� con SQLExpress 2019).

2. Agregue la primera migraci�n en la consola del manejador de paquetes (Package Manager Console) de Visual Studio con el siguiente c�digo, habiendo seleccionado CatalogoAranda.Infrastructure como el proyecto por defecto (Default roject): 
`add-migration InitialMigration`
Tambi�n puede usar una terminal y dotnet-ef desde el directorio ra�z:
`dotnet ef migrations add InitialMigration -p CatalogoAranda.Infrastructure -s CatalogoAranda.WebApi -o Migrations`

3. Actualice la base de datos, que previamente configur� en el primer paso, con el siguiente comando en la consola del manejador de paquetes:
`database-update`
Tambi�n puede usar una terminal y dotnet-ef desde el directorio ra�z:
`dotnet ef database update -p CatalogoAranda.WebApi`

Tenga en cuenta que solo las operaciones de lectura se pueden ejecutar sin credenciales. Para crear, actualizar y borrar se necesita estar autenticado. En el proyecto se incluyen unas credenciales en el archivo CatalogoDbContext.cs, estas son:
Usuario: `admin`
Contrase�a: `adminPassword`
Para obtener un JSON web token ingrese estos datos en /api/authentication/IniciarSesion