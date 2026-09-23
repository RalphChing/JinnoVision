using HalconDotNet;

try
{
    HOperatorSet.GetSystem("version", out HTuple version);
    Console.WriteLine($"HALCON version reported: {version}");

    HObject img;
    HOperatorSet.GenImageConst(out img, "byte", 100, 100);
    Console.WriteLine("Core engine call succeeded — assembly + native binaries + license are all working.");
}
catch (DllNotFoundException ex)
{
    Console.WriteLine($"Native binary not found — x64-win64 isn't reachable at runtime: {ex.Message}");
}
catch (HOperatorException ex)
{
    Console.WriteLine($"HALCON operator error (often a license problem): {ex.Message}");
}
catch (Exception ex)
{
    Console.WriteLine($"Unexpected: {ex.GetType().Name}: {ex.Message}");
}