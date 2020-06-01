using System;
using System.Messaging;
using System.Threading;
using LSL;

namespace Sync
{
    class SendStringMarkers
    {
        static bool start = true;

        public static void Send(string marker)
        {
            MessageQueue messageQueue = null;

            string path = @".\Private$\eeg";
            string message = marker+" recording";

            try

            {

                if (MessageQueue.Exists(path))

                {
                    messageQueue = new MessageQueue(path);
                    messageQueue.Label = marker;
                }

                else

                {

                    MessageQueue.Create(path);

                    messageQueue = new MessageQueue(path);

                    messageQueue.Label = marker;
                }
                var tx = new MessageQueueTransaction();
                tx.Begin();
                messageQueue.Send(message, marker, tx);
                tx.Commit();
                

            }

            catch

            {

                throw;

            }

            finally

            {
                messageQueue.Dispose();
            }
        }

    }
}
