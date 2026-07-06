using System.Diagnostics;
using Reloaded.Memory.Sigscan;

namespace Nep3ArchipelagoClient.MemoryInterface
{
    public unsafe static class FunctionScanner
    {
        private static Process thisProcess = Process.GetCurrentProcess();
        private static nint baseAddress = thisProcess.MainModule.BaseAddress;
        private static Int32 exeSize = thisProcess.MainModule.ModuleMemorySize;
        private static Scanner scanner = new Scanner((byte*) baseAddress, exeSize);

        public unsafe static bool FindFunction(string functionName, string pattern, out nuint offset)
        {
            offset = 0;
            // Search for a given pattern
            // Note: If created signature using SigMaker, replace ? with ??.
            var result = scanner.FindPattern(pattern);
            if (!result.Found)
            {
                Console.WriteLine($"Function {functionName} could not be found");
                return false;
            }
            Console.WriteLine($"Function {functionName} found at Offset:{result.Offset.ToString("X")}");
            offset = (nuint)result.Offset;
            return true;
        }

        public unsafe static bool FindFunctions(string functionName, string pattern, out nuint[] offset)
        {
            List<nuint> offsets = new List<nuint>();
            // Search for a given pattern
            // Note: If created signature using SigMaker, replace ? with ??.
            var result = scanner.FindPattern(pattern);
            if (!result.Found)
            {
                Console.WriteLine($"Function {functionName} could not be found");
                offset = offsets.ToArray();
                return false;
            }

            while (result.Found)
            {
                offsets.Add((nuint)result.Offset);
                result = scanner.FindPattern(pattern,result.Offset+1);
            }
            foreach(var off in offsets)
                Console.WriteLine($"Function {functionName} found at Offset:{off.ToString("X")}");
            offset = offsets.ToArray();
            return true;
        }


        public unsafe static bool JumpTarget(string target,string pattern, out nuint offset)
        {
            offset = 0;
            // Search for a given pattern
            // Note: If created signature using SigMaker, replace ? with ??.
            var result = scanner.FindPattern(pattern);
            if (!result.Found)
            {
                Console.WriteLine($"Jump to {target} could not be found");
                return false;
            }
            Console.WriteLine($"Jump to {target} found at Offset:{result.Offset.ToString("X")}");
            offset = (nuint)result.Offset;
            return true;
        }
    }
}
