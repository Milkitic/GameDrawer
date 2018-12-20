using Microsoft.VisualStudio.TestTools.UnitTesting;
using RomExplorer.IO;
using System;
using System.Diagnostics;

namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            for (int i = 0; i < 1000; i++)
            {
                var str = IdentityHelper.GetRelativePath(
                    @"C:\Users\YureruMiira\Source\Repos\RomExplorer\RomExplorer\RomExplorer\bin\Debug\Games\N64\Rom\SPM64.z64");
            }
            //Debug.WriteLine(str);
        }
    }
}
