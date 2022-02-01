# OrganizacnaStrukturaFirmy
REST API aplikácia pre manažovanie organizačnej štruktúry firmy

Spustenie Aplikácie:

	Priečinok OrganizacnaStrukturaAPI si otvorte vo VSCode
	
	Databázu si vytvorte pomocou skriptu v MS Sql Server Express - OrganizacnaStrukturaSqlScript.sql, alebo v termináli VSCode príkazom: dotnet ef database update --context CompaniesContext
	
	Aplikáciu spustíte príkazom dotnet run
	
	V subore CompanyStructure.postman_collection.json sú kolekcie do Postmanu, na prácu s aplikáciou, spolu so spustitelnymi testami

Ak by nefungovalo spojenie sql databázy, skontrolujte prosím ConnectionString v súbore appsettings.json

Cez PostMana môžeme vytvárať zamestnancov, upravovať a mazať. Týchto zamestnancov môžeme následne prideľovať ako vedúcich jednotlivých uzlov štruktúry.
Každému uzlu moôžeme prideliť Meno, ID vedúceho zamestnanca a ID vyššieho uzla. 

REST API bolo vytvorene ako zadanie ulohy pre externu firmu, testy boli vytvorene ako vystup z predmetu Zaklady testovania softveru - FRI
