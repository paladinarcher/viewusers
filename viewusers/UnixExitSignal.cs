using Mono.Unix;
using Mono.Unix.Native;
using System;
using System.Threading.Tasks;

namespace ViewUsers
{
  public class UnixExitSignal : IExitSignal
  {
    public event EventHandler Exit;

    UnixSignal[] signals = new UnixSignal[]{
        new UnixSignal(Signum.SIGTERM),
        new UnixSignal(Signum.SIGINT),
        new UnixSignal(Signum.SIGUSR1)
    };

    public UnixExitSignal()
    {
      Task.Factory.StartNew(() =>
      {
        // blocking call to wait for any kill signal
        int index = UnixSignal.WaitAny(signals, -1);

        if (Exit != null)
        {
          Exit(null, EventArgs.Empty);
        }

      });
    }

  }
}
