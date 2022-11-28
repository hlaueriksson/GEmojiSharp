call pack.bat

taskkill /f /im PowerToys.exe /t

xcopy /s /y .\bin\Release\GEmojiSharp\ "C:\Program Files\PowerToys\modules\launcher\Plugins\GEmojiSharp\"

start "" "C:\Program Files\PowerToys\PowerToys.exe"
