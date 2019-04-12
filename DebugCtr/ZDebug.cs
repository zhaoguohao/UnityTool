using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 一个方便管理的Debug输出类(默认打印颜色为黑色)
/// </summary>
public static class ZDebug
{
    /// <summary>
    /// 是否开启打印Log(正式发布时可选择关闭)
    /// </summary>
    public static bool enable = true;

    /// <summary>
    /// 输出内容的颜色
    /// </summary>
    public enum Color
    {
        /// <summary>
        /// 青色
        /// </summary>
        cyan,
        /// <summary>
        /// 白色
        /// </summary>
        white,
        /// <summary>
        /// 绿色
        /// </summary>
        green,
        /// <summary>
        /// 红色
        /// </summary>
        red,
        /// <summary>
        /// 黄色
        /// </summary>
        yellow,
        /// <summary>
        /// 蓝色
        /// </summary>
        blue,
        /// <summary>
        /// 黑色
        /// </summary>
        black
    }

    private static string Message(object message, Color color = Color.black, bool bold = false, bool italic = false)
    {
        var content = string.Format("<color={0}>{1}</color>", color.ToString(), message);
        content = bold ? string.Format("<b>{0}</b>", content) : content;
        content = italic ? string.Format("<i>{0}</i>", content) : content;

        return content;
    }

    private static string Title(object title, int level, Color color)
    {
        return Message(string.Format("{0}{1}{2}{1}", Space(level * 2), Character(16 - level * 2), title), color);
    }

    private static string ObjectMessage<T>(T content)
    {
        var result = "";

        var fields = typeof(T).GetFields();
        for (int i = 0; i < fields.Length; i++)
        {
            result += Space(4) + Message(fields[i].Name + " : ", Color.green, true) + fields[i].GetValue(content) + "\n";
        }

        var properties = typeof(T).GetProperties();
        for (int i = 0; i < properties.Length; i++)
        {
            result += Space(4) + Message(properties[i].Name + " : ", Color.green, true) + properties[i].GetValue(content, null) + "\n";
        }

        return result;
    }

    /// <summary>
    /// 普通输出
    /// </summary>
    /// <param name="message"></param>
    /// <param name="color"></param>
    public static void Log(object message, Color color = Color.black)
    {
        if (!enable)
        {
            return;
        }
        Debug.Log(Message(message, color));
    }

    /// <summary>
    /// 普通输出(标错log)
    /// </summary>
    /// <param name="message"></param>
    /// <param name="color"></param>
    public static void LogError(object message, Color color = Color.black)
    {
        if (!enable)
        {
            return;
        }
        Debug.LogError(Message(message, color));
    }

    /// <summary>
    /// 普通输出(警告log)
    /// </summary>
    /// <param name="message"></param>
    /// <param name="color"></param>
    public static void LogWarning(object message, Color color = Color.black)
    {
        if (!enable)
        {
            return;
        }
        Debug.LogWarning(Message(message, color));
    }

    /// <summary>
    /// 加粗输出
    /// </summary>
    /// <param name="message"></param>
    /// <param name="color"></param>
    public static void LogBold(object message, Color color = Color.black)
    {
        if (!enable)
        {
            return;
        }
        Debug.Log(Message(message, color, true));
    }

    /// <summary>
    /// 斜体输出
    /// </summary>
    /// <param name="message"></param>
    /// <param name="color"></param>
    public static void LogItalic(object message, Color color = Color.black)
    {
        if (!enable)
        {
            return;
        }
        Debug.Log(Message(message, color, false, true));
    }

    /// <summary>
    /// 加粗斜体输出
    /// </summary>
    /// <param name="message"></param>
    /// <param name="color"></param>
    public static void LogBoldItalic(object message, Color color = Color.black)
    {
        if (!enable)
        {
            return;
        }
        Debug.Log(Message(message, color, true, true));
    }

    /// <summary>
    /// 输出标题栏（形如:======Title======）
    /// </summary>
    /// <param name="title"></param>
    /// <param name="level"></param>
    /// <param name="color"></param>
    public static void LogTitle(object title, int level, Color color)
    {
        if (!enable)
        {
            return;
        }
        LogBold(Title(title, level, color));
    }

    /// <summary>
    /// 输出对象（自定义的）
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="content"></param>
    /// <param name="color"></param>
    public static void LogObject<T>(T content, Color color = Color.black)
    {
        if (!enable)
        {
            return;
        }
        Debug.Log(Message(typeof(T).Name, color, true) + "\n" + ObjectMessage(content));
    }

    /// <summary>
    /// 输出List
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="content"></param>
    public static void LogList<T>(List<T> content)
    {
        if (!enable || content == null || content.Count <= 0)
        {
            return;
        }

        var result = Title(typeof(T).Name + " List", 0, Color.green) + "\n\n";
        for (int i = 0; i < content.Count; i++)
        {
            result += Title(typeof(T).Name + "_" + i, 1, Color.green) + "\n";
            result += ObjectMessage<T>(content[i]) + "\n";
        }
        result += Title("End", 0, Color.green);

        Debug.Log(result);
    }

    /// <summary>
    /// 输出数组
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="content"></param>
    public static void LogArray<T>(T[] content)
    {
        if (!enable || content == null || content.Length <= 0)
        {
            return;
        }

        var result = Title(typeof(T).Name + " List", 0, Color.green) + "\n";
        for (int i = 0; i < content.Length; i++)
        {
            result += Title(typeof(T).Name + "_" + i, 1, Color.green) + "\n";
            result += ObjectMessage<T>(content[i]) + "\n";
        }
        result += Title("End", 0, Color.green);

        Debug.Log(result);
    }
    //搞事情的空位
    private static string Space(int number)
    {
        var space = "";
        for (int i = 0; i < number; i++)
        {
            space += " ";
        }
        return space;
    }
    //搞事情的=
    private static string Character(int number, string character = "=")
    {
        var result = "";
        for (int i = 0; i < number; i++)
        {
            result += "=";
        }
        return result;
    }
}
