using System;

namespace DAL.Repositories
{
    [AttributeUsage(AttributeTargets.Class)]
    public class RepositoryAttribute : Attribute
    {
        public Type Type { get; set; }
        public RepositoryAttribute(Type type)
        {
            Type = type;
        }
    }
}