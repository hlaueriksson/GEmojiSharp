dotnet build -c Release /p:TF_BUILD=true
dotnet msbuild /t:ILMerge

xcopy /y .\bin\Release\net6.0-windows\Images\ .\bin\Release\GEmojiSharp\Images\
xcopy /y .\bin\Release\net6.0-windows\GEmojiSharp.PowerToysRun.* .\bin\Release\GEmojiSharp\
xcopy /y .\bin\Release\net6.0-windows\plugin.json .\bin\Release\GEmojiSharp\

:: Zip '.\bin\Release\GEmojiSharp\' into 'GEmojiSharp.PowerToysRun-1.0.0.zip'