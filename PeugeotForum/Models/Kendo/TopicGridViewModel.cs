namespace PeugeotForum.Models.Kendo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Linq.Expressions;

    using PeugeotForum.Models;

    public class TopicGridViewModel
    {
        public static Expression<Func<Topic, TopicGridViewModel>> FromTopic
        {
            get
            {
                return t => new TopicGridViewModel()
                {
                    Title = "<a href=\"Topic/ViewTopic?topicId=" + t.TopicId + "&page=0\">" + t.Title + "</a>",
                    CreatedOn = t.DateCreated
                };
            }
        }

        [Required]
        public string Title { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }
    }
}