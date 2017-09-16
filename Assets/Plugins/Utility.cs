using System;
using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Text;

public static class Utility
{
    #region --- time ---

    private static DateTime epochStart = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
    private static double deltaTime = 0;

    public static double Time { get { return ((DateTime.UtcNow - epochStart).TotalSeconds + deltaTime); } }
    public static int TimeInt { get { return (int)Time; } }
    public static long TimeLong { get { return (long)Time; } }

   // private static string day;
   // private static string hour;
   // private static string min;
   // private static string sec;
   // public static bool initLoc = false;

    private static StringBuilder strConstructor = new StringBuilder();

    public static void SetServerTime(double serverTime)
    {
        double localTime = (DateTime.UtcNow - epochStart).TotalSeconds;
        deltaTime = serverTime - localTime;
    }

    #region ------ STRINGS ------
    private static void ClearStrConstructor()
    {
        strConstructor.Remove(0, strConstructor.Length);
    }

    public static string Format(string format, params object[] args) {
        ClearStrConstructor();
        strConstructor.AppendFormat(format, args);
        return strConstructor.ToString();
    }
    public static string Format(string format, object arg0)
    {
        ClearStrConstructor();
        strConstructor.AppendFormat(format, arg0);
        return strConstructor.ToString();
    }
    public static string Format(IFormatProvider provider, string format, params object[] args)
    {
        ClearStrConstructor();
        strConstructor.AppendFormat(provider, format, args);
        return strConstructor.ToString();
    }
    public static string Format(string format, object arg0, object arg1)
    {
        ClearStrConstructor();
        strConstructor.AppendFormat(format, arg0, arg1);
        return strConstructor.ToString();
    }
    public static string Format(string format, object arg0, object arg1, object arg2)
    {
        ClearStrConstructor();
        strConstructor.AppendFormat(format, arg0, arg1, arg2);
        return strConstructor.ToString();
    }

    public static int IncIntSafe(string key, int defaultValue = 0)
    {
        if (!PlayerPrefs.HasKey(key))
        {
            PlayerPrefs.SetInt(key,0);
           
            return 0;
        }
        int v = PlayerPrefs.GetInt(key);
        v++;
        PlayerPrefs.SetInt(key, v);
        return v;
    }





    #endregion

  /*  private static void InitLocs()
    {
        if (!initLoc) {
            day = Localization.Get("ui.word.day");
            hour = Localization.Get("ui.word.hour");
            min = Localization.Get("ui.word.min");
            sec = Localization.Get("ui.word.sec");
            initLoc = true;
        }
    }*/
    /*
    public static string GetLocalizedTime(int time)
    {
        InitLocs();
        return GetLocalizedTime(time, day, hour, min, sec);
    }
    public static string GetLocalizedTimeOnlyHours(int time)
    {
        InitLocs();
        return GetLocalizedTimeOnlyHours(time, day, hour, min, sec);
    }
    public static List<string> GetLocalizedTimeList(int time)
    {
        InitLocs();
        return GetLocalizedTimeList(time, day, hour, min, sec);
    }*/

    private static string shortConst = "{0:00}{1:00}";
    private static string longConst = "{0:00}{1:00} {2:00}{3:00}";


    private static List<string> GetLocalizedTimeList(int time, string d, string h, string m, string s)
    {
        List<string> res = new List<string>();
        time = Math.Max(0, time);

        int days = time / (86400);
        time = time - days * (86400);
        int hours = time / (3600);
        time = time - hours * (3600);
        int min = time / (60);
        int sec = time % (60);

        if(days > 0) {
            res.Add(Format(shortConst, days, d));
        } else if(hours > 0) {
            res.Add(Format(shortConst, hours, h));
            res.Add(Format(shortConst, min, m));
        } else if(min > 0) {
            res.Add(Format(shortConst, min, m));
            res.Add(Format(shortConst, sec, s));
        } else {
            res.Add(Format(shortConst, sec, s));
        }

        return res;
    }

    public static string GetLocalizedTime(int time, string d, string h, string m, string s)
    {
        time = Math.Max(0, time);

        int days = time / (86400);
        time = time - days * (86400);
        int hours = time / (3600);
        time = time - hours * (3600);
        int min = time / (60);
        int sec = time % (60);

        if (days > 0) {
            return Format(shortConst, days, d);
        } else if (hours > 0) {
            return /*min == 0 ? Format(shortConst, hours, h) :*/ Format(longConst, hours, h, min, m);
        } else if (min > 0) {
            return /*sec == 0 ? Format(shortConst, min, m) :*/ Format(longConst, min, m, sec, s);
        } else {
            return Format(shortConst, sec, s);
        }
    }

