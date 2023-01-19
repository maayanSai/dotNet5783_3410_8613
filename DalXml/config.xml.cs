using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    internal static class config
    {
        internal const int s_startOrderNumber = 1000;
        private static int s_nextOrderNumber = s_startOrderNumber;
        internal static int NextOrderNumber 
        { 
            get => s_nextOrderNumber++; 
        }


    }
}
