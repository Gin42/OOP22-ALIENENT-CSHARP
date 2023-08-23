using AlienEnt.CollisionHandler;

namespace AlienEntTest
{
    [TestClass]
    public class CollisionHandlerTest
    {
        private readonly ICollisionHandler _collisionHandler;
        
        public CollisionHandlerTest()
        {
            _collisionHandler = new SimpleCollisionHandler();
        }

        [TestMethod]
        public void TestCheckCollision()
        {
            
        }
    }
}