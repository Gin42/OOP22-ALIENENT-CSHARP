using System.Runtime.CompilerServices;

namespace AlienentShop;
internal static class ProjectSourcePath
{
    public static string GetSourceFilePathName([CallerFilePath] string? callerFilePath = null) //
        => callerFilePath ?? "";
    private const string myRelativePath = nameof(ProjectSourcePath) + ".cs";
    private static string? lazyValue;
    public static string Value => lazyValue ??= CalculatePath();

    private static string CalculatePath()
    {
        string pathName = GetSourceFilePathName();
        return pathName.Substring(0, pathName.Length - myRelativePath.Length);
    }
}