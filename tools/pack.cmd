@echo off

REM    FileMagic  Copyright (C) 2024  Aptivi
REM
REM    This file is part of FileMagic
REM
REM    FileMagic is free software: you can redistribute it and/or modify
REM    it under the terms of the GNU General Public License as published by
REM    the Free Software Foundation, either version 3 of the License, or
REM    (at your option) any later version.
REM
REM    FileMagic is distributed in the hope that it will be useful,
REM    but WITHOUT ANY WARRANTY; without even the implied warranty of
REM    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
REM    GNU General Public License for more details.
REM
REM    You should have received a copy of the GNU General Public License
REM    along with this program.  If not, see <https://www.gnu.org/licenses/>.

for /f "tokens=* USEBACKQ" %%f in (`type version`) do set version=%%f
set releaseconfig=%1
if "%releaseconfig%" == "" set releaseconfig=Release

:packbin
echo Packing binary...
"%ProgramFiles%\7-Zip\7z.exe" a -tzip %temp%/%version%-bin.zip "..\FileMagic.Bin\net8.0\*"
"%ProgramFiles%\7-Zip\7z.exe" a -tzip %temp%/%version%-bin-48.zip "..\FileMagic.Bin\net48\*"
"%ProgramFiles%\7-Zip\7z.exe" a -tzip %temp%/%version%-demo.zip "..\FileMagic.Console.Bin\net8.0\*"
"%ProgramFiles%\7-Zip\7z.exe" a -tzip %temp%/%version%-demo-48.zip "..\FileMagic.Console.Bin\net48\*"
if %errorlevel% == 0 goto :complete
echo There was an error trying to pack binary (%errorlevel%).
goto :finished

:complete
move %temp%\%version%-bin.zip
move %temp%\%version%-bin-48.zip
move %temp%\%version%-demo.zip
move %temp%\%version%-demo-48.zip

echo Pack successful.
:finished
