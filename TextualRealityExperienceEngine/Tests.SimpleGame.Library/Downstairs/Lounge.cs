/*
MIT License

Copyright (c) 2019 

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/
using TextualRealityExperienceEngine.GameEngine;
using TextualRealityExperienceEngine.GameEngine.Interfaces;
using TextualRealityExperienceEngine.GameEngine.TextProcessing.Interfaces;
using TextualRealityExperienceEngine.GameEngine.TextProcessing.Synonyms;

namespace TextualRealityExperienceEngine.Tests.SimpleGame.Library.Downstairs
{
    public class Lounge : Room
    {
        private readonly IObject _letter = new GameObject("Letter", "It is a hand written letter addressed to you.", "You pick up the letter.");

        public Lounge(IGame game) : base(game)
        {
            _letter.LongDescription = "You open the letter and read it. It says:\r\n\r\n   \"Son, There was an alien invasion. There is so much we havn't told you, but there is little time and we don't know where you are. You must make your way to our hidden crypt hidden in the garage. When you are there we will explain all.\"";

            game.ContentManagement.AddContentItem("LoungeName", "Lounge");
            game.ContentManagement.AddContentItem("LoungeDescription", "You are stand in the lounge. There is a sofa at the back of the room and a TV in the corner. Next to the TV there is a fireplace and mantlepiece with various objects on top fo it. There is a door back to the hallway to the east.");

            game.ContentManagement.AddContentItem("LetterDescription", "You open the letter and read it's contents. \r\n\r\n   It reads, \"Son, There was an alien invasion. There is so much we havn't told you, but there is little time and we don't know where you are. You must make your way to our hidden crypt hidden in the garage. When you are there we will explain all.\"");
            game.ContentManagement.AddContentItem("MentlePieceDescription", "In between various family photos there is a folded note with your name written on it.");
            game.ContentManagement.AddContentItem("AlreadyHaveLetter", "You already have the letter.");

            Name = Game.ContentManagement.RetrieveContentItem("LoungeName");
            Description = Game.ContentManagement.RetrieveContentItem("LoungeDescription");

            game.Parser.Nouns.Add("letter", "letter");
            game.Parser.Nouns.Add("envelope", "letter");
            game.Parser.Nouns.Add("note", "letter");
            game.Parser.Nouns.Add("folded", "letter");
            game.Parser.Nouns.Add("foldednote", "letter");
            game.Parser.Nouns.Add("paper", "letter");

            game.Parser.Nouns.Add("mantlepiece", "mantlepiece");
            game.Parser.Nouns.Add("fire", "mantlepiece");
            game.Parser.Nouns.Add("fireplace", "mantlepiece");
            game.Parser.Nouns.Add("hearth", "mantlepiece");

        }

        public override string ProcessCommand(ICommand command)
        {
            string response;

            switch (command.Noun)
            {
                case "letter":
                    response = HandleLetter(command);

                    if (!string.IsNullOrEmpty(response))
                    {
                        return response;
                    }
                    break;

                case "mantlepiece":
                    response = HandleMantlePiece(command);

                    if (!string.IsNullOrEmpty(response))
                    {
                        return response;
                    }                  
                    break;

            }
      
            if (command.ProfanityDetected)
            {
                return Game.ContentManagement.RetrieveContentItem("NoNeedToBeRude");
            }

            var reply = base.ProcessCommand(command);
            return reply;
        }

        private string HandleLetter(ICommand command)
        {
            switch (command.Verb)
            {
                case VerbCodes.Look:
                    {
                        Game.IncreaseScore(1);
                        Game.NumberOfMoves++;
                        return Game.ContentManagement.RetrieveContentItem("LetterDescription");
                    }

                case VerbCodes.Take:
                    {
                        if (!Game.Player.Inventory.Exists("letter"))
                        {
                            Game.Player.Inventory.Add(_letter.Name, _letter);
                            Game.IncreaseScore(1);
                            Game.NumberOfMoves++;
                            return _letter.PickUpMessage;
                        }

                        if (Game.Player.Inventory.Exists("letter"))
                        {
                            return Game.ContentManagement.RetrieveContentItem("AlreadyHaveLetter");
                        }
                    }
                    break;

            }

            return string.Empty;
        }

        private string HandleMantlePiece(ICommand command)
        {
            switch (command.Verb)
            {
                case VerbCodes.Look:
                    {
                        Game.NumberOfMoves++;
                        return Game.ContentManagement.RetrieveContentItem("MentlePieceDescription");
                    }
            }

            return string.Empty;
        }
    }
}