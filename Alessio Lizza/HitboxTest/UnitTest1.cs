using Alienent;
using Alienent.Api;
using Alienent.geometry;
using static Alienent.IBoundaryHitboxComponent;
using static Alienent.Api.IHitboxComponent;

namespace HitboxTest
{
    [TestClass]
    public class UnitTest1
    {
        private const int LIFE85 = 85;
        private const int LIFE999 = 999;
        private const int LIFE999N = -999;
        private const int LIFE899N = -899;
        private const int NUM5 = 5;
        private const int LIFE15 = 15;
        private const int LIFE50 = 50;
        private const int LIFE99 = 99;
        private static readonly Dictionary<Statistic, int> STATMAP1 = new() { { Statistic.HP, 100 }, { Statistic.DAMAGE, 50 } };
        private static readonly Dictionary<Statistic, int> STATMAP2 = new() { { Statistic.HP, 1 }, { Statistic.DAMAGE, 15 } };
        private static readonly Dictionary<Statistic, int> STATMAP3 = new() { { Statistic.HP, 100 }, { Statistic.DAMAGE, 1 } };
        private static readonly IGameObject OBJ1 = new GameObjectAbs(new Point2D(2, 0), Vector2D.NULL_VECTOR, STATMAP1, "null");
        private static readonly IGameObject OBJ2 = new GameObjectAbs(new Point2D(2, 0), Vector2D.NULL_VECTOR, STATMAP2, "null");
        private static readonly IGameObject OBJ3 = new GameObjectAbs(new Point2D(10, 0), Vector2D.NULL_VECTOR, STATMAP3, "null");
        private static readonly IGameObject OBJ4 = new GameObjectAbs(new Point2D(10, 10), Vector2D.NULL_VECTOR, STATMAP2, "null");

        [TestMethod]
        public void CanCollides()
        {
            var hitbox1 = new BomberHitboxComponentImpl(OBJ1, true, TypeObject.ENEMY, 2);
            var hitbox2 = new SimpleProjectileHitboxComponent(OBJ2, true, TypeObject.PROJECTILE, 2);
            hitbox2.SetShooter(TypeObject.PLAYER);
            var hitbox3 = new BoundaryHitboxComponent(OBJ3, true, TypeObject.BOUNDARY, new Point2D(0, 1), new Point2D(1, 1));
            hitbox3.SetLocations(Locations.DOWN);
            var hitbox4 = new SimpleProjectileHitboxComponent(OBJ4, true, TypeObject.PROJECTILE, 2);
            hitbox4.SetShooter(TypeObject.ENEMY);
            OBJ1.AddComponent(hitbox1);
            OBJ2.AddComponent(hitbox2);
            OBJ3.AddComponent(hitbox3);
            OBJ4.AddComponent(hitbox4);

            hitbox1.CanCollide(hitbox2);
            Assert.AreEqual(0, hitbox2.GetGameObject().GetHealth());
            Assert.AreEqual(LIFE85, hitbox1.GetGameObject().GetHealth());

            hitbox4.CanCollide(hitbox3);
            Assert.AreEqual(100, hitbox3.GetGameObject().GetHealth());

            hitbox2.CanCollide(hitbox3);
            Assert.AreEqual(LIFE999N, hitbox2.GetGameObject().GetHealth());

            hitbox1.CanCollide(hitbox4);
            Assert.AreEqual(LIFE85, hitbox1.GetGameObject().GetHealth());
            Assert.AreEqual(1, hitbox4.GetGameObject().GetHealth());
        }
        
