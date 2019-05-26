SonarScanner.MSBuild.exe begin /k:"IntegerMan_EmergenceWin" /o:"integerman-github" /d:sonar.host.url="https://sonarcloud.io" /d:sonar.login="b7b4509740d666b273996bbc0f691e8b897b80d8" /d:sonar.cs.nunit.reportsPaths="%CD%\NUnitResults.xml" /d:sonar.cs.opencover.reportsPaths="%CD%\opencover.xml"
"C:\Program Files (x86)\Microsoft Visual Studio\2019\Community\MSBuild\Current\Bin\MsBuild.exe" /t:Rebuild

OpenCover.Console.exe -output:"%CD%\opencover.xml" -register:user -target:"C:\Users\Matt\.nuget\packages\nunit.consolerunner\3.10.0\tools\nunit3-console.exe" -targetargs:"%CD%\MattEland.Emergence.Tests\bin\Debug\MattEland.Emergence.Tests.dll --result=%CD%\NUnitResults.xml"

SonarScanner.MSBuild.exe end /d:sonar.login="b7b4509740d666b273996bbc0f691e8b897b80d8"
Pause