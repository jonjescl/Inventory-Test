# Inventario App - Fullstack .NET + Angular

## 📋 Requisitos

- Node.js v22+
- Angular CLI v20+
- .NET 8 SDK
- SQL Server
- Visual Studio

---

## 🛠️ Backend (.NET)

### 🔧 Requisitos

- Visual Studio 2022 o superior
- .NET 8 SDK
- SQL Server 2019 o superior

### ▶️ Base de datos
1. Abre el script  `Inventory.sql` con Managment Studio.
2. Ejecutar y revisar que la base de datos se haya creado con éxito.
<img width="448" height="350" alt="image" src="https://github.com/user-attachments/assets/56a42d5e-ea72-4881-ab7c-55fcd68d347d" />


### ▶️ Ejecución del BackEnd

1. Abre `back/Inventory.sln` en Visual Studio.
2. Establece `ProductsService` y `TransactionsService` como proyectos de inicio múltiple.
3. Ejecuta con F5 o desde terminal:

```bash
cd back/ProductServices
dotnet run

cd ../TransactionServices
dotnet run
```
### ▶️ Ejecución del FrontEnd
1. Desde terminal escribe los comandos:

```bash
cd front/inventory-app
npm install
ng serve
```
2. Abre navegador de preferencia en la url: http://localhost:4200/
3. Encontrarás 2 menús: Productos y Transaciones
<img width="1761" height="275" alt="image" src="https://github.com/user-attachments/assets/23fd33bc-b6a3-4ac0-8646-c10bf78e4329" />

5. En el menú productos podrás registrar, ver el listado, eliminar o editar los registos
<img width="1789" height="418" alt="image" src="https://github.com/user-attachments/assets/6c846b48-463f-40d2-89ef-58e671336d0a" />
<img width="622" height="680" alt="image" src="https://github.com/user-attachments/assets/412e10a1-2692-493b-b318-0758b45981b3" />
<img width="1118" height="497" alt="image" src="https://github.com/user-attachments/assets/19948869-c0a8-4f34-9ed3-202f54b251e1" />

7. En el menú Transacciones podrás registrar, ver el listado, eliminar o editar los registosm adicionalmete ver los items que se compraron o vendieron
<img width="1744" height="375" alt="image" src="https://github.com/user-attachments/assets/9832569a-d9c1-4f73-926f-594c88cd8e37" />
<img width="1699" height="560" alt="image" src="https://github.com/user-attachments/assets/7be2bdb9-7416-49f8-a475-684649edacf0" />
<img width="1300" height="664" alt="image" src="https://github.com/user-attachments/assets/4fb741c4-1522-4b1e-909e-d7bad3bfeb3f" />
<img width="1741" height="556" alt="image" src="https://github.com/user-attachments/assets/bee68bf0-0b34-4834-95c4-a57f1fe0f641" />

