﻿namespace ResourceManager.SharedKernel;

public interface IDateTimeProvider
{
    public DateTime UtcNow { get; }
}

