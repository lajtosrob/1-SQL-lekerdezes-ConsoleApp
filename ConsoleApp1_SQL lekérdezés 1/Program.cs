using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient; // nuget MySQL.DATA telepítés szükséges!!!
// See https://aka.ms/new-console-template for more information

string kapcsolatLeiro = "datasource=127.0.0.1;port=3306;database=hardver;username=root;password=;";
// - host
// - port, MySQL default:3306
// - database
// - username + password

MySqlConnection SQLkapcsolat = new MySqlConnection(kapcsolatLeiro);

// Kivételkezelés
try
{
	SQLkapcsolat.Open();
}
catch (MySqlException hiba)
{
	Console.WriteLine(hiba.Message);
	Environment.Exit(1); //konzol alkalmazás leállítása hibakód küldésével
}

string SQLLekerdezesKategoriakraRendezetten = "SELECT DISTINCT kategória FROM termékek ORDER BY kategória;";

MySqlCommand SQLparancs = new MySqlCommand(SQLLekerdezesKategoriakraRendezetten, SQLkapcsolat);

//Kritikus, TRY szükséges!
MySqlDataReader eredmenytOlvaso = SQLparancs.ExecuteReader();

//Kritikus, TRY szükséges!
while (eredmenytOlvaso.Read()) //A következő rekordot olvassa és visszajelez van-e még újabb rekord
{
	//A mező nevének, vagy sorszámának megadásával kapjuk meg annak értékét az aktuális rekordon belül
	Console.WriteLine(eredmenytOlvaso.GetString("Kategória"));
}

//Mindig lezárjuk az erőforrásokat, így az eredménytáblát is ha már nincs szükség rá
eredmenytOlvaso.Close();

//Lezárjuk az adatbáziskapcsolatot is, ha már nem akarunk további parancsokat végrehajtani
SQLkapcsolat.Close();
