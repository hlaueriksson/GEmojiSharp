dotnet build -c Release /p:TF_BUILD=true
dotnet msbuild /t:ILMerge
