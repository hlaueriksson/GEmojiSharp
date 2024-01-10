dotnet build -c Release /p:TF_BUILD=true

xcopy /y .\bin\Release\net8.0-windows\Images\ .\bin\Release\GEmojiSharp\Images\
xcopy /y .\bin\Release\net8.0-windows\GEmojiSharp.* .\bin\Release\GEmojiSharp\
xcopy /y .\bin\Release\net8.0-windows\plugin.json .\bin\Release\GEmojiSharp\

:: Zip '.\bin\Release\GEmojiSharp\' into 'GEmojiSharp.PowerToysRun.3.1.3.zip'
