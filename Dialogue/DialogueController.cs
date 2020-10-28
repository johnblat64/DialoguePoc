
namespace DialoguePoc.Dialogue
{
    public class DialogueController
    {
        private readonly DialogueInitializer _dialogueInitializer;
        public DialogueNode CurrentDialogueNode {get; set;}

        public DialogueController(DialogueInitializer dialogueInitializer)
        {
            _dialogueInitializer = dialogueInitializer;
            CurrentDialogueNode = dialogueInitializer.BuildDialogueNodeStructureFromJson();
        }

        public void Process()
        {
            while(CurrentDialogueNode != null)
            {
                CurrentDialogueNode.Play(); // the "what-to-do" with the node should actually be done by something outside of the node itself.
                CurrentDialogueNode = GetNextNode();
            } 
        }

        public DialogueNode GetNextNode()
        {
            return CurrentDialogueNode switch
            {
                DialogueLine dl => dl.NextDialogueNode,
                DialoguePrompt dp => dp.SelectedOption.ResponseDialogueNode,
                _ => throw new System.Exception() // just throw a generic error for now if something goes wrong
            };
        }

    }
}