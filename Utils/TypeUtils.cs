using System;
using System.Reflection;

namespace KeyGeneralPurposeLibrary.Utils {
  public static class TypeUtils {
    public static T GetInstance<T>(this Type type, string name) where T : MemberInfo {
      if (typeof(T) == typeof(FieldInfo)) {
        if (type.GetField(name) != null) {
          return type.GetField(name) as T;
        }
        return type.GetField(name, BindingFlags.NonPublic | BindingFlags.Instance) as T;
      }
      if (typeof(T) == typeof(PropertyInfo)) {
        if (type.GetProperty(name) != null) {
          return type.GetProperty(name) as T;
        }
        return type.GetProperty(name, BindingFlags.NonPublic | BindingFlags.Instance) as T;
      }
      if (typeof(T) == typeof(MethodInfo)) {
        if (type.GetMethod(name) != null) {
          return type.GetMethod(name) as T;
        }
        return type.GetMethod(name, BindingFlags.NonPublic | BindingFlags.Instance) as T;
      }
      return null;
    }
  }
}