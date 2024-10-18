using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSO_P2
{
    public class FileParser
    {
        ParseStrategy? strat;

        public FileParser (string strategy, string filePath)
        {
            switch (strategy)
            {
                default: strat = new TXTStrategy(filePath); 
                    break;
            }
        }

        public List<ICommand> ReadFile()
        {
            return strat.ReadFile();
        }

    }
}
