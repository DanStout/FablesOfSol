using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public interface IStateListener
{
    void StateEntered(string name);
    void StateExited(string name);
}
