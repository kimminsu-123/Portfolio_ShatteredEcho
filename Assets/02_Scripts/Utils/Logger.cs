using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace ShEcho.Utils
{
	public static class Logger
	{
		[Conditional("ENABLE_DEBUG")]
        public static void Log(string title, string msg)
        {
            Debug.Log($"[{title}] : {msg}");
        }
        
        [Conditional("ENABLE_DEBUG")]
        public static void Log(string title, string msg, Color color)
        {
            Debug.Log($"<color=#{ColorUtility.ToHtmlStringRGBA(color)}>[{title}] : {msg}</color>");
        }
        
        [Conditional("ENABLE_DEBUG")]
        public static void LogError(string title, string msg)
        {
            Debug.LogError($"[{title}] : {msg}");
        }
        
        [Conditional("ENABLE_DEBUG")]
        public static void LogWarning(string title, string msg)
        {
            Debug.LogWarning($"[{title}] : {msg}");
        }
        
        [Conditional("ENABLE_DEBUG")]
        public static void Assert(bool condition, string title, string msg)
        {
            Debug.Assert(condition, $"[{title}] : {msg}");
        }
        
        [Conditional("ENABLE_DEBUG")]
        public static void LogException(Exception exception)
        {
            Debug.LogException(exception);
        }
        
        [Conditional("ENABLE_DEBUG")]
        public static void DrawLine(Vector3 from, Vector3 to, Color color, float duration = 0.2f)
        {
            Debug.DrawLine(from, to, color, duration);
        }
        
        [Conditional("ENABLE_DEBUG")]
        public static void DrawRay(Ray ray, Color color, float duration = 0.2f)
        {
            Debug.DrawRay(ray.origin, ray.direction, color, duration);
        }
        
        [Conditional("ENABLE_DEBUG")]
        public static void DrawRay(Vector3 start, Vector3 dir, Color color, float duration = 0.2f)
        {
            Debug.DrawRay(start, dir, color, duration);
        }

        [Conditional("ENABLE_DEBUG")]
        public static void Break()
        {
            Debug.Break();
        }
	}
}