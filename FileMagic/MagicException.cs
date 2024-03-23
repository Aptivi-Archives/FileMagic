//
// FileMagic  Copyright (C) 2024  Aptivi
//
// This file is part of FileMagic
//
// FileMagic is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// FileMagic is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY, without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <https://www.gnu.org/licenses/>.
//

using System;

namespace FileMagic
{
    /// <summary>
    /// Creates an instance of magic exception
    /// </summary>
    public class MagicException : Exception
    {
        /// <summary>
        /// Raises the libmagic exception
        /// </summary>
        public MagicException()
            : base("Generic libmagic error")
        { }

        /// <summary>
        /// Raises the libmagic exception
        /// </summary>
        /// <param name="message">Message for additional info</param>
        public MagicException(string message)
            : base(message)
        { }

        /// <summary>
        /// Raises the libmagic exception
        /// </summary>
        /// <param name="message">Message for additional info</param>
        /// <param name="innerException">Inner exception for additional diagnostic info</param>
        public MagicException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
