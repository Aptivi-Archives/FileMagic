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

using FileMagic.Native.Interop;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using System.IO;

namespace FileMagic.Tests
{
    [TestClass]
    public class MagicHandlerTests
    {
        [TestMethod]
        public void TestGetDllMagic()
        {
            string dllMagic = MagicHandler.GetMagicInfo(Path.GetFullPath("FileMagic.dll"));
            dllMagic.ShouldContain("PE32 executable for MS Windows");
            dllMagic.ShouldContain("(DLL), Intel i386 Mono/.Net assembly, 3 sections");
        }

        [TestMethod]
        public void TestGetDllMagicMimeInfo()
        {
            string dllMagic = MagicHandler.GetMagicMimeInfo(Path.GetFullPath("FileMagic.dll"));
            dllMagic.ShouldBe("application/vnd.microsoft.portable-executable; charset=binary");
        }

        [TestMethod]
        public void TestGetDllMagicMimeTypeInfo()
        {
            string dllMagic = MagicHandler.GetMagicMimeType(Path.GetFullPath("FileMagic.dll"));
            dllMagic.ShouldBe("application/vnd.microsoft.portable-executable");
        }

        [TestMethod]
        public void TestGetDllMagicCustomInfo1()
        {
            string dllMagic = MagicHandler.GetMagicCustomType(Path.GetFullPath("FileMagic.dll"), MagicFlags.Extension);
            dllMagic.ShouldBe("dll/cpl/tlb/ocx/acm/ax/ime");
        }

        [TestMethod]
        public void TestGetDllMagicCustomInfo2()
        {
            string dllMagic = MagicHandler.GetMagicCustomType(Path.GetFullPath("FileMagic.dll"), MagicFlags.MimeEncoding);
            dllMagic.ShouldBe("binary");
        }
    }
}
