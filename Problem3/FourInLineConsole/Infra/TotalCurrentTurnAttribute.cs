using System;
using System.Threading;
using System.Threading.Tasks;
using PostSharp.Aspects;

namespace FourInLineConsole.Infra
{
    [Serializable]
    public sealed class TotalCurrentTurnAttribute : OnMethodBoundaryAspect
    {
        const int S1 = 1000;
        private readonly int _maxSeconds;
        [NonSerialized]
        private CancellationTokenSource _cancelationTokenSource;
        [NonSerialized] 
        private Task _task;
        // Default constructor, invoked at build time.
        public TotalCurrentTurnAttribute()
        {
        }

        // Constructor specifying the tracing category, invoked at build time.
        public TotalCurrentTurnAttribute(int maxSeconds)
        {
            _maxSeconds = maxSeconds;
        }

        // Invoked at runtime before that target method is invoked.
        public override void OnEntry(MethodExecutionArgs args)
        {
            _cancelationTokenSource = new CancellationTokenSource();

            var token = _cancelationTokenSource.Token;
            _task = Task.Factory.StartNew(() =>
            {
                Thread.Sleep(_maxSeconds*S1);
            }, token).ContinueWith(t =>
            {
                token.ThrowIfCancellationRequested();
                Console.WriteLine("warning: current turn is over {0} seconds", _maxSeconds);
            }, token);
        }

        // Invoked at runtime after the target method is invoked (in a finally block).
        public override void OnExit(MethodExecutionArgs args)
        {
            _cancelationTokenSource.Cancel();       
        }
    }
}