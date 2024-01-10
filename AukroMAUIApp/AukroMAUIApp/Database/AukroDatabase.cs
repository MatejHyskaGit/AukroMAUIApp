using AukroMAUIApp.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AukroMAUIApp.Database
{
    internal class AukroDatabase
    {
        SQLiteAsyncConnection Database;

        public AukroDatabase()
        {

        }
        public async Task Init()
        {
            if (Database is not null)
                return;

            Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
            var result = await Database.CreateTableAsync<AukroUser>();
            await Database.CreateTableAsync<AukroItem>();
            await Database.CreateTableAsync<AukroCategory>();
        }
        public async Task<List<AukroUser>> GetUsersAsync()
        {
            await Init();
            return await Database.Table<AukroUser>().ToListAsync();
        }
        public async Task<List<AukroItem>> GetItemsAsync()
        {
            await Init();
            return await Database.Table<AukroItem>().ToListAsync();
        }
        public async Task<List<AukroCategory>> GetCategoriesAsync()
        {
            await Init();
            return await Database.Table<AukroCategory>().ToListAsync();
        }

        public async Task<AukroUser> GetUserAsync(int id)
        {
            await Init();
            return await Database.Table<AukroUser>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }
        public async Task<AukroItem> GetItemAsync(int id)
        {
            await Init();
            return await Database.Table<AukroItem>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }
        public async Task<AukroCategory> GetCategoryAsync(string name)
        {
            await Init();
            return await Database.Table<AukroCategory>().Where(i => i.Name == name).FirstOrDefaultAsync();
        }
        public async Task<int> SaveItemAsync(AukroUser user)
        {
            await Init();
            if (await Database.FindAsync<AukroUser>(user.Id) != null)
                return await Database.UpdateAsync(user);
            else
                return await Database.InsertAsync(user);
        }
        public async Task<int> SaveItemAsync(AukroItem item)
        {
            await Init();
            if (await Database.FindAsync<AukroItem>(item.Id) != null)
                return await Database.UpdateAsync(item);
            else
                return await Database.InsertAsync(item);
        }
        public async Task<int> SaveItemAsync(AukroCategory category)
        {
            await Init();
            if (await Database.FindAsync<AukroCategory>(category.Name) != null)
                return await Database.UpdateAsync(category);
            else
                return await Database.InsertAsync(category);
        }
        public async Task<int> DeleteItemAsync(AukroItem item)
        {
            await Init();
            return await Database.DeleteAsync(item);
        }
    }
}