        [TestMethod]
        public void Types()
        {
            OBJ2.SetPosition(new Point2D(NUM5, 0));
            var hitbox1 = new BomberHitboxComponentImpl(OBJ1, true, TypeObject.ENEMY, 2);
            var hitbox2 = new SimpleProjectileHitboxComponent(OBJ2, true, TypeObject.PROJECTILE, 2);
            var hitbox3 = new SimpleShipHitboxComponent(OBJ3, true, TypeObject.PLAYER, 2);
            OBJ1.AddComponent(hitbox1);
            OBJ2.AddComponent(hitbox2);
            OBJ3.AddComponent(hitbox3);
            Assert.AreEqual(TypeObject.ENEMY, hitbox1.GetTypeObject());
            Assert.AreEqual(TypeObject.PROJECTILE, hitbox2.GetTypeObject());
            Assert.AreEqual(TypeObject.PLAYER, hitbox3.GetTypeObject());
            Assert.AreNotEqual(hitbox1.GetTypeObject(), TypeObject.PROJECTILE);
            Assert.AreNotEqual(hitbox1.GetTypeObject(), TypeObject.PLAYER);
            Assert.AreNotEqual(hitbox2.GetTypeObject(), TypeObject.ENEMY);
            Assert.AreNotEqual(hitbox2.GetTypeObject(), TypeObject.PLAYER);
            Assert.AreNotEqual(hitbox3.GetTypeObject(), TypeObject.ENEMY);
            Assert.AreNotEqual(hitbox3.GetTypeObject(), TypeObject.PROJECTILE);
        }
        
        [TestMethod]
        public void CollidesHitbox()
        {
            OBJ1.SetPosition(new Point2D(0, 0));
            OBJ2.SetPosition(new Point2D(0, 0));
            OBJ3.SetPosition(new Point2D(0, 0));
            OBJ4.SetPosition(new Point2D(0, 0));
            OBJ1.Heal(LIFE15);
            OBJ2.Heal(1000);
            var hitbox1 = new BomberHitboxComponentImpl(OBJ1, true, TypeObject.ENEMY, 2);
            var hitbox2 = new SimpleProjectileHitboxComponent(OBJ2, true, TypeObject.PROJECTILE, 2);
            hitbox2.SetShooter(TypeObject.PLAYER);
            var hitbox3 = new SimpleShipHitboxComponent(OBJ3, true, TypeObject.PLAYER, 2);
            var hitbox4 = new SimpleProjectileHitboxComponent(OBJ4, true, TypeObject.PROJECTILE, 2);
            hitbox4.SetShooter(TypeObject.ENEMY);
            OBJ1.AddComponent(hitbox1);
            OBJ2.AddComponent(hitbox2);
            OBJ3.AddComponent(hitbox3);
            OBJ4.AddComponent(hitbox4);

            // Collisioni tra bomber e gli altri oggetti
            hitbox1.IsColliding(hitbox2);
            Assert.AreEqual(0, OBJ2.GetHealth());
            hitbox1.IsColliding(hitbox3);
            OBJ2.Heal(1);
            Assert.AreEqual(LIFE50, OBJ3.GetHealth());
            OBJ1.Heal(LIFE99);
            OBJ3.Heal(LIFE50);
            hitbox1.IsColliding(hitbox4);
            Assert.AreEqual(1, OBJ4.GetHealth());

            // Collisioni tra il proiettile sparato dal player e gli altri oggetti
            hitbox2.IsColliding(hitbox1);
            Assert.AreEqual(LIFE85, OBJ1.GetHealth());
            OBJ1.Heal(LIFE15);
            hitbox2.IsColliding(hitbox3);
            Assert.AreEqual(100, OBJ3.GetHealth());
            hitbox2.IsColliding(hitbox4);
            Assert.AreEqual(1, OBJ4.GetHealth());

            // Collisioni tra il player e gli altri oggetti
            hitbox3.IsColliding(hitbox1);
            Assert.AreEqual(100, OBJ1.GetHealth());
            hitbox3.IsColliding(hitbox2);
            Assert.AreEqual(1, OBJ2.GetHealth());
            hitbox3.IsColliding(hitbox4);
            Assert.AreEqual(0, OBJ4.GetHealth());
            OBJ4.Heal(1);

            // Collisioni tra il proiettile sparato dal nemico e gli altri oggetti
            hitbox4.IsColliding(hitbox1);
            Assert.AreEqual(100, OBJ1.GetHealth());
            hitbox4.IsColliding(hitbox2);
            Assert.AreEqual(1, OBJ2.GetHealth());
            hitbox4.IsColliding(hitbox3);
            Assert.AreEqual(LIFE85, OBJ3.GetHealth());
        }

