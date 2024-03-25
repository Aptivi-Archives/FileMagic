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
using System.Runtime.InteropServices;

namespace FileMagic.Native.Interop
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct MagicSet
    { }

    /// <summary>
    /// Magic helper
    /// </summary>
    internal static unsafe class MagicHelper
    {
        /// <summary>
        /// file_public struct magic_set* magic_open(int flags)
        /// </summary>
        [DllImport("libmagic")]
        public static extern MagicSet* magic_open(MagicFlags flags);

        /// <summary>
        /// file_public void magic_close(struct magic_set *ms)
        /// </summary>
        [DllImport("libmagic")]
        public static extern void magic_close(MagicSet* ms);

        /// <summary>
        /// file_public const char *magic_error(struct magic_set *ms)
        /// </summary>
        [DllImport("libmagic")]
        public static extern IntPtr magic_error(MagicSet* ms);

        /// <summary>
        /// file_public int magic_errno(struct magic_set *ms)
        /// </summary>
        [DllImport("libmagic")]
        public static extern int magic_errno(MagicSet* ms);

        /// <summary>
        /// file_public const char *magic_getpath(const char *magicfile, int action)
        /// </summary>
        [DllImport("libmagic")]
        public static extern IntPtr magic_getpath([In][MarshalAs(UnmanagedType.LPStr)] string magicfile, int action);

        /// <summary>
        /// file_public const char *magic_file(struct magic_set *ms, const char *inname)
        /// </summary>
        [DllImport("libmagic")]
        public static extern IntPtr magic_file(MagicSet* ms, [In][MarshalAs(UnmanagedType.LPStr)] string inname);

        /// <summary>
        /// file_public const char *magic_descriptor(struct magic_set *ms, int fd)
        /// </summary>
        [DllImport("libmagic")]
        public static extern IntPtr magic_descriptor(MagicSet* ms, int fd);

        /// <summary>
        /// file_public const char *magic_buffer(struct magic_set *ms, const void *buf, size_t nb)
        /// </summary>
        [DllImport("libmagic")]
        public static extern IntPtr magic_buffer(MagicSet* ms, IntPtr buf, [MarshalAs(UnmanagedType.SysInt)] int nb);

        /// <summary>
        /// file_public int magic_getflags(struct magic_set *ms)
        /// </summary>
        [DllImport("libmagic")]
        public static extern int magic_getflags(MagicSet* ms);

        /// <summary>
        /// file_public int magic_setflags(struct magic_set *ms, int flags)
        /// </summary>
        [DllImport("libmagic")]
        public static extern int magic_setflags(MagicSet* ms, MagicFlags flags);

        /// <summary>
        /// file_public int magic_load(struct magic_set *ms, const char *magicfile)
        /// </summary>
        [DllImport("libmagic")]
        public static extern int magic_load(MagicSet* ms, [In][MarshalAs(UnmanagedType.LPStr)] string magicfile);

        /// <summary>
        /// file_public int magic_load_buffers(struct magic_set *ms, void **bufs, size_t *sizes, size_t nbufs)
        /// </summary>
        [DllImport("libmagic")]
        public static extern int magic_load_buffers(MagicSet* ms, ref IntPtr bufs, ref int sizes, [MarshalAs(UnmanagedType.SysInt)] int nbufs);

        /// <summary>
        /// file_public int magic_compile(struct magic_set *ms, const char *magicfile)
        /// </summary>
        [DllImport("libmagic")]
        public static extern int magic_compile(MagicSet* ms, [In][MarshalAs(UnmanagedType.LPStr)] string magicfile);

        /// <summary>
        /// file_public int magic_check(struct magic_set *ms, const char *magicfile)
        /// </summary>
        [DllImport("libmagic")]
        public static extern int magic_check(MagicSet* ms, [In][MarshalAs(UnmanagedType.LPStr)] string magicfile);

        /// <summary>
        /// file_public int magic_list(struct magic_set *ms, const char *magicfile)
        /// </summary>
        [DllImport("libmagic")]
        public static extern int magic_list(MagicSet* ms, [In][MarshalAs(UnmanagedType.LPStr)] string magicfile);

        /// <summary>
        /// file_public int magic_setparam(struct magic_set *ms, int param, const void *val)
        /// </summary>
        [DllImport("libmagic")]
        public static extern int magic_setparam(MagicSet* ms, MagicParameters param, IntPtr val);

        /// <summary>
        /// file_public int magic_getparam(struct magic_set *ms, int param, void *val)
        /// </summary>
        [DllImport("libmagic")]
        public static extern int magic_getparam(MagicSet* ms, MagicParameters param, IntPtr val);

        internal static string GetError(MagicSet* handle)
        {
            var errorHandle = magic_error(handle);
            return Marshal.PtrToStringAnsi(errorHandle);
        }

        internal static int GetErrorNumber(MagicSet* handle) =>
            magic_errno(handle);
    }
}
