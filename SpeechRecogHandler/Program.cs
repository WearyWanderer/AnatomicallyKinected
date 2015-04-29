using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Speech.Recognition;
using System.IO;



namespace SpeechServer
{
    class CommandRecogniser
    {
        private readonly SpeechRecognitionEngine recogniser;
        private string utterance;

        public CommandRecogniser(SpeechRecognitionEngine recogniser)
        {
            this.recogniser = recogniser;
            this.utterance = null;
        }

        public string WaitFor()
        {
            recogniser.SpeechRecognized += this.SpeechRecognizedHandler;
            while (utterance == null)
            {
                recogniser.Recognize();
            }
            recogniser.SpeechRecognized -= this.SpeechRecognizedHandler;

            return utterance;
        }

        public void SpeechRecognizedHandler(object sender, SpeechRecognizedEventArgs args)
        {
            utterance = args.Result.Text;
        }
    }

    class Program
    {
        private readonly SpeechRecognitionEngine recogniser;

        public Program()
        {
            this.recogniser = new SpeechRecognitionEngine();
            this.recogniser.SetInputToDefaultAudioDevice();
            this.recogniser.LoadGrammar(MakeGrammar());
        }

        public void Start() //treat this as our class' main loop
        {
                while (true)
                {
                    CommandRecogniser commandRecogniser = new CommandRecogniser(recogniser);
                    Console.WriteLine("?");
                    String command = commandRecogniser.WaitFor();
                    Console.WriteLine("ACKNOWLEDGED: " + command);

                    int logNum = checkUtterance(command);

                    // Write each directory name to a file. 
                    using (StreamWriter sw = new StreamWriter("commandStream.txt"))
                    {
                        sw.WriteLine(logNum);
                    }
                }
        }

        private static Grammar MakeGrammar()
        {
            // TODO: This is part of the grammar mentioned in the lecture;
            // replace it with a grammar that recognises the commmands
            // listed on the exercise sheet
            
            Choices modeType = new Choices ("skeleton", "muscular", "full body", "human");

            Choices quitType = new Choices("quit", "close", "abandon", "kill");

            Choices showMeParts = new Choices("skull", "pelvis", "left arm", "right leg", "left leg", "right arm", "ribcage", "ribs", "spine", "skeleton", "whole skeleton");

            Choices postureChoices = new Choices("on", "off");

            GrammarBuilder switchModeBuild = new GrammarBuilder();
            switchModeBuild.Append("Switch to");
            switchModeBuild.Append(modeType);
            switchModeBuild.Append("mode");

            GrammarBuilder showMeBuild = new GrammarBuilder();
            showMeBuild.Append("Show me the");
            showMeBuild.Append(showMeParts);

            GrammarBuilder postureBuild = new GrammarBuilder();
            postureBuild.Append("Capture posture");
            postureBuild.Append(postureChoices);

            GrammarBuilder quitBuild = new GrammarBuilder();
            quitBuild.Append(quitType);
            quitBuild.Append("program");

            Choices multiChoice = new Choices(switchModeBuild, showMeBuild, postureBuild, quitBuild);

            return new Grammar(multiChoice);
        }

        static void Main(string[] args)
        {
            new Program().Start();
        }

        int checkUtterance(string fullUtterance)
        {
            int returnNumberToBeLogged = 0;
            string[] ssize = fullUtterance.Split(null);

            if (ssize[0] == "Switch")
            {
                if (ssize[2] == "skeleton")
                {
                    returnNumberToBeLogged = 0;
                }
                else if (ssize[2] == "muscular")
                {
                    returnNumberToBeLogged = 1;
                }
                else if (ssize[2] == "human" || ssize[2] + ssize[3] == "fullbody")
                {
                    returnNumberToBeLogged = 2;
                }
            }
            else if (ssize[0] == "Show")
            {
                if (ssize[3] == "skull")
                {
                    returnNumberToBeLogged = 3;
                }
                else if (ssize[3] == "pelvis")
                {
                    returnNumberToBeLogged = 4;
                }
                else if (ssize[3] == "ribcage")
                {
                    returnNumberToBeLogged = 9;
                }
                else if (ssize[3] == "ribs")
                {
                    returnNumberToBeLogged = 9;
                }
                else if (ssize[3] == "spine")
                {
                    returnNumberToBeLogged = 10;
                }
                else if (ssize[3] == "skeleton")
                {
                    returnNumberToBeLogged = 11;
                }
                else if (ssize[3] + ssize[4] == "leftarm")
                {
                    returnNumberToBeLogged = 5;
                }
                else if (ssize[3] + ssize[4] == "rightleg")
                {
                    returnNumberToBeLogged = 6;
                }
                else if (ssize[3] + ssize[4] == "leftleg")
                {
                    returnNumberToBeLogged = 7;
                }
                else if (ssize[3] + ssize[4] == "rightarm")
                {
                    returnNumberToBeLogged = 8;
                }
                else if (ssize[3] + ssize[4] == "ribcage")
                {
                    returnNumberToBeLogged = 9;
                }
                else if (ssize[3] + ssize[4] == "wholeskeleton")
                {
                    returnNumberToBeLogged = 11;
                }
            }
            else if(ssize[0] + ssize[1] == "Captureposture")
            {
                if(ssize[2] == "on")
                {
                    returnNumberToBeLogged = 12;
                }
                else if(ssize[2] == "off")
                {
                    returnNumberToBeLogged = 13;
                }
            }
            else if (ssize[1] == "program")
            {
                returnNumberToBeLogged = 14;
            }

            return returnNumberToBeLogged;
        }
    }



}
