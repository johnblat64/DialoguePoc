using DialoguePoc.Dialogue;

namespace DialoguePoc
{
    class Program
    {
        static void Main(string[] args)
        {
            var dialogueInitializer = new DialogueInitializer("Bobby", "School", "Day01", "Afternoon");
            var dialogueTraverser = new DialogueController(dialogueInitializer);
            dialogueTraverser.Process();
        }
    }
}

