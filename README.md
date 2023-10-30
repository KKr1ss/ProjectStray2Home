# Kóbor kereső (cím keresés alatt áll)

## Project leírása
Az kóbor keresőbe regisztárlva a felhasználóknak lehetősége van az elveszett állatok figyelésére, feltöltésére, és jelzésére. Az ország teljes területét lefedő alkalmazásban lehetősége lesz a felhasználónak, hogy az általa látogatott városokat/falvakat hozzáadva a profiljához értesítéseket kapjon a "környékén" elveszett állatokról.
Amennyiben egy kóbor állatot talált, feltöltheti annak akkori helyzetét, vagy akár ha ideiglenesen befogadta.

### Használt technológiák
- ASP.NET Core API - .NET 6.0 (LTS) Framework
- Angular v.15.2.9
- MS SQL

### Implementálásra váró szolgáltatások
- Cliens fejlesztése
- API befejezése


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