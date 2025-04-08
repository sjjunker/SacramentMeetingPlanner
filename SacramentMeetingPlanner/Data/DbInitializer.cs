using Microsoft.EntityFrameworkCore;
using SacramentMeetingPlanner.Models;

namespace SacramentMeetingPlanner.Data
{
    public static class DbInitializer
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new SacramentMeetingPlannerContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<SacramentMeetingPlannerContext>>()))
            {
                if (context.Planner.Any())
                {
                    return;   // DB has been seeded
                }

                var meetings = new Planner[]
                {
                    new Planner
                    {
                        Date = DateTime.Parse("2025-09-01"),
                        Conducting = "John English",
                        Invocation = "Amy Jones",
                        Benediction = "Aaron Paul",
                        OpeningHymn = "Be Thou Humble",
                        SacramentHymn = "Where Can I Turn for Peace?",
                        IntermediateHymn = "Does the Journey Seem Long?",
                        ClosingHymn = "When Faith Endures",
                        SpeakersTopics = new List<SpeakerTopic>{
                                new SpeakerTopic{
                                    Speaker = "James Paul",
                                    Topic = "The Spirit"
                                },
                                new SpeakerTopic{
                                    Speaker = "Garen Paul",
                                    Topic = "Fasting"
                                },
                            },

                    },
                    new Planner
                    {
                        Date = DateTime.Parse("2025-05-06"),
                        Conducting = "John Goodman",
                        Invocation = "Amy Palmer",
                        Benediction = "Robert Deniro",
                        OpeningHymn = "More Holiness Give Me",
                        SacramentHymn = "Father in Heaven?",
                        IntermediateHymn = "Does the Journey Seem Long?",
                        ClosingHymn = "I Believe in Christ",
                        SpeakersTopics = new List<SpeakerTopic>{
                                new SpeakerTopic{
                                    Speaker = "Garen Yammeth",
                                    Topic = "Spiritual Courage"
                                },
                            },

                    },
                    new Planner
                    {
                        Date = DateTime.Parse("2025-04-28"),
                        Conducting = "Ammon Aaronson",
                        Invocation = "Jane Austin",
                        Benediction = "Roberto Hernandez",
                        OpeningHymn = "Testimony",
                        SacramentHymn = " My Redeemer Lives",
                        IntermediateHymn = "I Know That My Redeemer Lives",
                        ClosingHymn = "Did You Think to Pray?",
                        SpeakersTopics = new List<SpeakerTopic>{
                                new SpeakerTopic{
                                    Speaker = "James",
                                    Topic = "Spirit and Revelation"
                                },
                                new SpeakerTopic{
                                    Speaker = "Mike",
                                    Topic = "Priesthood Authority"
                                },
                                new SpeakerTopic{
                                    Speaker = "Michelle Schmudge",
                                    Topic = "Service"
                                },
                            },

                    },
                    new Planner
                    {
                        Date = DateTime.Parse("2025-06-21"),
                        Conducting = "Nathaniel Pringle",
                        Invocation = "Janet Krammer",
                        Benediction = "Himmel Rodriguez",
                        OpeningHymn = "Sweet Is the Work",
                        SacramentHymn = "Prayer Is the Soul’s Sincere Desire",
                        IntermediateHymn = "Sweet Hour of Prayer",
                        ClosingHymn = "Let the Holy Spirit Guide",
                        SpeakersTopics = new List<SpeakerTopic>{
                                new SpeakerTopic{
                                    Speaker = "Jimmy Gordon",
                                    Topic = "Charity"
                                },
                            },

                    }
                };

                foreach (Planner m in meetings)
                {
                    context.Planner.Add(m);
                }
                context.SaveChanges();
            }
        }
    }
}
