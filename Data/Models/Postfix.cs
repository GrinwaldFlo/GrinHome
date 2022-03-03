namespace GrinHome.Data.Models
{
    public class Postfix
    {

        public long Id { get; set; }
        public DateTime DatePostfix { get; set; }

        public int Received { get; set; }
        public int Delivered { get; set; }
        public int Forwarded { get; set; }
        public int Deferred { get; set; }
        public int Bounced { get; set; }
        public int Rejected { get; set; }
        public int RejectedWarning { get; set; }

        public int Held { get; set; }
        public int Discarded { get; set; }

        public int BytesReceived { get; set; }
        public int BytesDelivered { get; set; }
        public int Senders { get; set; }
        public int SendingHostDomain { get; set; }
        public int Recipients { get; set; }
        public int RecipientHostDomain { get; set; }

        public string? TopMessageDelivery { get; set; } = "";

        public string? TopMessageReceived { get; set; } = "";
        public string? TopSendersCount { get; set; } = "";

        public string? TopRecipientsCount { get; set; } = "";

        public string? TopSendersSize { get; set; } = "";

        public string? TopRecipientsSize { get; set; } = "";

        public string? MessageRejectDetail { get; set; } = "";

        public int SmtpdWarnings { get; set; }
    }
}
