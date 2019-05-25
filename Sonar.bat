SonarScanner.MSBuild.exe begin /k:"IntegerMan_EmergenceWin" /o:"integerman-github" /d:sonar.host.url="https://sonarcloud.io" /d:sonar.login="b7b4509740d666b273996bbc0f691e8b897b80d8"
"C:\Program Files (x86)\Microsoft Visual Studio\2019\Community\MSBuild\Current\Bin\MsBuild.exe" /t:Rebuild
SonarScanner.MSBuild.exe end /d:sonar.login="b7b4509740d666b273996bbc0f691e8b897b80d8"
Pause