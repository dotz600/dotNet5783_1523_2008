using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PL.BoEntityWindows.Admin;

internal interface App 
{
    public int foo()
    {
        Console.WriteLine("hi");
        return 0;
    }
}
