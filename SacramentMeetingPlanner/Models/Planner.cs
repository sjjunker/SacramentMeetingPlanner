using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.TagHelpers;

namespace SacramentMeetingPlanner.Models
{
    public class Planner
    {
        public int PlannerId { get; set; }
        [DataType(DataType.Date)]
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public string? Conducting { get; set; }

        [Display(Name = "Opening Hymn")]
        [Required]
        public string? OpeningHymn { get; set; }
        [Required]
        public string? Invocation { get; set; }

        [Display(Name = "Sacrament Hymn")]
        [Required]
        public string? SacramentHymn { get; set; }

        [Display(Name = "Intermediate Hymn")]
        public string? IntermediateHymn { get; set; }

        [Display(Name = "Closing Hymn")]
        [Required]
        public string? ClosingHymn { get; set; }
        [Required]
        public string? Benediction { get; set; }
        [Required]
        public List<SpeakerTopic> SpeakersTopics { get; set; } = new List<SpeakerTopic>();

        public override string ToString()
        {
            string formatted = $"PlannerId: {PlannerId}, Conduction:{Conducting}, OpeningHymn: {OpeningHymn}, Invocation{Invocation}\n"+
                $"SacramentHymn: {SacramentHymn}, IntermediateHymn: {IntermediateHymn}, ClosingHymn: {ClosingHymn}, Benediction: {Benediction}\n";
            foreach(SpeakerTopic i in SpeakersTopics)
            {
                formatted += i.ToString();
                formatted += "\n";
            }
            return formatted;
        }

    }

    public class SpeakerTopic
    {
        public int SpeakerTopicId { get; set; }
        public int PlannerId { get; set; }

        public string? Speaker { get; set; }
        public string? Topic { get; set; }

        public override string ToString()
        {
            return $"SpeakerTopicId:{this.SpeakerTopicId}, PlannerId: {this.PlannerId}, Speaker: {this.Speaker}, Topic: {this.Topic}";
        }
    }
}
