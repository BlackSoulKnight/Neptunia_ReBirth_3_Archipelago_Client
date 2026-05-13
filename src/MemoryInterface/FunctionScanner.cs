using System.Diagnostics;
using Reloaded.Memory.Sigscan;

namespace Nep3ArchipelagoClient.src.MemoryInterface
{
    public unsafe static class FunctionScanner
    {
        private static Process thisProcess = Process.GetCurrentProcess();
        private static nint baseAddress = thisProcess.MainModule.BaseAddress;
        private static Int32 exeSize = thisProcess.MainModule.ModuleMemorySize;
        private static Scanner scanner = new Scanner((byte*) baseAddress, exeSize);

        public unsafe static bool FindFunction(string functionName,string pattern, out nuint offset)
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
            offset = (nuint)result.Offset;
            return true;
        }
    }
}
