using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MVC_CORE_WITHOUTENTTY.Models;

namespace MVC_CORE_WITHOUTENTTY.Controllers
{
    public class BookController : Controller
    {
        private readonly IConfiguration _configuration;

        public BookController(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        // GET: Book
        public IActionResult Index()
        {
            DataTable dtb = new DataTable();

            using (SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("DevConnection")))
            {
                sqlConnection.Open();
                SqlDataAdapter sqlda = new SqlDataAdapter("BookViewAll", sqlConnection);
                sqlda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sqlda.Fill(dtb);


            }
            return View(dtb);
        }




        // GET: Book/AddOEdit/
        public IActionResult AddOEdit(int? id)
        {
            BookViewModel bookViewModel = new BookViewModel();
            if (id > 0)
                bookViewModel = FetchBookByID(id);
            return View(bookViewModel);
        }

        // POST: Book/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddOEdit(int id, [Bind("BookID,Title,Author,Price")] BookViewModel bookViewModel)
        {


            if (ModelState.IsValid)
            {
                using (SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("DevConnection")))
                {
                    sqlConnection.Open();
                    SqlCommand sqlcmd = new SqlCommand("AddOEdit", sqlConnection);
                    sqlcmd.CommandType = CommandType.StoredProcedure;

                    sqlcmd.Parameters.AddWithValue("BookID", bookViewModel.BookID);
                    sqlcmd.Parameters.AddWithValue("Title", bookViewModel.Title);
                    sqlcmd.Parameters.AddWithValue("Author", bookViewModel.Author);
                    sqlcmd.Parameters.AddWithValue("Price", bookViewModel.Price);
                    sqlcmd.ExecuteNonQuery();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(bookViewModel);
        }

        // GET: Book/Delete/5
        public IActionResult Delete(int? id)
        {
            BookViewModel bookViewModel = FetchBookByID(id);

            return View(bookViewModel);
        }

        // POST: Book/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("DevConnection")))
            {
                sqlConnection.Open();
                SqlCommand sqlcmd = new SqlCommand("BookDeletedByID", sqlConnection);
                sqlcmd.CommandType = CommandType.StoredProcedure;

                sqlcmd.Parameters.AddWithValue("BookID",id);
                
                sqlcmd.ExecuteNonQuery();
            }
            return RedirectToAction(nameof(Index));
        }

        [NonAction]
        public BookViewModel FetchBookByID(int? id)
        {
            BookViewModel bookViewModel = new BookViewModel();
            using (SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("DevConnection")))
            {
                DataTable dbtl = new DataTable();

                sqlConnection.Open();
                SqlDataAdapter sqlda = new SqlDataAdapter("BookViewByID", sqlConnection);
                sqlda.SelectCommand.Parameters.AddWithValue("BookID", id);
                sqlda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sqlda.Fill(dbtl);
                if (dbtl.Rows.Count == 1)
                {
                    bookViewModel.BookID = Convert.ToInt32(dbtl.Rows[0]["BookID"].ToString());
                    bookViewModel.Title = dbtl.Rows[0]["Title"].ToString();
                    bookViewModel.Author = dbtl.Rows[0]["Author"].ToString();
                    bookViewModel.Price = Convert.ToInt32(dbtl.Rows[0]["Price"].ToString());
                }
                return bookViewModel;

            }
        }
    }
}