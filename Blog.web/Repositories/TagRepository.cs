﻿using Azure;
using Blog.web.Data;
using Blog.web.Models.Domain;
using Blog.web.Models.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace Blog.web.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly BlogDbContext _blogDbContext;

        public TagRepository(BlogDbContext blogDbContext)
        {
            this._blogDbContext = blogDbContext;

        }
        public async Task<Tag> AddAsync(Tag tag)
        {
            await _blogDbContext.Tags.AddAsync(tag);
            await _blogDbContext.SaveChangesAsync();
            return tag;
        }

        public async Task<Tag?> DeleteAsync(Guid id)
        {
            var existingTag = await _blogDbContext.Tags.FindAsync(id);
            if (existingTag != null)
            {
                _blogDbContext.Tags.Remove(existingTag);
                await _blogDbContext.SaveChangesAsync();
                return existingTag;

            }
            return null;
        }

        public async Task<IEnumerable<Tag>> GetAllAsync()
        {
            return await _blogDbContext.Tags.ToListAsync();
        }

        public async Task<Tag?> GetAsync(Guid id)
        {
            return await _blogDbContext.Tags.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Tag> UpdateAsync(Tag tag)
        {
            var existingTag = await _blogDbContext.Tags.FindAsync(tag.Id);
            if (existingTag != null)
            {
                existingTag.Name = tag.Name;
                existingTag.DisplayName = tag.DisplayName;

                await _blogDbContext.SaveChangesAsync();
                return existingTag;
            }
            return null;
        }
    }
}
