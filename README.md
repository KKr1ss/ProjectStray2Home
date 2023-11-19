# Szociális platform a kóbor háziállatok bejelentéséhez/megtalálásához

## Project leírása
Célom, egy szociális platformként funkcionáló weboldal készítése ASP.NET Core és Angular felhasználásával, ahol a felhasználóknak lehetősége van az elveszett állataik, vagy az adott településén található kóbor, vagy akár befogadott háziállatok feltöltésére. Az oldal továbbá használható lehetne menhelyek számára is, amelyek ide feltöltve a befogadott kóbor állatokat egy központi helyet nyújthatnának a gazdáknak.

Szociális platform részén kiválasztható, hogy mely városokban szeretné a felhasználó figyelni az állatokat, így ezekről a településekről értesítést kaphat, amennyiben új állat került feltöltésre. Az állattal kapcsolatos” hirdetések” pedig kommentálhatóak, vagy észlelésük jelezhető az állat gazdájának, ha éppen szembe találkozna vele egy felhasználó.

Továbbá célkitűzésem egy Magyarországon hiánypótló szoftver megalkotása, amelyet az emberek használhatnának egész ország területén, a menhelyekkel egyetemben, így hazajuttatva a gazdáiktól elkóborolt háziállatokat.

### Használt technológiák
- ASP.NET Core API - .NET 6.0 (LTS) Framework
- Angular v.15.2.9
- MS SQL

### Implementálásra váró szolgáltatások
- Értesítések implementálása
- Szerver clean architectúrára való váltása


## Fejlesztési beállítások

### Előfeltételek

#### API
- [Visual Studio] és csomagjai:
    - ASP.NET and web development
    - Node.js development
- [MS SQL]
    - Egy kapcsolat localhost\\SQLEXPRESS néven. Amennyiben szükséges megváltoztatni, a beállítások a /server/ProjectStrayToHomeApi/appsettings.json mappában érhetők el.

#### Kliens

1. Telepítse a [Node.js] szoftvert, amely tartalmazza a [Node Package Manager][npm]-t.
2. Telepítse (globálisan) az Angular CLI-t:
```
npm install -g @angular/cli@15
```
3. A telepítés következtében navigáljon a project /Client/ProjectStray2Home mappájába, ahol a Command Promtba írja a következő parancsot a függőségek telepítéséhez:
```
npm install
```

### Projekt futtatása

1. A /server/ mappába navigálva indítsa el az .sln-t, majd futtassa a projektet. Vagy a /server/ProjectStrayToHomeApi/ mappában futtassa a következő parancsot:
```
dotnet run
```
2. A /client/ProjectStray2Home mappában futtassa a parancsot:
```
npm start
```


# Tesztelés
Fejlesztés alatt...

[Visual Studio]: https://visualstudio.microsoft.com/free-developer-offers/
[MS SQL]: https://www.microsoft.com/en-us/sql-server/sql-server-downloads
[node.js]: https://nodejs.org/
[npm]: https://www.npmjs.com/get-npm