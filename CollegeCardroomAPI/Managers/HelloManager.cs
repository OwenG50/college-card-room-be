using CollegeCardroomAPI.Managers.Interfaces;
using CollegeCardroomAPI.Repositories.Interfaces;

namespace CollegeCardroomAPI.Managers
{
    public class HelloManager : IHelloManager
    {
        private readonly IHelloRepository helloRepository;

        public HelloManager(IHelloRepository helloRepository)
        {
            this.helloRepository = helloRepository;
        }

        public string GetHello()
        {
            var message = helloRepository.GetHelloMessage();

            return message;
        }
    }
}
