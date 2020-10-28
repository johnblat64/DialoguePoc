using System;
using System.Collections.Generic;
using System.Linq;

namespace DialoguePoc.Dialogue
{
    public class DialoguePrompt : DialogueNode
    {
        public List<DialogueOption> Options {get; set;} 
        public DialogueOption SelectedOption {get; set;}

        public override void Play()
        {
            Console.WriteLine("");
            Console.WriteLine("-----------------");
            Console.WriteLine("Speaker: "+SpeakerName);
            Console.WriteLine(Text);
            Console.WriteLine("Make choice: ");
            int i = 0;
            foreach(var option in Options)
            {
                i++;
                Console.WriteLine(i +": "+option.Text);
                
            }
            
            int choiceEntered;
            do {
                choiceEntered = Int32.Parse(Console.ReadLine());
            } while (choiceEntered <= 0 || choiceEntered > i);


            SelectedOption = Options.ElementAt(choiceEntered - 1); // answer is not zero indexed. The Element position is. 

            Console.WriteLine("----------------");
            Console.WriteLine("");
            
        }
    }
}