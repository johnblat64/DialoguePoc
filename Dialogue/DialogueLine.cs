using System;
namespace DialoguePoc.Dialogue
{
    public class DialogueLine : DialogueNode
    {
        
        #nullable enable
        public DialogueNode? NextDialogueNode {get; set;}

        public override void Play()
        {
            Console.WriteLine("");
            Console.WriteLine("-----------------");
            Console.WriteLine("Speaker: " + SpeakerName);
            Console.WriteLine("Emotion: " + Emotion);
            Console.WriteLine(Text);
            Console.WriteLine("-----------------");
            Console.WriteLine("");

            Console.WriteLine(" Hit Enter to Advance ->");
            Console.ReadKey();
        }
    }
}