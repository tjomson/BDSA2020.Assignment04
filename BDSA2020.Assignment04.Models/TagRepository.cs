

using System.Linq;
using BDSA2020.Assignment04.Entities;

namespace BDSA2020.Assignment04.Models{



    class TagRepository : ITagRepository
    {

         private readonly KanbanContext _context;
        public TagRepository(KanbanContext context)
        {
            _context = context;
        }
        public (Response response, int tagId) Create(TagCreateDTO tag)
        {
            var TagWithSameName = _context.Tags.FirstOrDefault(t => t.Name == tag.Name);
            if(TagWithSameName != null){
                return (Response.Conflict,-1);
            }
            Tag newTag = new Tag {Name = tag.Name};
            _context.Add<Tag>(newTag);
            _context.SaveChanges();
            return (Response.Created, newTag.Id);
            
        }

        public Response Delete(int tagId, bool force = false)
        {
            throw new System.NotImplementedException();
        }

        public IQueryable<TagDTO> Read()
        {
            throw new System.NotImplementedException();
        }

        public TagDTO Read(int tagId)
        {
            throw new System.NotImplementedException();
        }

        public Response Update(TagUpdateDTO tag)
        {
            throw new System.NotImplementedException();
        }
    }
}