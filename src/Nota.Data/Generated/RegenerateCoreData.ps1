Get-ChildItem *.cs |Remove-Item -ErrorAction SilentlyContinue
Remove-Item .\tools -Recurse -ErrorAction SilentlyContinue
#nuget install XmlSchemaClassGenerator-beta -ExcludeVersion -OutputDirectory 'Tools'
dotnet tool install dotnet-xscgen --version 2.0.206 --tool-path .\tools\

#Invoke-WebRequest -Uri https://nota-game.azurewebsites.net/schema/nota.xsd -OutFile .\Tools\nota.xsd -ErrorAction Stop

.\tools\xscgen.exe -0 --namespace=http://nota-game.azurewebsites.net/schema/nota=Core --prefix=Nota.Data.Generated https://nota-game.azurewebsites.net/schema/nota.xsd http://nota-game.azurewebsites.net/schema/misc.xsd http://nota-game.azurewebsites.net/schema/lebewesen.xsd http://nota-game.azurewebsites.net/schema/pfad.xsd http://nota-game.azurewebsites.net/schema/talent.xsd http://nota-game.azurewebsites.net/schema/fertigkeit.xsd http://nota-game.azurewebsites.net/schema/besonderheit.xsd http://nota-game.azurewebsites.net/schema/kampf/aktionen.xsd http://nota-game.azurewebsites.net/schema/kampf/ausstattung.xsd

Remove-Item .\tools -Recurse
