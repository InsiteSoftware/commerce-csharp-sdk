namespace CommerceApiSDK.Models
{
    public class FieldScoreDetailed : FieldScore
    {
        public float Boost { get; set; }

        public string MatchText { get; set; }

        public float TermFrequencyNormalized { get; set; }

        public float InverseDocumentFrequency { get; set; }

        public bool ScoreUsed { get; set; }
    }
}

