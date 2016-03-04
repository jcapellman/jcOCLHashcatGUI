using System;
using System.Collections.Generic;
using System.Linq;

using jcOCLHashcatGUI.WPF.Objects;

namespace jcOCLHashcatGUI.WPF.Helpers {
    public static class EnumToKeyValuePair {
        public static List<KeyValuePair> ToKeyValuePair<T>(bool orderByKeyAsc = true, string replaceUnderscoreCharacter = null) {
            return Enum.GetValues(typeof(T)).Cast<T>().Select(a => new KeyValuePair {
                Key = a.ToString().Replace("_", replaceUnderscoreCharacter),
                Value = a
            }).OrderBy(b => b.Key).ToList();
        }
    }
}