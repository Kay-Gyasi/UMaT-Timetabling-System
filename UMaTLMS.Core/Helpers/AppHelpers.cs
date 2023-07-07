using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMaTLMS.Core.Helpers;

public static class AppHelpers
{
    public static class HostEnvironment
    {
        public static bool IsDocker => Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") == "true";
    }

    public static string GetSubClassSuffix(int count)
    {
        return count switch
        {
            1 => "A",
            2 => "B",
            3 => "C",
            4 => "D",
            5 => "E",
            6 => "F",
            7 => "G",
            8 => "H",
            9 => "I",
            10 => "J",
            11 => "K",
            12 => "L",
            13 => "M",
            14 => "N",
            15 => "O",
            16 => "P",
            17 => "Q",
            18 => "R",
            _ => ""
        };
    }

    public static int[] GetSubClassSizes(this int? totalClassSize, int numOfSubClasses)
    {
        if (totalClassSize is null) return Array.Empty<int>();
        var baseSize = (totalClassSize / numOfSubClasses) ?? 0;
        var remainingSize = totalClassSize % numOfSubClasses;

        var sizes = new int[numOfSubClasses];

        for (int i = 0; i < numOfSubClasses; i++)
        {
            sizes[i] = baseSize;
        }

        for (int i = 0; i < remainingSize; i++)
        {
            sizes[i]++;
        }

        return sizes;
    }
    public static void Shuffle<T>(List<T> list)
    {
        var random = new Random();

        for (var i = list.Count - 1; i > 0; i--)
        {
            var j = random.Next(i + 1);
            (list[i], list[j]) = (list[j], list[i]);
        }
    }
}
