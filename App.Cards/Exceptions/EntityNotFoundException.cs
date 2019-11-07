﻿using System;

namespace App.Cards.Exceptions
{
    class EntityNotFoundException : Exception
    {
        public long Number { get; private set; }
        public Type EntityType { get; private set; }
        public EntityNotFoundException(long number, Type entityType) : base ($"Entity ///")
        {
            Number = number;
            EntityType = entityType;
        }
    }
}
