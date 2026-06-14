Dokumentace projektu: Kalkulačka bedniček
1. Zadání projektu
Cílem projektu je vytvořit konzolovou aplikaci v jazyce C#, která slouží jako kalkulačka a simulátor otevírání herních beden se skiny. Aplikace umožňuje uživateli načítat data o bednách z CSV souborů, analyzovat statistickou šanci na profit či ztrátu na základě cen jednotlivých skinů a simulovat náhodné otevírání zvoleného množství beden se započítáváním celkové finanční bilance (útrata vs. výdělek).
2. Jednoduchý model tříd a jejich vazeb
•	Truhla: Reprezentuje samotnou herní bednu, drží její název, pořizovací cenu a vypočítané souhrnné statistické pravděpodobnosti.
•	Item: Reprezentuje jednotlivé skiny/předměty, které uvnitř bedny mohou padnout (název, barva/vzácnost, cena a šance na drop).
Vazby mezi třídami:
•	Kompozice / Asociace: Třída Program vytváří a spravuje instanci třídy Truhla.
•	Agregace: Třída Program pracuje s dynamickým seznamem List<Item>, který reprezentuje vnitřní obsah aktuálně načtené bedny. Třída Truhla a Item jsou na sobě nezávislé, ale třída Program obě entity logicky propojuje při výpočtech.
3. Struktura aplikace
Tato sekce podrobně popisuje jednotlivé třídy, jejich vlastnosti (properties) a metody.
Třída Item
Reprezentuje jeden konkrétní skin v bedně.
•	Vlastnosti (Properties):
o	Name (string): Název skinu.
o	Color (string): Barva (vzácnost) skinu.
o	Cost (double): Hodnota skinu v dolarech.
o	Chance (double): Procentuální šance na získání předmětu.
Třída Truhla
Reprezentuje celou bednu a uchovává globální statistiky o úspěšnosti.
•	Vlastnosti (Properties):
o	Nazev (string): Název bedny (shodný s názvem souboru).
o	Cena_Bedny (double): Nákupní cena bedny.
o	Profit_Chance (double): Součet šancí předmětů, jejichž cena je vetší než cena bedny.
o	Lose_Chance (double): Součet šancí předmětů, jejichž cena je menší cena bedny.
o	Profit2_Chance (double): Šance na získání předmětu s hodnotou vyšší než dvojnásobek ceny bedny.
o	Mid_Chance (double): Šance na "udržení" hodnoty (cena předmětu je mezi 80 % až 100 % ceny bedny).
Třída Program
Hlavní statická třída, která pohání celou aplikaci.
•	Metody:
o	Main(string[] args): Vstupní bod programu, obsahuje hlavní herní smyčky (while) a textové menu (switch).
o	Over(): Pomocná metoda pro validaci uživatelského vstupu. Zajišťuje, že uživatel zadá korektní číselnou hodnotu (double.TryParse).
o	Profit(List<Item>, Truhla): Spočítá celkové procentuální šance (Profit, Lose, atd.) pro danou truhlu na základě položek v ní.
o	Barvicky(List<Item>): Vypíše kompletní seznam skinů v bedně a obarví text konzole podle vzácnosti (Color).
o	Otevreni(List<Item>): Algoritmus pro simulaci náhodného dropu jednoho předmětu na základě vah (procentuálních šancí) jednotlivých položek.
o	Simulace_Otevirani(List<Item>, double[], Truhla): Simuluje hromadné otevření X beden. Vypisuje padnuté itemy v barvách, počítá celkovou finanční bilanci (útrata vs. výdělek) a na konci zvýrazní nejdražší drop.
4. Popis práce se soubory
Aplikace využívá standardní jmenný prostor System.IO pro čtení a zápis textových dat ve formátu CSV.
Načítání dat (Čtení)
Při volbě načtení bedny uživatel zadá její název. Program se pokusí otevřít soubor {Nazev}.csv pomocí objektu StreamReader.
•	Každý řádek se očistí od uvozovek (.Replace("\"", "")) a rozdělí pomocí čárky jako oddělovače (.Split(",")) na pole textových řetězců.
•	Z těchto dat se naparsují hodnoty pro nový objekt Item, který se následně přidá do dynamického seznamu bedna. Parsování desetinných čísel probíhá invariantně (CultureInfo.InvariantCulture), aby se předešlo chybám kvůli rozdílům mezi českou čárkou a anglickou tečkou v desetinných číslech.
Ukládání statistik (Zápis)
V menu bedny je možnost uložit informace o aktuální analýze. Program vytvoří/přepíše soubor s názvem {Nazev}_info.csv pomocí objektu StreamWriter.
•	Zpětně zapíše hlavičku: Cena,Profit,Lose,Dvoj,Mid.
•	Na druhý řádek zapíše vypočítané statistické hodnoty oddělené čárkou, formátované opět pomocí CultureInfo.InvariantCulture.
5. Popis ovládání
Aplikace se kompletně ovládá v textovém rozhraní konzole pomocí zadávání číselných voleb.
1.	Úvodní nabídka: Po spuštění aplikace se zobrazí úvodní obrazovka, kde uživatel stisknutím klávesy 1 přejde do Hlavního menu.
2.	Hlavní menu:
o	1. Načtení nové bedny: Uživatel zadá přesný název souboru (bez přípony .csv) a následně cenu, za kterou se bedna kupuje. Pokud soubor existuje, data se nahrají do paměti.
o	2. Menu bedny: Zpřístupní se až po úspěšném nahrání bedny. Jinak aplikace uživatele upozorní červeným nápisem.
o	3. Úvodní Nabídka: Vrátí uživatele o krok zpět.
3.	Menu bedny: Zde se zobrazuje aktuální finanční stav (Útrata vs. Výdělek) a uživatel má na výběr z těchto možností:
o	1. Obsah bedny: Vypíše seznam všech skinů v truhle s jejich šancemi a cenami, přičemž text je obarven podle vzácnosti.
o	2. Šance z jedné bedny: Zobrazí matematickou pravděpodobnost v procentech, jaká je šance, že z jedné bedny budete v zisku, ve ztrátě, získáte dvojnásobek, nebo si udržíte rozpočet.
o	3. Simulace otevření X bedniček: Uživatel zadá, kolik beden chce otevřít. Konzole začne simulovat otevírání, vypisovat dropy v barvách a upraví celkovou útratu a výdělek. Na konci ukáže nejhodnotnější získaný předmět.
o	4. (reset balance): Vynuluje počítadlo útraty a výdělku na nulu.
o	5. Uložit informace o bedně: Exportuje statistiky z volby č. 2 do přehledného CSV souboru.
o	6. Hlavní menu: Návrat do předchozího menu, odkud je možné načíst jinou bednu

