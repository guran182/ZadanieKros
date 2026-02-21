## Vypracovanie zadania - Hierarchia firmy

Ide o prvotnú verziu.

### Opis projektu

Napísaný je v C# pre .NET Framework 4.8 s využitím WinForms a na backende je MS SQL. 

Okno pozostáva zo Záložiek (Tabs), na ktorých sa nachádzajú DataGridViews.
Prvú záložku tvoria Zamestnanci, práve z dôvodu, že pre založenie firmy je nevyhnutné mať pre začiatok pridaného aspoň jedného zamestnanca - riaditeľa. Každý zamestnanec je identifikovaný svojim osobným číslom, ktoré je v databázovej tabuľke vedený pod názvom id.

Keď je pridáný prvý zamestnanec - riaditeľ, tak je možné pristúpiť ku založeniu firmy na ďalšej záložke.
Ako unikátny identifikátor bolo použité IČO, takto je označený aj PRIMARY KEY v databázovej tabuľke. Názov firmy musí byť unikátny. <del>Čo sa týka Firmy, tu zohrala rolu moja nerozhodnosť, že firiem by mohlo byť aj viac. Tak som ponechal BindingNavigator aj s akciami Pridať, Odstrániť. No, treba povedať, že vzťah N firiem a N divízií som nevytvoril. Taktiež by bolo nevyhnutné dorobiť vzťah Master/Detail pre ostatné záložky - teda ktorú firmu používateľ zaklikne pre tú sa vylistujú Divízie, Projekty a Oddelenia. </del>
Nakoniec som sa teda rozhodol, že firma smie byť len jedna v celom projekte.

Ďalšia záložka - Divízie. Princíp pozostáva stále rovnaký, teda DataGridView s BindingNavigátorom. Vedúci divízie je vyberaný pomocou rolovacieho zoznamu (ComboBoxu). Keďže v slovenských firmách je bežné, že jeden človek robí všetko, tak som neobmedzoval vedúcim koľkých divízií môže byť jeden človek. Každopádne, názov divízie musí byť unikátny. 

Nasleduje záložka Projekty, tu som znovu uplatnil pravidlo z Divízií, že jeden človek - môže byť vedúcim viacerých projektov. Názov projektu musí byť unikátny.

Posledná záložka sú Oddelenia. V tejto záložke som uplatnil Master/Detail vzťah. Teda ak kliknete na oddelenie, tak sa vylistujú v spodnom DataGridView priradení zamestnanci.

### Spustenie projektu

Postup:
1. Vytvorenie databázy pomocou skriptu umiestneným v Kros/DB.sql
2. Nastavenie správneho Connection Stringu (cs) v Properties projektu  
3. Vybratie verzie .NET Frameworku 4.8 vo vlastnostiach projektu.
4. Spustiť.

### Zamyslenia

Akciu uloženia (disketa) niesol iba prvý BindingNavigator v záložke Zamestnanci. Každý nasledujúci pridaný BindingNavigator ju už neobsahoval. Keď som ju chcel pridať ručne aj na ostatné Binding Navigátory, tak som narazil na problém, že som nevedel dohľadať ikonku pre ňu. Prím tu hrá môj perfekcionizmus, chcel som mať jednotný dizajn panelov. Ako najjednoduchšia možnosť sa mi zdala vytvorenie globálnych tlačidiel na spodku okna `Uložiť zmeny`, a `Zrušiť zmeny`.

Predtým som nikdy nepracoval s WinForms. Prekvapilo ma ako to majú prepracované. Drvivú väčšinu času som len  vyklikával. Môj kód tvorí možno ~20 riadkov C# kódu. Jadro programu tvorí práve databáza, nad ktorou som strávil najviac času.

### Screenshoty

<img width="667" height="443" alt="Zamestnanci" src="https://github.com/user-attachments/assets/a0f6509a-b9f2-49e3-8971-6c592d8af8de" />

<img width="666" height="444" alt="Firma" src="https://github.com/user-attachments/assets/6f2af682-4b78-4601-b06a-158c6901f45b" />

<img width="669" height="445" alt="Divizia" src="https://github.com/user-attachments/assets/39660599-bf6a-4118-bb25-a21082e5ec2a" />

<img width="669" height="445" alt="Projekty" src="https://github.com/user-attachments/assets/52333a77-95cd-4630-a062-7c9ca8a67e46" />

<img width="670" height="444" alt="Oddelenia" src="https://github.com/user-attachments/assets/25f940d0-dbe8-4ef5-a7e2-de9ea0da573e" />
