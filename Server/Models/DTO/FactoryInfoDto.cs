using System;

namespace Server.Models.DTO
{
    public class FactoryInfoDto
    {
        public Guid Id { get; private set; }
        public string DisplayName { get; set; }
        public DateTime CreatedAt { get; private set; }
        public int ItemsCount { get; }
        public int RecipesCount { get; }
        
        public FactoryInfoDto(Factory factory)
        {
            Id = factory.Id;
            DisplayName = factory.DisplayName;
            CreatedAt = factory.CreatedAt;
            ItemsCount = factory.ItemsCount;
            RecipesCount = factory.RecipesCount;
        }
    }
}