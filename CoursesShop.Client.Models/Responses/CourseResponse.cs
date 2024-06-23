﻿namespace CoursesShop.Client.Models.Responses
{
    public sealed class CourseResponse
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public double ReviewsAverge { get; set; }
        public string? ImageUrl { get; set; }
        public string TeacherId { get; set; }
        public string TeacherName { get; set; }


    }
}
