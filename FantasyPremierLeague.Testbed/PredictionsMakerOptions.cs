using System;
using System.Collections.Generic;
using System.Text;

namespace FantasyPremierLeague.Testbed
{
    public class PredictionsMakerOptions
    {
        public string OutputPath { get; private set; }
        public int ElementType { get; private set; }
        public int NumPredictions { get; private set; }

        private static int PositionToElementType(string position)
        {
            switch (position)
            {
                case "GK":
                    return 1;
                default:
                    throw new ArgumentException($"Unrecognized position '{position}'", nameof(position));
            }
        }

        public static PredictionsMakerOptions FromArgs(IEnumerable<string> args)
        {
            string outputPath = String.Empty;
            int elementType = 0;
            int numPredictions = 5;

            foreach(string arg in args)
            {
                string[] keyValueSplit = arg.Split('=');
                if (keyValueSplit.Length != 2)
                    continue;

                string key = keyValueSplit[0];
                string value = keyValueSplit[1];

                switch(key)
                {
                    case "path":
                        outputPath = value;
                        break;
                    case "position":
                        elementType = PositionToElementType(value);
                        break;
                }
            }

            if (outputPath == String.Empty)
                throw new ArgumentException("No path argument specified", nameof(args));

            if (elementType == 0)
                throw new ArgumentException("No position argument specified", nameof(args));

            return new PredictionsMakerOptions
            {
                OutputPath = outputPath,
                ElementType = elementType,
                NumPredictions = numPredictions
            };
        }
    }
}
