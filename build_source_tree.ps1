#
#	Copyright ©2002-2014 Daniel Bullington (dpbullington@gmail.com)
#	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
#

cls
$root = [System.Environment]::CurrentDirectory

$build_flavor_dir = "Debug"
$build_tool_cfg = "Debug"
$src_dir = ".\src"
$msbuild_exe = "C:\Program Files (x86)\MSBuild\12.0\bin\amd64\msbuild.exe"

echo "The operation is starting..."

&"$msbuild_exe" /verbosity:quiet /consoleloggerparameters:ErrorsOnly "$src_dir\2ndAssetSuite.sln" /t:clean /p:Configuration="$build_tool_cfg" /p:VisualStudioVersion=12.0

if (!($LastExitCode -eq $null -or $LastExitCode -eq 0))
{ echo "An error occurred during the operation."; return; }

&"$msbuild_exe" /verbosity:quiet /consoleloggerparameters:ErrorsOnly "$src_dir\2ndAssetSuite.sln" /t:build /p:Configuration="$build_tool_cfg" /p:VisualStudioVersion=12.0

if (!($LastExitCode -eq $null -or $LastExitCode -eq 0))
{ echo "An error occurred during the operation."; return; }

echo "The operation completed successfully."