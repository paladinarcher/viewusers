using System;

namespace ViewUsers
{
    public interface IExitSignal
    {
      event EventHandler Exit;
    }
}