using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace jcOCLHashcatGUI.WPF.Helpers {
    public static class Extensions {
        public static void AddRange<T>(this ObservableCollection<T> collection, IEnumerable<T> list) {
            foreach (var item in list) {
                collection.Add(item);
            }
        }
    }
}