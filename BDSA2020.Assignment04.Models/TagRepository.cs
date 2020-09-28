

using System.Collections.Generic;
using System.Linq;
using BDSA2020.Assignment04.Entities;

namespace BDSA2020.Assignment04.Models{

    public class TagRepository : ITagRepository
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
 
            var tag = _context.Tags.Find(tagId);
            if (tag == null) return Response.NotFound;

            
            if (!tag.Tasks.Select(TaskTag => TaskTag.Task).All(Task => Task.State == State.New) && !force)
                return Response.Conflict;

            _context.Tags.Remove(tag);
            _context.SaveChanges();
            return Response.Deleted;
                            
        }

        public IQueryable<TagDTO> Read()
        {
            var returnlist = new List<TagDTO>();
            foreach (var tag in _context.Tags) 
            {
                returnlist.Add(Read(tag.Id));
            }
            return returnlist.AsQueryable();
        }

        public TagDTO Read(int tagId)
        {
            var tag = _context.Tags.Find(tagId);
            if(tag == null) return null;
        
            return new TagDTO(){
                Id = tag.Id,
                Name = tag.Name,
                New = tag.Tasks.Count(TaskTag => TaskTag.Task.State == State.New),
                Active = tag.Tasks.Count(TaskTag => TaskTag.Task.State == State.Active),
                Resolved = tag.Tasks.Count(TaskTag => TaskTag.Task.State == State.Resolved),
                Closed = tag.Tasks.Count(TaskTag => TaskTag.Task.State == State.Closed),
                Removed = tag.Tasks.Count(TaskTag => TaskTag.Task.State == State.Removed)
            };
        }

        public Response Update(TagUpdateDTO tag)
        {
             var asTag = _context.Tags.Find(tag.Id);
             if(asTag == null) return Response.NotFound;

             asTag.Name = tag.Name;
             _context.Tags.Update(asTag);
             _context.SaveChanges();
             

            return Response.Updated;
        }
    }
}