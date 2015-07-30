#
#	Copyright ©2002-2015 Daniel Bullington (dpbullington@gmail.com) (dpbullington@gmail.com)
#	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
#

cls
$root = [System.Environment]::CurrentDirectory

$build_flavor_dir = "Debug"
$build_tool_cfg = "Debug"
$doc_dir = ".\doc"
$src_dir = ".\src"
$lib_dir = ".\lib"
$templates_dir = ".\templates"
$pkg_dir = ".\pkg"

$wix_install_dir = "D:\development\2nd-asset\tools\wix"
$wix_targets_dir = "$wix_install_dir\Wix.targets"

$msbuild_exe = "C:\Program Files (x86)\MSBuild\12.0\bin\amd64\msbuild.exe"

echo "The operation is starting..."

if ((Test-Path -Path $pkg_dir))
{
	Remove-Item $pkg_dir -recurse
}

New-Item -ItemType directory -Path $pkg_dir
New-Item -ItemType directory -Path ($pkg_dir + "\bin")

Copy-Item "$lib_dir" "$pkg_dir\lib" -recurse
Copy-Item "$templates_dir" "$pkg_dir\templates" -recurse
Copy-Item ".\trunk.bat" "$pkg_dir\."
Copy-Item "$doc_dir\IMPORTS.txt" "$pkg_dir\."
Copy-Item "$doc_dir\LICENSE.txt" "$pkg_dir\."
Copy-Item "$src_dir\2ndAsset.Core\bin\$build_flavor_dir\2ndAsset.Core.dll" "$pkg_dir\bin\."
Copy-Item "$src_dir\2ndAsset.Core\bin\$build_flavor_dir\2ndAsset.Core.xml" "$pkg_dir\bin\."
Copy-Item "$src_dir\2ndAsset.Core\bin\$build_flavor_dir\2ndAsset.Core.pdb" "$pkg_dir\bin\."

Copy-Item "$src_dir\2ndAsset.Ssis.Components\bin\$build_flavor_dir\2ndAsset.Ssis.Components.dll" "$pkg_dir\bin\."
Copy-Item "$src_dir\2ndAsset.Ssis.Components\bin\$build_flavor_dir\2ndAsset.Ssis.Components.xml" "$pkg_dir\bin\."
Copy-Item "$src_dir\2ndAsset.Ssis.Components\bin\$build_flavor_dir\2ndAsset.Ssis.Components.pdb" "$pkg_dir\bin\."

Copy-Item "$src_dir\2ndAsset.Ssis.Components.UI\bin\$build_flavor_dir\2ndAsset.Ssis.Components.UI.dll" "$pkg_dir\bin\."
Copy-Item "$src_dir\2ndAsset.Ssis.Components.UI\bin\$build_flavor_dir\2ndAsset.Ssis.Components.UI.xml" "$pkg_dir\bin\."
Copy-Item "$src_dir\2ndAsset.Ssis.Components.UI\bin\$build_flavor_dir\2ndAsset.Ssis.Components.UI.pdb" "$pkg_dir\bin\."

Copy-Item "$src_dir\2ndAsset.Ssis.Components.UI.Test.WindowsTool\bin\$build_flavor_dir\ObfuConfTestTool.exe" "$pkg_dir\bin\."
Copy-Item "$src_dir\2ndAsset.Ssis.Components.UI.Test.WindowsTool\bin\$build_flavor_dir\ObfuConfTestTool.xml" "$pkg_dir\bin\."
Copy-Item "$src_dir\2ndAsset.Ssis.Components.UI.Test.WindowsTool\bin\$build_flavor_dir\ObfuConfTestTool.pdb" "$pkg_dir\bin\."
Copy-Item "$src_dir\2ndAsset.Ssis.Components.UI.Test.WindowsTool\bin\$build_flavor_dir\ObfuConfTestTool.exe.config" "$pkg_dir\bin\."

Copy-Item "$src_dir\2ndAsset.Ssis.SrcToDstPkgGen.ConsoleTool\bin\$build_flavor_dir\SsisSrcToDstPkgGenTool.exe" "$pkg_dir\bin\."
Copy-Item "$src_dir\2ndAsset.Ssis.SrcToDstPkgGen.ConsoleTool\bin\$build_flavor_dir\SsisSrcToDstPkgGenTool.xml" "$pkg_dir\bin\."
Copy-Item "$src_dir\2ndAsset.Ssis.SrcToDstPkgGen.ConsoleTool\bin\$build_flavor_dir\SsisSrcToDstPkgGenTool.pdb" "$pkg_dir\bin\."
Copy-Item "$src_dir\2ndAsset.Ssis.SrcToDstPkgGen.ConsoleTool\bin\$build_flavor_dir\SsisSrcToDstPkgGenTool.exe.config" "$pkg_dir\bin\."

Copy-Item "$src_dir\2ndAsset.Utilities.DataObfu.ConsoleTool\bin\$build_flavor_dir\DataObfuscationTool.exe" "$pkg_dir\bin\."
Copy-Item "$src_dir\2ndAsset.Utilities.DataObfu.ConsoleTool\bin\$build_flavor_dir\DataObfuscationTool.xml" "$pkg_dir\bin\."
Copy-Item "$src_dir\2ndAsset.Utilities.DataObfu.ConsoleTool\bin\$build_flavor_dir\DataObfuscationTool.pdb" "$pkg_dir\bin\."
Copy-Item "$src_dir\2ndAsset.Utilities.DataObfu.ConsoleTool\bin\$build_flavor_dir\DataObfuscationTool.exe.config" "$pkg_dir\bin\."


#
# Wix Installer
# <WixTargetsPath>D:\development\_tools_\wix\Wix.targets</WixTargetsPath>
# <WixInstallPath>D:\development\_tools_\wix\</WixInstallPath>

&"$msbuild_exe" /verbosity:quiet /consoleloggerparameters:ErrorsOnly "$src_dir\2ndAsset.Setup\2ndAsset.Setup.wixproj" /t:clean /p:Configuration="$build_tool_cfg" /p:VisualStudioVersion=12.0 /p:WixInstallPath="$wix_install_dir" /p:WixTargetsPath="$wix_targets_dir"

if (!($LastExitCode -eq $null -or $LastExitCode -eq 0))
{ echo "An error occurred during the operation."; return; }

&"$msbuild_exe" /verbosity:quiet /consoleloggerparameters:ErrorsOnly "$src_dir\2ndAsset.Setup\2ndAsset.Setup.wixproj" /t:build /p:Configuration="$build_tool_cfg" /p:VisualStudioVersion=12.0 /p:WixInstallPath="$wix_install_dir" /p:WixTargetsPath="$wix_targets_dir"

if (!($LastExitCode -eq $null -or $LastExitCode -eq 0))
{ echo "An error occurred during the operation."; return; }

echo "The operation completed successfully."