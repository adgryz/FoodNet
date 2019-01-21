using FoodNET.WebAPI.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodNet.ModelCore.Domain;
using FoodNet.DataAccessCore;

namespace FoodNET.WebAPI.Data
{
    public class TagsRepository : ITagsRepository
    {
        private FoodNetDbContext _context;

        public TagsRepository(FoodNetDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Tag> GetAllTags()
        {
            return _context.Tags.ToList();
        }

        public Tag GetTagById(Guid tagId)
        {
            return _context.Tags.Where(t => t.Id == tagId).FirstOrDefault();
        }
    }
}
