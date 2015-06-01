#
#	Copyright ©2002-2014 Daniel Bullington (dpbullington@gmail.com)
#	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
#

cls
$root = [System.Environment]::CurrentDirectory

$filesToKill = Get-ChildItem $root -Recurse `
	-Include "obj", "bin", "debug", "release", "clientbin", "output", "pkg", "_ReSharper.*", ".partial", "*.suo", "*.user", "*.cache", "*.resharper", "*.visualstate.xml", "*.pidb", "*.userprefs", "*.DotSettings", "thumbs.db", "desktop.ini", ".DS_Store", "TestResult.xml" -Force

foreach ($fileToKill in $filesToKill)
{
	"Cleaning: " + $fileToKill.FullName

	try { Remove-Item $fileToKill.FullName -Recurse -Force } catch { }
}
