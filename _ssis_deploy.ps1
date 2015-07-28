#
#	Copyright ©2002-2015 Daniel Bullington (info@2ndasset.com) (info@2ndasset.com)
#	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
#

cls
$root = [System.Environment]::CurrentDirectory

$build_flavor_dir = "Debug"
$src_dir = ".\src"
$lib_dir = ".\lib"

$gacutil_exe = "C:\Program Files (x86)\Microsoft SDKs\Windows\v8.1A\bin\NETFX 4.5.1 Tools\gacutil.exe"
$dts_plc_x86_dir = "C:\Program Files (x86)\Microsoft SQL Server\120\DTS\PipelineComponents"
$dts_plc_x64_dir = "C:\Program Files\Microsoft SQL Server\120\DTS\PipelineComponents"

echo "The operation is starting..."

$lib_subdir_name = "Newtonsoft.Json"

$lib_deploy_assembly_names = @("Newtonsoft.Json")

foreach ($lib_deploy_assembly_name in $lib_deploy_assembly_names)
{
	&"$gacutil_exe" -u "$lib_deploy_assembly_name"

	if (!($LastExitCode -eq $null -or $LastExitCode -eq 0))
	{ echo "A warning occurred during the operation."; }

	&"$gacutil_exe" -i "$lib_dir\$lib_subdir_name\$lib_deploy_assembly_name.dll"

	if (!($LastExitCode -eq $null -or $LastExitCode -eq 0))
	{ echo "An error occurred during the operation."; return; }
}


$lib_subdir_name = "Solder"

$lib_deploy_assembly_names = @("Solder.Framework")

foreach ($lib_deploy_assembly_name in $lib_deploy_assembly_names)
{
	&"$gacutil_exe" -u "$lib_deploy_assembly_name"

	if (!($LastExitCode -eq $null -or $LastExitCode -eq 0))
	{ echo "A warning occurred during the operation."; }

	&"$gacutil_exe" -i "$lib_dir\$lib_subdir_name\$lib_deploy_assembly_name.dll"

	if (!($LastExitCode -eq $null -or $LastExitCode -eq 0))
	{ echo "An error occurred during the operation."; return; }
}


$src_deploy_assembly_names = @("2ndAsset.Common.WinForms",
	"2ndAsset.ObfuscationEngine.Core",
	"2ndAsset.ObfuscationEngine.UI",
	"2ndAsset.Ssis.Components",
	"2ndAsset.Ssis.Components.UI")

foreach ($src_deploy_assembly_name in $src_deploy_assembly_names)
{
	&"$gacutil_exe" -u "$src_deploy_assembly_name"

	if (!($LastExitCode -eq $null -or $LastExitCode -eq 0))
	{ echo "A warning occurred during the operation."; }

	&"$gacutil_exe" -i "$src_dir\$src_deploy_assembly_name\bin\$build_flavor_dir\$src_deploy_assembly_name.dll"

	if (!($LastExitCode -eq $null -or $LastExitCode -eq 0))
	{ echo "An error occurred during the operation."; return; }
}


$src_deploy_assembly_names = @(	"2ndAsset.Ssis.Components")

foreach ($src_deploy_assembly_name in $src_deploy_assembly_names)
{
	Copy-Item "$src_dir\$src_deploy_assembly_name\bin\$build_flavor_dir\$src_deploy_assembly_name.dll" "$dts_plc_x86_dir\."
	Copy-Item "$src_dir\$src_deploy_assembly_name\bin\$build_flavor_dir\$src_deploy_assembly_name.dll" "$dts_plc_x64_dir\."

	Copy-Item "$src_dir\$src_deploy_assembly_name\bin\$build_flavor_dir\$src_deploy_assembly_name.pdb" "$dts_plc_x86_dir\."
	Copy-Item "$src_dir\$src_deploy_assembly_name\bin\$build_flavor_dir\$src_deploy_assembly_name.pdb" "$dts_plc_x64_dir\."
}

echo "The operation completed successfully."