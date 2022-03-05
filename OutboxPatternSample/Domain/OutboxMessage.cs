namespace OutboxPatternSample.Domain
{
    public class OutboxMessage
    {

        //		CREATE TABLE app.OutboxMessages
        //(
        //	[Id] UNIQUEIDENTIFIER NOT NULL,
        //	[OccurredOn] DATETIME2 NOT NULL,
        //	[Type] VARCHAR(255) NOT NULL,
        //	[Data] VARCHAR(MAX) NOT NULL,
        //	[ProcessedDate] DATETIME2 NULL,
        //	CONSTRAINT[PK_app_OutboxMessages_Id] PRIMARY KEY([Id] ASC)
        //)
        public OutboxMessage(Guid id, DateTime time, string type, string data)
        {
            Id = id;
            Time = time;
            Type = type;
            Data = data;
        }
        public void MarkAsProcessed(DateTime processingTime)
        {
            IsProcessed = true;
            ProcessingTime = processingTime;
        }
        public Guid Id { get; private set; }
        public DateTime Time { get; private set; }
        public string Type { get; private set; }
        public string Data { get; private set; }

        public bool IsProcessed { get; private set; }
        public DateTime? ProcessingTime { get; private set; }
    }
}
