namespace DialoguePoc.Dialogue
{
    /*
        When engaging in dialogue in this game, two things can happen. Either the dialogue just advances, or the player
        is given a prompt to which they must respond. 
        A dialogue tree structure is the resulting data structure because the dialogue can have many branching paths. 
        Because the dialogue nodes can have a few forms, we need an abstract class to represent the different node in the tree. 
        In the future we should probably have nodes to record important decisions or events, or bake those right into each node.

    */
    public abstract class DialogueNode
    {
        public string SpeakerName {get; set;}
        public string Emotion {get; set;}
        public string DialogueExitEvent {get; set;}
        public string Text {get; set;}
        public abstract void Play();

    }

}