        [TestMethod]
        public void CollideBoundary()
        {
            Dictionary<Statistic, int> statMap1 = new()
            {
                { Statistic.HP, 100 },
                { Statistic.DAMAGE, 1 }
            };
            var obj1 = new GameObjectAbs(new Point2D(2, 0), Vector2D.NULL_VECTOR, statMap1, "null");
            var obj2 = new GameObjectAbs(new Point2D(NUM5, 0), Vector2D.NULL_VECTOR, statMap1, "null");
            var obj3 = new GameObjectAbs(new Point2D(10, 0), Vector2D.NULL_VECTOR, statMap1, "null");
            var wallUp = new GameObjectAbs(new Point2D(0, 0), Vector2D.NULL_VECTOR, statMap1, "null");
            var wallRight = new GameObjectAbs(new Point2D(0, 0), Vector2D.NULL_VECTOR, statMap1, "null");
            var wallDown = new GameObjectAbs(new Point2D(0, 0), Vector2D.NULL_VECTOR, statMap1, "null");
            var wallLeft = new GameObjectAbs(new Point2D(0, 0), Vector2D.NULL_VECTOR, statMap1, "null");
            var hitbox1 = new BomberHitboxComponentImpl(obj1, true, TypeObject.ENEMY, 2);
            var hitbox2 = new SimpleProjectileHitboxComponent(obj2, true, TypeObject.PROJECTILE, 2);
            var hitbox3 = new SimpleShipHitboxComponent(obj3, true, TypeObject.PLAYER, 2);
            var hitboxUp = new BoundaryHitboxComponent(wallUp, true, TypeObject.BOUNDARY, new Point2D(0, 0), new Point2D(1, 1));
            hitboxUp.SetLocations(Locations.UP);
            var hitboxRight = new BoundaryHitboxComponent(wallRight, true, TypeObject.BOUNDARY, new Point2D(0, 0), new Point2D(1, 1));
            hitboxRight.SetLocations(Locations.RIGHT);
            var hitboxDown = new BoundaryHitboxComponent(wallDown, true, TypeObject.BOUNDARY, new Point2D(0, 0), new Point2D(1, 1));
            hitboxDown.SetLocations(Locations.DOWN);
            var hitboxLeft = new BoundaryHitboxComponent(wallLeft, true, TypeObject.BOUNDARY, new Point2D(0, 0), new Point2D(1, 1));
            hitboxLeft.SetLocations(Locations.LEFT);
            obj1.AddComponent(hitbox1);
            obj2.AddComponent(hitbox2);
            obj3.AddComponent(hitbox3);
            wallUp.AddComponent(hitboxUp);
            wallRight.AddComponent(hitboxRight);
            wallDown.AddComponent(hitboxDown);
            wallLeft.AddComponent(hitboxLeft);
            hitboxUp.IsColliding(hitbox2);
            Assert.AreEqual(LIFE899N, obj2.GetHealth());
            obj2.Heal(LIFE999);
            hitboxRight.IsColliding(hitbox2);
            Assert.AreEqual(LIFE899N, obj2.GetHealth());
            obj2.Heal(LIFE999);
            hitboxDown.IsColliding(hitbox2);
            Assert.AreEqual(LIFE899N, obj2.GetHealth());
            obj2.Heal(LIFE999);
            hitboxLeft.IsColliding(hitbox2);
            Assert.AreEqual(LIFE899N, obj2.GetHealth());
        }

    }
}