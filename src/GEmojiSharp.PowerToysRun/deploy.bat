call pack.bat

taskkill /f /im PowerToys.exe /t

xcopy /s /y .\bin\Release\GEmojiSharp\ "%LocalAppData%\Microsoft\PowerToys\PowerToys Run\Plugins\GEmojiSharp\"

start "" "C:\Program Files\PowerToys\PowerToys.exe"
