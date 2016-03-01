using System;
using System.Diagnostics;
using System.Dynamic;
using FourInLineConsole.Interfaces;
using ImpromptuInterface;

namespace FourInLineConsole.Infra
{
    public class Wrapper<T> : DynamicObject
    {
        private readonly T m_wrappedObject;
        private readonly IGameContainer m_gameGameContainer;
        private readonly ILogger m_logger;

        public static T1 Wrap<T1>(T obj, IGameContainer gameGameContainer, ILogger logger) where T1 : class
        {
            if (!typeof(T1).IsInterface)
                throw new ArgumentException("T1 must be an Interface");

            return new Wrapper<T>(obj, gameGameContainer, logger).ActLike<T1>();
        }

        //you can make the constructor private so you are forced to use the Wrap method.
        private Wrapper(T obj, IGameContainer gameGameContainer, ILogger logger)
        {
            m_wrappedObject = obj;
            m_gameGameContainer = gameGameContainer;
            m_logger = logger;
        }

        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            try
            {
                bool nextStepMethod = binder.Name.ToLower() == "nextstep";

                Stopwatch stopWatch = new Stopwatch();
                if (nextStepMethod)
                {
                    stopWatch.Start();
                }

//                //do stuff here
//                Console.WriteLine("wrapper <before>");

                //call _wrappedObject object
                result = m_wrappedObject.GetType().GetMethod(binder.Name).Invoke(m_wrappedObject, args);

                if (nextStepMethod)
                {
                    stopWatch.Stop();
                    TimeSpan ts = stopWatch.Elapsed;
                    m_logger.Info("elapsed time: player={0}, duration = {1} ms", m_gameGameContainer.GetLastStep().Player.Name, ts.TotalMilliseconds);
                }

//                Console.WriteLine("wrapper <after>");
//
                return true;
            }
            catch
            {
                result = null;
                return false;
            }
        }
    }
}