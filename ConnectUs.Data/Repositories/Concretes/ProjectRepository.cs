using ConnectUs.Data.Context;
using ConnectUs.Data.Repositories.Abstractions;
using ConnectUs.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectUs.Data.Repositories.Concretes
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly AppDbContext _context;

        public ProjectRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Project> FindByIdAsync(long id)
        {
            return await _context.Project.FindAsync(id);
        }

        public async Task<List<Project>> FindAllAsync()
        {
            return await _context.Project.ToListAsync();
        }

        public async Task SaveAsync(Project project)
        {
            _context.Project.Add(project);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Project project)
        {
            _context.Project.Remove(project);
            await _context.SaveChangesAsync();
        }
    }
}

