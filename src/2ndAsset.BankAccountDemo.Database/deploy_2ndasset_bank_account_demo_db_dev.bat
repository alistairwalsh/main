@echo off

REM
REM	Copyright ©2002-2015 Daniel Bullington (dpbullington@gmail.com) (dpbullington@gmail.com)
REM	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
REM

set SHOULD_CREATE_DB=false
set SQLCMD_EXE=sqlcmd.exe
set SQL_DIR=.

set DB_SERVER=(local)
set DB_DATABASE_MASTER=master
set DB_SA_USERNAME=sa
set DB_SA_PASSWORD=???

set DB_DATABASE=2ndasset_bank_account_demo
set DB_LOGIN=2ndasset_bank_account_demo_mssql_dev_login
set DB_PASSWORD=LrJGmP6UfW8TEp7x3wWhECUYULE6zzMcWQ03R6UxeB4xzVmnq5S4Lx0vApegZVH
set DB_USER=2ndasset_bank_account_demo_mssql_dev_user


CALL deploy_2ndasset_bank_account_demo_db.bat
IF %ERRORLEVEL% NEQ 0 goto pkgError


goto pkgSuccess


:pkgError
echo An error occured.
pause > nul
goto :eof

:pkgSuccess
echo Completed successfully.
pause > nul
goto :eof
