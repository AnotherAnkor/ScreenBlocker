schtasks /create /SC ONLOGON /TN "ScreenBlocker" /RU Admin /RP pass  /TR "c:\Program Files (x86)\ScreenBlocker\screenblocker.exe"
SETLOCAL enabledelayedexpansion enableextensions
SET STARTUP_FOLDER=
FOR /F "tokens=1,2,*" %I in ('reg query "HKCU\Software\Microsoft\Windows\CurrentVersion\Explorer\User Shell Folders" /v Startup ^| %WINDIR%\System32\FIND.exe "Startup"') do SET STARTUP_FOLDER="%K"
REM fsutil hardlink create "%APPDATA%\Roaming\Microsoft\Windows\Start Menu\Programs\Startup"
IF DEFINED STARTUP_FOLDER (
fsutil hardlink create !STARTUP_FOLDER!
)
