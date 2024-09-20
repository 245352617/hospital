
dotnet sonarscanner begin /k:"ECIS-BS" /d:sonar.host.url="http://192.168.1.179:59000"  /d:sonar.login="aff57c8c7a7e022e6b71cb285da48283ee666a32"

dotnet build

dotnet sonarscanner end /d:sonar.login="aff57c8c7a7e022e6b71cb285da48283ee666a32"
