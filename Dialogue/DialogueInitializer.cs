using System.Text;
using System.IO;
using System.Text.Json;
using System;


namespace DialoguePoc.Dialogue
{
    public class DialogueInitializer
    {
        private readonly string _speaker;
        private readonly string _location;
        private readonly string _day;
        private readonly string _timeOfDay;

        public DialogueInitializer(
            string speaker,
            string location,
            string day,
            string timeOfDay)
        {
            _speaker = speaker;
            _location = location;
            _day = day;
            _timeOfDay = timeOfDay;
        }

        public DialogueNode BuildDialogueNodeStructureFromJson()
        {
            /*
                For now, I'm storing the raw dialogue json in a directory structure for Days, TimeOfDay,
                Location, and Speaker you're talking to. The raw dialogue file will be here.
            */
            var projectRoot = Directory.GetCurrentDirectory();
            var filepath = projectRoot + "/Dialogue/DialogueFiles/" +  _day + "/" + _timeOfDay + "/" + _location + "/" + _speaker + "/dialogue02.json";
            
            string jsonText = File.ReadAllText(filepath);

            /* 
                We can't directly deserialize the Json because the dialogue node structure is based on abstract nodes in a 
                tree structure. Each node links to the next, or possible next nodes in some way. The nodes can be of different
                concrete types. Because of this we need to handle the Conversion manually. 
                A Utf8Json reader is needed to do the reading.
            */
            var utf8JsonText = Encoding.UTF8;

            byte[] utfBytesJsontext = utf8JsonText.GetBytes(jsonText);
            
            System.Buffers.ReadOnlySequence<byte> readOnlySequence = new System.Buffers.ReadOnlySequence<byte>(utfBytesJsontext);

            var reader = new Utf8JsonReader(readOnlySequence);

            
            DialogueNode builtDialogueNode = DialogueNodeJsonConverter.Read(ref reader);
            
            // TODO: Once knowledge can affect which dialogue gets set, that needs to be configured here or inside the json converter

            return builtDialogueNode;
        }
    }
}