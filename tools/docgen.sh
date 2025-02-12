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

# Check for dependencies
msbuildpath=`which docfx`
if [ ! $? == 0 ]; then
	echo DocFX is not found.
	exit 1
fi

# Build KS
echo Building documentation...
docfx ../DocGen/docfx.json
if [ ! $? == 0 ]; then
	echo Build failed.
	exit 1
fi

# Inform success
echo Build successful.
exit 0
