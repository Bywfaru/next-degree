﻿using DegreePlanner.Services;

namespace DegreePlanner.Entities;

public abstract class BaseEntity : IAuditable
{
    /**
     * The unique identifier for the entity.
     */
    public string Id { get; set; } = Guid.NewGuid().ToString();

    /**
     * The date and time that the entity was created.
     */
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /**
     * The date and time that the entity was last updated.
     */
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}