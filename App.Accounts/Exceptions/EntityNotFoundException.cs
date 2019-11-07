using System;

namespace App.Accounts.Exceptions
{
    public class EntityNotFoundException : SystemException
    {
        public Type EntityType { get; private set; }
        public EntityNotFoundException(Type entityType)
        {
            EntityType = entityType;
        }
    }
}
