# MovieRatingWithDatabase
Testing SQL, DataSets, Adaptors and Interfaces.

Calls imdb8.p.rapidapi.com to search for movies/tvshows.
Connected upto a MySQL80 database to store bookmarked items.

Uses a MySqlDataAdapter to interface with a MySQL80 Server.
Uses Passwords.end in build folder to store passwords and API keys.

Password.end should have the following format:

{
  "xrapidapikey": "",
  "SQLAccountName": "",
  "SQLPassword": "",
  "SQLAddress": "",
  "SQLPort": "",
  "SQLDatabase" : ""
}
