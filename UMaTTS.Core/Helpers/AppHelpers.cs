using System;
using System.Collections.Generic;
using UMaTLMS.Core.Entities;

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

    public static void Shuffle<T>(this List<T> list, int seed = 82)
    {
        var random = new Random(seed);
        for (var i = list.Count - 1; i > 0; i--)
        {
            var j = random.Next(i + 1);
            (list[i], list[j]) = (list[j], list[i]);
        }
    }
    
    public static void Shuffle(this List<Lecture> lectures, int seed = 82)
    {
        var random = new Random(seed);

        for (var i = lectures.Count - 1; i > 0; i--)
        {
            var j = random.Next(i + 1);
            (lectures[i], lectures[j]) = (lectures[j], lectures[i]);
        }

        var practicalLectures = lectures.Where(lecture => lecture.IsPractical &&
            lecture.SubClassGroups.Any(x => x.Name.Split(" ")[1].Contains("A")) ||
            lecture.SubClassGroups.Any(x => x.Name.Split(" ")[1].Contains("B")))
            .OrderBy(x => x.Lecturer?.Name)
            .ThenBy(x => x.SubClassGroups.FirstOrDefault()?.Name)
            .ToList();

        var otherLectures = lectures.Where(lecture => !(lecture.IsPractical &&
            lecture.SubClassGroups.Any(x => x.Name.Split(" ")[1].Contains("A")) ||
            lecture.SubClassGroups.Any(x => x.Name.Split(" ")[1].Contains("B")))).ToList();

        // Shuffle non-practical lectures
        otherLectures = otherLectures.OrderBy(x => random.Next()).ToList();

        // Merge the two lists back, keeping practical lectures in place
        int practicalIndex = 0;
        int otherIndex = 0;

        for (int i = 0; i < lectures.Count; i++)
        {
            var isPracticalAndDivided = lectures[i].IsPractical &&
                lectures[i].SubClassGroups.Any(x => x.Name.Split(" ")[1].Contains("A")) ||
                lectures[i].SubClassGroups.Any(x => x.Name.Split(" ")[1].Contains("B"));

            if (isPracticalAndDivided)
            {
                lectures[i] = practicalLectures[practicalIndex];
                practicalIndex++;
            }
            else
            {
                lectures[i] = otherLectures[otherIndex];
                otherIndex++;
            }
        }
    }

    public const string WhiteSpace = " ";
}