    public static string GetLocalizedTimeOnlyHours(int time, string d, string h, string m, string s)
    {
        time = Math.Max(0, time);

        int days = time / (86400);
        time = time - days * (86400);
        int hours = time / (3600);
        time = time - hours * (3600);
        int min = time / (60);
        int sec = time % (60);

        if(days > 0) {
            return Format(shortConst, days, d);
        } else if(hours > 0) {
            if(min == 0 && sec == 0) {
                return Format(shortConst, hours, h);
            } else {
                return Format(longConst, hours, h, min, m);
            }
        } else if(min > 0) {
            return Format(longConst, min, m, sec, s);
        } else {
            return Format(shortConst, sec, s);
        }
    }

   /* public static string GetLocalizedTimeWithoutZerros(int time)
    {
        InitLocs();
        return GetLocalizedTimeWithoutZerros(time, day, hour, min, sec);
    }*/
    public static string GetLocalizedTimeWithoutZerros(int time, string d, string h, string m, string s)
    {
        time = Math.Max(0, time);

        int days = time / (86400);
        time = time - days * (86400);
        int hours = time / (3600);
        time = time - hours * (3600);
        int min = time / (60);
        int sec = time % (60);

        if (days > 0)
        {
            if (hours == 0)
            {
                return Format("{0}{1}", days, d);
            }
            else
            {
                return Format("{0}{1} {2}{3}", days, d, hours, h);
            }
        }
        else if (hours > 0)
        {
            if (min == 0)
            {
                return Format("{0}{1}", hours, h);
            }
            else
            {
                return Format("{0}{1} {2}{3}", hours, h, min, m);
            }
        }
        else if (min > 0)
        {
            if (sec == 0)
            {
                return Format("{0}{1}", min, m);
            }
            else
            {
                return Format("{0}{1} {2}{3}", min, m, sec, s);
            }
        }
        else
        {
            return Format(shortConst, sec, s);
        }
    }

   /* public static string GetLocalizedTimeWithoutSecconds(int time)
    {
        InitLocs();
        return GetLocalizedTimeWithoutSecconds(time, day, hour, min);
    }*/

    public static string GetLocalizedTimeWithoutSecconds(int time, string d, string h, string m)
    {
        time = Math.Max(0, time);

        int days = time/(86400);
        time = time - days*(86400);
        int hours = time/(3600);
        time = time - hours*(3600);
        int min = time/(60);
        int sec = time%(60);

        if (days > 0) {
            if (hours == 0) {
                return Format("{0}{1}", days, d);
            }
            else {
                return Format("{0}{1} {2}{3}", days, d, hours, h);
            }
        }
        else if (hours > 0) {
            if (min == 0) {
                return Format("{0}{1}", hours, h);
            }
            else {
                return Format("{0}{1} {2}{3}", hours, h, min, m);
            }
        }
        else {
            return Format("{0}{1}", min, m);
        }
    }

    public static string GetTimer(int time)
    {
        time = Math.Max(0, time);

        int hours = time / (60 * 60);
        time = time - hours * (60 * 60);
        int min = time / (60);
        int sec = time % (60);
               
        return Format("{0:00}:{1:00}:{2:00}", hours, min, sec);
    }

    #endregion

    #region --- random ---

    // возвращает рандомный список значений, сумма которых равна заданному числу
    public static List<int> GenerateRanges(int rangesAmount, int rangesLenght, int rangeMin = 1)
    {
        int sum = 0;
        List<int> ranges = new List<int>();

        // распределяем общую длину rangesLenght между отрезками в кол-ве rangesAmount, с минимальной длиной отрезка rangeMin
        for (int i = 1; i <= rangesAmount; ++i) {
            int range = UnityEngine.Random.Range(rangeMin, (rangesLenght - sum - rangeMin * (rangesAmount - i)) + 1);
            sum += range;
            ranges.Add(range);
        }

        // проверяем накопленную сумму и добиваем какой-нибудь отрезок нужной длиной
        sum = 0;
        foreach (int range in ranges) {
            sum += range;
        }

        if (sum < rangesLenght) {
            int add = rangesLenght - sum;
            ranges[UnityEngine.Random.Range(0, ranges.Count)] += add;
        }

        return ranges;
    }

    #endregion

    #region --- NGUI ---

   /* public static void UpdateTransformDepth(Transform transform, int add)
    {
        UIWidget w = transform.gameObject.GetComponent<UIWidget>();
        if (w != null) {
            w.depth += add;
        }
        if (transform.childCount > 0) {
            foreach (Transform child in transform) {
                UpdateTransformDepth(child, add);
            }
        }
    }*/

    #endregion


    


    public static T CopyComponent<T>(T original, GameObject destination) where T : Component
    {
        System.Type type = original.GetType();
        Component copy = destination.AddComponent(type);
        System.Reflection.FieldInfo[] fields = type.GetFields();
        foreach (System.Reflection.FieldInfo field in fields)
        {
            field.SetValue(copy, field.GetValue(original));
        }
        return copy as T;
    }

    public static string PerfectNumber(long l, char delimiter = ' ')
    {
        string res = "";
        int ost = 0;
        for (int i = 0; i < 7; ++i)
        {
            ost = (int)(l % 1000);
            l = l / 1000;

            if (l == 0)
            {
                res = Format("{1}{2}{0}", res, ost, delimiter);
                break;
            }
            else
            {
                res = Format("{1:000}{2}{0}", res, ost, delimiter);
            }
        }
        res = res.Remove(res.Length - 1);
        return res;
    }

