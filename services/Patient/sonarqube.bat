dotnet sonarscanner begin /k:"Ecis-Patient" /d:sonar.host.url="http://192.168.1.179:59000"  /d:sonar.login="2ecc1f5b41411ca8e9a98de606fb81a580ab1229"
dotnet build
dotnet sonarscanner end /d:sonar.login="2ecc1f5b41411ca8e9a98de606fb81a580ab1229"


@pause