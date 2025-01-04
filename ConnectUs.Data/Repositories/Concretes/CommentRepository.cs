using ConnectUs.Data.Context;
using ConnectUs.Data.Repositories.Abstractions;
using ConnectUs.Entity.Entities;
using ConnectUs.Entity.Entities.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectUs.Data.Repositories.Concretes
{
    public class CommentRepository : ICommentRepository
    {
        private readonly AppDbContext _context;

        public CommentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Comment>> FindByProjectIdAndStatusAsync(long projectId, EStatus status)
        {
            return await _context.Comment
                .Where(c => c.ProjectId == projectId && c.Status == status)
                .ToListAsync();
        }

        public async Task<List<Comment>> FindByProjectIdAsync(long projectId)
        {
            return await _context.Comment
                .Where(c => c.ProjectId == projectId)
                .ToListAsync();
        }

        public async Task<Comment> FindByIdAsync(long commentId)
        {
            return await _context.Comment
                .FirstOrDefaultAsync(c => c.Id == commentId);
        }

        public async Task SaveAsync(Comment comment)
        {
            if (comment.Id == 0)
                _context.Comment.Add(comment);
            else
                _context.Comment.Update(comment);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Comment comment)
        {
            _context.Comment.Remove(comment);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Comment>> GetAllAsync()
        {
            return await _context.Comment.ToListAsync();
        }
    }
}