    public static void SetColorToShader(Color32 _color, GameObject gameObject)
    {
        MeshRenderer[] meshes = gameObject.GetComponentsInChildren<MeshRenderer>();
        for (int i = 0; i < meshes.Length; i++)
        {
            foreach (Material material in meshes[i].materials)
            {
                material.SetColor("_ColorRili", _color);
            }
        }

        SkinnedMeshRenderer[] meshesS = gameObject.GetComponentsInChildren<SkinnedMeshRenderer>();
        for (int i = 0; i < meshesS.Length; i++)
        {
            foreach (Material material in meshesS[i].materials)
            {
                material.SetColor("_ColorRili", _color);
            }
        }
    }

    #region --- files ---

    public static void CreateDirectoryForFile(string file)
    {
        string folder = Path.GetDirectoryName(file);
        if (Directory.Exists(folder) == false) {
            Directory.CreateDirectory(folder);
        }
    }

    public static void WriteAllBytes(string path, byte[] bytes)
    {
#if UNITY_WP8
            BinaryWriter writer = new BinaryWriter (File.OpenWrite(path));
            writer.Write(bytes);
            writer.Flush ();
#endif
        File.WriteAllBytes(path, bytes);
    }

    public static string ReadAllText(string path)
    {
        byte[] data = Utility.ReadAllBytes(path);
        return System.Text.Encoding.UTF8.GetString(data, 0, data.Length);
    }

    public static void WriteAllText(string path, string text)
    {
        byte[] bytes = System.Text.Encoding.UTF8.GetBytes(text);
        WriteAllBytes(path, bytes);
    }

    public static byte[] ReadAllBytes(string path)
    {
#if UNITY_WP8
            FileStream stream = File.OpenRead(path);
            byte[] data;
            int bytesRead;
            using (MemoryStream ms = new MemoryStream()){
                using (BinaryReader reader = new BinaryReader(stream)) {
                    while ((bytesRead = reader.Read(buffer, 0, BUFFER_SIZE)) > 0) {
                        ms.Write(buffer, 0, bytesRead);
                    }
                }
                data = ms.ToArray();
            }
            return data;
#else
        return File.ReadAllBytes(path);
#endif
    }

    public static string[] GetAllFiles(string directory, string exclude, string include)
    {
        List<string> files = new List<string>();
        GetAllFiles(files, directory, exclude, include);
        return files.ToArray();
    }

    public static string[] GetAllFiles(string directory)
    {
        return GetAllFiles(directory, string.Empty, string.Empty);
    }

    public static void GetAllFiles(List<string> allFiles, string directory, string exclude, string include)
    {
        Regex excludeRegex = string.IsNullOrEmpty(exclude) == false ? new Regex(exclude) : null;
        Regex includeRegex = string.IsNullOrEmpty(include) == false ? new Regex(include) : null;
        GetAllFiles(allFiles, directory, excludeRegex, includeRegex);
    }

    public static void GetAllFiles(List<string> allFiles, string directory, Regex exclude, Regex include)
    {
        if (Directory.Exists(directory)) {
            string[] localFiles = Directory.GetFiles(directory);
            for (int i = 0; i < localFiles.Length; i++) {
                string name = localFiles[i];
                if (exclude != null) {
                    if (exclude.IsMatch(name) == true) {
                        continue;
                    }
                }
                if (include != null) {
                    if (include.IsMatch(name) == false) {
                        continue;
                    }
                }
                allFiles.Add(name);
            }
            string[] directories = Directory.GetDirectories(directory);
            for (int i = 0; i < directories.Length; i++) {
                GetAllFiles(allFiles, directories[i], exclude, include);
            }
        }
    }

    public static void DeleteAllFiles(string directory)
    {
        DeleteAllFiles(directory, null, null);
    }

    public static void DeleteAllFiles(string directory, string exclude, string include)
    {
        if (Directory.Exists(directory) == false) {
            return;
        }
        string[] files = GetAllFiles(directory, exclude, include);
        for (int i = 0; i < files.Length; i++) {
            File.Delete(files[i]);
        }
        DeleteEmptyFolders(directory);
    }

    public static void DeleteEmptyFolders(string directory)
    {
        string[] dirs = Directory.GetDirectories(directory);
        string[] files = Directory.GetFiles(directory);

        if (dirs.Length == 0) {
            if (files.Length == 0) {
                Directory.Delete(directory);
            }
        } else {
            for (int i = 0; i < dirs.Length; i++) {
                DeleteEmptyFolders(dirs[i]);
            }
            dirs = Directory.GetDirectories(directory);
            if (dirs.Length == 0) {
                if (files.Length == 0) {
                    Directory.Delete(directory);
                }
            }
        }
    }

    #endregion
}
