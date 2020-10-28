namespace DialoguePoc.Dialogue
{
    /* 
        If the dialogue node is a prompt, we need a way to point to the next dialogue node depending on
        what option the player chooses
    */
    public class DialogueOption 
    {
        public string Text {get; set;}
        public DialogueNode ResponseDialogueNode {get; set;}
 
    }
}

