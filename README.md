# MovieRatingWithDatabase
Testing SQL, DataSets, Adaptors and Interfaces.

Calls imdb8.p.rapidapi.com to search for movies/tvshows.
Connected upto a microsoft sql database to store bookmarked items.

Uses a TableAdaptor to interface with a Microsoft SQL Server on a localhost.
Uses Passwords.end in build folder to store passwords and API keys.

Password.end should have the following format:

{
  "xrapidapikey": "",
  "sqlAccountName": "",
  "sqlPassword": "",
  "sqlAddress "localhost" 
}

Currently only localhost is supported as a sqlAddress.
