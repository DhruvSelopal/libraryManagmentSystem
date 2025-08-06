using Microsoft.EntityFrameworkCore;
public static class SqlFunctions
{
    // Check if user exists by username
    private static bool UserExists(LibraryContext lib, string username)
    {
        // Count users with username
        int count = lib.Users
            .FromSqlRaw("SELECT * FROM dbo.Users WHERE UserName = {0}", username)
            .Count();

        return count > 0;
    }

    public static bool CreateUser(LibraryContext lib, SignUpRequest user)
    {
        if (UserExists(lib, user.UserName))
            return false;

        int rows = lib.Database.ExecuteSqlRaw(@"
            INSERT INTO dbo.Users
            (UserName, FirstName, LastName, PhoneNo, UserAddress, password, email, age)
            VALUES
            ({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7})",
            user.UserName, user.FirstName, user.LastName, user.PhoneNo, user.UserAddress,
            user.password, user.email, user.age);

        return rows > 0;
    }

    public static bool Login(LibraryContext lib, LoginRequest login)
    {
        // Check username and password match
        int count = lib.Users
            .FromSqlRaw("SELECT * FROM dbo.Users WHERE UserName = {0} AND password = {1}",
                login.Username, login.Password)
            .Count();

        return count > 0;
    }

    public static List<Books> GetBooksIssuedByUser(LibraryContext lib, string username)
    {
        var books = lib.Books
            .FromSqlRaw(@"
                SELECT b.*
                FROM dbo.Books b
                INNER JOIN dbo.IssueRecords ir ON b.BookId = ir.BookId
                WHERE ir.UserName = {0} AND ir.IsReturned = 0",
                username)
            .ToList();

        return books;
    }

    public static bool UpdateUserDetails(LibraryContext lib, SignUpRequest user, string username)
    {
        int rows = lib.Database.ExecuteSqlRaw(@"
            UPDATE dbo.Users
            SET FirstName = {0},
                LastName = {1},
                PhoneNo = {2},
                UserAddress = {3},
                password = {4},
                email = {5},
                age = {6}
            WHERE UserName = {7}",
            user.FirstName, user.LastName, user.PhoneNo, user.UserAddress,
            user.password, user.email, user.age, username);

        return rows > 0;
    }

    public static bool ReturnBook(LibraryContext lib, string username, int bookId)
    {
        // Mark the IssueRecord as returned (IsReturned = 1) and update ReturnDate
        int rows = lib.Database.ExecuteSqlRaw(@"
            UPDATE dbo.IssueRecords
            SET IsReturned = 1,
                ReturnDate = GETDATE()
            WHERE UserName = {0} AND BookId = {1} AND IsReturned = 0",
            username, bookId);

        if (rows == 0)
            return false;

        // Increase book count by 1
        lib.Database.ExecuteSqlRaw(@"
            UPDATE dbo.Books
            SET BookCount = BookCount + 1
            WHERE BookId = {0}",
            bookId);

        return true;
    }

    public static bool IssueBook(LibraryContext lib, int bookId, string username)
    {
        // Check if book count is available
        var book = lib.Books
            .FromSqlRaw("SELECT * FROM dbo.Books WHERE BookId = {0}", bookId)
            .FirstOrDefault();

        if (book == null || (book.BookCount ?? 0) <= 0)
            return false;

        // Insert new IssueRecord
        int rows = lib.Database.ExecuteSqlRaw(@"
            INSERT INTO dbo.IssueRecords
            (UserName, BookId, IssueDate, IsReturned)
            VALUES
            ({0}, {1}, GETDATE(), 0)",
            username, bookId);

        if (rows == 0)
            return false;

        // Decrease book count by 1
        lib.Database.ExecuteSqlRaw(@"
            UPDATE dbo.Books
            SET BookCount = BookCount - 1
            WHERE BookId = {0}",
            bookId);

        return true;
    }

    public static bool ReturnBookByBookId(LibraryContext lib, int bookId)
    {
        // Increase book count (used for /books/return/{bookid})
        int rows = lib.Database.ExecuteSqlRaw(@"
            UPDATE dbo.Books
            SET BookCount = BookCount + 1
            WHERE BookId = {0}",
            bookId);

        return rows > 0;
    }

    public static bool IssueBookByBookId(LibraryContext lib, int bookId)
    {
        // Decrease book count (used for /books/issue/{bookid})
        // Should check if count > 0 before reducing
        var book = lib.Books
            .FromSqlRaw("SELECT * FROM dbo.Books WHERE BookId = {0}", bookId)
            .FirstOrDefault();

        if (book == null || (book.BookCount ?? 0) <= 0)
            return false;

        int rows = lib.Database.ExecuteSqlRaw(@"
            UPDATE dbo.Books
            SET BookCount = BookCount - 1
            WHERE BookId = {0}",
            bookId);

        return rows > 0;
    }

    public static bool AddBook()
    {
        return false;
    }
}
