@ECHO off

REM
REM	Copyright ©2002-2014 Daniel Bullington (dpbullington@gmail.com)
REM	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
REM

CD "."
GOTO setEnv


:setEnv

SET GACUTIL_EXE=C:\Program Files (x86)\Microsoft SDKs\Windows\v8.0A\bin\NETFX 4.0 Tools\gacutil.exe
SET DTSx64_PLC_DIR=C:\Program Files\Microsoft SQL Server\110\DTS\PipelineComponents
SET DTSx32_PLC_DIR=C:\Program Files (x86)\Microsoft SQL Server\110\DTS\PipelineComponents


SET TM_LIB_ASM_DIR=.\lib\TextMetal

SET TM_COMMON_CORE_ASM_NAME=TextMetal.Common.Core
SET TM_COMMON_CORE_ASM_DLL_NAME=%TM_COMMON_CORE_ASM_NAME%.dll

SET TM_COMMON_DATA_ASM_NAME=TextMetal.Common.Data
SET TM_COMMON_DATA_ASM_DLL_NAME=%TM_COMMON_DATA_ASM_NAME%.dll

SET TM_COMMON_WINFORMS_ASM_NAME=TextMetal.Common.WinForms
SET TM_COMMON_WINFORMS_ASM_DLL_NAME=%TM_COMMON_WINFORMS_ASM_NAME%.dll


SET SA_CORE_ASM_DIR=.\src\2ndAsset.Core\bin\Debug
SET SA_CORE_ASM_NAME=2ndAsset.Core
SET SA_CORE_ASM_DLL_NAME=%SA_CORE_ASM_NAME%.dll

SET SA_SSIS_COMPONENTS_ASM_DIR=.\src\2ndAsset.Ssis.Components\bin\Debug
SET SA_SSIS_COMPONENTS_ASM_NAME=2ndAsset.Ssis.Components
SET SA_SSIS_COMPONENTS_ASM_DLL_NAME=%SA_SSIS_COMPONENTS_ASM_NAME%.dll

SET SA_SSIS_COMPONENTS_IU_ASM_DIR=.\src\2ndAsset.Ssis.Components.UI\bin\Debug
SET SA_SSIS_COMPONENTS_IU_ASM_NAME=2ndAsset.Ssis.Components.UI
SET SA_SSIS_COMPONENTS_IU_ASM_DLL_NAME=%SA_SSIS_COMPONENTS_IU_ASM_NAME%.dll


"%GACUTIL_EXE%" -u "%TM_COMMON_CORE_ASM_NAME%"
IF %ERRORLEVEL% NEQ 0 GOTO pkgError

"%GACUTIL_EXE%" -u "%TM_COMMON_DATA_ASM_NAME%"
IF %ERRORLEVEL% NEQ 0 GOTO pkgError

"%GACUTIL_EXE%" -u "%TM_COMMON_WINFORMS_ASM_NAME%"
IF %ERRORLEVEL% NEQ 0 GOTO pkgError

"%GACUTIL_EXE%" -u "%SA_CORE_ASM_NAME%"
IF %ERRORLEVEL% NEQ 0 GOTO pkgError

"%GACUTIL_EXE%" -u "%SA_SSIS_COMPONENTS_ASM_NAME%"
IF %ERRORLEVEL% NEQ 0 GOTO pkgError

"%GACUTIL_EXE%" -u "%SA_SSIS_COMPONENTS_IU_ASM_NAME%"
IF %ERRORLEVEL% NEQ 0 GOTO pkgError




del "%DTSx64_PLC_DIR%\%SA_SSIS_COMPONENTS_ASM_DLL_NAME%"
IF %ERRORLEVEL% NEQ 0 GOTO pkgError

del "%DTSx32_PLC_DIR%\%SA_SSIS_COMPONENTS_ASM_DLL_NAME%"
IF %ERRORLEVEL% NEQ 0 GOTO pkgError


del "%DTSx64_PLC_DIR%\%SA_SSIS_COMPONENTS_IU_ASM_DLL_NAME%"
IF %ERRORLEVEL% NEQ 0 GOTO pkgError

del "%DTSx32_PLC_DIR%\%SA_SSIS_COMPONENTS_IU_ASM_DLL_NAME%"
IF %ERRORLEVEL% NEQ 0 GOTO pkgError


GOTO pkgSuccess


:pkgError
ECHO An error occured during the operation.
GOTO :eof


:pkgSuccess
ECHO The operation completed successfully.
GOTO :eof