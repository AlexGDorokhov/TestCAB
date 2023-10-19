using System;

namespace Services.Attributes
{
    [AttributeUsage(AttributeTargets.Field)]
    public class InjectService : Attribute
    {
        
    }
    
    [AttributeUsage(AttributeTargets.Class)]
    public class ServiceInjectAttribute : Attribute
    {
        public readonly Type injectedType;

        public ServiceInjectAttribute(Type injectedType)
        {
            this.injectedType = injectedType;
        }
    }


}

