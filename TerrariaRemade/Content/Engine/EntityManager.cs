using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerrariaRemade.Content.Engine
{
    static class EntityManager
    {
        static List<Entity> entities = new List<Entity>();
        static bool isUpdating;
        static List<Entity> addedEntities = new List<Entity>();
        public static int Count { get { return entities.Count; } }
        public static void Instantiate(Entity entity)
        {
            if (!isUpdating)
                entities.Add(entity);
            else
                addedEntities.Add(entity);

            entity.Awake();
            entity.Start();
        }
        public static void Update()
        {
            isUpdating = true;

            foreach (var entity in entities)
                entity.Update();

            isUpdating = false;

            foreach (var entity in addedEntities)
                entities.Add(entity);
                
            addedEntities.Clear();
            
            entities = entities.Where(x => !x.isExpired).ToList();
        }
        public static void Draw(SpriteBatch spriteBatch)
        {
            foreach (var entity in entities)
                entity.renderer.Render(spriteBatch, entity.transform);
        }
    }
}
