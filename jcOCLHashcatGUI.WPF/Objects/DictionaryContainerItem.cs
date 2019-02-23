using System.Collections.Generic;
using System.Runtime.Serialization;

namespace jcOCLHashcatGUI.Objects {
    [DataContract]
    public class DictionaryContainerItem {
        [DataMember]
        public List<DictionaryItem> Dictionaries { get; set; }

        public DictionaryContainerItem() {
            Dictionaries = new List<DictionaryItem>();
        }
    }
}