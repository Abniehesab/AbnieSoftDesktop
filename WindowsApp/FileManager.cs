using System;
using System.IO;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;

public class FileManager
{
    public static void CheckAndCreateDriveAndFolder()
    {
        try
        {
            // بررسی وجود درایو D
            string driveLetter = "D:";
            if (Directory.Exists(driveLetter))
            {
                // در صورتی که درایو D موجود باشد، بررسی پوشه AbnieSoft
                string folderPath = Path.Combine(driveLetter, "AbnieSoft");
                if (!Directory.Exists(folderPath))
                {
                    // اگر پوشه AbnieSoft موجود نباشد، آن را ایجاد می‌کند
                    Directory.CreateDirectory(folderPath);
                    MessageBox.Show("پوشه 'AbnieSoft' در درایو D ایجاد شد.", "اطلاعیه", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("پوشه 'AbnieSoft' قبلاً موجود است.", "اطلاعیه", MessageBoxButton.OK, MessageBoxImage.Information);
                }

                // حالا دیتابیس را در این مسیر ایجاد کنیم
                CreateDatabaseInFolder(folderPath);
            }
            else
            {
                // اگر درایو D موجود نباشد
                MessageBox.Show("درایو D وجود ندارد.", "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"خطا در بررسی یا ایجاد پوشه: {ex.Message}", "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
        }

    }

    public static void CreateDatabaseInFolder(string folderPath)
    {
        try
        {
            // مسیر کامل برای فایل دیتابیس SQLite
            string databasePath = Path.Combine(folderPath, "AbnieSoftDatabase.db");

            // ایجاد و پیکربندی DbContext برای SQLite
            var optionsBuilder = new DbContextOptionsBuilder<AbnieSoftDbContext>();
            optionsBuilder.UseSqlite($"Data Source={databasePath};");

            using (var context = new AbnieSoftDbContext(optionsBuilder.Options))
            {
                // اطمینان از اینکه دیتابیس و جداول ایجاد شوند
                context.Database.EnsureCreated();
                MessageBox.Show("دیتابیس SQLite با موفقیت در مسیر مشخص شده ایجاد شد.", "اطلاعیه", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"خطا در ایجاد دیتابیس: {ex.Message}\n{ex.StackTrace}", "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
