using System;
using System.Collections.Generic;
using System.Linq;

namespace jcOCLHashcatGUI.Helpers {
    public static class EnumToKeyValuePair {
        public static List<KeyValuePair<string, T>> ToKeyValuePair<T>(bool orderByKeyAsc = true, string replaceUnderscoreCharacter = null) {
            return Enum.GetValues(typeof(T)).Cast<T>().Select(a => new KeyValuePair<string,T>(
                a.ToString().Replace("_", replaceUnderscoreCharacter),
                a)).OrderBy(b => b.Key).ToList();
        }
    }
}