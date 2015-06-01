@ECHO off

REM
REM	Copyright ©2002-2014 Daniel Bullington (dpbullington@gmail.com)
REM	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
REM

CD "."
GOTO setEnv


:setEnv

CALL undeploy_ssis.bat
IF %ERRORLEVEL% NEQ 0 GOTO pkgError


CALL deploy_ssis.bat
IF %ERRORLEVEL% NEQ 0 GOTO pkgError



GOTO pkgSuccess


:pkgError
ECHO An error occured during the operation.
GOTO :eof


:pkgSuccess
ECHO The operation completed successfully.
GOTO :eof