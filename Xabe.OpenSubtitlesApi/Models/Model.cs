namespace Xabe.OpenSubtitlesApi
{
    public class Model
    {
        public class Root
        {
            public string sent { get; set; }
            public Infos infos { get; set; }
            public string converted { get; set; }
            public Messages messages { get; set; }
            public float process_time { get; set; }
        }

        public class Infos
        {
            public string language { get; set; }
            public string language_iso { get; set; }
            public Language_Reliable language_reliable { get; set; }
            public Input input { get; set; }
            public Output output { get; set; }
        }

        public class Language_Reliable
        {
            public string name { get; set; }
            public string code { get; set; }
            public bool reliable { get; set; }
        }

        public class Input
        {
            public float fps { get; set; }
            public string charset_encoding { get; set; }
            public string data_compression { get; set; }
            public string sub_format { get; set; }
            public int words { get; set; }
            public int chars { get; set; }
            public string first_timestamp { get; set; }
            public string last_timestamps { get; set; }
        }

        public class Output
        {
            public string fps { get; set; }
            public string charset_encoding { get; set; }
            public string sub_format { get; set; }
            public int words { get; set; }
            public int chars { get; set; }
            public string first_timestamp { get; set; }
            public string last_timestamps { get; set; }
            public string data_encoding { get; set; }
            public string stripped_html { get; set; }
        }

        public class Messages
        {
            public string timeshift { get; set; }
            public string framerate { get; set; }
            public string convert_to { get; set; }
            public string output_data_encoding { get; set; }
        }
    }
}
