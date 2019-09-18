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

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic;

namespace TextualRealityExperienceEngine.GameEngine.Interfaces
{
    public interface IGame
    {
        string Prologue { get; set; }
        string HelpText { get; set; }
        IRoom StartRoom { get; set; }
        IRoom CurrentRoom { get; set; }
        IVisitedRooms VisitedRooms { get; set; }

        DifficultyEnum Difficulty { get; set; }       
        IParser Parser { get; }
        IGlobalState GlobalState { get; }
        int NumberOfMoves { get; set; }
        int Score { get; }
        bool HintSystemEnabled { get; set; }
        int HintCost { get; }
        IPlayer Player { get; set; }
        DateTime GameClock { get; set; }
        ITextSubstitute TextSubstitute { get; }

        void IncreaseScore(int increaseBy);
        void DecreaseScore(int decreaseBy);
        GameReply ProcessCommand(string command);
        IContentManagement ContentManagement { get; }
        ReadOnlyCollection<ICommand> SaveGame();
        void LoadGame(ReadOnlyCollection<ICommand> commands);
    }
}
