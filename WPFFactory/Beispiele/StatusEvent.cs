namespace WPFFactory.Beispiele
{
    using System;

    public sealed class StatusEvent
    {
        public StatusEvent(Guid id, string message)
        {
            this.Id = id;
            this.Message = message;
        }

        public Guid Id { get; private set; }
        public string Message { get; private set; }
    }
}
