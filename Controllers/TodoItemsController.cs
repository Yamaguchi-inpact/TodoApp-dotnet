using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using aspnetserver.Models;

namespace aspnetserver.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly TodoContext _context;

        // TodoContextクラスをControllerに挿入
        public TodoItemsController(TodoContext context)
        {
            _context = context;
        }

        // GET: api/TodoItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodoItems()
        {
          if (_context.TodoItems == null)
          {
              return NotFound();
          }
            // DB内のToDoデータを全て取得して返す
            return await _context.TodoItems.ToListAsync();
        }

        // GET: api/TodoItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItem>> GetTodoItem(long id)
        {
          if (_context.TodoItems == null)
          {
              return NotFound();
          }
            // リクエストされたIDと一致するDB内のTodoアイテムを取得
            var todoItem = await _context.TodoItems.FindAsync(id);

            if (todoItem == null)
            {
                // 404 Not Found
                return NotFound();
            }

            // 200 OK
            return todoItem;
        }

        // GET: api/TodoItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItem>> Index(long id)
        {
            if (_context.TodoItems == null)
            {
                return NotFound();
            }
            // リクエストされたIDと一致するDB内のTodoアイテムを取得
            var todoItem = await _context.TodoItems.FindAsync(id);

            if (todoItem == null)
            {
                // 404 Not Found
                return NotFound();
            }

            // 200 OK
            return todoItem;
        }

        // POST: api/TodoItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TodoItem>> PostTodoItem(TodoItem todoItem)
        {
          if (_context.TodoItems == null)
          {
              return Problem("Entity set 'TodoContext.TodoItems'  is null.");
          }
            //POST時に日時を代入
            todoItem.Modified = DateTime.Now;
            todoItem.Created = DateTime.Now;
            // POSTされたデータをContextクラスに追加する
            _context.TodoItems.Add(todoItem);

            // Contextクラスに追加されたデータをDBに登録する（INSERT文が実行される）
            await _context.SaveChangesAsync();

            // ステータスコード201(Created)を返し、GetTodoItemメソッドを呼び出す
            return CreatedAtAction("GetTodoItem", new { id = todoItem.Id }, todoItem);
        }

        // PUT: api/TodoItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItem(long id, TodoItem todoItem)
        {
            if (id != todoItem.Id)
            {
                return BadRequest();
            }

            // エンティティ（todoItem）の状態をUnchangedからModifiedに変更する
            _context.Entry(todoItem).State = EntityState.Modified;

                var nowData = await _context.TodoItems.FindAsync(id);
            try
            {
                //nowData.Title = todoItem.Title;
                //nowData.Text = todoItem.Text;
                //nowData.IsComplete = todoItem.IsComplete;
                //PUT時に日時を代入

                nowData.Modified = todoItem.Modified;
                todoItem.Created = DateTime.Now;
                await _context.SaveChangesAsync();
            }
            // 同時実行制御に関する例外（更新中に別のユーザーによって値が書き換えられた場合など）
            catch (DbUpdateConcurrencyException)
            {
                if (!TodoItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/TodoItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            if (_context.TodoItems == null)
            {
                return NotFound();
            }
            var todoItem = await _context.TodoItems.FindAsync(id);
            if (todoItem == null)
            {
                return NotFound();
            }

            // Removeメソッドで該当のTodoアイテムを削除する
            _context.TodoItems.Remove(todoItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TodoItemExists(long id)
        {
            return (_context.TodoItems?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
