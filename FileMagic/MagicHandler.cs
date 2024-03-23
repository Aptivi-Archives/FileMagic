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

using FileMagic.Native;
using FileMagic.Native.Interop;
using System;
using System.IO;
using System.Runtime.InteropServices;

namespace FileMagic
{
    /// <summary>
    /// Magic handling tools
    /// </summary>
	public static class MagicHandler
	{
        private const string magicFileName = "magic.mgc";
        private static readonly string magicPathDefault = Path.GetFullPath(magicFileName);

        /// <summary>
        /// Gets the file magic information
        /// </summary>
        /// <param name="filePath">Target file path</param>
        public static string GetMagicInfo(string filePath) =>
            GetMagicInfo(filePath, magicPathDefault);

        /// <summary>
        /// Gets the file magic information
        /// </summary>
        /// <param name="filePath">Target file path</param>
        /// <param name="magicPath">Path to the file magic database</param>
        public static string GetMagicInfo(string filePath, string magicPath) =>
            HandleMagic(filePath, !string.IsNullOrEmpty(magicPath) ? magicPath : magicPathDefault, MagicFlags.None);

        /// <summary>
        /// Gets the file magic Mime information
        /// </summary>
        /// <param name="filePath">Target file path</param>
        public static string GetMagicMimeInfo(string filePath) =>
            GetMagicMimeInfo(filePath, magicPathDefault);

        /// <summary>
        /// Gets the file magic Mime information
        /// </summary>
        /// <param name="filePath">Target file path</param>
        /// <param name="magicPath">Path to the file magic database</param>
        public static string GetMagicMimeInfo(string filePath, string magicPath) =>
            HandleMagic(filePath, !string.IsNullOrEmpty(magicPath) ? magicPath : magicPathDefault, MagicFlags.Mime);

        /// <summary>
        /// Gets the file magic Mime type information
        /// </summary>
        /// <param name="filePath">Target file path</param>
        public static string GetMagicMimeType(string filePath) =>
            GetMagicMimeType(filePath, magicPathDefault);

        /// <summary>
        /// Gets the file magic Mime type information
        /// </summary>
        /// <param name="filePath">Target file path</param>
        /// <param name="magicPath">Path to the file magic database</param>
        public static string GetMagicMimeType(string filePath, string magicPath) =>
            HandleMagic(filePath, !string.IsNullOrEmpty(magicPath) ? magicPath : magicPathDefault, MagicFlags.MimeType);

        /// <summary>
        /// Gets the file magic Mime type information
        /// </summary>
        /// <param name="filePath">Target file path</param>
        /// <param name="flags">Magic flags to customize the output and the behavior of the native library</param>
        public static string GetMagicCustomType(string filePath, MagicFlags flags) =>
            GetMagicCustomType(filePath, magicPathDefault, flags);

        /// <summary>
        /// Gets the file magic Mime type information
        /// </summary>
        /// <param name="filePath">Target file path</param>
        /// <param name="magicPath">Path to the file magic database</param>
        /// <param name="flags">Magic flags to customize the output and the behavior of the native library</param>
        public static string GetMagicCustomType(string filePath, string magicPath, MagicFlags flags) =>
            HandleMagic(filePath, !string.IsNullOrEmpty(magicPath) ? magicPath : magicPathDefault, flags);

        internal static string HandleMagic(string filePath, string magicPath, MagicFlags flags)
        {
            IntPtr magicStringHandle;

            // Check the paths
            if (!File.Exists(filePath))
                throw new MagicException($"Failed to load file from path {filePath} because it wasn't found.");
            if (!File.Exists(magicPath))
                throw new MagicException($"Failed to load magic file from path {magicPath} because it wasn't found.");

            // Now, let's go back to the dark side
            unsafe
            {
                // Open the magic handle
                var handle = MagicHelper.magic_open(flags);

                // Use this handle to load the magic database
                int loadResult = MagicHelper.magic_load(handle, magicPath);
                if (loadResult != 0)
                    throw new MagicException($"Failed to load magic database file from path {magicPath}: [{MagicHelper.GetErrorNumber(handle)}] {MagicHelper.GetError(handle)}");

                // Now, get the magic
                magicStringHandle = MagicHelper.magic_file(handle, filePath);
                if (magicStringHandle == IntPtr.Zero)
                    throw new MagicException($"Failed to get magic of file {filePath} from magic database {magicPath}: [{MagicHelper.GetErrorNumber(handle)}] {MagicHelper.GetError(handle)}");
            }

            // Return the magic
            string magicString = Marshal.PtrToStringAnsi(magicStringHandle);
            return magicString;
        }

        static MagicHandler()
        {
            // Initialize native library
            Initializer.InitializeNative();
        }
    }
}
