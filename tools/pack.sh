#!/bin/bash

#   FileMagic  Copyright (C) 2024  Aptivi
# 
#   This file is part of FileMagic
# 
#   FileMagic is free software: you can redistribute it and/or modify
#   it under the terms of the GNU General Public License as published by
#   the Free Software Foundation, either version 3 of the License, or
#   (at your option) any later version.
# 
#   FileMagic is distributed in the hope that it will be useful,
#   but WITHOUT ANY WARRANTY; without even the implied warranty of
#   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
#   GNU General Public License for more details.
# 
#   You should have received a copy of the GNU General Public License
#   along with this program.  If not, see <https://www.gnu.org/licenses/>.

# This script builds and packs the artifacts. Use when you have MSBuild installed.
version=$(cat version)
releaseconf=$1
if [ -z $releaseconf ]; then
	releaseconf=Release
fi

# Check for dependencies
zippath=`which zip`
if [ ! $? == 0 ]; then
	echo zip is not found.
	exit 1
fi

# Pack binary
echo Packing binary...
cd "../FileMagic.Bin/net8.0/" && "$zippath" -r /tmp/$version-bin.zip . && cd -
cd "../FileMagic.Bin/net48/" && "$zippath" -r /tmp/$version-bin-48.zip . && cd -
cd "../FileMagic.ConsoleTest.Bin/net8.0/" && "$zippath" -r /tmp/$version-bin.zip . && cd -
cd "../FileMagic.ConsoleTest.Bin/net48/" && "$zippath" -r /tmp/$version-bin-48.zip . && cd -
if [ ! $? == 0 ]; then
	echo Packing using zip failed.
	exit 1
fi

# Inform success
mv ~/tmp/$version-bin.zip .
mv ~/tmp/$version-bin-48.zip .
mv ~/tmp/$version-demo.zip .
mv ~/tmp/$version-demo-48.zip .
echo Build and pack successful.
exit 0
