using System.Drawing;
using AlienEnt.CollisionHandler;
using AlienEnt.Commons;
using AlienEnt.Props;

namespace AlienEntTest
{
    [TestClass]
    public class CollisionHandlerTest
    {
        private static readonly Dictionary<PropStatistic, int> s_stats = new()
        {
            {PropStatistic.HP, 10}
        };
        private readonly ICollisionHandler _collisionHandler;
        
        public CollisionHandlerTest()
        {
            _collisionHandler = new SimpleCollisionHandler();
        }

        [TestMethod]
        public void TestCheckCollision()
        {
            var obj1 = new PropGameObject(Point2D.Origin, s_stats, "player");
            var hitbox1 = new PropHitbox(obj1, Point2D.Origin);
            obj1.Hitbox = hitbox1;
            var obj2 = new PropGameObject(new(1,0), s_stats, "enemy");
            var hitbox2 = new PropHitbox(obj2, Point2D.Origin);
            obj2.Hitbox = hitbox2;
            var obj3 = new PropGameObject(new(2,0), s_stats, "player");
            var hitbox3 = new PropHitbox(obj3, Point2D.Origin);
            obj3.Hitbox = hitbox3;
            List<PropGameObject> gameObj = new(){obj1, obj2, obj3};
            List<PropHitbox> hitboxes = new(){hitbox1, hitbox2, hitbox3};

            _collisionHandler.AddHitbox(hitbox1);
            _collisionHandler.AddHitbox(hitbox2);
            _collisionHandler.AddHitbox(hitbox3);
            _collisionHandler.AddHitbox(null);

            _collisionHandler.CheckCollisions();
            gameObj.ForEach(obj => Assert.IsTrue(obj.IsAlive));

            // Makes obj2 collide with obj3
            hitbox2.Mov = new(1,0);
            gameObj.ForEach(obj => obj.Update(1.0));
            _collisionHandler.CheckCollisions();
            gameObj.ForEach(obj => obj.Update(1.0));
            Assert.IsFalse(obj3.IsAlive);
            Assert.IsFalse(obj2.IsAlive);
            Assert.IsTrue(obj1.IsAlive);
            _collisionHandler.RemoveHitbox(hitbox3);
            _collisionHandler.RemoveHitbox(hitbox2);

            // Makes obj1 reach last obj3 position; shows it does not die.
            hitbox1.Mov = new(2,0);
            gameObj.ForEach(obj => obj.Update(1.0));
            _collisionHandler.CheckCollisions();
            gameObj.ForEach(obj => obj.Update(1.0));
            Assert.IsTrue(obj1.IsAlive);
        }
    }
}