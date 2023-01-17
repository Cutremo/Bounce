using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Bounce.Gameplay.Application.Runtime;
using Bounce.Gameplay.Domain.Runtime;
using UnityEngine;

namespace Bounce.Gameplay.Presentation.Tests.Runtime.Bounce.Gameplay.Presentation.Runtime
{
    public class UnityScoreView : ScoreView
    {
        readonly List<PointsText> pointTexts;

        public UnityScoreView(List<PointsText> pointText)
        {
            this.pointTexts = pointText;
        }
        public Task SetScore(Score _, CancellationToken ct)
        {
            var tasks = new List<Task>();
            foreach(var pointText in pointTexts)
                tasks.Add(pointText.UpdatePoints(ct));
            return Task.WhenAll(tasks.ToArray());
        }
    }
}