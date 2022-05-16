using System.Collections.Generic;

namespace CommerceApiSDK.Models
{
    public class ScoreExplanation
    {
        public float TotalBoost { get; set; }

        public IList<FieldScore> AggregateFieldScores { get; set; }

        public IList<FieldScoreDetailed> DetailedFieldScores { get; set; }
    }
}
