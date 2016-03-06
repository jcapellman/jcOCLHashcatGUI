using System.Runtime.Serialization;

namespace jcOCLHashcatGUI.WPF.Objects {
    [DataContract]
    public class DictionaryItem {
        [DataMember]
        public string FilePath { get; set; }

        [DataMember]
        public bool IsChecked { get; set; }
    }
}