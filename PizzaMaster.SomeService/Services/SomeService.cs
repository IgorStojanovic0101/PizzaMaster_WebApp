using PizzaMaster.Application.Services;

namespace PizzaMaster.BusinessLogic.Services
{
    // Implement the service
    public class SomeService : ISomeService
    {
        private readonly Guid serviceId;

        public SomeService()
        {
            serviceId = Guid.NewGuid();
            Console.WriteLine($"Service instance created with ID: {serviceId}");
        }

        public Guid DoSomething()
        {
            return serviceId;
        }
    }
}