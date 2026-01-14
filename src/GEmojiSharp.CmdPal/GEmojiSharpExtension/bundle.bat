dotnet build --configuration Release -p:GenerateAppxPackageOnBuild=true -p:Platform=x64 -p:AppxPackageDir="AppPackages\x64\"
dotnet build --configuration Release -p:GenerateAppxPackageOnBuild=true -p:Platform=ARM64 -p:AppxPackageDir="AppPackages\ARM64\"

"C:\Program Files (x86)\Windows Kits\10\bin\10.0.19041.0\x64\makeappx.exe" bundle /f bundle_mapping.txt /p GEmojiSharpExtension_0.0.1.0_Bundle.msixbundle
