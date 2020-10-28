using System.Text.Json;
using System.Collections.Generic;

namespace DialoguePoc.Dialogue
{
    public class DialogueNodeJsonConverter
    {
        /*
            Each abstract object in the json structure needs a type discriminator so that we know what 
            type of node the object is
        */
        enum TypeDiscriminator
        {
            DialogueLine = 1,
            DialoguePrompt = 2
        }

        public static DialogueNode Read(ref Utf8JsonReader reader)
        {
            /* 
            
                This first if block is checking to see if the reader has yet to read anything. This method can be called recursively 
                for nested json structures. Its called recursively with the reference to the same reader - a new one is not creates.
                If the reader is already in the middle of the json structure, this block won't execute. 
                This really just "initializes" the reader to point to the first Token in the json structure rather than nothing.
            
            */
            if (reader.TokenType == JsonTokenType.None) 
            {
                reader.Read();
            }
            
            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException();
            }
            
            reader.Read();
            if (reader.TokenType != JsonTokenType.PropertyName)
            {
                throw new JsonException();
            }

            string propertyForType = reader.GetString();
            if (propertyForType != "Type")
            {
                throw new JsonException();
            }

            reader.Read();
            if (reader.TokenType != JsonTokenType.Number)
            {
                throw new JsonException();
            }

            TypeDiscriminator typeDiscriminator = (TypeDiscriminator)reader.GetInt32();
            DialogueNode dialogueNode = typeDiscriminator switch
            {
                TypeDiscriminator.DialogueLine => new DialogueLine(),
                TypeDiscriminator.DialoguePrompt => new DialoguePrompt(),
                _ => throw new JsonException()
            };

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndObject)
                {
                    return dialogueNode;
                }

                if (reader.TokenType == JsonTokenType.PropertyName)
                {
                    string propertyName = reader.GetString();
                    reader.Read();
                    switch (propertyName)
                    {
                        case "SpeakerName":
                            string speakerName = reader.GetString();
                            dialogueNode.SpeakerName = speakerName;
                            break;
                        case "Emotion":
                            string emotion = reader.GetString();
                            dialogueNode.Emotion = emotion;
                            break;
                        case "Text":
                            string text = reader.GetString();
                            dialogueNode.Text = text;
                            break;
                        case "NextDialogueNode":
                            ((DialogueLine)dialogueNode).NextDialogueNode = Read(ref reader);
                            break;
                        case "DialogueOptions" : 
                            if (reader.TokenType != JsonTokenType.StartArray) 
                            {
                                throw new JsonException();
                            }

                            ((DialoguePrompt)dialogueNode).Options = new List<DialogueOption>();

                            reader.Read();
                            while(reader.TokenType != JsonTokenType.EndArray)
                            {

                                if (reader.TokenType != JsonTokenType.StartObject) 
                                {
                                    throw new JsonException();
                                }

                                var dialogueOption = new DialogueOption();

                                while(reader.TokenType != JsonTokenType.EndObject)
                                {
                                    reader.Read();

                                    if (reader.TokenType != JsonTokenType.PropertyName) 
                                    {
                                        throw new JsonException();
                                    }
                                    string dialogueOptionPropertyName = reader.GetString();
                                    reader.Read();
                                    switch (dialogueOptionPropertyName) 
                                    {
                                        case "Text" :
                                            dialogueOption.Text = reader.GetString();
                                            break;
                                        case "ResponseDialogueNode" :
                                            dialogueOption.ResponseDialogueNode = Read(ref reader);
                                            break;
                                    }
                                }
                                reader.Read();
                                reader.Read();
                                ((DialoguePrompt)dialogueNode).Options.Add(dialogueOption);
                            }
                            break;
                    }
                }
            }
            throw new JsonException();
        }
    }
}
       