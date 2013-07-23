for /f %%a IN ('dir /b /s /x packages.config') do ..\tools\nuget i "%%a" -o packages

PAUSE