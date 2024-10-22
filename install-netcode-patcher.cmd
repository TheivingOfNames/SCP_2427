cd ../
dotnet new tool-manifest
dotnet tool install --local evaisa.netcodepatcher.cli --version 3.*
cd SCP_2427
start /b del "install-netcode-patcher.cmd"
