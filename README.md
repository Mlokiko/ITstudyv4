# ITstudy

<h1>instrukcje pierwszego uruchomienia projektu</h1>
    <li>Zainstalowanie PostgreSQL - <a>https://www.postgresql.org/download/windows/</a></li> 
    <li>Upewnić się że postgres działa, istnieje (domyślny) użytkownik postgres, ma domyślne hasło (w przypadku innego użytkownika/hasła, można zmienić connection stringa projektu w appsettings.json)</li>
    <li>Upewnienie się że Port postgresa jest prawidłowy - domyślnie w projekcie to 5432, można zmienić, tak samo jak wyżej</li>
    <li>Polecenie “update-database” w konsoli nuget</li>
    <li>Można uruchomić projekt (nie potrzeba gotowej bazy danych, projekt sam seeduje dane)</li>
    
<br>
<h3>Domyślne dane</h3>
  <h4>Projekt domyślnie seeduje dane</h4>
  <li>Można znależć ich dane w Data/SeedData.cs </li>
  <li>Domyślny admin ma username: Admin, Hasło: Admin@123</li>
  <li>przykładowy użytkownik: username: Baran, Hasło: blazej@123</li>
