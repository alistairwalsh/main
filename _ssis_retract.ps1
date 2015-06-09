#
#	Copyright ©2002-2014 Daniel Bullington (dpbullington@gmail.com)
#	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
#

cls
$root = [System.Environment]::CurrentDirectory

$gacutil_exe = "C:\Program Files (x86)\Microsoft SDKs\Windows\v8.1A\bin\NETFX 4.5.1 Tools\gacutil.exe"
$dts_plc_x86_dir = "C:\Program Files (x86)\Microsoft SQL Server\120\DTS\PipelineComponents"
$dts_plc_x64_dir = "C:\Program Files\Microsoft SQL Server\120\DTS\PipelineComponents"

echo "The operation is starting..."

$zzz_retract_assembly_names = @("2ndAsset.ObfuscationEngine.Core",
	"2ndAsset.Ssis.Components",
	"2ndAsset.Ssis.Components.UI",

	"TextMetal.Middleware.Solder",
	"TextMetal.Middleware.Common",
	"TextMetal.Middleware.Data",

	"Newtonsoft.Json")

foreach ($zzz_retract_assembly_name in $zzz_retract_assembly_names)
{
	&"$gacutil_exe" -u "$zzz_retract_assembly_name"

	if (!($LastExitCode -eq $null -or $LastExitCode -eq 0))
	{ echo "A warning occurred during the operation."; }

	Remove-Item "$dts_plc_x86_dir\$zzz_retract_assembly_name.dll"
	Remove-Item "$dts_plc_x64_dir\$zzz_retract_assembly_name.dll"
}

echo "The operation completed successfully."